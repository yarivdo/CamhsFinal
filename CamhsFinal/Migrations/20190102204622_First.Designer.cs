﻿// <auto-generated />
using System;
using CamhsFinal.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CamhsFinal.Migrations
{
    [DbContext(typeof(CamhsContext))]
    [Migration("20190102204622_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CamhsFinal.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Age");

                    b.Property<string>("First");

                    b.Property<string>("Last");

                    b.Property<string>("Location");

                    b.Property<string>("NHI");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CamhsFinal.Models.Episode", b =>
                {
                    b.Property<int>("EpisodeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientID");

                    b.Property<string>("Comments");

                    b.Property<float>("Grade");

                    b.Property<string>("Outcome");

                    b.Property<int>("ReferralID");

                    b.HasKey("EpisodeID");

                    b.HasIndex("ClientID");

                    b.HasIndex("ReferralID");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("CamhsFinal.Models.Referral", b =>
                {
                    b.Property<int>("ReferralID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.HasKey("ReferralID");

                    b.ToTable("Referrals");
                });

            modelBuilder.Entity("CamhsFinal.Models.Episode", b =>
                {
                    b.HasOne("CamhsFinal.Models.Client", "Client")
                        .WithMany("Episodes")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CamhsFinal.Models.Referral", "Referral")
                        .WithMany("Episodes")
                        .HasForeignKey("ReferralID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
