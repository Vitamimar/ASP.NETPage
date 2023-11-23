using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.School_dbModels;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassDetail> ClassDetails { get; set; }

    public virtual DbSet<CurricularUnit> CurricularUnits { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=School_db;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ClassDetails).HasColumnName("CLass_Details");

            entity.HasOne(d => d.ClassDetailsNavigation).WithMany()
                .HasForeignKey(d => d.ClassDetails)
                .HasConstraintName("FK__Classes__CLass_D__2F10007B");

            entity.HasOne(d => d.StudentsNavigation).WithMany()
                .HasForeignKey(d => d.Students)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classes__Student__300424B4");
        });

        modelBuilder.Entity<ClassDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class_De__3214EC07120FF06E");

            entity.ToTable("Class_Details");

            entity.Property(e => e.ClassName)
                .HasMaxLength(40)
                .HasColumnName("Class_Name");
            entity.Property(e => e.ClassYear)
                .HasMaxLength(9)
                .IsFixedLength()
                .HasColumnName("Class_Year");
            entity.Property(e => e.CurricularUnit).HasColumnName("Curricular_unit");

            entity.HasOne(d => d.CurricularUnitNavigation).WithMany(p => p.ClassDetails)
                .HasForeignKey(d => d.CurricularUnit)
                .HasConstraintName("FK__Class_Det__Curri__2C3393D0");

            entity.HasOne(d => d.TeacherNavigation).WithMany(p => p.ClassDetails)
                .HasForeignKey(d => d.Teacher)
                .HasConstraintName("FK__Class_Det__Teach__2D27B809");
        });

        modelBuilder.Entity<CurricularUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curricul__3214EC07ABA75722");

            entity.ToTable("Curricular_Units");

            entity.Property(e => e.Objectives).HasMaxLength(400);
            entity.Property(e => e.UnitName)
                .HasMaxLength(120)
                .HasColumnName("Unit_Name");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__People__3214EC07C47A38B6");

            entity.Property(e => e.Birth).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(40);
            entity.Property(e => e.LastName).HasMaxLength(40);

            entity.HasOne(d => d.RolesNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.Roles)
                .HasConstraintName("FK__People__Roles__276EDEB3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07ED32FE9C");

            entity.Property(e => e.Labels).HasMaxLength(50);
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(500)
                .HasColumnName("Role_Description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
