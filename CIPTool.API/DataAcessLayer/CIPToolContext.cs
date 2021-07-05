using BusinessObjectLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer
{
    public class CIPToolContext : IdentityDbContext<IdentityUser> 
    {
        public override DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdeaEntity> Ideas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FinancialReportEntity> FinancialReports { get; set; }
        public DbSet<LeaderResponse> LeaderResponses { get; set; }
        public DbSet<BonusEntity> Bonuses { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<BonusRangeEntity> BonusRanges { get; set; }
        public DbSet<BonusCorrectionFactorEntity> BonusCorrectionFactors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("server=CLJ-C-0005V;database=CIPTool;trusted_connection=true")
                //.UseSqlServer("Server=CLJZ2230\\SQLEXPRESS,49172; Database=CIPTool_Test; Integrated Security=False; User Id=cip;Password=Pas$1234;")
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Associate>();
            modelBuilder.Entity<Leader>();

            modelBuilder.Entity<Associate>()
               .HasOne(a => a.Leader)
               .WithMany(l => l.Associates);

            modelBuilder.Entity<Associate>()
                .HasMany(a => a.Ideas)
                .WithOne(i => i.Associate)
                .HasForeignKey(i => i.AssociateId);

            modelBuilder.Entity<Associate>()
               .HasMany(a => a.Responses)
               .WithOne(i => i.Reviewer)
               .HasForeignKey(i => i.ReviewerId);

            modelBuilder.Entity<IdeaEntity>()
                .HasMany(i => i.Attachments)
                .WithOne(a => a.Idea);

            modelBuilder.Entity<IdeaEntity>()
                .HasMany(i => i.LeaderResponses)
                .WithOne(lr => lr.Idea)
                .HasForeignKey(lr => lr.IdeaId);

            modelBuilder.Entity<FinancialReportEntity>()
                .HasOne(fr => fr.Idea)
                .WithOne(i => i.FinancialReport)
                .HasForeignKey<FinancialReportEntity>(fr => fr.IdeaId);

            modelBuilder.Entity<BonusEntity>()
                .HasOne(b => b.FinancialReport)
                .WithOne(fr => fr.Bonus)
                .HasForeignKey<BonusEntity>(b => b.FinancialReportId);

            SeedStatuses(modelBuilder);
        }

        private void SeedStatuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 1, LowerBound = -10000000.0m, UpperBound = 500.0m, Award = 0.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 2, LowerBound = 500.0m, UpperBound = 1000.0m, Award = 50.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 3, LowerBound = 1000.0m, UpperBound = 2000.0m, Award = 90.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 4, LowerBound = 2000.0m, UpperBound = 3000.0m, Award = 160.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 5, LowerBound = 3000.0m, UpperBound = 6000.0m, Award = 300.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 6, LowerBound = 6000.0m, UpperBound = 10000.0m, Award = 300.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 7, LowerBound = 10000.0m, UpperBound = 20000.0m, Award = 500.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 8, LowerBound = 20000.0m, UpperBound = 30000.0m, Award = 900.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 9, LowerBound = 30000.0m, UpperBound = 40000.0m, Award = 1100.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 10, LowerBound = 40000.0m, UpperBound = 50000.0m, Award = 1200.0m });
            modelBuilder.Entity<BonusRangeEntity>().HasData(new BonusRangeEntity { Id = 11, LowerBound = 50000.0m, UpperBound = 10000000.0m, Award = 1500.0m });

            modelBuilder.Entity<BonusCorrectionFactorEntity>().HasData(new BonusCorrectionFactorEntity { Id = 1, CorrectionFactor = 1, Text = "never discussed in the organization" });
            modelBuilder.Entity<BonusCorrectionFactorEntity>().HasData(new BonusCorrectionFactorEntity { Id = 2, CorrectionFactor = 0.5m, Text = "previously discussed in the organization" });
        }
    }
}
