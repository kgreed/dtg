using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.ExpressApp.EFCore.Updating;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
 

namespace Dtg.Module.BusinessObjects
{

    // This code allows our Model Editor to get relevant EF Core metadata at design time.
    // For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
    public class DtgContextInitializer : DbContextTypesInfoInitializerBase
    {
        protected override DbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DtgEFCoreDbContext>()
                .UseSqlServer(@";");
            return new DtgEFCoreDbContext(optionsBuilder.Options);
        }
    }
    public class DtgDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DtgEFCoreDbContext>
    {
        public DtgEFCoreDbContext CreateDbContext(string[] args)
        {
            throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
            var optionsBuilder = new DbContextOptionsBuilder<DtgEFCoreDbContext>();
            optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Dtg");
            return new DtgEFCoreDbContext(optionsBuilder.Options);
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

        //todo add unique name key
        public DbSet<Guru> Gurus { get; set; }

        //todo users should only be able to edit or delete the records they added
        public DbSet<RatingHeader> RatingHeaders { get; set; }
        public DbSet<RatingEntry> RatingEntries { get; set; }
        public DbSet<Rater> Raters{ get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<ReportDataV2> ReportDataV2 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ApplicationUserLoginInfo>(b => {
                b.HasIndex(nameof(ISecurityUserLoginInfo.LoginProviderName), 
                    nameof(ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
            });
            modelBuilder.Entity<Guru>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Metric>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<RatingEntry>().Property(x => x.Score).HasPrecision(18, 8);

            modelBuilder.Entity<RatingEntry>().HasIndex(p => new { p.RatingHeaderId, p.MetricId }).IsUnique();

        }
    }

    [NavigationItem("Main")]
    [Table("RatingHeaders")]
    [DefaultProperty("Summary")]
    public class RatingHeader
    {
        public RatingHeader()
        {
            Entries = new List<RatingEntry>();

        }

        [Browsable(false)]
        [Key] public int Id { get; set; }
        [Browsable(false)]
        public int GuruId { get; set; }
        [ForeignKey("GuruId")] [System.ComponentModel.DataAnnotations.Required] public virtual Guru Guru { get; set; }

        public DateTime ScoredAt { get; set; }
        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Required] public int RaterId { get; set; }
        [ForeignKey("RaterId")] public virtual Rater Rater { get; set; }

        public string Summary => $"{Rater?.Name} {ScoredAt.Date.ToLocalTime()} {Guru?.Name}";
        [Aggregated]
        public virtual List<RatingEntry> Entries { get; set; }
    }

    [NavigationItem("Main")]
    [Table("RatingEntries")]
    [VisibleInReports]
    public class RatingEntry
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Browsable(false)]
        public int RatingHeaderId { get; set; }
        [ForeignKey("RatingHeaderId")]
        public virtual RatingHeader RatingHeader { get; set; }
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        //[DataType("decimal(18,5)")] //https://stackoverflow.com/questions/8243008/format-of-the-initialization-string-does-not-conform-to-specification-starting-a
        public decimal Score { get; set; }
        [Browsable(false)]
        public int MetricId { get; set; }
        [ForeignKey("MetricId")] [System.ComponentModel.DataAnnotations.Required] public virtual Metric Metric { get; set; }

        public string MetricName => Metric?.Name;
        public string RaterName => RatingHeader.Rater.Name;
        public string GuruName => RatingHeader.Guru.Name;
    }
    [NavigationItem("Main")]
    [Table("Metrics")]
    public class Metric
    {
        [Key]
        [Browsable(false)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    [NavigationItem("Main")]
    [Table("Gurus")]
    public class Guru
    {
        [Key]
        [Browsable(false)]
        public int Id { get; set; }

        public string Name { get; set; }



    }
    [NavigationItem("Main")]
    [Table("Rater")]
    public class Rater
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}