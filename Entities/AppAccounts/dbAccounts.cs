using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ALBAB.Entities.AppAccounts
{
        public class dbAccount
    {
        public int Id { get; set; }

       [Required]
        public string KeyId { get; set; }

        [Required]
        public string Name { get; set; }


        public int lvl { get; set; }

        public DateTime Created { get; set; }   = DateTime.Now;

        public dbAccount Parent { get; set; }

       // Reference for self

        public int? ParentId {get;set;}
        public ICollection <dbAccount> Children { get; set; }

        public bool IsExpandable {get;set;} = false;

      /*   public dbAccounts(){

            Children = new Collection<dbAccounts>();

        } */

    }


}