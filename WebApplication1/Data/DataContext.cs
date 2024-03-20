using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApplication1.Entity;

namespace WebApplication1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SubjectStudent> SubjectStudents { get; set; }
        public DbSet<Content> Contents {  get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<OrganizationSch> organizations { get; set; }
        public DbSet<SubjectFee> SubjectFee { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<TimeTableEntry> TimeTableEntries { get; set; }
        public DbSet<Year> Years { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasMany(g => g.Students)
                      .WithOne(s => s.CurrentGrade)
                      .HasForeignKey(s => s.CurrentGradeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(g => g.Subjects)
                      .WithOne(s => s.Grade)
                      .HasForeignKey(s => s.GradeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne(s => s.CurrentGrade)
                      .WithMany(g => g.Students)
                      .HasForeignKey(s => s.CurrentGradeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Teacher)
                      .WithMany(t => t.Student)
                      .HasForeignKey(s => s.teacherId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Organization)
                      .WithMany(o => o.Students)
                      .HasForeignKey(s => s.OrgId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasOne(s => s.Grade)
                      .WithMany(g => g.Subjects)
                      .HasForeignKey(s => s.GradeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.Contents)
                      .WithOne(c => c.Subject)
                      .HasForeignKey(c => c.SubjectId);

                entity.HasOne(s => s.Organization)
                      .WithMany(o => o.Subjects)
                      .HasForeignKey(s => s.OrgId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SubjectStudent>(entity =>
            {
                entity.HasKey(ss => new { ss.SubjectId, ss.StudentId });

                entity.HasOne(ss => ss.Subject)
                      .WithMany(s => s.SubjectStudents)
                      .HasForeignKey(ss => ss.SubjectId)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(ss => ss.Student)
                      .WithMany(s => s.SubjectStudents)
                      .HasForeignKey(ss => ss.StudentId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Subject>()
               .HasOne(s => s.SubjectFee)
               .WithOne(sf => sf.Subject)
               .HasForeignKey<Subject>(s => s.FeeId)
               .OnDelete(DeleteBehavior.Cascade);

               modelBuilder.Entity<Subject>()
              .HasOne(s => s.SubjectFee)
              .WithOne(sf => sf.Subject)
              .HasForeignKey<Subject>(s => s.FeeId)
              .OnDelete(DeleteBehavior.Cascade);

             modelBuilder.Entity<TimeTableEntry>(entity =>
             {
                entity.HasKey(tte => tte.Id);
             });
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasOne(r => r.Organization)
                      .WithMany()
                      .HasForeignKey(r => r.OrgId)
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);

        }

    }

}