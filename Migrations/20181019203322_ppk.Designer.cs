﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MusiCoLab2.Models;
using System;

namespace MusiCoLab2.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20181019203322_ppk")]
    partial class ppk
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusiCoLab2.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AudioUrl");

                    b.Property<string>("Comments");

                    b.Property<string>("Daw");

                    b.Property<string>("Instruments");

                    b.Property<bool>("IsPrivate");

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectOwnerId");

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.HasIndex("ProjectOwnerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MusiCoLab2.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<int?>("ProjectId");

                    b.Property<string>("Role");

                    b.Property<string>("Salt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MusiCoLab2.Models.Project", b =>
                {
                    b.HasOne("MusiCoLab2.Models.User", "ProjectOwner")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectOwnerId");
                });

            modelBuilder.Entity("MusiCoLab2.Models.User", b =>
                {
                    b.HasOne("MusiCoLab2.Models.Project")
                        .WithMany("ProjectContributors")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
