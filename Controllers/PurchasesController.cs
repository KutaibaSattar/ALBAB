using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.Purchases;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Controllers
{
    public class PurchasesController : BaseController
    {
         private readonly DataContext _context;
       private readonly IMapper _mapper;
      
      

        public PurchasesController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
         
        }

          [HttpGet("purchinv")]
         public async Task<ActionResult<IEnumerable<PurchHDRDto>>> Getpurchases()
         {
           var purchases = await _context.PurchHDRs.Include(d => d.purchDTL).ThenInclude(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<PurchHDRDto>>(purchases);
           
           return Ok(result);  

         }  

          [HttpGet("purchdtl")]
         public async Task<ActionResult<IEnumerable<PurchDTLDto>>> GetpurchDetails()
         {
           var purchdetails = await _context.PurchDTLs.Include(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<PurchDTLDto>>(purchdetails);
           
           return Ok(result);  

         }  

          [HttpPost]
         public async  Task<ActionResult<PurchHDRDto>> CreatePurchase(PurchHDRDto purchHDRDto)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);
         
         var purch = _mapper.Map<PurchHDRDto,PurchHDR>(purchHDRDto);

          purch.LastUpdate = DateTime.Now;

           foreach (var item in purch.purchDTL)
            {
                item.LastUpdate = DateTime.Now;
            }

         _context.PurchHDRs.Add(purch);

         await _context.SaveChangesAsync();

          var result = _mapper.Map<PurchHDR,PurchHDRDto>(purch);

          return  Ok(result);

         }
          [HttpPut("{id}")] // api/purchases/id
         public async  Task<ActionResult<PurchHDRDto>> UpdatePurchase(int id,PurchHDRDto purchHDRDto)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);
         
         var purch = await _context.PurchHDRs.FindAsync(id);

          purch.LastUpdate = DateTime.Now;

          _mapper.Map<PurchHDRDto,PurchHDR>(purchHDRDto,purch);

          /*  foreach (var item in purch.purchDTL)
            {
                item.LastUpdate = DateTime.Now;
            } */

          await _context.SaveChangesAsync();

          var result = _mapper.Map<PurchHDR,PurchHDRDto>(purch);

          return  Ok(result);

         }
    }
}