﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web3_tp_final.Data;

namespace web3_tp_final.Migrations
{
    [DbContext(typeof(SitAPupContext))]
    [Migration("20211111172325_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("web3_tp_final.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("web3_tp_final.Models.Availability", b =>
                {
                    b.Property<int>("AvailabilityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AvailabilityID");

                    b.HasIndex("UserId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("web3_tp_final.Models.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Recipient")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sender")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("MessageID");

                    b.HasIndex("UserID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("web3_tp_final.Models.Pet", b =>
                {
                    b.Property<int>("PetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BirthYear")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBeingSitted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("BLOB");

                    b.Property<int>("Sitter")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SittingEnd")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SittingStart")
                        .HasColumnType("TEXT");

                    b.Property<string>("SpecieString")
                        .HasColumnType("TEXT")
                        .HasColumnName("Specie");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("PetID");

                    b.HasIndex("UserID");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("web3_tp_final.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Stars")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WrittenBy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WrittenTo")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReviewID");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("web3_tp_final.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("web3_tp_final.Models.Availability", b =>
                {
                    b.HasOne("web3_tp_final.Models.User", null)
                        .WithMany("Availabilities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("web3_tp_final.Models.Message", b =>
                {
                    b.HasOne("web3_tp_final.Models.User", null)
                        .WithMany("Messages")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("web3_tp_final.Models.Pet", b =>
                {
                    b.HasOne("web3_tp_final.Models.User", null)
                        .WithMany("Pets")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("web3_tp_final.Models.Review", b =>
                {
                    b.HasOne("web3_tp_final.Models.User", null)
                        .WithMany("Reviews")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("web3_tp_final.Models.User", b =>
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