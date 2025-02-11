﻿// <auto-generated />
using System;
using Kanboom.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250210204130_FixBoardUserRel")]
    partial class FixBoardUserRel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kanboom.Models.Database.Board", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("Fk_BoardOwner")
                        .HasColumnType("bigint");

                    b.Property<long?>("Fk_GroupId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsGroupBoard")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("StagesCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Fk_GroupId");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("Kanboom.Models.Database.BoardUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("Fk_BoardId")
                        .HasColumnType("bigint");

                    b.Property<long>("Fk_UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Fk_BoardId");

                    b.HasIndex("Fk_UserId");

                    b.ToTable("BoardUser");
                });

            modelBuilder.Entity("Kanboom.Models.Database.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("Fk_OwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("GroupLink")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("Kanboom.Models.Database.StageLevels", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("Fk_Board")
                        .HasColumnType("bigint");

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("StageNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Fk_Board");

                    b.ToTable("StageLevels");
                });

            modelBuilder.Entity("Kanboom.Models.Database.Task", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<long>("Fk_Board")
                        .HasColumnType("bigint");

                    b.Property<long>("Fk_UserAssigned")
                        .HasColumnType("bigint");

                    b.Property<int>("StageNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Fk_Board");

                    b.HasIndex("Fk_UserAssigned");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("Kanboom.Models.Database.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Username")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Kanboom.Models.Database.UserGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("Fk_GroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("Fk_UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Fk_UserId");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("Kanboom.Models.Database.Board", b =>
                {
                    b.HasOne("Kanboom.Models.Database.Group", null)
                        .WithMany("Board")
                        .HasForeignKey("Fk_GroupId");
                });

            modelBuilder.Entity("Kanboom.Models.Database.BoardUser", b =>
                {
                    b.HasOne("Kanboom.Models.Database.Board", null)
                        .WithMany("BoardUser")
                        .HasForeignKey("Fk_BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kanboom.Models.Database.User", null)
                        .WithMany("BoardUser")
                        .HasForeignKey("Fk_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kanboom.Models.Database.StageLevels", b =>
                {
                    b.HasOne("Kanboom.Models.Database.Board", null)
                        .WithMany("StageLevels")
                        .HasForeignKey("Fk_Board")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kanboom.Models.Database.Task", b =>
                {
                    b.HasOne("Kanboom.Models.Database.Board", null)
                        .WithMany("Task")
                        .HasForeignKey("Fk_Board")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kanboom.Models.Database.User", null)
                        .WithMany("Task")
                        .HasForeignKey("Fk_UserAssigned")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kanboom.Models.Database.UserGroup", b =>
                {
                    b.HasOne("Kanboom.Models.Database.User", null)
                        .WithMany("UserGroup")
                        .HasForeignKey("Fk_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kanboom.Models.Database.Board", b =>
                {
                    b.Navigation("BoardUser");

                    b.Navigation("StageLevels");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Kanboom.Models.Database.Group", b =>
                {
                    b.Navigation("Board");
                });

            modelBuilder.Entity("Kanboom.Models.Database.User", b =>
                {
                    b.Navigation("BoardUser");

                    b.Navigation("Task");

                    b.Navigation("UserGroup");
                });
#pragma warning restore 612, 618
        }
    }
}
