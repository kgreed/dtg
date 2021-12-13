using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects {
    // This code allows our Model Editor to get relevant EF Core metadata at design time.
    // For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
	public class DtgContextInitializer : DbContextTypesInfoInitializerBase {
		protected override DbContext CreateDbContext() {
			var optionsBuilder = new DbContextOptionsBuilder<DtgEFCoreDbContext>()
                .UseSqlServer(@";");
            return new DtgEFCoreDbContext(optionsBuilder.Options);
		}
	}
	//This factory creates DbContext for design-time services. For example, it is required for database migration.
	public class DtgDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DtgEFCoreDbContext> {
		public DtgEFCoreDbContext CreateDbContext(string[] args) {
			throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
			//var optionsBuilder = new DbContextOptionsBuilder<DtgEFCoreDbContext>();
			//optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Dtg");
			//return new DtgEFCoreDbContext(optionsBuilder.Options);
		}
	}
	[TypesInfoInitializer(typeof(DtgContextInitializer))]
	public class DtgEFCoreDbContext : DbContext {
		public DtgEFCoreDbContext(DbContextOptions<DtgEFCoreDbContext> options) : base(options) {
		}
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
	    public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationUserLoginInfo> UserLoginInfos { get; set; }

        public DbSet<Guru> Gurus { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Attribute> Attributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Dtg.Module.BusinessObjects.ApplicationUserLoginInfo>(b => {
                b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
            });
        }
	}
    [NavigationItem("Main")]
	[Table("Gurus")]
    public class Guru
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


    }
    [NavigationItem("Main")]
    [Table("Ratings")]

    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public virtual User User { get; set; }

        public decimal Score { get; set; }

        public int AttributeId { get; set; }
        [ForeignKey("AttributeId")]
        [Required]
        public virtual Attribute Attribute { get; set; }

        public DateTime ScoredAt { get; set; }


    }
    [NavigationItem("Main")]
    [Table("Attributes")]
    public class Attribute
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class User : ApplicationUser
    {
        public string DiscordName { get; set; }
    }
}
