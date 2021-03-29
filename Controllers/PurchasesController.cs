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
        

         var purch = await _context.PurchHDRs.Include(pd => pd.purchDTL).SingleOrDefaultAsync(p => p.Id == id);
           //var purch = await _context.PurchHDRs.Include(pd => pd.purchDTL).ProjectTo<PurchHDRDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(p => p.Id == id);
        /*  foreach (var item in purch.purchDTL)
            {
                item.LastUpdate = DateTime.Now;
            }
         
          purch.LastUpdate = DateTime.Now; */

      var newpurchase = _mapper.Map<PurchHDRDto,PurchHDR>(purchHDRDto,purch);
                         _mapper.Map<PurchDTLDto,PurchDTL>(purchHDRDto.purchDTLDtos,purch.purchDTL);
           /* foreach (var item in purch.purchDTL)
            {
                  item.LastUpdate = DateTime.Now;
            } */
        
       /*  var AddedEntities = _context.ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

        AddedEntities.ForEach(E =>
        {
            E.Property("LastUpdate").CurrentValue = DateTime.Now;
        }); */


        var EditedEntities = _context.ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

        EditedEntities.ForEach(E =>
        {
            E.Property("LastUpdate").CurrentValue = DateTime.Now;
        });

     
                
         
         /*  var modifiedEntries =  _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
          
          foreach (var entity in modifiedEntries)
          {
                  
                  if (entity.State == EntityState.Modified)
                  {
                    
                  }
                  
                    foreach (var propName in entity.Properties)
                    {
                        if (propName.IsModified){
                         var current = entity.CurrentValues[propName.ToString()];
                        var original = entity.OriginalValues[propName.ToString()];
                        }
                       
                    }
          } */

         
        
          await _context.SaveChangesAsync();

          var result = _mapper.Map<PurchHDR,PurchHDRDto>(purch);

          return  Ok(result);

         }
    }
}