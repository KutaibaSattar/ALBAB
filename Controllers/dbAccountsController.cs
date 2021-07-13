using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ALBAB.Entities.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ALBAB.Entities.AppAccounts;
using AutoMapper.QueryableExtensions;
using ALBAB.Entities;

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
        public async Task<ActionResult<IEnumerable<dbAccountsRes>>> GetdbAccounts()

        {
            //var dbaccounts = await _context.dbAccounts.ToListAsync();

        //    var dbaccountsTree = _context.dbAccounts.Include(ch => ch.Children).AsEnumerable().Where(p => p.ParentId == null)
        //     .AsQueryable(); //.ToListAsync();

        //    var dbaccountsTreeResults = await Task.FromResult(dbaccountsTree.ToList());

           // var dbaccountsTreeResults = await _context.dbAccounts.Where( x => x.ParentId == null).ToListAsync();

            var dbaccounts = _context.dbAccounts.AsEnumerable().Where( x => x.ParentId == null).AsQueryable();//.ToListAsync();

             var result = await Task.FromResult(dbaccounts.ToList());

            //var result = _mapper.Map<IEnumerable<dbAccountsDto>>(dbaccountsTreeResults);
            //var result2 = _mapper.Map<IEnumerable<dbAccountsDto>>(x);


           // var result = _mapper.Map<IEnumerable<dbAccountsDto>>(dbaccounts);
            return Ok(_mapper.Map<IEnumerable<dbAccountsRes>>(result));
        }

        [HttpGet("Flatten")]
        public async Task<ActionResult<IEnumerable<dbAccountsFlattenRes>>> GetFlattdbAccounts()

        {

          //var x = _context.dbAccounts.AsEnumerable().Where( x => x.ParentId == null).ToList();

          var result = await  _context.dbAccounts.ProjectTo<dbAccountsFlattenRes>(_mapper.ConfigurationProvider).ToListAsync();

          return Ok(result);
        }



       [HttpPost]
         public async Task<ActionResult<dbAccountsRes>> createDbAccounts(dbAccountsRes dbAccount)
         {


           var dbaccount = _mapper.Map<dbAccountsRes,dbAccount>(dbAccount);



          _context.dbAccounts.Add(dbaccount);

           await _context.SaveChangesAsync();

           //var journals = await _context.journals.Include(j => j.journalAccounts).ToListAsync();

           var result = _mapper.Map<dbAccountsRes>(dbaccount);

           return Ok(result);

         }

        //[Authorize]
        // api/dbAccounts/3
        [HttpGet("{id}")]
        public async Task<ActionResult<dbAccount>> GetUser(int id)
        {
            var dbAccount = await _context.dbAccounts.FindAsync(id);

            var result = _mapper.Map<dbAccountsRes>(dbAccount);

            return Ok(result);


        }





    }


}