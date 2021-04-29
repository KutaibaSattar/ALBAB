using System;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities.Journal
{
    public class JournalEntryAccount
    {
         public int Id { get; private set; }

        [Required]
        public int JournalEntryId { get; private set; }
        public JournalEntry JournalEntry { get; private set; }

        [Required]
        public DateTime DueDate { get; set; }
        
        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public int AccountId { get; private set; }
        public dbAccounts Account { get; private set; }
         [Required]
        public decimal Amount { get; set; }

        public AmountType AmountType = AmountType.Credit;
        
        [Required]
        public DateTime Created { get; private set; }

        
    }
}