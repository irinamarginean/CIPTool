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
        public DbSet<IdeaStatusEntity> IdeaStatuses { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }
        public DbSet<FinancialReportEntity> FinancialReports { get; set; }
        public DbSet<BonusEntity> Bonuses { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=CLJ-C-0005V;database=CIPTool;trusted_connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Associate>();
            modelBuilder.Entity<Leader>();

            modelBuilder.Entity<StatusEntity>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Associate>()
               .HasOne(a => a.Leader)
               .WithMany(l => l.Associates);

            modelBuilder.Entity<Associate>()
                .HasMany(a => a.Ideas)
                .WithOne(i => i.Associate);

            modelBuilder.Entity<IdeaEntity>()
                .HasMany(i => i.ApprovalStatuses)
                .WithOne(astat => astat.Idea);

            modelBuilder.Entity<IdeaEntity>()
                .HasMany(i => i.Attachments)
                .WithOne(a => a.Idea);

            modelBuilder.Entity<IdeaEntity>()
                .HasOne(i => i.FinancialReport)
                .WithOne(fr => fr.Idea)
                .HasForeignKey<IdeaEntity>(i => i.FinancialReportId);

            modelBuilder.Entity<FinancialReportEntity>()
                .HasOne(fr => fr.Bonus)
                .WithOne(b => b.FinancialReport)
                .HasForeignKey<FinancialReportEntity>(fr => fr.BonusId);
        }
    }
}
