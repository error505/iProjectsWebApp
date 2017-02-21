namespace iProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelSt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Story", "ReleaseId");
            RenameColumn(table: "dbo.Story", name: "Releases_Id", newName: "ReleaseId");
            RenameIndex(table: "dbo.Story", name: "IX_Releases_Id", newName: "IX_ReleaseId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Story", name: "IX_ReleaseId", newName: "IX_Releases_Id");
            RenameColumn(table: "dbo.Story", name: "ReleaseId", newName: "Releases_Id");
            AddColumn("dbo.Story", "ReleaseId", c => c.Int());
        }
    }
}
