using System;

namespace ALBAB.Entities.AppAccounts
{
    public class dbAccountsFlattenRes
    {
        public int Id { get; set; }

        public string KeyId { get; set; }

        public string Name { get; set; }


        public int lvl { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public int? ParentId { get; set; }

        public bool IsExpandable {get;set;} = false;




    }
}