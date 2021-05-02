using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ALBAB.Entities.Journal
{
    public class JournalEntry
    {
        public int Id { get; private set; }
      
        [Required]
        public int JournalNo { get; set; }
        public int JournalType { get; set; }
        public string Note { get; set; }
        public DateTime Created { get; set; }

        public ICollection<JournalAccount> journalAccounts { get; set; } 
            = new Collection<JournalAccount>();
        
        
    }
}