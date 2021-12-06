﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service_Final_Rest_API.Models;

namespace Service_Final_Rest_API.Migrations
{
    [DbContext(typeof(sitapupContext))]
    partial class sitapupContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Service_Final_Rest_API.Models.Admin", b =>
                {
                    b.Property<long>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("AdminID");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Appointment", b =>
                {
                    b.Property<long>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("AppointmentID");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("IsActive")
                        .HasColumnType("INTEGER(1)");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("SitterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(255)")
                        .HasDefaultValueSql("'pending'");

                    b.HasKey("AppointmentId");

                    b.HasIndex(new[] { "OwnerId" }, "IX_Appointments_OwnerId");

                    b.HasIndex(new[] { "SitterId" }, "IX_Appointments_UserId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Availability", b =>
                {
                    b.Property<long>("AvailabilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("AvailabilityID");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AvailabilityId");

                    b.HasIndex(new[] { "UserId" }, "IX_Availabilities_UserId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Message", b =>
                {
                    b.Property<long>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("MessageID");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("Recipient")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Sender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TimeStamp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserID");

                    b.HasKey("MessageId");

                    b.HasIndex(new[] { "UserId" }, "IX_Messages_UserID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Pet", b =>
                {
                    b.Property<long>("PetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("PetID");

                    b.Property<long>("BirthYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("BLOB");

                    b.Property<string>("Specie")
                        .HasColumnType("TEXT");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserID");

                    b.HasKey("PetId");

                    b.HasIndex(new[] { "UserId" }, "IX_Pets_UserID");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.PetAppointment", b =>
                {
                    b.Property<long>("PetAppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("PetAppointmentID");

                    b.Property<long>("AppointmentId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PetAppointmentId");

                    b.HasIndex(new[] { "AppointmentId" }, "IX_PetAppointments_AppointmentId");

                    b.HasIndex(new[] { "PetId" }, "IX_PetAppointments_PetId");

                    b.ToTable("PetAppointments");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Review", b =>
                {
                    b.Property<long>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ReviewID");

                    b.Property<long>("AppointmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<long>("Stars")
                        .HasColumnType("INTEGER(5)");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReviewId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(255)")
                        .HasDefaultValueSql("'active'");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Appointment", b =>
                {
                    b.HasOne("Service_Final_Rest_API.Models.User", "Owner")
                        .WithMany("AppointmentOwners")
                        .HasForeignKey("OwnerId");

                    b.HasOne("Service_Final_Rest_API.Models.User", "Sitter")
                        .WithMany("AppointmentSitters")
                        .HasForeignKey("SitterId")
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Sitter");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Availability", b =>
                {
                    b.HasOne("Service_Final_Rest_API.Models.User", "User")
                        .WithMany("Availabilities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Message", b =>
                {
                    b.HasOne("Service_Final_Rest_API.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Pet", b =>
                {
                    b.HasOne("Service_Final_Rest_API.Models.User", "User")
                        .WithMany("Pets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.PetAppointment", b =>
                {
                    b.HasOne("Service_Final_Rest_API.Models.Appointment", "Appointment")
                        .WithMany("PetAppointments")
                        .HasForeignKey("AppointmentId")
                        .IsRequired();

                    b.HasOne("Service_Final_Rest_API.Models.Pet", "Pet")
                        .WithMany("PetAppointments")
                        .HasForeignKey("PetId")
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Review", b =>
                {
                    b.HasOne("Service_Final_Rest_API.Models.Appointment", "Appointment")
                        .WithMany("Reviews")
                        .HasForeignKey("AppointmentId")
                        .IsRequired();

                    b.HasOne("Service_Final_Rest_API.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Appointment", b =>
                {
                    b.Navigation("PetAppointments");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Pet", b =>
                {
                    b.Navigation("PetAppointments");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.User", b =>
                {
                    b.Navigation("AppointmentOwners");

                    b.Navigation("AppointmentSitters");

                    b.Navigation("Availabilities");

                    b.Navigation("Messages");

                    b.Navigation("Pets");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
