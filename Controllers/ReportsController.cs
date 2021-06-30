

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.JournalEntry;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class ReportsController : BaseController
    {

       private readonly DataContext _context;
       private readonly IMapper _mapper;
        public ReportsController(DataContext context, IMapper mapper)
        {
             _mapper = mapper;
            _context = context;
        }

         [HttpGet("accountstatement")]
         public async Task<ActionResult<IEnumerable<AccountStatementRes>>> getJournals()
         {

          var journalAccounts = await _context.journalAccounts.Include( j => j.Journal).Include(a => a.dbAccount).Include(s => s.AppUser).ToListAsync();
           //var journals = await _context.journals.Include( a => a.journalAccounts).ToListAsync();

          var result = _mapper.Map<IEnumerable<AccountStatementRes>>(journalAccounts);
          // var result = _mapper.Map<AccountStatementRes>(journals);

          return Ok(result);

         }


    }
}