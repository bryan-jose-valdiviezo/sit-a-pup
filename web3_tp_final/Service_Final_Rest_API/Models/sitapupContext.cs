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
        public virtual DbSet<Availability> Availabilities { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("AdminID");
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

                entity.Property(e => e.SittingEnd).IsRequired();

                entity.Property(e => e.SittingStart).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.UserId);
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
