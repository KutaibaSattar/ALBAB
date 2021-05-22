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
        public int JENo { get; set; }
        public string Type { get; set; } = Journal.JournalType.Journal ;
        public string Note { get; set; }
        public DateTime entryDate { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<JournalAccount> journalAccounts { get; set; }
            = new Collection<JournalAccount>();


    }
}