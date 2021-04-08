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

          [HttpGet("purchases")]
         public async Task<ActionResult<IEnumerable<SavePurchHDRDto>>> Getpurchases()
         {
           var purchases = await _context.PurchHDRs.Include(d => d.purchDTL).ThenInclude(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<SavePurchHDRDto>>(purchases);

           _mapper.Map<IEnumerable<SavePurchHDRDto>,IEnumerable<PurchHDR>>(result,purchases);
          
          
           var result2 = _mapper.Map<IEnumerable<PurchHDR>>(result);
           
           return Ok(result);  

         }  
        
          [HttpGet("purchinv/{id}")]
         public async Task<ActionResult<IEnumerable<SavePurchHDRDto>>> GetpurchaseInv(int id)
         {
           var purchases = await _context.PurchHDRs.Include(d => d.purchDTL)
           .ThenInclude(p =>p.Product).SingleOrDefaultAsync(p => p.Id == id);

          

         var result =  _mapper.Map<PurchHDR,SavePurchHDRDto>(purchases);
          
                         
          return Ok(result);  

         }  

          [HttpGet("purchdtl")]
         public async Task<ActionResult<IEnumerable<PurchDTLDto>>> GetpurchDetails()
         {
           var purchdetails = await _context.PurchDTLs.Include(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<PurchDTLDto>>(purchdetails);

            var purch = _mapper.Map<IEnumerable<PurchDTL>>(result);
           
            var newpurchase = _mapper.Map<IEnumerable<PurchDTLDto>,IEnumerable<PurchDTL>>(result,purchdetails);

           
           return Ok(purch);  

         }  

          [HttpPost]
         public async  Task<ActionResult<SavePurchHDRDto>> CreatePurchase(SavePurchHDRDto purchHDRDto)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);
         
         var purch = _mapper.Map<SavePurchHDRDto,PurchHDR>(purchHDRDto);

          purch.LastUpdate = DateTime.Now;

           foreach (var item in purch.purchDTL)
            {
                item.LastUpdate = DateTime.Now;
            }

         _context.PurchHDRs.Add(purch);

         await _context.SaveChangesAsync();

          var result = _mapper.Map<PurchHDR,SavePurchHDRDto>(purch);

          return  Ok(result);

         }
          [HttpPut("{id}")] // api/purchases/id
         public async  Task<ActionResult<SavePurchHDRDto>> UpdatePurchase(int id,SavePurchHDRDto purchHDRDto)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);
        

         var purch = await _context.PurchHDRs.Include(pd => pd.purchDTL).SingleOrDefaultAsync(p => p.Id == id);
          

       _mapper.Map<SavePurchHDRDto,PurchHDR>(purchHDRDto,purch);
                     
                 
       
        var EditedEntities = _context.ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

        EditedEntities.ForEach(E =>
        {
            E.Property("LastUpdate").CurrentValue = DateTime.Now;
        });

         
         
         
         
        
          await _context.SaveChangesAsync();

          var result = _mapper.Map<PurchHDR,SavePurchHDRDto>(purch);

          return  Ok(result);

         }
    }
}