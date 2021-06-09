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
        public string RefNo { get; set; }
        [Required]
        public DateTime Created { get; set; }

    //     private int? _userId ;
    //     public int? AppUserId { // if 0 then set null and update accountId in JournalAccount entity

    //           get => _userId;
    //           set => _userId = (value ==0 ) ? null : value ;
    //          }
    //    private int _accountId;
    //     public int AccountId { // if AppUserId is not null then AccountId = 30(Clients)

    //          get => _accountId;

    //           set => _accountId = (AppUserId == null) ? value : (int)(ReservedAccountsType.Clients); }

        public int? AppUserId{ get; set; }
       [Required]
        public int AccountId { get; set; }

        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }

    }
}