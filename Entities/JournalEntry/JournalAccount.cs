using System;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities.JournalEntry
{
    public class JournalAccount
    {
        public JournalAccount()
        {
        }

        public JournalAccount(DateTime created, DateTime dueDate, int? appUserId, int accountId, decimal? credit,decimal? debit)
        {
           this.Id = Id;
            this.Created = created;
            this.DueDate = dueDate;
            this.AppUserId = appUserId;
            this.dbAccountId = accountId;
            this.Credit = credit;
            this.Debit = debit;

        }
        // public JournalAccount(int Id, DateTime created, DateTime dueDate, int? appUserId, int accountId, decimal? credit,decimal? debit)
        // {
        //    this.Id = Id;
        //     this.Created = created;
        //     this.DueDate = dueDate;
        //     this.AppUserId = appUserId;
        //     this.AccountId = accountId;
        //     this.Credit = credit;
        //     this.Debit = debit;

        // }

        public int Id { get; set; }
        public Journal Journal { get; set; }
        public int JournalId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now;
        public AppUser AppUser { get; set; }
        public int? AppUserId { get; set; }
        public dbAccount dbAccount { get; set; }
        public int dbAccountId { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string RefNo { get; set; }
    }
}