﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mindr.Api.Persistence;

#nullable disable

namespace Mindr.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230513185011_Add-PersonalCredentials")]
    partial class AddPersonalCredentials
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpBody", b =>
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

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpBodyOption", b =>
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

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpBodyOptionRaw", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HttpBodyOptionRaw");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpCookie", b =>
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

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpHeader", b =>
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

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConnectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLoading")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RequestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ConnectorId");

                    b.HasIndex("RequestId");

                    b.ToTable("HttpItems");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpRequest", b =>
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

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpRequestUrl", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Host")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Protocol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Raw")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HttpRequestUrl");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpRequestUrlQuery", b =>
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

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpResponse", b =>
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

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpVariable", b =>
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

            modelBuilder.Entity("Mindr.Domain.Models.DTO.Connector.Connector", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Connectors");
                });

            modelBuilder.Entity("Mindr.Domain.Models.DTO.Connector.ConnectorEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConnectorColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ConnectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConnectorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConnectorEvents");
                });

            modelBuilder.Entity("Mindr.Domain.Models.DTO.Connector.ConnectorEventVariable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConnectorEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Key")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConnectorEventId");

                    b.ToTable("ConnectorEventVariable");
                });

            modelBuilder.Entity("Mindr.Domain.Models.DTO.Connector.ConnectorVariable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConnectorEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConnectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConnectorEventId");

                    b.HasIndex("ConnectorId");

                    b.ToTable("ConnectorVariables");
                });

            modelBuilder.Entity("Mindr.Domain.Models.DTO.PersonalCredential.PersonalCredential", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExpiresIn")
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scope")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PersonalCredentials");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpBody", b =>
                {
                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpBodyOption", "Options")
                        .WithMany()
                        .HasForeignKey("OptionsId");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpBodyOption", b =>
                {
                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpBodyOptionRaw", "Raw")
                        .WithMany()
                        .HasForeignKey("RawId");

                    b.Navigation("Raw");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpCookie", b =>
                {
                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpResponse", null)
                        .WithMany("Cookie")
                        .HasForeignKey("HttpResponseId");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpHeader", b =>
                {
                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpRequest", null)
                        .WithMany("Header")
                        .HasForeignKey("HttpRequestId");

                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpResponse", null)
                        .WithMany("Header")
                        .HasForeignKey("HttpResponseId");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpItem", b =>
                {
                    b.HasOne("Mindr.Domain.Models.DTO.Connector.Connector", null)
                        .WithMany("Pipeline")
                        .HasForeignKey("ConnectorId");

                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpRequest", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpRequest", b =>
                {
                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpBody", "Body")
                        .WithMany()
                        .HasForeignKey("BodyId");

                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpRequestUrl", "Url")
                        .WithMany()
                        .HasForeignKey("UrlId");

                    b.Navigation("Body");

                    b.Navigation("Url");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpRequestUrlQuery", b =>
                {
                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpRequestUrl", null)
                        .WithMany("Query")
                        .HasForeignKey("HttpRequestUrlId");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpResponse", b =>
                {
                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpItem", null)
                        .WithMany("Response")
                        .HasForeignKey("HttpItemId");

                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpRequest", "OriginalRequest")
                        .WithMany()
                        .HasForeignKey("OriginalRequestId");

                    b.Navigation("OriginalRequest");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpVariable", b =>
                {
                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpRequest", null)
                        .WithMany("Variables")
                        .HasForeignKey("HttpRequestId");

                    b.HasOne("Mindr.Domain.HttpRunner.Models.HttpResponse", null)
                        .WithMany("Variables")
                        .HasForeignKey("HttpResponseId");
                });

            modelBuilder.Entity("Mindr.Domain.Models.DTO.Connector.ConnectorEventVariable", b =>
                {
                    b.HasOne("Mindr.Domain.Models.DTO.Connector.ConnectorEvent", null)
                        .WithMany("EventParameters")
                        .HasForeignKey("ConnectorEventId");
                });

            modelBuilder.Entity("Mindr.Domain.Models.DTO.Connector.ConnectorVariable", b =>
                {
                    b.HasOne("Mindr.Domain.Models.DTO.Connector.ConnectorEvent", null)
                        .WithMany("ConnectorVariables")
                        .HasForeignKey("ConnectorEventId");

                    b.HasOne("Mindr.Domain.Models.DTO.Connector.Connector", null)
                        .WithMany("Variables")
                        .HasForeignKey("ConnectorId");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpItem", b =>
                {
                    b.Navigation("Response");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpRequest", b =>
                {
                    b.Navigation("Header");

                    b.Navigation("Variables");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpRequestUrl", b =>
                {
                    b.Navigation("Query");
                });

            modelBuilder.Entity("Mindr.Domain.HttpRunner.Models.HttpResponse", b =>
                {
                    b.Navigation("Cookie");

                    b.Navigation("Header");

                    b.Navigation("Variables");
                });

            modelBuilder.Entity("Mindr.Domain.Models.DTO.Connector.Connector", b =>
                {
                    b.Navigation("Pipeline");

                    b.Navigation("Variables");
                });

            modelBuilder.Entity("Mindr.Domain.Models.DTO.Connector.ConnectorEvent", b =>
                {
                    b.Navigation("ConnectorVariables");

                    b.Navigation("EventParameters");
                });
#pragma warning restore 612, 618
        }
    }
}