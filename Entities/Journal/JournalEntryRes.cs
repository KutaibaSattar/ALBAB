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
       public int JournalNo { get; set; }
       public int JournalType { get; set; }
       public string Note { get; set; }
       public DateTime Created { get; set; }

        public ICollection<JournalAccountRes> JournalAccount { get; set; } 
            = new Collection<JournalAccountRes>();
        
       



        
    }
}