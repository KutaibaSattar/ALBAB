using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ALBAB.Entities.JournalEntry
{
    public class AccountStatementRes
    {

       public int Id { get; set; }
        public string JournalJENo { get; set; }
        public string JournalType { get; set; }
        public string JournalNote { get; set; }
        public DateTime JournalEntryDate { get; set; }
        public DateTime Created { get; set; }
        public int JournalId { get; set; }
        public DateTime DueDate { get; set; }
        public int? AppUserId { get; set; }
        public int AccountId { get; set; }

        public String AccountName { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string RefNo { get; set; }






    }
}