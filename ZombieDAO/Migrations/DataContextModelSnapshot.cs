﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ZombieDAO;

#nullable disable

namespace ZombieDAO.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeConfirmationModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("signature");

                    b.Property<Guid>("TransactionID")
                        .HasColumnType("uuid")
                        .HasColumnName("transaction");

                    b.Property<string>("UserWallet")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user");

                    b.HasKey("ID");

                    b.HasIndex("TransactionID");

                    b.HasIndex("UserWallet");

                    b.ToTable("safe_confirmations");
                });

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<int>("ChainID")
                        .HasColumnType("integer")
                        .HasColumnName("chain_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("ProjectID")
                        .HasColumnType("uuid")
                        .HasColumnName("project_id");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.ToTable("gnosis_safes");
                });

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeTokenModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<Guid>("SafeID")
                        .HasColumnType("uuid")
                        .HasColumnName("safe_id");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("symbol");

                    b.HasKey("ID");

                    b.HasIndex("SafeID");

                    b.ToTable("gnosis_safe_tokens");
                });

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeTransactionModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("BaseGas")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("base_gas");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("data");

                    b.Property<bool>("Executed")
                        .HasColumnType("boolean")
                        .HasColumnName("executed");

                    b.Property<string>("GasPrice")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gas_price");

                    b.Property<string>("GasToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gas_token");

                    b.Property<Guid>("GnosisSafeID")
                        .HasColumnType("uuid")
                        .HasColumnName("safe");

                    b.Property<int>("Nonce")
                        .HasColumnType("integer")
                        .HasColumnName("nonce");

                    b.Property<int>("Operation")
                        .HasColumnType("integer")
                        .HasColumnName("operation");

                    b.Property<string>("RefundReceiver")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("refund_receiver");

                    b.Property<string>("SafeTxGas")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("safe_tx_gas");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("to");

                    b.Property<string>("UserWallet")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("ID");

                    b.HasIndex("GnosisSafeID");

                    b.HasIndex("UserWallet");

                    b.ToTable("gnosis_safe_transactions");
                });

            modelBuilder.Entity("ZombieDAO.Models.ProjectMemberModel", b =>
                {
                    b.Property<Guid>("ProjectID")
                        .HasColumnType("uuid")
                        .HasColumnName("project_id");

                    b.Property<string>("UserWallet")
                        .HasColumnType("text")
                        .HasColumnName("user_wallet");

                    b.Property<byte>("Level")
                        .HasColumnType("smallint")
                        .HasColumnName("level");

                    b.HasKey("ProjectID", "UserWallet");

                    b.HasIndex("UserWallet");

                    b.ToTable("project_members");
                });

            modelBuilder.Entity("ZombieDAO.Models.ProjectModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email_address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("website");

                    b.HasKey("ID");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("ZombieDAO.Models.UserModel", b =>
                {
                    b.Property<string>("Wallet")
                        .HasColumnType("text")
                        .HasColumnName("wallet");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.HasKey("Wallet");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeConfirmationModel", b =>
                {
                    b.HasOne("ZombieDAO.Models.GnosisSafeTransactionModel", "Transaction")
                        .WithMany("Confirmations")
                        .HasForeignKey("TransactionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZombieDAO.Models.UserModel", "User")
                        .WithMany("GnosisSafeConfirmations")
                        .HasForeignKey("UserWallet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeModel", b =>
                {
                    b.HasOne("ZombieDAO.Models.ProjectModel", "Project")
                        .WithMany("GnosisSafes")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeTokenModel", b =>
                {
                    b.HasOne("ZombieDAO.Models.GnosisSafeModel", "Safe")
                        .WithMany("Tokens")
                        .HasForeignKey("SafeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Safe");
                });

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeTransactionModel", b =>
                {
                    b.HasOne("ZombieDAO.Models.GnosisSafeModel", "GnosisSafe")
                        .WithMany("Transactions")
                        .HasForeignKey("GnosisSafeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZombieDAO.Models.UserModel", "User")
                        .WithMany("GnosisSafeTransactions")
                        .HasForeignKey("UserWallet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GnosisSafe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZombieDAO.Models.ProjectMemberModel", b =>
                {
                    b.HasOne("ZombieDAO.Models.ProjectModel", "Project")
                        .WithMany("Members")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZombieDAO.Models.UserModel", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserWallet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeModel", b =>
                {
                    b.Navigation("Tokens");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("ZombieDAO.Models.GnosisSafeTransactionModel", b =>
                {
                    b.Navigation("Confirmations");
                });

            modelBuilder.Entity("ZombieDAO.Models.ProjectModel", b =>
                {
                    b.Navigation("GnosisSafes");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("ZombieDAO.Models.UserModel", b =>
                {
                    b.Navigation("GnosisSafeConfirmations");

                    b.Navigation("GnosisSafeTransactions");

                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
