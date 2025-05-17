using Aton_ITTP.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aton_ITTP.DataAccess
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(x => x.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                builder.Property(x => x.Login)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(x => x.Password)
                    .HasMaxLength(100)
                    .IsRequired();

                builder.Property(x => x.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                builder.Property(x => x.Gender)
                    .IsRequired()
                    .HasConversion<int>();

                builder.Property(x => x.Birthday)
                    .IsRequired(false);

                builder.Property(x => x.Admin)
                    .IsRequired()
                    .HasDefaultValue(false);

                builder.Property(x => x.CreatedOn)
                    .IsRequired()
                    .HasDefaultValueSql("NOW()");

                builder.Property(x => x.CreatedBy)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(x => x.ModifiedOn)
                    .IsRequired()
                    .HasDefaultValueSql("NOW()");

                builder.Property(x => x.ModifiedBy)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(x => x.RevokedOn)
                    .IsRequired(false);

                builder.Property(x => x.RevokedBy)
                    .HasMaxLength(50)
                    .IsRequired(false);

                // Indexes
                builder.HasIndex(x => x.Login).IsUnique();
            });
        }
    }
}
