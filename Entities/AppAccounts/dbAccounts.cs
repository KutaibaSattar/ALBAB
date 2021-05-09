using System;
using System.Collections.Generic;


namespace ALBAB.Entities.AppAccounts
{
        public class dbAccounts
    {
        public int Id { get; set; }

        public string KeyId { get; set; }

        public string Name { get; set; }

        public int lvl { get; set; }

        public DateTime Created { get; set; }   = DateTime.Now;

        public dbAccounts Parent { get; set; }

       // Reference for self
        public int? ParentId {get;set;}
        public ICollection <dbAccounts> Children { get; set; }

        public bool IsExpandable {get;set;} = false;

      /*   public dbAccounts(){

            Children = new Collection<dbAccounts>();

        } */

    }


}