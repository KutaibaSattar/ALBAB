using System;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities.Journal
{
    public class JournalAccount
    {
        public int Id { get; private set; }
        public int JournalId { get; set; }
        public JournalEntry Journal { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now;
        public dbAccounts Account { get; set; }
        public int AccountId { get; set; }
        public AppUser AppUser { get; set; }
        public int? AppUserId { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string RefNo { get; set; }
    }
}