using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeITCMS.Models
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
            : base("DefaultConnection_DB", throwIfV1Schema: false)
        {
        }

        public DbSet<MenuContext> MenuContexts { get; set; }
        public DbSet<BannerContext> BannerContexts { get; set; }
        public DbSet<PageContext> PageContexts { get; set; }
        public DbSet<PhoneContext> PhoneContexts { get; set; }
        public DbSet<LogoContext> LogoContexts { get; set; }
        public DbSet<FooterContext> FooterContexts { get; set; }
        public DbSet<QueryContext> QueryContexts { get; set; }
        public DbSet<BlogContext> BlogContexts { get; set; }
        public DbSet<HelpAndAdviceCategory> HelpAndAdviceCategories { get; set; }
        public DbSet<HelpAndAdviceDetail> HelpAndAdviceDetails { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class MenuContext
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public int TabIndex { get; set; }
    }

    public class BannerContext
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string ImagePath { get; set; }
    }

    public class PageContext
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PageTitle { get; set; }

        public string PageContent { get; set; }

        public string LinkedMenu { get; set; }

        public string FeatureImage { get; set; }

        public string FeatureText { get; set; }
    }

    public class PhoneContext
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Phone { get; set; }
    }

    public class LogoContext
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AltText { get; set; }

        public string LogoPath { get; set; }
    }

    public class FooterContext
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

    }

    public class QueryContext
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Query { get; set; }
    }

    public class BlogContext
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string BlogName { get; set; }

        public string BlogContent { get; set; }

        public string BloggerName { get; set; }

        public string BlogDate { get; set; }
    }

    public class HelpAndAdviceCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Category { get; set; }
    }

    public class HelpAndAdviceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public int CategoryId { get; set; }
        public string Content { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}