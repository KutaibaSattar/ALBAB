using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ALBAB.Entities.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Controllers
{
    public class dbAccountsController : BaseController
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;


        public dbAccountsController(IMapper mapper, DataContext context)
        {

            _context = context;

            _mapper = mapper;

        }


               //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbAccountsDto>>> GetdbAccounts()

        {
            //var dbaccounts = await _context.dbAccounts.ToListAsync();

           var dbaccountsTree = _context.dbAccounts.Include(ch => ch.Children).AsEnumerable().Where(p => p.ParentId == null)
            .AsQueryable(); //.ToListAsync();

           var dbaccountsTreeResults = await Task.FromResult(dbaccountsTree.ToList());

            var result = _mapper.Map<IEnumerable<dbAccountsDto>>(dbaccountsTreeResults);

           // var result = _mapper.Map<IEnumerable<dbAccountsDto>>(dbaccounts);
            return Ok(result);
        }

        [HttpGet("Flatten")]
        public async Task<ActionResult<IEnumerable<dbAccountsFlattenRes>>> GetFlattdbAccounts()

        {
            var dbaccounts = await _context.dbAccounts.ToListAsync();

          var x = _context.dbAccounts.AsEnumerable().Where( x => x.ParentId == null).ToList();

          var y = _context.dbAccounts.Include(ch => ch.Children).ToList();

           var dbaccountsTree = _context.dbAccounts.Include(ch => ch.Children).AsEnumerable().Where(p => p.ParentId == null)
            .AsQueryable(); //.ToListAsync();

           var dbaccountsTreeResults = await Task.FromResult(dbaccountsTree.ToList());

           // var result = _mapper.Map<IEnumerable<dbAccountsFlattenRes>>(dbaccounts);

           // var result = _mapper.Map<IEnumerable<dbAccountsDto>>(dbaccounts);
            return Ok(dbaccounts);
        }



       [HttpPost]
         public async Task<ActionResult<dbAccountsDto>> createDbAccounts(dbAccountsDto dbAccount)
         {


           var dbaccount = _mapper.Map<dbAccountsDto,dbAccounts>(dbAccount);



          _context.dbAccounts.Add(dbaccount);

           await _context.SaveChangesAsync();

           //var journals = await _context.journals.Include(j => j.journalAccounts).ToListAsync();

           var result = _mapper.Map<dbAccountsDto>(dbaccount);

           return Ok(result);

         }

        //[Authorize]
        // api/dbAccounts/3
        [HttpGet("{id}")]
        public async Task<ActionResult<dbAccounts>> GetUser(int id)
        {
            var dbAccount = await _context.dbAccounts.FindAsync(id);

            var result = _mapper.Map<dbAccountsDto>(dbAccount);

            return Ok(result);


        }





    }


}