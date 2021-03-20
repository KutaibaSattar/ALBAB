using ALBAB.Entities.DB;
using AutoMapper;

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
    }
}