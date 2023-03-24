﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mindr.Api.Persistence;

#nullable disable

namespace Mindr.API.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mindr.Core.Models.Connector.Connector", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Connectors");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.ConnectorEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConnectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ConnectorEvents");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.ConnectorParam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("ConnectorEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConnectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConnectorEventId");

                    b.HasIndex("ConnectorId");

                    b.ToTable("ConnectorParam");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.EventParam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("ConnectorEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConnectorEventId");

                    b.ToTable("EventParam");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpBody", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OptionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Raw")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OptionsId");

                    b.ToTable("HttpBody");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpBodyOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RawId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RawId");

                    b.ToTable("HttpBodyOption");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpBodyOptionRaw", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HttpBodyOptionRaw");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpCookie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Expires")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("HttpResponseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("HttpResponseId");

                    b.ToTable("HttpCookie");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpHeader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("HttpRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HttpResponseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HttpRequestId");

                    b.HasIndex("HttpResponseId");

                    b.ToTable("HttpHeader");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConnectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("HttpItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsLoading")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RequestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ConnectorId");

                    b.HasIndex("HttpItemId");

                    b.HasIndex("RequestId");

                    b.ToTable("HttpItem");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BodyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UrlId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BodyId");

                    b.HasIndex("UrlId");

                    b.ToTable("HttpRequest");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpRequestUrl", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Protocol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Raw")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HttpRequestUrl");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpRequestUrlQuery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("HttpRequestUrlId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HttpRequestUrlId");

                    b.ToTable("HttpRequestUrlQuery");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<Guid?>("HttpItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OriginalRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PostmanPreviewLanguage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HttpItemId");

                    b.HasIndex("OriginalRequestId");

                    b.ToTable("HttpResponse");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpVariable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HttpRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HttpResponseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HttpRequestId");

                    b.HasIndex("HttpResponseId");

                    b.ToTable("HttpVariable");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.ConnectorParam", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.ConnectorEvent", null)
                        .WithMany("ConnectorParams")
                        .HasForeignKey("ConnectorEventId");

                    b.HasOne("Mindr.Core.Models.Connector.Connector", null)
                        .WithMany("Variables")
                        .HasForeignKey("ConnectorId");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.EventParam", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.ConnectorEvent", null)
                        .WithMany("EventParams")
                        .HasForeignKey("ConnectorEventId");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpBody", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpBodyOption", "Options")
                        .WithMany()
                        .HasForeignKey("OptionsId");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpBodyOption", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpBodyOptionRaw", "Raw")
                        .WithMany()
                        .HasForeignKey("RawId");

                    b.Navigation("Raw");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpCookie", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpResponse", null)
                        .WithMany("Cookie")
                        .HasForeignKey("HttpResponseId");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpHeader", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpRequest", null)
                        .WithMany("Header")
                        .HasForeignKey("HttpRequestId");

                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpResponse", null)
                        .WithMany("Header")
                        .HasForeignKey("HttpResponseId");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpItem", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.Connector", null)
                        .WithMany("Pipeline")
                        .HasForeignKey("ConnectorId");

                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpItem", null)
                        .WithMany("Items")
                        .HasForeignKey("HttpItemId");

                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpRequest", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpRequest", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpBody", "Body")
                        .WithMany()
                        .HasForeignKey("BodyId");

                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpRequestUrl", "Url")
                        .WithMany()
                        .HasForeignKey("UrlId");

                    b.Navigation("Body");

                    b.Navigation("Url");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpRequestUrlQuery", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpRequestUrl", null)
                        .WithMany("Query")
                        .HasForeignKey("HttpRequestUrlId");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpResponse", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpItem", null)
                        .WithMany("Response")
                        .HasForeignKey("HttpItemId");

                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpRequest", "OriginalRequest")
                        .WithMany()
                        .HasForeignKey("OriginalRequestId");

                    b.Navigation("OriginalRequest");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpVariable", b =>
                {
                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpRequest", null)
                        .WithMany("Variables")
                        .HasForeignKey("HttpRequestId");

                    b.HasOne("Mindr.Core.Models.Connector.Http.HttpResponse", null)
                        .WithMany("Variables")
                        .HasForeignKey("HttpResponseId");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Connector", b =>
                {
                    b.Navigation("Pipeline");

                    b.Navigation("Variables");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.ConnectorEvent", b =>
                {
                    b.Navigation("ConnectorParams");

                    b.Navigation("EventParams");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpItem", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Response");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpRequest", b =>
                {
                    b.Navigation("Header");

                    b.Navigation("Variables");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpRequestUrl", b =>
                {
                    b.Navigation("Query");
                });

            modelBuilder.Entity("Mindr.Core.Models.Connector.Http.HttpResponse", b =>
                {
                    b.Navigation("Cookie");

                    b.Navigation("Header");

                    b.Navigation("Variables");
                });
#pragma warning restore 612, 618
        }
    }
}
