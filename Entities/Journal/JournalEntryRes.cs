using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALBAB.Entities.Journal
{
    public class JournalEntryRes

    {
       public int Id { get; private set; }
       public string JENo { get; set; }

       public JournalType Type { get; set; }
       public string Note { get; set; }
       public DateTime Created { get; set; }
      public DateTime entryDate { get; set; } = DateTime.Now;
        public ICollection<JournalAccountRes> journalAccounts { get; set; }
            = new Collection<JournalAccountRes>();

    }
}