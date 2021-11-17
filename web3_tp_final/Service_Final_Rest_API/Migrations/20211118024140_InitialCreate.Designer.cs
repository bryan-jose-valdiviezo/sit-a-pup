﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service_Final_Rest_API.Models;

namespace Service_Final_Rest_API.Migrations
{
    [DbContext(typeof(sitapupContext))]
    [Migration("20211118024140_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<long>("IsBeingSitted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("BLOB");

                    b.Property<long>("Sitter")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SittingEnd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SittingStart")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Specie")
                        .HasColumnType("TEXT");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserID");

                    b.HasKey("PetId");

                    b.HasIndex(new[] { "UserId" }, "IX_Pets_UserID");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.Review", b =>
                {
                    b.Property<long>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ReviewID");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<long>("Stars")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TimeStamp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserID");

                    b.Property<long>("WrittenBy")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WrittenTo")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReviewId");

                    b.HasIndex(new[] { "UserId" }, "IX_Reviews_UserID");

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

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
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

            modelBuilder.Entity("Service_Final_Rest_API.Models.Review", b =>
                {
                    b.HasOne("Service_Final_Rest_API.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Service_Final_Rest_API.Models.User", b =>
                {
                    b.Navigation("Availabilities");

                    b.Navigation("Messages");

                    b.Navigation("Pets");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}