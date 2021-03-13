using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBAB.Migrations
{
    public partial class dbAccountSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('All Accounts','All Accounts',NULL,0,'2021/01/01')");//1
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Balance Sheet','Balance Sheet',1,1,'2021/01/01')");//2
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Assets','Assets',2,2,'2021/01/01')");//3
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Fixed Assets','Fixed Assets',3,3,'2021/01/01')");//4
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Current Assets','Current Assets',3,3,'2021/01/01')");//5
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Inventory','Inventory',5,3,'2021/01/01')");//6
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Cashs','Cashs',5,3,'2021/01/01')");//7
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Banks','Banks',5,3,'2021/01/01')");//8
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Accounts Receivables','Accounts Receivables',5,3,'2021/01/01')");//9
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Liabilities','Liabilities',2,2,'2021/01/01')");//10
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Fixed Liabilities','Fixed Liabilities',10,3,'2021/01/01')");//11
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Capital','Capital',11,4,'2021/01/01')");//12
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Current Liabilities','Current Liabilities',10,3,'2021/01/01')");//13
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Accounts Payable','Accounts Payable',13,4,'2021/01/01')");//14
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Accumulated Profit','Accumulated Profit',13,4,'2021/01/01')");//15
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Income statement','Income statement',1,1,'2021/01/01')"); //16
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Expenses','Expenses',16,2,'2021/01/01')"); //17
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Trading Expenses','Trading Expenses',17,3,'2021/01/01')");//18
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Selling Cost','Selling Cost',18,4,'2021/01/01')");//19
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Cost Goods Sold','Cost Goods Sold',18,4,'2021/01/01')");//20
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Other Expenses','Other Expenses',17,3,'2021/01/01')"); //21
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Incomes','Incomes',16,2,'2021/01/01')"); //22
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Operating Revenues','Operating Revenues',22,3,'2021/01/01')"); //23
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Revenue','Revenue',23,4,'2021/01/01')"); //24
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Sales Revenue','Sales Revenue',23,4,'2021/01/01')"); //25
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Service revenue','Service revenue',23,4,'2021/01/01')"); //26
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Discount Given','Discount Given',23,4,'2021/01/01')"); //27
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Other Income','Other Income',22,3,'2021/01/01')"); //28
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created) values ('Miscellaneous revenues','Miscellaneous revenues',28,4,'2021/01/01')"); //29

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
