using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBaB.Migrations
{
    public partial class SeedingAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('All Accounts','All Accounts',NULL,0,'2021/1/1','2021/1/1')");//1
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Balance Sheet','Balance Sheet',1,1,'2021/1/1','2021/1/1')");//2
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Assets','Assets',2,2,'2021/1/1','2021/1/1')");//3
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Fixed Assets','Fixed Assets',3,3,'2021/1/1','2021/1/1')");//4
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Current Assets','Current Assets',3,3,'2021/1/1','2021/1/1')");//5
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Inventory','Inventory',5,3,'2021/1/1','2021/1/1')");//6
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Cashs','Cashs',5,3,'2021/1/1','2021/1/1')");//7
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Banks','Banks',5,3,'2021/1/1','2021/1/1')");//8
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Accounts Receivables','Accounts Receivables',5,3,'2021/1/1','2021/1/1')");//9
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Liabilities','Liabilities',2,2,'2021/1/1','2021/1/1')");//10
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Fixed Liabilities','Fixed Liabilities',10,3,'2021/1/1','2021/1/1')");//11
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Capital','Capital',11,4,'2021/1/1','2021/1/1')");//12
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Current Liabilities','Current Liabilities',10,3,'2021/1/1','2021/1/1')");//13
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Accounts Payable','Accounts Payable',13,4,'2021/1/1','2021/1/1')");//14
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Accumulated Profit','Accumulated Profit',13,4,'2021/1/1','2021/1/1')");//15
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Income statement','Income statement',1,1,'2021/1/1','2021/1/1')"); //16
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Expenses','Expenses',16,2,'2021/1/1','2021/1/1')"); //17
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Trading Expenses','Trading Expenses',17,3,'2021/1/1','2021/1/1')");//18
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Selling Cost','Selling Cost',18,4,'2021/1/1','2021/1/1')");//19
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Cost Goods Sold','Cost Goods Sold',18,4,'2021/1/1','2021/1/1')");//20
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Other Expenses','Other Expenses',17,3,'2021/1/1','2021/1/1')"); //21
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Incomes','Incomes',16,2,'2021/1/1','2021/1/1')"); //22
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Operating Revenues','Operating Revenues',22,3,'2021/1/1','2021/1/1')"); //23
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Revenue','Revenue',23,4,'2021/1/1','2021/1/1')"); //24
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Sales Revenue','Sales Revenue',23,4,'2021/1/1','2021/1/1')"); //25
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Service revenue','Service revenue',23,4,'2021/1/1','2021/1/1')"); //26
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Discount Given','Discount Given',23,4,'2021/1/1','2021/1/1')"); //27
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Other Income','Other Income',22,3,'2021/1/1','2021/1/1')"); //28
            migrationBuilder.Sql("Insert Into dbAccounts (Name,KeyId,ParentId,lvl,Created,LastActive) values ('Miscellaneous revenues','Miscellaneous revenues',28,4,'2021/1/1','2021/1/1')"); //29

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
