using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.EntityFrameworkCore;
namespace Dtg.Module.BusinessObjects
{

    // This code allows our Model Editor to get relevant EF Core metadata at design time.
    // For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
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
}