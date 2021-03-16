using System.Collections.Generic;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.Products;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Controllers
{
    public class BrandsController :BaseController
    {
         
         private readonly DataContext _context;
       private readonly IMapper _mapper;
      
      

        public BrandsController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
           

        }
         
         [HttpGet("brands")]
         public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
         {
           var brands = await _context.brands.Include(m => m.models).ToListAsync();

           var result = _mapper.Map<IEnumerable<BrandDto>>(brands);
           
           return Ok(result);  

         }  
        
    }
}