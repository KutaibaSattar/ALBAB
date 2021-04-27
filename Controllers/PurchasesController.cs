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

          [HttpGet("purchlist")]
         public async Task<ActionResult<IEnumerable<SavePurchHdrDto>>> Getpurchases()
         {
           var purchases = await _context.PurchHDRs.Include(d => d.purchDtl).ThenInclude(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<SavePurchHdrDto>>(purchases);

           _mapper.Map<IEnumerable<SavePurchHdrDto>,IEnumerable<PurchHdr>>(result,purchases);
          
          
           var result2 = _mapper.Map<IEnumerable<PurchHdr>>(result);
           
           return Ok(result);  

         }  
        
          [HttpGet("purchnos")]
         public async Task<ActionResult> GetpurchaseInves()
         {
          
          var listId = await  _context.PurchHDRs.Select(pur => new {Id =pur.Id, purNo = pur.purNo}).ToListAsync();

                                   
          return Ok(listId);  

         }  
          [HttpGet("purchinv/{id}")]
         public async Task<ActionResult<IEnumerable<SavePurchHdrDto>>> GetpurchaseInv(int id)
         {
           var purchases = await _context.PurchHDRs.Include(d => d.purchDtl)
           .ThenInclude(p =>p.Product).SingleOrDefaultAsync(p => p.Id == id);

          

         var result =  _mapper.Map<PurchHdr,SavePurchHdrDto>(purchases);
          
                         
          return Ok(result);  

         }  

          [HttpGet("purchdtl")]
         public async Task<ActionResult<IEnumerable<PurchDtlDto>>> GetpurchDetails()
         {
           var purchdetails = await _context.PurchDTLs.Include(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<PurchDtlDto>>(purchdetails);

            var purch = _mapper.Map<IEnumerable<PurchDtl>>(result);
           
            var newpurchase = _mapper.Map<IEnumerable<PurchDtlDto>,IEnumerable<PurchDtl>>(result,purchdetails);

           
           return Ok(purch);  

         }  

          [HttpPost]
         public async  Task<ActionResult<SavePurchHdrDto>> CreatePurchase(SavePurchHdrDto purchHDRDto)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);
         
         var purch = _mapper.Map<SavePurchHdrDto,PurchHdr>(purchHDRDto);

          purch.LastUpdate = DateTime.Now;

           foreach (var item in purch.purchDtl)
            {
                item.LastUpdate = DateTime.Now;
            }
            

         _context.PurchHDRs.Add(purch);

         await _context.SaveChangesAsync();

          var result = _mapper.Map<PurchHdr,SavePurchHdrDto>(purch);

          return  Ok(result);

         }
          [HttpPut] // api/purchases/id
         public async  Task<ActionResult<SavePurchHdrDto>> UpdatePurchase(SavePurchHdrDto purchHDRDto)
         {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);
        

        var purch = await _context.PurchHDRs.Include(pd => pd.purchDtl).SingleOrDefaultAsync(p => p.Id == purchHDRDto.Id);
        
       
       _mapper.Map<SavePurchHdrDto,PurchHdr>(purchHDRDto,purch);
                     
                 
       
        var EditedEntities = _context.ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

        EditedEntities.ForEach(E =>
        {
            E.Property("LastUpdate").CurrentValue = DateTime.Now;
        });

         
         
         
         
        
          await _context.SaveChangesAsync();

          var result = _mapper.Map<PurchHdr,SavePurchHdrDto>(purch);

          return  Ok(result);

         }

         [HttpDelete("{id}")]
         public async Task<ActionResult> DeletePurchase(int id)
         {
           var invoice = await _context.PurchHDRs.FindAsync(id);

           if (invoice == null)
              return BadRequest("No invoice found");

             _context.Remove(invoice);
             await _context.SaveChangesAsync();

              return Ok(id);
  
         }
    }
}