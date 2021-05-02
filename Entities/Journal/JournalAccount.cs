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

        public DateTime DueDate { get; set; }

        public JournalType JournalType { get; set; } = JournalType.JRV;
               
        public int AccountId { get; set; }
        public dbAccounts Account { get; set; }
        
        public decimal Amount { get; set; }

        public AmountType AmountType { get; set; } = AmountType.Credit;

        public DateTime Created { get; set; }

    }
}