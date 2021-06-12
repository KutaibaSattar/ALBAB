using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.Purchases;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using ALBAB.Entities;
using ALBAB.Entities.Journal;
using ALBAB.Entities.AppAccounts;

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
         public async Task<ActionResult<IEnumerable<InvoiceRes>>> getInvoices()
         {
           var invoices = await _context.Invoices.Include(d => d.InvDetail).ThenInclude(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<InvoiceRes>>(invoices);

           return Ok(result);

         }

          [HttpGet("invNos")]
         public async Task<ActionResult> getInvNos()
         {

          var listId = await  _context.Invoices.Select(pur => new {Id =pur.Id, invNo = pur.InvNo}).ToListAsync();


          return Ok(listId);

         }
          [HttpGet("invoice/{id}")]
         public async Task<ActionResult<IEnumerable<InvoiceRes>>> getInvId(int id)
         {
           var invoices = await _context.Invoices.Include(d => d.InvDetail)
           .ThenInclude(p =>p.Product).SingleOrDefaultAsync(p => p.Id == id);



         var result =  _mapper.Map<Invoice,InvoiceRes>(invoices);


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
         public async  Task<ActionResult<InvoiceRes>> createInvoice(InvoiceRes invRes)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

          // if (invRes.AppUserId > 0)
          // {
          //     invRes.AccountId = (int)(ReservedAccountsType.Clients);

          // }

         var invoice = _mapper.Map<InvoiceRes,Invoice>(invRes);

          invoice.LastUpdate = DateTime.Now;
           foreach (var item in invoice.InvDetail)
            {
                item.LastUpdate = DateTime.Now;
            }

          _context.Invoices.Add(invoice);

          var journal = new JournalEntry(invoice.InvNo, JournalType.PRCH,invoice.Date);

           journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,invoice.AppUserId,invoice.AccountId,invoice.TotalAmount,null));
           journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,null,invoice.DebitAcctId,null,invoice.TotalAmount));
          _context.journals.Add(journal);

         
         await _context.SaveChangesAsync();


          var result = _mapper.Map<InvoiceRes>(invoice);

          return  Ok(result);

         }
          [HttpPut] // api/purchases/id
         public async  Task<ActionResult<InvoiceRes>> updateInvoice(InvoiceRes invRes)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

          //  if (invRes.AppUserId > 0)
          // {
          //     invRes.AccountId = (int)(ReservedAccountsType.Clients);
          // }

        var invoice = await _context.Invoices.Include(pd => pd.InvDetail).SingleOrDefaultAsync(p => p.Id == invRes.Id);

       _mapper.Map<InvoiceRes,Invoice>(invRes,invoice);

        var EditedEntities = _context.ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

        EditedEntities.ForEach(E =>
        {
            E.Property("LastUpdate").CurrentValue = DateTime.Now;
        });





          await _context.SaveChangesAsync();

          var result = _mapper.Map<Invoice,InvoiceRes>(invoice);

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