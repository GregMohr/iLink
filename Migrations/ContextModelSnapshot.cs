using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using exam4.Models;

namespace exam4.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("exam4.Models.Connection", b =>
                {
                    b.Property<int>("connectionId");

                    b.Property<DateTime>("created_at");

                    b.Property<int>("id");

                    b.Property<DateTime>("updated_at");

                    b.Property<int>("userId");

                    b.HasKey("connectionId");

                    b.HasIndex("connectionId");

                    b.HasIndex("userId");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("exam4.Models.Invite", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<int>("invitedId");

                    b.Property<DateTime>("updated_at");

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.HasIndex("invitedId");

                    b.HasIndex("userId");

                    b.ToTable("Invites");
                });

            modelBuilder.Entity("exam4.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("description")
                        .IsRequired();

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("first")
                        .IsRequired();

                    b.Property<string>("last")
                        .IsRequired();

                    b.Property<string>("password")
                        .IsRequired();

                    b.Property<DateTime>("updated_at");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("exam4.Models.Connection", b =>
                {
                    b.HasOne("exam4.Models.User", "connection")
                        .WithMany()
                        .HasForeignKey("connectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("exam4.Models.User", "user")
                        .WithMany("connections")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("exam4.Models.Invite", b =>
                {
                    b.HasOne("exam4.Models.User", "invited")
                        .WithMany()
                        .HasForeignKey("invitedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("exam4.Models.User", "user")
                        .WithMany("invites")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
