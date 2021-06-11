﻿// <auto-generated />
using System;
using ALBAB.Entities.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ALBaB.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210611162003_InvoiceUpdate2")]
    partial class InvoiceUpdate2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrencystamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("normalizedname");

                    b.HasKey("Id")
                        .HasName("pk_aspnetroles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("rolenameindex");

                    b.ToTable("aspnetroles");
                });

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrencystamp");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("email");

                    b.Property<string>("Interests")
                        .HasColumnType("text")
                        .HasColumnName("interests");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime")
                        .HasColumnName("lastactive");

                    b.Property<string>("LookingFor")
                        .HasColumnType("text")
                        .HasColumnName("lookingfor");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("normalizedemail");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("normalizedusername");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("passwordhash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phonenumber");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("securitystamp");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("username");

                    b.Property<int>("type")
                        .HasColumnType("int")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_aspnetusers");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("emailindex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("usernameindex");

                    b.ToTable("aspnetusers");
                });

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("roleid");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_aspnetuserroles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_aspnetuserroles_roleid");

                    b.ToTable("aspnetuserroles");
                });

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.dbAccounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created");

                    b.Property<bool>("IsExpandable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("isexpandable");

                    b.Property<string>("KeyId")
                        .HasColumnType("text")
                        .HasColumnName("keyid");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parentid");

                    b.Property<int>("lvl")
                        .HasColumnType("int")
                        .HasColumnName("lvl");

                    b.HasKey("Id")
                        .HasName("pk_dbaccounts");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("ix_dbaccounts_parentid");

                    b.ToTable("dbaccounts");
                });

            modelBuilder.Entity("ALBAB.Entities.Journal.JournalAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("accountid");

                    b.Property<int?>("AppUserId")
                        .HasColumnType("int")
                        .HasColumnName("appuserid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created");

                    b.Property<decimal?>("Credit")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("credit");

                    b.Property<decimal?>("Debit")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("debit");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime")
                        .HasColumnName("duedate");

                    b.Property<int>("JournalId")
                        .HasColumnType("int")
                        .HasColumnName("journalid");

                    b.Property<string>("RefNo")
                        .HasColumnType("text")
                        .HasColumnName("refno");

                    b.HasKey("Id")
                        .HasName("pk_journalaccounts");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_journalaccounts_accountid");

                    b.HasIndex("AppUserId")
                        .HasDatabaseName("ix_journalaccounts_appuserid");

                    b.HasIndex("JournalId")
                        .HasDatabaseName("ix_journalaccounts_journalid");

                    b.ToTable("journalaccounts");
                });

            modelBuilder.Entity("ALBAB.Entities.Journal.JournalEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created");

                    b.Property<string>("JENo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("jeno");

                    b.Property<string>("Note")
                        .HasColumnType("text")
                        .HasColumnName("note");

                    b.Property<DateTime>("entryDate")
                        .HasColumnType("datetime")
                        .HasColumnName("entrydate");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_journals");

                    b.ToTable("journals");
                });

            modelBuilder.Entity("ALBAB.Entities.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int")
                        .HasColumnName("appuserid");

                    b.Property<DateTimeOffset>("OrderDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("orderdate");

                    b.Property<int?>("OrderMethodId")
                        .HasColumnType("int")
                        .HasColumnName("ordermethodid");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("text")
                        .HasColumnName("paymentintentid");

                    b.Property<int>("Status")
                        .HasMaxLength(5)
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("subtotal");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("AppUserId")
                        .HasDatabaseName("ix_orders_appuserid");

                    b.HasIndex("OrderMethodId")
                        .HasDatabaseName("ix_orders_ordermethodid");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("ALBAB.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderid");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("price");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_orderitems");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_orderitems_orderid");

                    b.ToTable("orderitems");
                });

            modelBuilder.Entity("ALBAB.Entities.OrderAggregate.OrderMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("OrderTime")
                        .HasColumnType("text")
                        .HasColumnName("ordertime");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("price");

                    b.Property<string>("ShortName")
                        .HasColumnType("text")
                        .HasColumnName("shortname");

                    b.HasKey("Id")
                        .HasName("pk_ordermethod");

                    b.ToTable("ordermethod");
                });

            modelBuilder.Entity("ALBAB.Entities.Products.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_brands");

                    b.ToTable("brands");
                });

            modelBuilder.Entity("ALBAB.Entities.Products.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("BrandId")
                        .HasColumnType("int")
                        .HasColumnName("brandid");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_models");

                    b.HasIndex("BrandId")
                        .HasDatabaseName("ix_models_brandid");

                    b.ToTable("models");
                });

            modelBuilder.Entity("ALBAB.Entities.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("ModelId")
                        .HasColumnType("int")
                        .HasColumnName("modelid");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("text")
                        .HasColumnName("pictureurl");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("price");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("ModelId")
                        .HasDatabaseName("ix_products_modelid");

                    b.ToTable("products");
                });

            modelBuilder.Entity("ALBAB.Entities.Purchases.InvDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int")
                        .HasColumnName("invoiceid");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime")
                        .HasColumnName("lastupdate");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("price");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productid");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_invdetails");

                    b.HasIndex("InvoiceId")
                        .HasDatabaseName("ix_invdetails_invoiceid");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_invdetails_productid");

                    b.ToTable("invdetails");
                });

            modelBuilder.Entity("ALBAB.Entities.Purchases.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("accountid");

                    b.Property<int?>("AppUserId")
                        .HasColumnType("int")
                        .HasColumnName("appuserid");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<int>("DebitAccountId")
                        .HasColumnType("int")
                        .HasColumnName("debitaccountid");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("discount");

                    b.Property<string>("InvNo")
                        .HasColumnType("text")
                        .HasColumnName("invno");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime")
                        .HasColumnName("lastupdate");

                    b.Property<decimal?>("SubTotal")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("subtotal");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("totalamount");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<decimal?>("Vat")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("vat");

                    b.Property<int>("VatAccountId")
                        .HasColumnType("int")
                        .HasDefaultValue(33)
                        .HasColumnName("vataccountid");

                    b.HasKey("Id")
                        .HasName("pk_invoices");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_invoices_accountid");

                    b.HasIndex("AppUserId")
                        .HasDatabaseName("ix_invoices_appuserid");

                    b.ToTable("invoices");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claimtype");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claimvalue");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("roleid");

                    b.HasKey("Id")
                        .HasName("pk_aspnetroleclaims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_aspnetroleclaims_roleid");

                    b.ToTable("aspnetroleclaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claimtype");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claimvalue");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.HasKey("Id")
                        .HasName("pk_aspnetuserclaims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_aspnetuserclaims_userid");

                    b.ToTable("aspnetuserclaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("loginprovider");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("providerkey");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("providerdisplayname");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_aspnetuserlogins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_aspnetuserlogins_userid");

                    b.ToTable("aspnetuserlogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("loginprovider");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_aspnetusertokens");

                    b.ToTable("aspnetusertokens");
                });

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.AppUser", b =>
                {
                    b.OwnsOne("ALBAB.Entities.DB.Address", "Address", b1 =>
                        {
                            b1.Property<int>("id")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .HasColumnType("text")
                                .HasColumnName("country");

                            b1.Property<string>("Line1")
                                .HasColumnType("text")
                                .HasColumnName("line1");

                            b1.Property<string>("Line2")
                                .HasColumnType("text")
                                .HasColumnName("line2");

                            b1.Property<string>("Region")
                                .HasColumnType("text")
                                .HasColumnName("region");

                            b1.Property<int>("appuserid")
                                .HasColumnType("int");

                            b1.HasKey("id");

                            b1.ToTable("aspnetusers");

                            b1.WithOwner()
                                .HasForeignKey("id")
                                .HasConstraintName("fk_aspnetusers_aspnetusers_appuserid");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.AppUserRole", b =>
                {
                    b.HasOne("ALBAB.Entities.AppAccounts.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_aspnetuserroles_aspnetroles_roleid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALBAB.Entities.AppAccounts.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetuserroles_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.dbAccounts", b =>
                {
                    b.HasOne("ALBAB.Entities.AppAccounts.dbAccounts", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("fk_dbaccounts_dbaccounts_parentid");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ALBAB.Entities.Journal.JournalAccount", b =>
                {
                    b.HasOne("ALBAB.Entities.AppAccounts.dbAccounts", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .HasConstraintName("fk_journalaccounts_dbaccounts_accountid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALBAB.Entities.AppAccounts.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .HasConstraintName("fk_journalaccounts_aspnetusers_appuserid");

                    b.HasOne("ALBAB.Entities.Journal.JournalEntry", "Journal")
                        .WithMany("journalAccounts")
                        .HasForeignKey("JournalId")
                        .HasConstraintName("fk_journalaccounts_journals_journalid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("AppUser");

                    b.Navigation("Journal");
                });

            modelBuilder.Entity("ALBAB.Entities.OrderAggregate.Order", b =>
                {
                    b.HasOne("ALBAB.Entities.AppAccounts.AppUser", "AppUser")
                        .WithMany("orders")
                        .HasForeignKey("AppUserId")
                        .HasConstraintName("fk_orders_aspnetusers_appuserid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALBAB.Entities.OrderAggregate.OrderMethod", "OrderMethod")
                        .WithMany()
                        .HasForeignKey("OrderMethodId")
                        .HasConstraintName("fk_orders_ordermethod_ordermethodid");

                    b.OwnsOne("ALBAB.Entities.DB.Address", "Address", b1 =>
                        {
                            b1.Property<int>("id")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .HasColumnType("text")
                                .HasColumnName("country");

                            b1.Property<string>("Line1")
                                .HasColumnType("text")
                                .HasColumnName("line1");

                            b1.Property<string>("Line2")
                                .HasColumnType("text")
                                .HasColumnName("line2");

                            b1.Property<string>("Region")
                                .HasColumnType("text")
                                .HasColumnName("region");

                            b1.HasKey("id");

                            b1.ToTable("orders");

                            b1.WithOwner()
                                .HasForeignKey("id")
                                .HasConstraintName("fk_orders_orders_orderid");
                        });

                    b.Navigation("Address");

                    b.Navigation("AppUser");

                    b.Navigation("OrderMethod");
                });

            modelBuilder.Entity("ALBAB.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("ALBAB.Entities.OrderAggregate.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("fk_orderitems_orders_orderid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ALBAB.Entities.Products.Model", b =>
                {
                    b.HasOne("ALBAB.Entities.Products.Brand", "Brand")
                        .WithMany("models")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("fk_models_brands_brandid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("ALBAB.Entities.Products.Product", b =>
                {
                    b.HasOne("ALBAB.Entities.Products.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .HasConstraintName("fk_products_models_modelid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("ALBAB.Entities.Purchases.InvDetail", b =>
                {
                    b.HasOne("ALBAB.Entities.Purchases.Invoice", "Invoice")
                        .WithMany("InvDetail")
                        .HasForeignKey("InvoiceId")
                        .HasConstraintName("fk_invdetails_invoices_invoiceid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALBAB.Entities.Products.Product", "Product")
                        .WithMany("PurchDTLs")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fk_invdetails_products_productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ALBAB.Entities.Purchases.Invoice", b =>
                {
                    b.HasOne("ALBAB.Entities.AppAccounts.dbAccounts", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .HasConstraintName("fk_invoices_dbaccounts_accountid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALBAB.Entities.AppAccounts.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .HasConstraintName("fk_invoices_aspnetusers_appuserid");

                    b.Navigation("Account");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("ALBAB.Entities.AppAccounts.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_aspnetroleclaims_aspnetroles_roleid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("ALBAB.Entities.AppAccounts.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetuserclaims_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("ALBAB.Entities.AppAccounts.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetuserlogins_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("ALBAB.Entities.AppAccounts.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetusertokens_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.AppUser", b =>
                {
                    b.Navigation("orders");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ALBAB.Entities.AppAccounts.dbAccounts", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("ALBAB.Entities.Journal.JournalEntry", b =>
                {
                    b.Navigation("journalAccounts");
                });

            modelBuilder.Entity("ALBAB.Entities.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ALBAB.Entities.Products.Brand", b =>
                {
                    b.Navigation("models");
                });

            modelBuilder.Entity("ALBAB.Entities.Products.Product", b =>
                {
                    b.Navigation("PurchDTLs");
                });

            modelBuilder.Entity("ALBAB.Entities.Purchases.Invoice", b =>
                {
                    b.Navigation("InvDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
