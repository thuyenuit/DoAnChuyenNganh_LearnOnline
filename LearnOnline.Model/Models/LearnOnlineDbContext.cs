namespace LearnOnline.Model.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LearnOnlineDbContext : DbContext
    {
        public LearnOnlineDbContext()
            : base("name=LearnOnlineDbContext")
        {
        }

        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<ConfirmTransfer> ConfirmTransfers { get; set; }
        public virtual DbSet<DetailExam> DetailExams { get; set; }
        public virtual DbSet<DetailExamResult> DetailExamResults { get; set; }
        public virtual DbSet<DetailThematic> DetailThematics { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamResult> ExamResults { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Thematic> Thematics { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>()
                .HasMany(e => e.ConfirmTransfers)
                .WithRequired(e => e.Bank)
                .HasForeignKey(e => e.BankTransferPerson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConfirmTransfer>()
                .Property(e => e.AmountTransfer)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ConfirmTransfer>()
                .Property(e => e.BankAccount)
                .IsUnicode(false);

            modelBuilder.Entity<ConfirmTransfer>()
                .Property(e => e.UsernameEnjoy)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.LinkDocument)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.DetailThematics)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.DetailExams)
                .WithRequired(e => e.Exam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExamResult>()
                .HasMany(e => e.DetailExamResults)
                .WithRequired(e => e.ExamResult)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.Exams)
                .WithRequired(e => e.Level)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.ExamResults)
                .WithRequired(e => e.Level)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.Province)
                .HasForeignKey(e => e.ProvincesID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Province)
                .HasForeignKey(e => e.ProvincesID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.DetailExamResults)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Exams)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Thematics)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Videos)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Thematic>()
                .Property(e => e.Fees)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Thematic>()
                .HasMany(e => e.DetailThematics)
                .WithRequired(e => e.Thematic)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.SSN)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ExamResults)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Thematics)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Videos)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Video>()
                .Property(e => e.LinkVideo)
                .IsUnicode(false);

            modelBuilder.Entity<Video>()
                .HasMany(e => e.DetailThematics)
                .WithRequired(e => e.Video)
                .WillCascadeOnDelete(false);
        }
    }
}
