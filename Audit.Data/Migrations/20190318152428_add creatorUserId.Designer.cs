﻿// <auto-generated />
using System;
using Audit.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Audit.Data.Migrations
{
    [DbContext(typeof(AuditContext))]
    [Migration("20190318152428_add creatorUserId")]
    partial class addcreatorUserId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Audit.Data.Audit_Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuditAction");

                    b.Property<DateTime>("AuditDate");

                    b.Property<string>("AuditUser");

                    b.Property<Guid>("AuditUserId");

                    b.Property<DateTime>("CreatorUserId");

                    b.Property<Guid>("EntityId");

                    b.Property<bool>("Read");

                    b.Property<string>("TextData");

                    b.Property<string>("Tittle");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Audit_Notifications");
                });

            modelBuilder.Entity("Audit.Data.Audit_User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuditAction");

                    b.Property<DateTime>("AuditDate");

                    b.Property<string>("AuditUser");

                    b.Property<Guid>("AuditUserId");

                    b.Property<string>("Dni");

                    b.Property<Guid>("EntityId");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Audit_Users");
                });
#pragma warning restore 612, 618
        }
    }
}
