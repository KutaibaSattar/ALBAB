using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ALBAB.Entities.JournalEntry
{
    public class AccountStatementRes
    {

       public int Id { get; set; }
        public string JENo { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime Created { get; set; }
        public int JournalId { get; set; }
        public DateTime DueDate { get; set; }
        public int? AppUserId { get; set; }
        public String SupplierName { get; set; }
        public int dbAccountId { get; set; }
        public String AccountName { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string RefNo { get; set; }






    }
}