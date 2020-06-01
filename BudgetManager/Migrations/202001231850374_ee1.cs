namespace BudgetManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ee1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Budget", "NameBud", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Budget", "NameBud", c => c.String(maxLength: 50));
        }
    }
}
