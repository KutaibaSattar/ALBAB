using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Entities.JournalEntry
{

   //[Index(nameof(JENo), nameof(Type), IsUnique = true)]
    public class Journal
    {
        public Journal(){}

        public Journal(string jeNo, JournalType type, DateTime entryDate){
            this.JENo = jeNo;
            this.Type = type;
            this.EntryDate = entryDate;

        }

        public int Id { get;set; }

        [Required]
        [MaxLength(20)]
        public string JENo { get; set; }
        [Required]
        [MaxLength(10)]
        public JournalType Type { get; set; }
        public string Note { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<JournalAccount> journalAccounts { get; set; }
            = new Collection<JournalAccount>();


    }
}