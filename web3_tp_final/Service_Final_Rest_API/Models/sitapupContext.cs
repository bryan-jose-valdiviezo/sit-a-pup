using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class sitapupContext : DbContext
    {
        public sitapupContext()
        {
        }

        public sitapupContext(DbContextOptions<sitapupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Availability> Availabilities { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<PetAppointment> PetAppointments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=c:\\\\\\\\sqlite\\\\\\\\sitapup.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("AdminID");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasIndex(e => e.SitterId, "IX_Appointments_UserId");

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.EndDate).IsRequired();

                entity.Property(e => e.IsActive).HasColumnType("INTEGER(1)");

                entity.Property(e => e.StartDate).IsRequired();

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.AppointmentOwners)
                    .HasForeignKey(d => d.OwnerId);

                entity.HasOne(d => d.Sitter)
                    .WithMany(p => p.AppointmentSitters)
                    .HasForeignKey(d => d.SitterId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Availability>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Availabilities_UserId");

                entity.Property(e => e.AvailabilityId).HasColumnName("AvailabilityID");

                entity.Property(e => e.EndDate).IsRequired();

                entity.Property(e => e.StartDate).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Availabilities)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Messages_UserID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.TimeStamp).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Pets_UserID");

                entity.Property(e => e.PetId).HasColumnName("PetID");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<PetAppointment>(entity =>
            {
                entity.HasIndex(e => e.AppointmentId, "IX_PetAppointments_AppointmentId");

                entity.HasIndex(e => e.PetId, "IX_PetAppointments_PetId");

                entity.Property(e => e.PetAppointmentId).HasColumnName("PetAppointmentID");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.PetAppointments)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.PetAppointments)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Reviews_UserID");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.TimeStamp).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.Property(e => e.UserName).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
