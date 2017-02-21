namespace iProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sprint", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sprint", "IsActive");
        }
    }
}
