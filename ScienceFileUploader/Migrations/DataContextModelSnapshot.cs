﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScienceFileUploader.Data;

#nullable disable

namespace ScienceFileUploader.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("ScienceFileUploader.Entities.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ScienceFileUploader.Entities.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountOfExperiments")
                        .HasColumnType("INTEGER");

                    b.Property<double>("AvgByParameters")
                        .HasColumnType("REAL");

                    b.Property<double>("AvgExperimentDuration")
                        .HasColumnType("REAL");

                    b.Property<int>("FileName")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FirstExperimentTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastExperimentTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxExperimentDuration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxParameterValue")
                        .HasColumnType("INTEGER");

                    b.Property<double>("MedianByParameters")
                        .HasColumnType("REAL");

                    b.Property<int>("MinExperimentDuration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinParameterValue")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FileName")
                        .IsUnique();

                    b.ToTable("Results");
                });

            modelBuilder.Entity("ScienceFileUploader.Entities.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Parameter")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeInMs")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FileName");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("ScienceFileUploader.Entities.Result", b =>
                {
                    b.HasOne("ScienceFileUploader.Entities.File", "File")
                        .WithOne("Result")
                        .HasForeignKey("ScienceFileUploader.Entities.Result", "FileName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("ScienceFileUploader.Entities.Value", b =>
                {
                    b.HasOne("ScienceFileUploader.Entities.File", "File")
                        .WithMany("Values")
                        .HasForeignKey("FileName")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("ScienceFileUploader.Entities.File", b =>
                {
                    b.Navigation("Result")
                        .IsRequired();

                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
