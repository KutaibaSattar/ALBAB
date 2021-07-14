using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.JournalEntry;
using ALBAB.Entities.Invoices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ALBAB.Entities.AppAccounts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ALBAB.Controllers
{
    public class SalesController : BaseController
    {
         private readonly DataContext _context;
       private readonly IMapper _mapper;



        public SalesController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

          [HttpPost]
         public async  Task<ActionResult<InvoiceSaveRes>> createInvoice(InvoiceSaveRes invRes)
         {

          // return BadRequest("Testing");
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         if (invRes.Type != JournalType.SALES )
                return BadRequest("Please check invoice type");



        var rowCount =  invRes.invDetails.Where(p => p.ProductId != null).GroupBy( p => p.ProductId)
        .Select( g => new {count = g.Count()}).FirstOrDefault(c => c.count > 1);


       if (  !(rowCount==null) && rowCount.count  > 1 )
              return BadRequest("Item sholud not be dublicated");




         var invoice = _mapper.Map<InvoiceSaveRes,Invoice>(invRes);

          invoice.LastUpdate = DateTime.Now;
           foreach (var item in invoice.InvDetail)
            {
                item.LastUpdate = DateTime.Now;
            }

           var  invSales  = invRes.invDetails ;



          var  store  = _context.products.Where(s => invSales.Select( p => p.ProductId).Contains(s.Id)).ToList();

        // decimal stockCost = 0;


          store.ForEach( stock => {

            decimal newItemQty = invRes.invDetails.FirstOrDefault(i => i.ProductId == stock.Id).Quantity;

           if(newItemQty !=0){
            //stockCost += stock.Price * newItemQty ;
            stock.Quantity -=  newItemQty;
           var x =  invoice.InvDetail.Where( p => p.ProductId == stock.Id).First().Cost = stock.Price;
           }


            });



          _context.Invoices.Add(invoice);

          var journal = new Journal(invoice.InvNo, invoice.Type,invoice.Date);
          var userId = _context.Address.FirstOrDefault(id => id.AppUserId == invoice.AddressId).AppUserId;

         if(invoice.InvCost >0){

          journal.journalAccounts.Add(new JournalAccount
          (invoice.Date,invoice.Date,null,(int)AccountType.Store,invoice.InvCost,null)); // Store Credit
            journal.journalAccounts.Add(new JournalAccount
          (invoice.Date,invoice.Date,null,(int)AccountType.CostGoodsSold,null,invoice.InvCost)); // Store Debit

         }

           journal.journalAccounts.Add(new JournalAccount
           (invoice.Date,invoice.Date,null,invoice.ActionAcctId,invoice.TotalAmount,null)); // Sales profit Credit

             journal.journalAccounts.Add(new JournalAccount
           (invoice.Date,invoice.Date,userId,invoice.dbAccountId,null,invoice.TotalAmount)); // Client Debit




          _context.journals.Add(journal);



         await _context.SaveChangesAsync();
         var result = _mapper.Map<InvoiceSaveRes>(invoice);
        return  Ok(result);

         }

        [HttpPut] // api/sales
         public async  Task<ActionResult<InvoiceSaveRes>> updateInvoice(InvoiceSaveRes invRes)
         {

         if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var rowCount =  invRes.invDetails.Where(p => p.ProductId != null).GroupBy( p => p.ProductId)
        .Select( g => new {count = g.Count()}).FirstOrDefault(c => c.count > 1);


       if (  !(rowCount==null) && rowCount.count  > 1 )
              return BadRequest("Item sholud not be dublicated");


       var invoice = await _context.Invoices.Include(pd => pd.InvDetail).SingleOrDefaultAsync(p => p.Id == invRes.Id);


        foreach (var item in invoice.InvDetail)
        {
            if (item.Id > 0 & item.ProductId != null){

                if (invRes.invDetails.FirstOrDefault(p => p.Id == item.Id & p.ProductId == item.ProductId) == null)
                     return BadRequest("Old item sholud be deleted instead of replacing");

            }


        }

    //    var updatedItems =  invoice.InvDetail
    //     .Where(invres => invRes.invDetails.Any(productRes => productRes.ProductId == invres.ProductId ) && invres.Id > 0).ToList();

    //    if (invRes.invDetails.Where( p => p.ProductId != null).Count() !=  updatedItems.Count)
    //        return BadRequest("Old item sholud be deleted instead of replacing");

        var  salesDetailsRes  = invRes.invDetails ;

       var  salesDetails  = invoice.InvDetail ;


       var  newInvStoreItem  = _context.products.Where(s => salesDetailsRes.Select( p => p.ProductId).Contains(s.Id)).ToList();

            var newInvItems = new List<int?>();



            newInvStoreItem.ForEach(stock =>
            {
                var qty = stock.Quantity;
                //var amount = stock.Quantity * stock.Price;

                var newItems = salesDetailsRes.FirstOrDefault(s => s.ProductId == stock.Id);
                var oldItems = salesDetails.FirstOrDefault(s => s.ProductId == stock.Id);



                  newInvItems.Add(newItems.ProductId);

                if (newItems.Id == null) //only new
                {
                    stock.Quantity -= newItems.Quantity;

                }

               // same item
                else if ( oldItems.Quantity != newItems.Quantity || oldItems.Price != newItems.Price)
                {
                    stock.Quantity += oldItems.Quantity;
                    stock.Quantity -= newItems.Quantity;
                }

            });


        var deletedItems = invoice.InvDetail.Where( id => !newInvItems.Contains(id.ProductId)).ToList();

       if (deletedItems.Count > 0) {

                var deletedStoreItem = _context.products.Where(s => deletedItems.Select(p => p.ProductId).Contains(s.Id)).ToList();

                deletedStoreItem.ForEach(stock =>
                   {
                       var deletedInvItems = invoice.InvDetail.FirstOrDefault(j => j.ProductId == stock.Id);
                //var oldItems = invProductDetail.FirstOrDefault(j => j.ProductId == stock.Id);
                stock.Quantity += deletedInvItems.Quantity;

                   }

                   );

       }


        _mapper.Map<InvoiceSaveRes,Invoice>(invRes,invoice);


        var EditedEntities = _context.ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

        EditedEntities.ForEach(E =>
        {
            E.Property("LastUpdate").CurrentValue = DateTime.Now;
        });

        var journal = _context.journals.Include( ja => ja.journalAccounts).FirstOrDefault( j => j.JENo.Equals(invRes.InvNo) & j.Type.Equals(invRes.Type));


        if(journal.JENo != invoice.InvNo) journal.JENo = invoice.InvNo;
        if(journal.Note != invoice.Comment) journal.Note = invoice.Comment;
        if(journal.EntryDate != invoice.Date) journal.EntryDate = invoice.Date;

         var entry =  journal.journalAccounts.ToList();

         entry.ForEach( E =>

         {
            //aaaa
           if (E.Credit > 0 & E.dbAccountId == (int)AccountType.Store){

             if (E.Created != invoice.Date) E.Created = invoice.Date;
             if (E.DueDate != invoice.Date) E.DueDate = invoice.Date;
            //  if (E.AppUserId != invoice.AppUserId) E.AppUserId = invoice.AppUserId;
            //  if (E.AccountId != invoice.AccountId) E.AccountId= invoice.AccountId;
             if (E.Credit != invoice.InvCost) E.Credit= invoice.InvCost;
           }
           else if (E.Debit > 0 & E.dbAccountId == (int)AccountType.CostGoodsSold){

             if (E.Created != invoice.Date) E.Created = invoice.Date;
             if (E.DueDate != invoice.Date) E.DueDate = invoice.Date;
             //if (E.AppUserId != invoice.AppUserId) E.AppUserId = invoice.AppUserId;
             //if (E.AccountId != invoice.ActionAcctId) E.AccountId= invoice.ActionAcctId;
             if (E.Debit != invoice.InvCost) E.Debit= invoice.InvCost;
           }
           else if (E.Credit > 0 & E.dbAccountId != (int)AccountType.Store){

             if (E.Created != invoice.Date) E.Created = invoice.Date;
             if (E.DueDate != invoice.Date) E.DueDate = invoice.Date;
            //  if (E.AppUserId != invoice.AppUserId) E.AppUserId = invoice.AppUserId;
            //  if (E.AccountId != invoice.AccountId) E.AccountId= invoice.AccountId;
             if (E.Credit != invoice.TotalAmount) E.Credit= invoice.TotalAmount;
           }
           else if (E.Debit > 0 & E.dbAccountId != (int)AccountType.CostGoodsSold){

             if (E.Created != invoice.Date) E.Created = invoice.Date;
             if (E.DueDate != invoice.Date) E.DueDate = invoice.Date;
             //if (E.AppUserId != invoice.AppUserId) E.AppUserId = invoice.AppUserId;
             //if (E.AccountId != invoice.ActionAcctId) E.AccountId= invoice.ActionAcctId;
             if (E.Debit != invoice.TotalAmount) E.Debit= invoice.TotalAmount;
           }

         });


        //NewJournal.journalAccounts.da = journal.Id


           //_mapper.Map<JournalEntry,JournalEntry>(NewJournal,journal);


          await _context.SaveChangesAsync();

          var result = _mapper.Map<Invoice,InvoiceSaveRes>(invoice);

          return  Ok(result);

         }




         [HttpDelete("{id}")]
         public async Task<ActionResult> deleteInv(int id)
         {

          var invoice = await _context.Invoices.Include(i => i.InvDetail).FirstOrDefaultAsync(inv => inv.Id == id);

          if (invoice == null)
              return BadRequest("No invoice found");


        var deletedItems = invoice.InvDetail;


            if (deletedItems.Count > 0)
            {

                var deletedStoreItem = _context.products.Where(s => deletedItems.Select(p => p.ProductId).Contains(s.Id)).ToList();
                deletedStoreItem.ForEach(stock =>
                   {
                       var deletedInvItems = invoice.InvDetail.FirstOrDefault(j => j.ProductId == stock.Id);
                       stock.Quantity += deletedInvItems.Quantity;
                   }
                   );
            }

            var journal = await _context.journals.FirstOrDefaultAsync( j => j.JENo == invoice.InvNo & j.Type == JournalType.SALES);

            _context.Remove(invoice);
           _context.Remove(journal);
          await _context.SaveChangesAsync();
          return Ok(id);

         }
    }
}