namespace iProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintFinish : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sprint", "Finished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sprint", "Finished");
        }
    }
}
