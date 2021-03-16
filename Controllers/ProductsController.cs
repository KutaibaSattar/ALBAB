using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.Products;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Controllers
{
    public class ProductsController : BaseController
    {
       private readonly DataContext _context;
       private readonly IMapper _mapper;
      
      

        public ProductsController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
           

        }

        [HttpGet("products")]
         public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
         {
           var products = await _context.products.Include(m => m.Model).ThenInclude(b => b.Brand).ToListAsync();

           var result = _mapper.Map<IEnumerable<ProductDto>>(products);
           
           return Ok(result);  

         }  
        
    }
}