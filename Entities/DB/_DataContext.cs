using System;
using ALBAB.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.Products;
using ALBAB.Entities.OrderAggregate;
using ALBAB.Entities.Purchases;
using ALBAB.Entities.Journal;

namespace ALBAB.Entities.DB
{
   
   
   /* if we weren't interested in dealing with roles and getting a list of roles,this would be all 
    <AppUser, AppRole, int>
    because we want to get a list of the user roles, then we need to go a bit further and we
    need to identify every single type, unfortunately, that we need to add to identity. 
    We can't just specify our other class, which is our 
    <AppUserRole>,
    If we specify that, then we have to specify many others.
    it does give us an opportunity to ensure they're using integers, 
    just like we're using for the*/
    public class DataContext : IdentityDbContext<AppUser, AppRole, int ,IdentityUserClaim<int>,
     AppUserRole, IdentityUserLogin<int>,IdentityRoleClaim<int>,IdentityUserToken<int>> 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<PurchDtl> PurchDTLs { get; set; }
        public DbSet<PurchHdr> PurchHDRs { get; set; }

         public DbSet<dbAccounts> dbAccounts {get;set;}
         public DbSet<Product> products {get;set;}  
         public DbSet<Brand> brands  {get;set;} 

         public DbSet<Model> models {get;set;} 
         public DbSet<Order> orders { get; set; }
         public DbSet<OrderItem> orderItems { get; set; }

         public DbSet<JournalAccount> journalAccounts { get; set; }
         public DbSet<Journal.Journal> journals { get; set; }


      
        protected override void OnModelCreating(ModelBuilder builder)
        {
        base.OnModelCreating(builder);
       
        builder.Entity<AppUser>().Ignore(e => e.EmailConfirmed);
        builder.Entity<AppUser>().Ignore(e => e.PhoneNumberConfirmed);
        builder.Entity<AppUser>().Ignore(e => e.TwoFactorEnabled);
        builder.Entity<AppUser>().Ignore(e => e.LockoutEnabled);
        builder.Entity<AppUser>().Ignore(e => e.LockoutEnd);
        builder.Entity<AppUser>().Ignore(e => e.AccessFailedCount);

      
        //builder.Entity<AppUser>(x => x.Property(m => m.NormalizedUserName).HasMaxLength(256));

        // We are using int here because of the change on the PK
        builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(256));
        builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.ProviderKey).HasMaxLength(256));

        // We are using int here because of the change on the PK
        builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(256));
        builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.Name).HasMaxLength(256));
   
        
      
        
        foreach(var entity in builder.Model.GetEntityTypes())
        {
           
            
            
            // Replace table names
            entity.SetTableName(entity.GetTableName().ToSnakeCase());
           
            // Replace column names            
            foreach(var property in entity.GetProperties())
            {
               
                property.SetColumnName(property.Name.ToSnakeCase()) ;
            } 

            foreach(var key in entity.GetKeys())
            {
                key.SetName(key.GetName().ToString().ToSnakeCase());
            }

           foreach(var key in entity.GetForeignKeys())
            {
                key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
            } 

            foreach(var index in entity.GetIndexes())
            {
               index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
              
            }
        }
       
      /*  builder.Entity<AppUser>(entity =>
       {
           entity.ToTable(name:"Users");
           entity.Property(e => e.UserName).HasColumnName("UserId");
           

       }); */
       /*  builder.Entity<AppUser>().Property(u => u.UserName)
                .IsRequired().HasMaxLength(256).HasAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("UserNameIndex") { IsUnique = true, Order = 1})); */
      
      

       /* We need to configure our relationship between our AppUser to our
          AppRole and the many-to-many table to join a table that we created as well.
        So what we'll do is we'll just add some configuration in here for that. */
       
                 builder.Entity<AppUser>()
                .HasMany (ur => ur.UserRoles)
                .WithOne (u => u.User)
                .HasForeignKey (ur => ur.UserId)
                .IsRequired(); 

             builder.Entity<AppRole>()
                .HasMany (ur => ur.UserRoles)
                .WithOne (u => u.Role)
                .HasForeignKey (ur => ur.RoleId)
                .IsRequired();  

             builder.Entity<AppUser>().OwnsOne(a => a.Address, u => {
                    u.WithOwner().HasForeignKey("id");
                    u.Property<int>("appuserid");});

               builder.Entity<Order>().OwnsOne(o => o.Address, a =>{
                   a.WithOwner().HasForeignKey("id");
                   a.Property<int>("id");

               });

                 
               
               
                       
          
         

               
        }

        private string ToSnakeCase()
        {
            throw new NotImplementedException();
        }
    }
}