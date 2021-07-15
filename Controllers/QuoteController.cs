using System;
using System.Linq;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.JournalEntry;
using ALBAB.Entities.Invoices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Controllers
{
    public class QuoteController : BaseController
    {
       private readonly DataContext _context;
       private readonly IMapper _mapper;

        public QuoteController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
            [HttpGet("lastquote")]

            public  ActionResult<string>getLastquote ()
            {
               DateTime firstDay = new DateTime(DateTime.Now.Year , 1, 1);
               var quote =   _context.Invoices.OrderBy(id => id.Id).Where(inv => (inv.Type == JournalType.QUOTE) && (inv.Date >= firstDay) ).Select(i => i.InvNo).LastOrDefault();
               return string.IsNullOrEmpty(quote) ? InvoiceNoMask.GetMask(): quote;
            }

        [HttpGet("quoteListNo")]
         public async Task<ActionResult> getInvNos()
         {

          var listId = await  _context.Invoices.Where(t => t.Type == JournalType.QUOTE).Select(pur => new {Id =pur.Id, invNo = pur.InvNo}).ToListAsync();
          return Ok(listId);
         }

         [HttpGet("invoice/{id}")]
         public async Task<ActionResult<IEnumerable<SaveInvRes>>> getInvId(int id)
         {
           var invoices = await _context.Invoices.Include(d => d.InvDetail)
           .ThenInclude(p =>p.Product).SingleOrDefaultAsync(p => p.Id == id);

         var result =  _mapper.Map<Invoice,SaveInvRes>(invoices);
          return Ok(result);

         }

          [HttpPost]
         public async  Task<ActionResult<SaveInvRes>> createInvoice(SaveInvRes invRes)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

          invRes.Status = InvStatusType.Pending;
          invRes.Type = JournalType.QUOTE;
          invRes.VatAcctId = (int)AccountType.Vat;
          invRes.ActionAcctId = (int)AccountType.Store;

         if (invRes.Type != JournalType.QUOTE )
                return BadRequest("Please check invoice type");



        var rowCount =  invRes.invDetails.Where(p => p.ProductId != null).GroupBy( p => p.ProductId)
        .Select( g => new {count = g.Count()}).FirstOrDefault(c => c.count > 1);


       if (  !(rowCount==null) && rowCount.count  > 1 )
              return BadRequest("Item sholud not be dublicated");




         var invoice = _mapper.Map<SaveInvRes,Invoice>(invRes);

          invoice.LastUpdate = DateTime.Now;
           foreach (var item in invoice.InvDetail)
            {
                item.LastUpdate = DateTime.Now;
            }

          _context.Invoices.Add(invoice);


         await _context.SaveChangesAsync();
         var result = _mapper.Map<SaveInvRes>(invoice);
        return  Ok(result);

         }

            [HttpDelete("deleteallquote")]
         public async  Task<ActionResult<SaveInvRes>> deleteInvoice()
         {
                _context.Invoices.RemoveRange(_context.Invoices.Where(t => t.Type == JournalType.QUOTE ));
                await _context.SaveChangesAsync();
                return Ok("All quotations has been deleted");

         }

    }
}