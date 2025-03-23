using Children_Care_API.Models;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace Children_Care_API.Data
{
    public class ChildrenCareDbContext : DbContext
    {
        public ChildrenCareDbContext(DbContextOptions<ChildrenCareDbContext> options) : base(options)
        {
        }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<MedicalExamination> MedicalExaminations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - Unique Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Reservation Relationships
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Service)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prescription Relationships
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(u => u.PrescriptionsAsDoctor)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Customer)
                .WithMany(u => u.PrescriptionsAsCustomer)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Reservation)
                .WithMany(r => r.Prescriptions)
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment Relationship
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Feedback Relationships
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Service)
                .WithMany(s => s.Feedbacks)
                .HasForeignKey(f => f.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Blog Relationship
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Slider
            modelBuilder.Entity<Slider>();

            // Setting
            modelBuilder.Entity<Setting>();

            // Medical Examination Relationships
            modelBuilder.Entity<MedicalExamination>()
                .HasOne(m => m.Reservation)
                .WithMany(r => r.MedicalExaminations)
                .HasForeignKey(m => m.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalExamination>()
                .HasOne(m => m.Doctor)
                .WithMany(u => u.MedicalExaminations)
                .HasForeignKey(m => m.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
