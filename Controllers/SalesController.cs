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



          store.ForEach( s => {
            var qty = s.Quantity;
            var amount = s.Quantity * s.Price;
            totalCoast += amount ;
            s.Quantity -=  s.Quantity;
            });



          _context.Invoices.Add(invoice);

          var journal = new JournalEntry(invoice.InvNo, invoice.Type,invoice.Date);

           journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,invoice.AppUserId,invoice.AccountId,null,invoice.TotalAmount));
           journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,null,invoice.ActionAcctId,invoice.TotalAmount,null));

           journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,null,(int)AccountType.CostGoodsSold,totalCoast,null));
           journal.journalAccounts.Add(new JournalAccount(invoice.Date,invoice.Date,null,(int)AccountType.Store,null,totalCoast));


          _context.journals.Add(journal);



         await _context.SaveChangesAsync();
         var result = _mapper.Map<InvoiceSaveRes>(invoice);
          return  Ok(result);

         }
    }
}