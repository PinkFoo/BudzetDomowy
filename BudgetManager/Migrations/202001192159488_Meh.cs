namespace BudgetManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Meh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Budget", "EmountBud", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Budget", "NameBud", c => c.String(maxLength: 50));
            AlterColumn("dbo.Expense", "Desription", c => c.String(maxLength: 100));
            AlterColumn("dbo.Expense", "Amount", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Category", "NazwaKat", c => c.String(maxLength: 50));
            AlterColumn("dbo.User", "NazwaUzyt", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "NazwaUzyt", c => c.String());
            AlterColumn("dbo.Category", "NazwaKat", c => c.String());
            AlterColumn("dbo.Expense", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Expense", "Desription", c => c.String());
            AlterColumn("dbo.Budget", "NameBud", c => c.String());
            AlterColumn("dbo.Budget", "EmountBud", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
