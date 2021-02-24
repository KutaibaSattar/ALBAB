using System.Reflection;
using ALBaB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ALBaB.Data
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

         public DbSet<dbAccounts> dbAccounts {get;set;}


      
        protected override void OnModelCreating(ModelBuilder builder)
        {
        base.OnModelCreating(builder);
        builder.Entity<AppUser>().Ignore(e => e.EmailConfirmed);
        builder.Entity<AppUser>().Ignore(e => e.PhoneNumberConfirmed);
        builder.Entity<AppUser>().Ignore(e => e.TwoFactorEnabled);
        builder.Entity<AppUser>().Ignore(e => e.LockoutEnabled);
        builder.Entity<AppUser>().Ignore(e => e.LockoutEnd);
        builder.Entity<AppUser>().Ignore(e => e.AccessFailedCount);
       
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
                       
          
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

               
        }

       
    }
}