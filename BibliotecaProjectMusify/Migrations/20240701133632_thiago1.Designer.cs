﻿// <auto-generated />
using System;
using BibliotecaProjectMusify;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BibliotecaProjectMusify.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240701133632_thiago1")]
    partial class thiago1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BibliotecaProjectMusify.Album", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Userid")
                        .HasColumnType("int");

                    b.Property<int>("artistid")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.HasIndex("artistid");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Playlist", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Song", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Albumid")
                        .HasColumnType("int");

                    b.Property<int?>("Playlistid")
                        .HasColumnType("int");

                    b.Property<int>("artistid")
                        .HasColumnType("int");

                    b.Property<int>("duration")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Albumid");

                    b.HasIndex("Playlistid");

                    b.HasIndex("artistid");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Admin", b =>
                {
                    b.HasBaseType("BibliotecaProjectMusify.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Artist", b =>
                {
                    b.HasBaseType("BibliotecaProjectMusify.User");

                    b.HasDiscriminator().HasValue("Artist");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Album", b =>
                {
                    b.HasOne("BibliotecaProjectMusify.User", null)
                        .WithMany("FavoritedAlbums")
                        .HasForeignKey("Userid");

                    b.HasOne("BibliotecaProjectMusify.Artist", "artist")
                        .WithMany("Albums")
                        .HasForeignKey("artistid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("artist");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Playlist", b =>
                {
                    b.HasOne("BibliotecaProjectMusify.User", "user")
                        .WithMany("Playlists")
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Song", b =>
                {
                    b.HasOne("BibliotecaProjectMusify.Album", null)
                        .WithMany("Songs")
                        .HasForeignKey("Albumid");

                    b.HasOne("BibliotecaProjectMusify.Playlist", null)
                        .WithMany("Songs")
                        .HasForeignKey("Playlistid");

                    b.HasOne("BibliotecaProjectMusify.Artist", "artist")
                        .WithMany("Songs")
                        .HasForeignKey("artistid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("artist");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Album", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Playlist", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.User", b =>
                {
                    b.Navigation("FavoritedAlbums");

                    b.Navigation("Playlists");
                });

            modelBuilder.Entity("BibliotecaProjectMusify.Artist", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
