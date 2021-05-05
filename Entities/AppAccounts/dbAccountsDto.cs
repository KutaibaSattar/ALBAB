using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ALBAB.Entities.AppAccounts
{
    public class dbAccountsDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string KeyId { get; set; }

        public string Name { get; set; }


        public int lvl { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public int? ParentId { get; set; }

       public ICollection<dbAccountsDto> Children { get; set; }
        /*  public dbAccountsDto(){

             Children = new Collection<dbAccountsDto>();

         } */


    }
}