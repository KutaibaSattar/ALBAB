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
        public int EntryId { get; set; }
       
        [Required]
        public DateTime EntryDate { get; set; }
        public string Note { get; set; }
       
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; private set; }

        public ICollection<JournalEntryAccount> Accounts { get; set; } = new Collection<JournalEntryAccount>();

        
        
    }
}