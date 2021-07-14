using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.Invoices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ALBAB.Entities.JournalEntry;
using ALBAB.Entities.Products;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Controllers
{
    public class PurchaseController : BaseController
    {
         private readonly DataContext _context;
       private readonly IMapper _mapper;



        public PurchaseController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

          [HttpGet("invlist")]
         public async Task<ActionResult<IEnumerable<InvoiceSaveRes>>> getInvoices()
         {
           var invoices = await _context.Invoices.Include(d => d.InvDetail).ThenInclude(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<InvoiceSaveRes>>(invoices);

           return Ok(result);

         }

          [HttpGet("purchListNo")]
         public async Task<ActionResult> getInvNos()
         {

          var listId = await  _context.Invoices.Where(t => t.Type == JournalType.PURCH).Select(pur => new {Id =pur.Id, invNo = pur.InvNo}).ToListAsync();


          return Ok(listId);

         }
          [HttpGet("invoice/{id}")]
         public async Task<ActionResult<IEnumerable<InvoiceSaveRes>>> getInvId(int id)
         {
           var invoices = await _context.Invoices.Include(d => d.InvDetail)
           .ThenInclude(p =>p.Product).SingleOrDefaultAsync(p => p.Id == id);



         var result =  _mapper.Map<Invoice,InvoiceSaveRes>(invoices);


          return Ok(result);

         }

          [HttpGet("invdetails")]
         public async Task<ActionResult<IEnumerable<InvDetailsRes>>> getInvDetails()
         {
           var invDetails = await _context.InvDetails.Include(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<InvDetailsRes>>(invDetails);

            var invoice = _mapper.Map<IEnumerable<InvDetail>>(result);

            //var newpurchase = _mapper.Map<IEnumerable<PurchDtlDto>,IEnumerable<PurchDtl>>(result,purchdetails);


           return Ok(invoice);

         }

          [HttpPost]
         public async  Task<ActionResult<InvoiceSaveRes>> createInvoice(InvoiceSaveRes invRes)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

          invRes.Status = InvStatusType.Pending;
          invRes.Type = JournalType.PURCH;
          invRes.VatAcctId = (int)AccountType.Vat;
          invRes.ActionAcctId = (int)AccountType.Store;

          if (invRes.Type != JournalType.PURCH )
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

           var  invProducts  = invRes.invDetails ;/* .GroupBy(ac => ac.ProductId)
                      .Select(group =>
                      new {Quantity = group.Sum(q => q.Quantity)
                       , Total = group.Sum(r => r.Price * r.Quantity)
                       ,Id = group.Max(r => r.ProductId)});
 */


            var  store  = _context.products.Where(s => invProducts.Select( p => p.ProductId).Contains(s.Id)).ToList();

           // var join3 = _context.products.Join( product, ps => ps.Id, p => p.Id, (ps,p) => new {ps.Id,ps.Quantity, p.Total}).ToList();


          store.ForEach( s => {
            var qty = s.Quantity;
             //var amount = s.TotalValue;
             var newItems = invProducts.FirstOrDefault(p => p.ProductId == s.Id);
             s.Price = (newItems.Price * newItems.Quantity + (s.Price * s.Quantity))/ (s.Quantity + newItems.Quantity);
             s.Quantity +=  newItems.Quantity;
             invoice.InvDetail.Where( p => p.ProductId == s.Id).First().Cost = s.Price;

            });



         _context.Invoices.Add(invoice);

          var journal = new Journal(invoice.InvNo, invoice.Type,invoice.Date);

          var userId = _context.Address.FirstOrDefault(id => id.AppUserId == invoice.AddressId).AppUserId;

          journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,userId,invoice.dbAccountId,invoice.TotalAmount,null));
           journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,null,invoice.ActionAcctId,null,invoice.TotalAmount));

          _context.journals.Add(journal);



         await _context.SaveChangesAsync();


          var result = _mapper.Map<InvoiceSaveRes>(invoice);

          return  Ok(result);

         }



          [HttpPut] // api/purchases/id
         public async  Task<ActionResult<InvoiceSaveRes>> updateInvoice(InvoiceSaveRes invRes)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var rowCount =  invRes.invDetails.GroupBy( p => p.ProductId)
        .Select( g => new {count = g.Count()}).FirstOrDefault(c => c.count > 1);


       if (  !(rowCount==null) && rowCount.count  > 1 )
              return BadRequest("Item sholud not be dublicated");

       invRes.Type = JournalType.PURCH;



         var invoice = await _context.Invoices.Include(pd => pd.InvDetail).SingleOrDefaultAsync(p => p.Id == invRes.Id);

      //   var updatedItems =  invoice.InvDetail.Where(invres => invRes.invDetails.Any(productRes => productRes.ProductId == invres.ProductId ) && invres.Id > 0).ToList();

      //  if ( invRes.invDetails.Count !=  updatedItems.Count)
      //      return BadRequest("Old item sholud be deleted instead of replacing");

      foreach (var item in invoice.InvDetail)
        {
            if (item.Id > 0 & item.ProductId != null){

                if (invRes.invDetails.FirstOrDefault(p => p.Id == item.Id & p.ProductId == item.ProductId) == null)
                     return BadRequest("Old item sholud be deleted instead of replacing");

            }


        }


       var  purchDetailsRes  = invRes.invDetails ;
       var  purchDetails  = invoice.InvDetail ;

       var  newInvStoreItem  = _context.products.Where(s => purchDetailsRes.Select( p => p.ProductId).Contains(s.Id)).ToList();

            List<InvDetail> newInvItems = new List<InvDetail>();
            //var newInvItems = new List<int>();
            newInvStoreItem.ForEach(stock =>
            {
                var qty = stock.Quantity;
                //var amount = stock.Quantity * stock.Price;

                var newItem = purchDetailsRes.FirstOrDefault(j => j.ProductId == stock.Id);
                var oldItem = purchDetails.FirstOrDefault(j => j.ProductId == stock.Id);

                newInvItems.Add(oldItem);

                if (newItem.Id == null ) //only new
                {

                    stock.Price = (newItem.Price * newItem.Quantity + (stock.Price * stock.Quantity)) / (stock.Quantity + newItem.Quantity);
                    stock.Quantity += newItem.Quantity;
                    invRes.invDetails.Where( p => p.ProductId == stock.Id).First().Cost = stock.Price;
                }
                else
                {


                    if (oldItem.Quantity != newItem.Quantity || oldItem.Price != newItem.Price)
                    {


                       //step one: pull

                       if( stock.Quantity != oldItem.Quantity){

                       stock.Price = (stock.Price * stock.Quantity -oldItem.TotalValue) / (stock.Quantity - oldItem.Quantity);
                       stock.Quantity -= oldItem.Quantity;
                       invoice.InvDetail.Where( p => p.ProductId == stock.Id).First().Cost = stock.Price;
                       }
                      else{

                       stock.Price  = stock.Quantity = 0;
                       invoice.InvDetail.Where( p => p.ProductId == stock.Id).First().Cost = 0;
                      }

                       //step two: push
                        stock.Price = ((stock.Price * stock.Quantity) + (newItem.Price * newItem.Quantity)) / (stock.Quantity+ newItem.Quantity);
                        stock.Quantity += newItem.Quantity;
                        invoice.InvDetail.Where( p => p.ProductId == stock.Id).First().Cost = stock.Price = stock.Price;

                    }
                }
            });



         //var deletedItems1 = invoice.InvDetail.Where(s => !newInvItems.Select( p => p.ProductId).Contains(s.ProductId)).ToList();
         var deletedItems = invoice.InvDetail.Where( id => !newInvItems.Contains(id));


         var  deletedStoreItem  = _context.products.Where(s => deletedItems.Select( p => p.ProductId).Contains(s.Id) ).ToList();

         deletedStoreItem.ForEach(stock =>
            {
                var deletedInvItems = deletedItems.FirstOrDefault(p => p.ProductId == stock.Id);
               //var oldItems = invProductDetail.FirstOrDefault(j => j.ProductId == stock.Id);

                if(stock.Quantity!= deletedInvItems.Quantity)
                {
                stock.Price = ((stock.Price * stock.Quantity) - deletedInvItems.TotalValue)/(stock.Quantity-deletedInvItems.Quantity) ;
                stock.Quantity -= deletedInvItems.Quantity;
                invoice.InvDetail.Where( p => p.ProductId == stock.Id).First().Cost = stock.Price;
                }
                else{
                stock.Quantity = stock.Price = 0;
                invoice.InvDetail.Where( p => p.ProductId == stock.Id).First().Cost = 0;
                }

         }

            );


        _mapper.Map<InvoiceSaveRes,Invoice>(invRes,invoice);


        var EditedEntities = _context.ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

        EditedEntities.ForEach(E =>
        {
            E.Property("LastUpdate").CurrentValue = DateTime.Now;
        });


        var journal = _context.journals.Include( ja => ja.journalAccounts).FirstOrDefault( j => j.JENo.Equals(invRes.InvNo) & j.Type.Equals(invRes.Type));

        // var NewJournal = new JournalEntry(invoice.InvNo, invoice.Type,invoice.Date);

        // NewJournal.journalAccounts.Add(new JournalAccount(journal.journalAccounts.FirstOrDefault( c => c.Credit > 0).Id, invoice.Date,invoice.Date,invoice.AppUserId,invoice.AccountId,invoice.TotalAmount,null));
        // NewJournal.journalAccounts.Add(new JournalAccount(journal.journalAccounts.FirstOrDefault( d => d.Debit > 0).Id,invoice.Date,invoice.Date,null,invoice.ActionAcctId,null,invoice.TotalAmount));


        if(journal.JENo != invoice.InvNo) journal.JENo = invoice.InvNo;
        if(journal.Note != invoice.Comment) journal.Note = invoice.Comment;
        if(journal.EntryDate != invoice.Date) journal.EntryDate = invoice.Date;

         var entry =  journal.journalAccounts.ToList();
         var userId = _context.Address.FirstOrDefault(id => id.AppUserId == invoice.AddressId).AppUserId;

         entry.ForEach( E =>

         {
            //aaaa
           if (E.Credit > 0){

             if (E.Created != invoice.Date) E.Created = invoice.Date;
             if (E.DueDate != invoice.Date) E.DueDate = invoice.Date;
             if (E.AppUserId != userId) E.AppUserId = userId;
             if (E.dbAccountId != invoice.dbAccountId) E.dbAccountId= invoice.dbAccountId;
             if (E.Credit != invoice.TotalAmount) E.Credit= invoice.TotalAmount;
           }
           if (E.Debit > 0){

             if (E.Created != invoice.Date) E.Created = invoice.Date;
             if (E.DueDate != invoice.Date) E.DueDate = invoice.Date;
             //if (E.AppUserId != invoice.AppUserId) E.AppUserId = invoice.AppUserId;
             if (E.dbAccountId != invoice.ActionAcctId) E.dbAccountId= invoice.ActionAcctId;
             if (E.Debit != invoice.TotalAmount) E.Debit= invoice.TotalAmount;
           }

         }


         );

        //NewJournal.journalAccounts.da = journal.Id


        // _mapper.Map<JournalEntry,JournalEntry>(NewJournal,journal);


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


         var  deletedStoreItem  = _context.products.Where(s => deletedItems.Select( p => p.ProductId).Contains(s.Id) ).ToList();

         deletedStoreItem.ForEach(stock =>
            {
                var deletedInvItems = deletedItems.FirstOrDefault(p => p.ProductId == stock.Id);
               //var oldItems = invProductDetail.FirstOrDefault(j => j.ProductId == stock.Id);

                if(stock.Quantity!= deletedInvItems.Quantity)
                {
                stock.Price = ((stock.Price * stock.Quantity) - deletedInvItems.TotalValue)/(stock.Quantity-deletedInvItems.Quantity) ;
                stock.Quantity -= deletedInvItems.Quantity;
                invoice.InvDetail.Where( p => p.ProductId == stock.Id).First().Cost = stock.Price;
                }
                else{
                stock.Quantity = stock.Price = 0;
                invoice.InvDetail.Where( p => p.ProductId == stock.Id).First().Cost = 0;
                }

         }

            );

            var journal = await _context.journals.FirstOrDefaultAsync( j => j.JENo == invoice.InvNo & j.Type == JournalType.PURCH);

            _context.Remove(invoice);

            _context.Remove(journal);

          // var journal = await _context.journals.Where( j =)




             await _context.SaveChangesAsync();

              return Ok(id);

         }
    }
}