namespace BudgetManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class echMigracja : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "NazwaUzyt", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "NazwaUzyt", c => c.String(maxLength: 50));
        }
    }
}
