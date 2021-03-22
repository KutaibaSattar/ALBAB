using System.Collections.Generic;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.Purchases;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

          [HttpGet("purchinv")]
         public async Task<ActionResult<IEnumerable<PurchHDR>>> Getpurchases()
         {
           var purchases = await _context.PurchHDRs.Include(d => d.purchDTL).ThenInclude(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<PurchHDRDto>>(purchases);
           
           return Ok(result);  

         }  

          [HttpGet("purchdtl")]
         public async Task<ActionResult<IEnumerable<PurchHDR>>> GetpurchDetails()
         {
           var purchdetails = await _context.PurchDTLs.Include(p =>p.Product).ToListAsync();

           var result = _mapper.Map<IEnumerable<PurchDTLDto>>(purchdetails);
           
           return Ok(result);  

         }  
    }
}