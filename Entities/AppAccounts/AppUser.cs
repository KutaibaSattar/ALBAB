using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.DB;
using ALBAB.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;

namespace ALBAB.Entities.AppAccounts
{
    public class AppUser : IdentityUser<int>
    {

      [Required]
      [MaxLength(50)]
      public string Name {get;set;}
      public DateTime Created { get; set; }   = DateTime.Now;
      public DateTime LastActive { get; set; } =  DateTime.Now;
      public AccountType type { get; set; }
      public string LookingFor { get; set; }
      public string Interests { get; set; }
     public Address Address {get;set;}
     public ICollection<AppUserRole> UserRoles { get; set; }
     public ICollection<Order> orders { get; set; }

    public AppUser(){

      orders = new Collection<Order>();

    }


    }
}