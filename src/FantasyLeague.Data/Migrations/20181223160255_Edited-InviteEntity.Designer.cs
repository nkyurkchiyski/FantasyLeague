﻿// <auto-generated />
using System;
using FantasyLeague.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FantasyLeague.Data.Migrations
{
    [DbContext(typeof(FantasyLeagueDbContext))]
    [Migration("20181223160255_Edited-InviteEntity")]
    partial class EditedInviteEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Result");

                    b.Property<int>("Status");

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

                    b.Property<Guid>("PlayerId");

                    b.Property<string>("PublicId")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("FantasyLeague.Models.Invite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InviteeId")
                        .IsRequired();

                    b.Property<string>("InviterId")
                        .IsRequired();

                    b.Property<Guid>("LeagueId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("InviteeId");

                    b.HasIndex("InviterId");

                    b.HasIndex("LeagueId");

                    b.ToTable("Invite");
                });

            modelBuilder.Entity("FantasyLeague.Models.League", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatorId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("FantasyLeague.Models.Matchday", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Week");

                    b.HasKey("Id");

                    b.ToTable("Matchdays");
                });

            modelBuilder.Entity("FantasyLeague.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<Guid>("ImageId");

                    b.Property<bool>("Injured");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int>("Position");

                    b.Property<double>("Price");

                    b.Property<Guid>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FantasyLeague.Models.Roster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Formation");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rosters");
                });

            modelBuilder.Entity("FantasyLeague.Models.RosterPlayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

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

                    b.Property<Guid>("FixtureId");

                    b.Property<Guid>("PlayerId");

                    b.Property<double>("Rating");

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

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FantasyLeague.Models.Transfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("MatchdayId");

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("RosterId");

                    b.Property<int>("TransferType");

                    b.HasKey("Id");

                    b.HasIndex("MatchdayId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RosterId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("FantasyLeague.Models.UserLeague", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("LeagueId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersLeagues");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

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

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
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
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

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

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FantasyLeague.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("Active");

                    b.Property<int>("Age");

                    b.Property<string>("ClubName")
                        .IsRequired();

                    b.Property<string>("Country");

                    b.Property<Guid>("FavouriteTeamId");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasIndex("ClubName")
                        .IsUnique()
                        .HasFilter("[ClubName] IS NOT NULL");

                    b.HasIndex("FavouriteTeamId");

                    b.HasDiscriminator().HasValue("User");
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

            modelBuilder.Entity("FantasyLeague.Models.Image", b =>
                {
                    b.HasOne("FantasyLeague.Models.Player", "Player")
                        .WithOne("Image")
                        .HasForeignKey("FantasyLeague.Models.Image", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.Invite", b =>
                {
                    b.HasOne("FantasyLeague.Models.User", "Invitee")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("InviteeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.User", "Inviter")
                        .WithMany("SentInvitations")
                        .HasForeignKey("InviterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.League", "League")
                        .WithMany("Invites")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.League", b =>
                {
                    b.HasOne("FantasyLeague.Models.User", "Creator")
                        .WithMany("CreatedLeagues")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.Player", b =>
                {
                    b.HasOne("FantasyLeague.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.Roster", b =>
                {
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

            modelBuilder.Entity("FantasyLeague.Models.Transfer", b =>
                {
                    b.HasOne("FantasyLeague.Models.Matchday", "Matchday")
                        .WithMany("Transfers")
                        .HasForeignKey("MatchdayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.Player", "Player")
                        .WithMany("Transfers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.Roster", "Roster")
                        .WithMany("Transfers")
                        .HasForeignKey("RosterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.UserLeague", b =>
                {
                    b.HasOne("FantasyLeague.Models.League", "League")
                        .WithMany("Users")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FantasyLeague.Models.User", "User")
                        .WithMany("AllLeagues")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FantasyLeague.Models.User", b =>
                {
                    b.HasOne("FantasyLeague.Models.Team", "FavouriteTeam")
                        .WithMany("Fans")
                        .HasForeignKey("FavouriteTeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}