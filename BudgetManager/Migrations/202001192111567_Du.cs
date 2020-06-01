namespace BudgetManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Du : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        NazwaKat = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            Sql("INSERT INTO dbo.Category (NazwaKat) VALUES ('Edukacja')");


            AddColumn("dbo.Expense", "CategoryID", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Expense", "CategoryID");
            AddForeignKey("dbo.Expense", "CategoryID", "dbo.Category", "CategoryID", cascadeDelete: true);
            DropColumn("dbo.Expense", "Kategoria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expense", "Kategoria", c => c.Int(nullable: false));
            DropForeignKey("dbo.Expense", "CategoryID", "dbo.Category");
            DropIndex("dbo.Expense", new[] { "CategoryID" });
            DropColumn("dbo.Expense", "CategoryID");
            DropTable("dbo.Category");
        }
    }
}
