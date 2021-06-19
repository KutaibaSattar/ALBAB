using System;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities.Journal
{
    public class JournalAccount
    {
        public JournalAccount()
        {
        }

        public JournalAccount( DateTime created, DateTime dueDate, int? appUserId, int accountId, decimal? credit,decimal? debit)
        {
            this.Created = created;
            this.DueDate = dueDate;
            this.AppUserId = appUserId;
            this.AccountId = accountId;
            this.Credit = credit;
            this.Debit = debit;

        }

        public int Id { get; set; }
        public JournalEntry Journal { get; set; }
        public int JournalId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now;
        public AppUser AppUser { get; set; }
        public int? AppUserId { get; set; }
        public dbAccounts Account { get; set; }
        public int AccountId { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string RefNo { get; set; }
    }
}