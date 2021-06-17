using System.Data.Common;
using System.Text.RegularExpressions;
using System.Security.AccessControl;
using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.Purchases;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ALBAB.Entities.Journal;
using ALBAB.Entities.Products;

namespace ALBAB.Controllers
{
    public class InvoicesController : BaseController
    {
         private readonly DataContext _context;
       private readonly IMapper _mapper;



        public InvoicesController(DataContext context, IMapper mapper)
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

          [HttpGet("invNos")]
         public async Task<ActionResult> getInvNos()
         {

          var listId = await  _context.Invoices.Select(pur => new {Id =pur.Id, invNo = pur.InvNo}).ToListAsync();


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

          // if (invRes.AppUserId > 0)
          // {
          //     invRes.AccountId = (int)(ReservedAccountsType.Clients);

          // }

         var invoice = _mapper.Map<InvoiceSaveRes,Invoice>(invRes);

          invoice.LastUpdate = DateTime.Now;
           foreach (var item in invoice.InvDetail)
            {
                item.LastUpdate = DateTime.Now;
            }

           var  invProducts  = invRes.invDetails.GroupBy(ac => ac.ProductId)
                      .Select(group =>
                      new {Quantity = group.Sum(q => q.Quantity)
                       , Total = group.Sum(r => r.Price * r.Quantity)
                       ,Id = group.Max(r => r.ProductId)});




            var  store  = _context.products.Where(s => invProducts.Select( p => p.Id).Contains(s.Id)).ToList();


           // var join3 = _context.products.Join( product, ps => ps.Id, p => p.Id, (ps,p) => new {ps.Id,ps.Quantity, p.Total}).ToList();


          store.ForEach( s => {
            var qty = s.Quantity;
             var amount = s.Quantity * s.Price;
             var newItems = invProducts.FirstOrDefault(j => j.Id == s.Id);

             s.Quantity +=  newItems.Quantity;
             s.Price = (newItems.Total + amount)/ s.Quantity;


            });



          _context.Invoices.Add(invoice);

          var journal = new JournalEntry(invoice.InvNo, invoice.Type,invoice.Date);

           journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,invoice.AppUserId,invoice.AccountId,invoice.TotalAmount,null));
           journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,null,invoice.DebitAcctId,null,invoice.TotalAmount));
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

          //  if (invRes.AppUserId > 0)
          // {
          //     invRes.AccountId = (int)(ReservedAccountsType.Clients);
          // }


        var invoice = await _context.Invoices.Include(pd => pd.InvDetail).SingleOrDefaultAsync(p => p.Id == invRes.Id);



        var  invProductDetailRes  = invRes.invDetails.GroupBy(ac => ac.ProductId)
                      .Select(group =>
                      new {Quantity = group.Sum(q => q.Quantity)
                       , Total = group.Sum(r => r.Price * r.Quantity)
                       ,productId = group.Max(r => r.ProductId)
                       ,price = group.Max(r => r.Price)}).ToList();

        var  invProductDetail  = invoice.InvDetail.GroupBy(ac => ac.ProductId)
                      .Select(group =>
                      new {Quantity = group.Sum(q => q.Quantity)
                       , Total = group.Sum(r => r.Price * r.Quantity)
                       ,productId = group.Max(r => r.ProductId)
                       ,price = group.Max(r => r.Price)}).ToList();


         var  store  = _context.products.Where(s => invProductDetailRes.Select( p => p.productId).Contains(s.Id)).ToList();


            store.ForEach(stock =>
            {
                var qty = stock.Quantity;
                var amount = stock.Quantity * stock.Price;
                var newItems = invProductDetailRes.FirstOrDefault(j => j.productId == stock.Id);
                var oldItems = invProductDetail.FirstOrDefault(j => j.productId == stock.Id);

                if (oldItems == null) //only new
                {
                    stock.Quantity += newItems.Quantity;
                    stock.Price = (newItems.Total + amount) / stock.Quantity;
                }
                else
                {

                    if (oldItems.Quantity != newItems.Quantity || oldItems.price != newItems.price)
                    {
                        stock.Quantity -= oldItems.Quantity;
                        stock.Price = ((stock.Price * stock.Quantity) + newItems.Total) / (stock.Quantity+ newItems.Quantity);
                        stock.Quantity += newItems.Quantity;

                    }
                }
            });

        _mapper.Map<InvoiceSaveRes,Invoice>(invRes,invoice);


        var EditedEntities = _context.ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

        EditedEntities.ForEach(E =>
        {
            E.Property("LastUpdate").CurrentValue = DateTime.Now;
        });


        var journal = _context.journals.Include( ja => ja.journalAccounts).FirstOrDefault( j => j.JENo.Equals(invRes.InvNo) & j.Type.Equals(invRes.Type));

        var NewJournal = new JournalEntry(invoice.InvNo, invoice.Type,invoice.Date);


        NewJournal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,invoice.AppUserId,invoice.AccountId,invoice.TotalAmount,null));
        NewJournal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,null,invoice.DebitAcctId,null,invoice.TotalAmount));

        //NewJournal.journalAccounts.da = journal.Id


           _mapper.Map<JournalEntry,JournalEntry>(NewJournal,journal);


          await _context.SaveChangesAsync();

          var result = _mapper.Map<Invoice,InvoiceSaveRes>(invoice);

          return  Ok(result);

         }

         [HttpDelete("{id}")]
         public async Task<ActionResult> deleteInv(int id)
         {
           var invoice = await _context.Invoices.FindAsync(id);

           if (invoice == null)
              return BadRequest("No invoice found");

             _context.Remove(invoice);
             await _context.SaveChangesAsync();

              return Ok(id);

         }
    }
}