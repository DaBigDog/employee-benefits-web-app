using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EmployeeBenefits.Database.Models
{
    public partial class EmployeeBenefitsContext : DbContext
    {
        public EmployeeBenefitsContext()
        {
        }

        public EmployeeBenefitsContext(DbContextOptions<EmployeeBenefitsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Benefit> Benefits { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Dependent> Dependents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeBenefitsDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Benefit>(entity =>
            {
                entity.Property(e => e.Id);  //.UseIdentityColumn();  <-- Removed this so EF Core uses SQL auto generated ID!!!

                entity.Property(e => e.DepYearlyBenefitCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountMatch).HasMaxLength(10);

                entity.Property(e => e.EmpYearlyBenefitCost).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Benefits)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyBenefits");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company"); //.UseIdentityColumn();  <-- EF Core  >:-[

                entity.Property(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.ToTable("Dependent"); //.UseIdentityColumn();  <-- Grrrr....

                entity.Property(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.MiddleName).HasMaxLength(500);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Dependents)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeDependent");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee"); //.UseIdentityColumn();  <-- EF Core makes it much harder to do DB first

                entity.Property(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.MiddleName).HasMaxLength(500);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyEmployee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
