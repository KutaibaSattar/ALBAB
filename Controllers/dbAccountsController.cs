using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALBaB.Entities;
using AutoMapper;
using ALBaB.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ALBaB.Controllers;
using ALBaB.Entities.DTOs;

namespace ALBaB.Controllers
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


            var dbaccounts = await _context.dbAccounts.ToListAsync();

           /*  var dbaccounts = _context.dbAccounts.Include(ch => ch.Children).AsEnumerable().Where(p => p.ParentId == null)
            .AsQueryable(); //.ToListAsync(); */

            //var dbaccountsTree = await Task.FromResult(dbaccounts.ToList());
           
            var result = _mapper.Map<IEnumerable<dbAccountsDto>>(dbaccounts);
     

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