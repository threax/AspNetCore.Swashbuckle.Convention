﻿// <auto-generated />
using DevApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DevApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DevApp.Database.ValueEntity", b =>
                {
                    b.Property<Guid>("ValueId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("ValueId");

                    b.HasIndex("Created")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("Values");
                });

            modelBuilder.Entity("Threax.AspNetCore.UserBuilder.Entities.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(450);

                    b.HasKey("RoleId");

                    b.ToTable("spc.auth.Roles");
                });

            modelBuilder.Entity("Threax.AspNetCore.UserBuilder.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(450);

                    b.HasKey("UserId");

                    b.ToTable("spc.auth.Users");
                });

            modelBuilder.Entity("Threax.AspNetCore.UserBuilder.Entities.UserToRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("spc.auth.UsersToRoles");
                });

            modelBuilder.Entity("Threax.AspNetCore.UserBuilder.Entities.UserToRole", b =>
                {
                    b.HasOne("Threax.AspNetCore.UserBuilder.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Threax.AspNetCore.UserBuilder.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
