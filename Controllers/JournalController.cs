using System.Collections.Generic;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using ALBAB.Entities.Journal;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Controllers
{
    public class JournalController : BaseController
    {
         private readonly DataContext _context;
       private readonly IMapper _mapper;



        public JournalController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpGet("journallist")]
         public async Task<ActionResult<IEnumerable<JournalEntryRes>>> getJournals()
         {

           var journals = await _context.journals.Include(j => j.journalAccounts).ToListAsync();

           var result = _mapper.Map<IEnumerable<JournalEntryRes>>(journals);

           return Ok(result);

         }


        [HttpPost]
         public async Task<ActionResult<JournalEntryRes>> createJournals(JournalEntryRes journalEntryRes)
         {

           

           var journal = _mapper.Map<JournalEntryRes,JournalEntry>(journalEntryRes);

          _context.journals.Add(journal);

           await _context.SaveChangesAsync();

           //var journals = await _context.journals.Include(j => j.journalAccounts).ToListAsync();

           var result = _mapper.Map<JournalEntryRes>(journal);

           return Ok(result);

         }
        [HttpPut]
         public async Task<ActionResult<JournalEntryRes>> updateJournals(JournalEntryRes journalEntryRes)
         {


            if (!ModelState.IsValid)
               return BadRequest(ModelState);
          var journal = await _context.journals.Include(pd => pd.journalAccounts).SingleOrDefaultAsync(p => p.Id == journalEntryRes.Id);

          if (journal == null)
              return BadRequest("Journal not founded");



         _mapper.Map<JournalEntryRes,JournalEntry>(journalEntryRes,journal);

           await _context.SaveChangesAsync();

           //var journals = await _context.journals.Include(j => j.journalAccounts).ToListAsync();

           var result = _mapper.Map<JournalEntryRes>(journal);

           return Ok(result);

         }




    }
}