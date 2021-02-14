using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ALBaB.Entities.DTOs
{
    public class dbAccountsDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Key { get; set; }

        public string Name { get; set; }


        public int lvl { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public int? ParentId { get; set; }

        //public ICollection<dbAccountsDto> Children { get; set; }
        /*  public dbAccountsDto(){

             Children = new Collection<dbAccountsDto>();

         } */


    }
}