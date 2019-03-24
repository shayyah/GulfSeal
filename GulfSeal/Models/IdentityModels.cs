using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GulfSeal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleContent> ArticleContents { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Media> Medias { get; set; } 
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyTransition> FamilyTransitions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<partenrs> partenrs { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<InformationType> InformationTypes { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<InformationTranslation> InformationTranslations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTranslation> ProjectTranslations { get; set; }

        public DbSet<StaticContent> StaticContents { get; set; }
        public DbSet<StaticContentTranslation> StaticContentTranslations { get; set; }


    }
}
