namespace VinhEdu.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EduVinhContext : DbContext
    {
        public EduVinhContext()
            : base("name=EDUVinhCtx")
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassMember> ClassMembers { get; set; }
        public virtual DbSet<Configure> Configures { get; set; }
        public virtual DbSet<PointBoard> PointBoards { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .Property(e => e.StudentList)
                .IsUnicode(false);

            modelBuilder.Entity<Class>()
                .Property(e => e.TeacherList)
                .IsUnicode(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.ClassMembers)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.PointBoards)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configure>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.Configure)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<School>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.School)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.StudentID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.StudentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ClassMembers)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PointBoards)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.StudentID)
                .WillCascadeOnDelete(false);
        }
    }
}
