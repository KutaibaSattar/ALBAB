using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace ALBAB.Entities.Journal
{
    public class JournalEntry
    {

        public int Id { get; private set; }
        [Required]
        public string JENo { get; set; }
        public JournalType type { get; set; }
        public string Note { get; set; }
        public DateTime entryDate { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<JournalAccount> journalAccounts { get; set; }
            = new Collection<JournalAccount>();


    }
}