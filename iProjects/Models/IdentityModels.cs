using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace iProjects.Models
{
    
    public class ApplicationUserLogin : IdentityUserLogin<string> { }
    public class ApplicationUserClaim : IdentityUserClaim<string> { }
    public class ApplicationUserRole : IdentityUserRole<string> { }

    // Must be expressed in terms of our custom Role and other types:
    public class ApplicationUser
        : IdentityUser<string, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

        }


        public async Task<ClaimsIdentity>
            GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<TasksToDo> TasksToDos { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string User_name { get; set; }

        public string City { get; set; }        
    }


    // Must be expressed in terms of our custom UserRole:
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

    }


    // Must be expressed in terms of our custom types:
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        //static ApplicationDbContext()
        //{
        //    Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // Add the ApplicationGroups property:
        public virtual IDbSet<ApplicationGroup> ApplicationGroups { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        // Override OnModelsCreating:
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Project>()
                .Property(p => p.ProjectId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TasksToDo>()
                .Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationUserGroup>((ApplicationGroup g) => g.ApplicationUsers)
                .WithRequired().HasForeignKey<string>((ApplicationUserGroup ag) => ag.ApplicationGroupId);
            modelBuilder.Entity<ApplicationUserGroup>()
                .HasKey((ApplicationUserGroup r) =>
                    new
                    {
                        ApplicationUserId = r.ApplicationUserId,
                        ApplicationGroupId = r.ApplicationGroupId
                    }).ToTable("ApplicationUserGroups");

            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationGroupRole>((ApplicationGroup g) => g.ApplicationRoles)
                .WithRequired().HasForeignKey<string>((ApplicationGroupRole ap) => ap.ApplicationGroupId);
            modelBuilder.Entity<ApplicationGroupRole>().HasKey((ApplicationGroupRole gr) =>
                new
                {
                    ApplicationRoleId = gr.ApplicationRoleId,
                    ApplicationGroupId = gr.ApplicationGroupId
                }).ToTable("ApplicationGroupRoles");

            modelBuilder.Entity<Project>().HasRequired(d => d.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<TasksToDo>().HasRequired(d => d.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Project>().HasOptional(s => s.ProjectCategory).WithMany().HasForeignKey(t => t.ProjectCategoryId);
            modelBuilder.Entity<Project>().HasOptional(s => s.StatusOfStoriesTasks).WithMany().HasForeignKey(t => t.StatusId);
            modelBuilder.Entity<Project>().HasOptional(s => s.Priority).WithMany().HasForeignKey(t => t.PriorityId);
            modelBuilder.Entity<Story>().HasOptional(s => s.StatusOfStoriesTasks).WithMany().HasForeignKey(t => t.StatusId);
            modelBuilder.Entity<Story>().HasOptional(s => s.Releases).WithMany().HasForeignKey(t => t.ReleaseId);
            modelBuilder.Entity<Story>().HasOptional(s => s.Priority).WithMany().HasForeignKey(t => t.PriorityId);

        }

        public System.Data.Entity.DbSet<iProjects.Models.ProjectCategory> ProjectCategories { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Clients> Clients { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.ProjectFilesDb> ProjectFilesDbs { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.SupportTicket> SupportTickets { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.TasksToDo> TasksToDoes { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Payment> Payments { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Invoice> Invoices { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.BugReport> BugReports { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.BugType> BugTypes { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.CrashReport> CrashReports { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.HeadLineOne> HeadLineOnes { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.HeadLineTwo> HeadLineTwos { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.HeadLineThree> HeadLineThrees { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Dictionary> Dictionaries { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Epic> Epics { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Priority> Priorities { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Sprint> Sprints { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.StatusOfStoriesTasks> StatusOfStoriesTasks { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Releases> Releases { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.Story> Stories { get; set; }

        public System.Data.Entity.DbSet<iProjects.Models.InternalMail> InternalMails { get; set; }

     
    }


    // custom versions of all the other types:
    public class ApplicationUserStore
        : UserStore<ApplicationUser, ApplicationRole, string,
            ApplicationUserLogin, ApplicationUserRole,
            ApplicationUserClaim>, IUserStore<ApplicationUser, string>,
        IDisposable
    {
        public ApplicationUserStore()
            : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }
    }


    public class ApplicationRoleStore
    : RoleStore<ApplicationRole, string, ApplicationUserRole>,
    IQueryableRoleStore<ApplicationRole, string>,
    IRoleStore<ApplicationRole, string>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationRoleStore(DbContext context)
            : base(context)
        {
        }
    }


    public class ApplicationGroup
    {
        public ApplicationGroup()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ApplicationRoles = new List<ApplicationGroupRole>();
            this.ApplicationUsers = new List<ApplicationUserGroup>();
        }

        public ApplicationGroup(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationGroup(string name, string description)
            : this(name)
        {
            this.Description = description;
        }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ApplicationGroupRole> ApplicationRoles { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUsers { get; set; }
    }


    public class ApplicationUserGroup
    {
        public string ApplicationUserId { get; set; }
        public string ApplicationGroupId { get; set; }
    }

    public class ApplicationGroupRole
    {
        public string ApplicationGroupId { get; set; }
        public string ApplicationRoleId { get; set; }
    }

}