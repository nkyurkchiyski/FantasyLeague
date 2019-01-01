﻿// <auto-generated />
using System;
using FantasyLeague.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FantasyLeague.Data.Migrations
{
    [DbContext(typeof(FantasyLeagueDbContext))]
    partial class FantasyLeagueDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FantasyLeague.Models.Fixture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AwayTeamGoals");

                    b.Property<Guid>("AwayTeamId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("HomeTeamGoals");

                    b.Property<Guid>("HomeTeamId");

                    b.Property<Guid>("MatchdayId");

                    b.Property<int>("Status");

                    b.Property<int>("Winner");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("MatchdayId");

                    b.ToTable("Fixtures");
                });

            modelBuilder.Entity("FantasyLeague.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageType")
                        .IsRequired();

                    b.Property<Guid?>("PlayerId");

                    b.Property<string>("PublicId")
                        .IsRequired();

                    b.Property<Guid?>("TeamId");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Images");

                    b.HasDiscriminator<string>("ImageType");
                });

            modelBuilder.Entity("FantasyLeague.Models.Matchday", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("MatchdayStatus");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TransferWindowStatus");

                    b.Property<int>("Week");

                    b.HasKey("Id");

                    b.ToTable("Matchdays");
                });

            modelBuilder.Entity("FantasyLeague.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Nationality")
                        .IsRequired();

                    b.Property<Guid>("PlayerImageId");

                    b.Property<int>("Position");

                    b.Property<double>("Price");

                    b.Property<Guid>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerImageId")
                        .IsUnique();

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FantasyLeague.Models.Roster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Budget");

                    b.Property<int>("Formation");

                    b.Property<Guid>("MatchdayId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MatchdayId");

                    b.HasIndex("UserId");

                    b.ToTable("Rosters");
                });

            modelBuilder.Entity("FantasyLeague.Models.RosterPlayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsSub");

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("RosterId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RosterId");

                    b.ToTable("RostersPlayers");
                });

            modelBuilder.Entity("FantasyLeague.Models.Score", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Assists");

                    b.Property<bool>("CleanSheet");

                    b.Property<Guid>("FixtureId");

                    b.Property<int>("Goals");

                    b.Property<int>("GoalsConceded");

                    b.Property<bool?>("IsWiner");

                    b.Property<int>("PlayedMinutes");

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("RedCards");

                    b.Property<int>("Shots");

                    b.Property<int>("Tackles");

                    b.Property<int>("YellowCards");

                    b.HasKey("Id");

                    b.HasIndex("FixtureId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("FantasyLeague.Models.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnType("NCHAR(3)");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("TeamImageId");

                    b.HasKey("Id");

                    b.HasIndex("TeamImageId")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FantasyLeague.Models.Transfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("RosterId");

                    b.Property<int>("TransferType");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RosterId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("FantasyLeague.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Active");

                    b.Property<string>("ClubName")
                        .IsRequired();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<Guid?>("FavouriteTeamId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ClubName")
                        .IsUnique();

                    b.HasIndex("FavouriteTeamId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FantasyLeague.Models.Fixture", b =>
                {
                    b.HasOne("FantasyLeague.Models.Team", "AwayTeam")
                        .WithMany("AwayFixtures")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.Team", "HomeTeam")
                        .WithMany("HomeFixtures")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.Matchday", "Matchday")
                        .WithMany("Fixtures")
                        .HasForeignKey("MatchdayId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.Player", b =>
                {
                    b.HasOne("FantasyLeague.Models.Image", "PlayerImage")
                        .WithOne("Player")
                        .HasForeignKey("FantasyLeague.Models.Player", "PlayerImageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FantasyLeague.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.Roster", b =>
                {
                    b.HasOne("FantasyLeague.Models.Matchday", "Matchday")
                        .WithMany("Rosters")
                        .HasForeignKey("MatchdayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.User", "User")
                        .WithMany("Rosters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.RosterPlayer", b =>
                {
                    b.HasOne("FantasyLeague.Models.Player", "Player")
                        .WithMany("Rosters")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.Roster", "Roster")
                        .WithMany("Players")
                        .HasForeignKey("RosterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.Score", b =>
                {
                    b.HasOne("FantasyLeague.Models.Fixture", "Fixture")
                        .WithMany("Scores")
                        .HasForeignKey("FixtureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.Player", "Player")
                        .WithMany("Scores")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.Team", b =>
                {
                    b.HasOne("FantasyLeague.Models.Image", "TeamImage")
                        .WithOne("Team")
                        .HasForeignKey("FantasyLeague.Models.Team", "TeamImageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FantasyLeague.Models.Transfer", b =>
                {
                    b.HasOne("FantasyLeague.Models.Player", "Player")
                        .WithMany("Transfers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.Roster", "Roster")
                        .WithMany("Transfers")
                        .HasForeignKey("RosterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.User", b =>
                {
                    b.HasOne("FantasyLeague.Models.Team", "FavouriteTeam")
                        .WithMany("Fans")
                        .HasForeignKey("FavouriteTeamId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FantasyLeague.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FantasyLeague.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FantasyLeague.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
