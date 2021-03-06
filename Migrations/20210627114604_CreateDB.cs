﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ALBaB.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aspnetroles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    normalizedname = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    concurrencystamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetroles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    lastactive = table.Column<DateTime>(type: "datetime", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    lookingfor = table.Column<string>(type: "text", nullable: true),
                    interests = table.Column<string>(type: "text", nullable: true),
                    line1 = table.Column<string>(type: "text", nullable: true),
                    line2 = table.Column<string>(type: "text", nullable: true),
                    region = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true),
                    Address_appuserid = table.Column<int>(type: "int", nullable: true),
                    username = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    normalizedusername = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    normalizedemail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    passwordhash = table.Column<string>(type: "text", nullable: true),
                    securitystamp = table.Column<string>(type: "text", nullable: true),
                    concurrencystamp = table.Column<string>(type: "text", nullable: true),
                    phonenumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetusers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dbaccounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    keyid = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    lvl = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    parentid = table.Column<int>(type: "int", nullable: true),
                    isexpandable = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dbaccounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_dbaccounts_dbaccounts_parentid",
                        column: x => x.parentid,
                        principalTable: "dbaccounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "journals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    jeno = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    entrydate = table.Column<DateTime>(type: "datetime", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_journals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ordermethod",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    shortname = table.Column<string>(type: "text", nullable: true),
                    ordertime = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ordermethod", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetroleclaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    roleid = table.Column<int>(type: "int", nullable: false),
                    claimtype = table.Column<string>(type: "text", nullable: true),
                    claimvalue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetroleclaims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnetroleclaims_aspnetroles_roleid",
                        column: x => x.roleid,
                        principalTable: "aspnetroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserclaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    userid = table.Column<int>(type: "int", nullable: false),
                    claimtype = table.Column<string>(type: "text", nullable: true),
                    claimvalue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetuserclaims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnetuserclaims_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserlogins",
                columns: table => new
                {
                    loginprovider = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    providerkey = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    providerdisplayname = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetuserlogins", x => new { x.loginprovider, x.providerkey });
                    table.ForeignKey(
                        name: "fk_aspnetuserlogins_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserroles",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false),
                    roleid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetuserroles", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "fk_aspnetuserroles_aspnetroles_roleid",
                        column: x => x.roleid,
                        principalTable: "aspnetroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_aspnetuserroles_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusertokens",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false),
                    loginprovider = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetusertokens", x => new { x.userid, x.loginprovider, x.name });
                    table.ForeignKey(
                        name: "fk_aspnetusertokens_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "models",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    brandid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_models", x => x.id);
                    table.ForeignKey(
                        name: "fk_models_brands_brandid",
                        column: x => x.brandid,
                        principalTable: "brands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    invno = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    lastupdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    appuserid = table.Column<int>(type: "int", nullable: true),
                    dbaccountid = table.Column<int>(type: "int", nullable: false),
                    actionacctid = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(10, 3)", nullable: true),
                    discount = table.Column<decimal>(type: "decimal(10, 3)", nullable: true),
                    vatacctid = table.Column<int>(type: "int", nullable: false),
                    vat = table.Column<decimal>(type: "decimal(10, 3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoices", x => x.id);
                    table.ForeignKey(
                        name: "fk_invoices_aspnetusers_appuserid",
                        column: x => x.appuserid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_invoices_dbaccounts_dbaccountid",
                        column: x => x.dbaccountid,
                        principalTable: "dbaccounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "journalaccounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    journalid = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    duedate = table.Column<DateTime>(type: "datetime", nullable: false),
                    appuserid = table.Column<int>(type: "int", nullable: true),
                    dbaccountid = table.Column<int>(type: "int", nullable: false),
                    credit = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    debit = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    refno = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_journalaccounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_journalaccounts_aspnetusers_appuserid",
                        column: x => x.appuserid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_journalaccounts_dbaccounts_dbaccountid",
                        column: x => x.dbaccountid,
                        principalTable: "dbaccounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_journalaccounts_journals_journalid",
                        column: x => x.journalid,
                        principalTable: "journals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    orderdate = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                    line1 = table.Column<string>(type: "text", nullable: true),
                    line2 = table.Column<string>(type: "text", nullable: true),
                    region = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true),
                    ordermethodid = table.Column<int>(type: "int", nullable: true),
                    subtotal = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    status = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    paymentintentid = table.Column<string>(type: "text", nullable: true),
                    appuserid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_aspnetusers_appuserid",
                        column: x => x.appuserid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_ordermethod_ordermethodid",
                        column: x => x.ordermethodid,
                        principalTable: "ordermethod",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(7, 2)", nullable: false),
                    pictureurl = table.Column<string>(type: "text", nullable: true),
                    modelid = table.Column<int>(type: "int", nullable: false),
                    lastupdate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_models_modelid",
                        column: x => x.modelid,
                        principalTable: "models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderitems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    orderid = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderitems", x => x.id);
                    table.ForeignKey(
                        name: "fk_orderitems_orders_orderid",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invdetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    quantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    cost = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    lastupdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    invoiceid = table.Column<int>(type: "int", nullable: false),
                    productid = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invdetails", x => x.id);
                    table.ForeignKey(
                        name: "fk_invdetails_invoices_invoiceid",
                        column: x => x.invoiceid,
                        principalTable: "invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_invdetails_products_productid",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_aspnetroleclaims_roleid",
                table: "aspnetroleclaims",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "rolenameindex",
                table: "aspnetroles",
                column: "normalizedname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_aspnetuserclaims_userid",
                table: "aspnetuserclaims",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_aspnetuserlogins_userid",
                table: "aspnetuserlogins",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_aspnetuserroles_roleid",
                table: "aspnetuserroles",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "emailindex",
                table: "aspnetusers",
                column: "normalizedemail");

            migrationBuilder.CreateIndex(
                name: "usernameindex",
                table: "aspnetusers",
                column: "normalizedusername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_dbaccounts_parentid",
                table: "dbaccounts",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "IX_invdetails_invoiceid_productid",
                table: "invdetails",
                columns: new[] { "invoiceid", "productid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_invdetails_productid",
                table: "invdetails",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_invoices_appuserid",
                table: "invoices",
                column: "appuserid");

            migrationBuilder.CreateIndex(
                name: "ix_invoices_dbaccountid",
                table: "invoices",
                column: "dbaccountid");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_invno_type",
                table: "invoices",
                columns: new[] { "invno", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_journalaccounts_appuserid",
                table: "journalaccounts",
                column: "appuserid");

            migrationBuilder.CreateIndex(
                name: "ix_journalaccounts_dbaccountid",
                table: "journalaccounts",
                column: "dbaccountid");

            migrationBuilder.CreateIndex(
                name: "ix_journalaccounts_journalid",
                table: "journalaccounts",
                column: "journalid");

            migrationBuilder.CreateIndex(
                name: "IX_journals_jeno_type",
                table: "journals",
                columns: new[] { "jeno", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_models_brandid",
                table: "models",
                column: "brandid");

            migrationBuilder.CreateIndex(
                name: "ix_orderitems_orderid",
                table: "orderitems",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "ix_orders_appuserid",
                table: "orders",
                column: "appuserid");

            migrationBuilder.CreateIndex(
                name: "ix_orders_ordermethodid",
                table: "orders",
                column: "ordermethodid");

            migrationBuilder.CreateIndex(
                name: "ix_products_modelid",
                table: "products",
                column: "modelid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aspnetroleclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserlogins");

            migrationBuilder.DropTable(
                name: "aspnetuserroles");

            migrationBuilder.DropTable(
                name: "aspnetusertokens");

            migrationBuilder.DropTable(
                name: "invdetails");

            migrationBuilder.DropTable(
                name: "journalaccounts");

            migrationBuilder.DropTable(
                name: "orderitems");

            migrationBuilder.DropTable(
                name: "aspnetroles");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "journals");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "dbaccounts");

            migrationBuilder.DropTable(
                name: "models");

            migrationBuilder.DropTable(
                name: "aspnetusers");

            migrationBuilder.DropTable(
                name: "ordermethod");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
