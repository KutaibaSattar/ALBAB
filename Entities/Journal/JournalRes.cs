using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALBAB.Entities.Journal
{
    public class JournalRes
    {
         public int Id { get; private set; }
       
        [Required]
        public int EntryId { get; set; }
       
        [Required]
        public DateTime EntryDate { get; set; }
        public string Note { get; set; }
       
        public DateTime Created { get; set; }

        public ICollection<JournalAccountRes> Accounts  { get; set; } 
            = new Collection<JournalAccountRes>(); 
       



        
    }
}