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

        [HttpPost]
         public async Task<ActionResult<IEnumerable<JournalEntryRes>>> getJournals(JournalEntry journalEntryRes)
         {
                    
          
           //var journal = _mapper.Map<JournalEntryRes,JournalEntry>(journalEntryRes);
           
           _context.journals.Add(journalEntryRes);
          
           await _context.SaveChangesAsync();

           var journals = await _context.journals.Include(j => j.journalAccounts).ToListAsync();
           
           return Ok(journals);  

         }  


   
        
    }
}