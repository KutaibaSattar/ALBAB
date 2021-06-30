using System;
using System.Linq;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.JournalEntry;
using ALBAB.Entities.Purchases;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
          [HttpPost]
         public async  Task<ActionResult<InvoiceSaveRes>> createInvoice(InvoiceSaveRes invRes)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         if (invRes.Type != JournalType.QUOTE )
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

          _context.Invoices.Add(invoice);


         await _context.SaveChangesAsync();
         var result = _mapper.Map<InvoiceSaveRes>(invoice);
        return  Ok(result);

         }




    }
}