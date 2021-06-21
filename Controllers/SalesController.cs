using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.Journal;
using ALBAB.Entities.Purchases;
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
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         if (invRes.Type != JournalType.SALES )
                return BadRequest("Please check invoice type");



        var rowCount =  invRes.invDetails.GroupBy( p => p.ProductId)
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

         decimal totalCoast = 0;



          store.ForEach( stock => {

            decimal newItemQty = invRes.invDetails.FirstOrDefault(i => i.ProductId == stock.Id).Quantity;

           if(newItemQty !=0){
            totalCoast += stock.Price * newItemQty ;
            stock.Quantity -=  newItemQty;
           }


            });



          _context.Invoices.Add(invoice);

          var journal = new JournalEntry(invoice.InvNo, invoice.Type,invoice.Date);

          journal.journalAccounts.Add(new JournalAccount
          (invoice.Date,invoice.Date,null,(int)AccountType.Store,totalCoast,null)); // Store Credit

           journal.journalAccounts.Add(new JournalAccount
           (invoice.Date,invoice.Date,invoice.AppUserId,invoice.AccountId,null,invoice.TotalAmount)); // Client Debit


          if(invoice.TotalAmount-totalCoast>0)
           journal.journalAccounts.Add(new JournalAccount
           (invoice.Date,invoice.Date,null,(int)AccountType.SellingGoods,invoice.TotalAmount-totalCoast,null)); // Sales profit
          else
             journal.journalAccounts.Add(new JournalAccount
             (invoice.Date,invoice.Date,null,(int)AccountType.SellingGoods,null,totalCoast-invoice.TotalAmount)); // Sales loss




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

        var rowCount =  invRes.invDetails.GroupBy( p => p.ProductId)
        .Select( g => new {count = g.Count()}).FirstOrDefault(c => c.count > 1);


       if (  !(rowCount==null) && rowCount.count  > 1 )
              return BadRequest("Item sholud not be dublicated");


       var invoice = await _context.Invoices.Include(pd => pd.InvDetail).SingleOrDefaultAsync(p => p.Id == invRes.Id);



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

                if (oldItems == null) //only new
                {
                    stock.Quantity += newItems.Quantity;
                    stock.Price = (newItems.Price * newItems.Quantity + (stock.Price * stock.Quantity)) / stock.Quantity;
                }
                else
                {

                    if (oldItems.Quantity != newItems.Quantity || oldItems.Price != newItems.Price)
                    {
                        stock.Quantity -= oldItems.Quantity;
                        stock.Price = ((stock.Price * stock.Quantity) + (newItems.Price * newItems.Quantity)) / (stock.Quantity+ newItems.Quantity);
                        stock.Quantity += newItems.Quantity;

                    }
                }
            });


         var deletedItems = invoice.InvDetail.Where( p => !newInvItems.Contains(p.Id));

         var  deletedStoreItem  = _context.products.Where(s => deletedItems.Select( p => p.ProductId).Contains(s.Id) ).ToList();

         deletedStoreItem.ForEach(stock =>
            {
                var deletedInvItems = invoice.InvDetail.FirstOrDefault(j => j.ProductId == stock.Id);
               //var oldItems = invProductDetail.FirstOrDefault(j => j.ProductId == stock.Id);
                stock.Quantity += deletedInvItems.Quantity;

         }

            );



        _mapper.Map<InvoiceSaveRes,Invoice>(invRes,invoice);


        var EditedEntities = _context.ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

        EditedEntities.ForEach(E =>
        {
            E.Property("LastUpdate").CurrentValue = DateTime.Now;
        });


        var journal = _context.journals.Include( ja => ja.journalAccounts).FirstOrDefault( j => j.JENo.Equals(invRes.InvNo) & j.Type.Equals(invRes.Type));

        var NewJournal = new JournalEntry(invoice.InvNo, invoice.Type,invoice.Date);


        NewJournal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,invoice.AppUserId,invoice.AccountId,invoice.TotalAmount,null));
        NewJournal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,null,invoice.ActionAcctId,null,invoice.TotalAmount));

        //NewJournal.journalAccounts.da = journal.Id


           _mapper.Map<JournalEntry,JournalEntry>(NewJournal,journal);


          await _context.SaveChangesAsync();

          var result = _mapper.Map<Invoice,InvoiceSaveRes>(invoice);

          return  Ok(result);

         }
    }
}