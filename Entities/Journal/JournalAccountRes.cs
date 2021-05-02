using System;
using System.ComponentModel.DataAnnotations;

namespace ALBAB.Entities.Journal
{
    public class JournalAccountRes
    {
        public int Id { get; set; }
  
        public  int journalId { get; set; }
  
        [Required]
        public DateTime DueDate { get; set; }

         public JournalType JournalType { get; set; } = JournalType.JRV;
        
        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public int AccountId { get; set; }
     
        
        public decimal Amount { get; set; }

        public AmountType AmountType { get; set; } = AmountType.Credit;
        
        [Required]
        public DateTime Created { get; set; }

    }
}