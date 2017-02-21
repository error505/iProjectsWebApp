namespace iProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Story : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Story", "EpicId", "dbo.Epic");
            DropIndex("dbo.Story", new[] { "EpicId" });
            DropIndex("dbo.Story", new[] { "Priority_Id" });
            DropIndex("dbo.Story", new[] { "StatusOfStoriesTasks_Id" });
            DropColumn("dbo.Story", "PriorityId");
            DropColumn("dbo.Story", "StatusId");
            RenameColumn(table: "dbo.Story", name: "Priority_Id", newName: "PriorityId");
            RenameColumn(table: "dbo.Story", name: "StatusOfStoriesTasks_Id", newName: "StatusId");
            AlterColumn("dbo.Story", "PriorityId", c => c.Int());
            AlterColumn("dbo.Story", "StatusId", c => c.Int());
            AlterColumn("dbo.Story", "SprintsId", c => c.Int());
            AlterColumn("dbo.Story", "EpicId", c => c.Int());
            AlterColumn("dbo.Story", "ReleaseId", c => c.Int());
            CreateIndex("dbo.Story", "PriorityId");
            CreateIndex("dbo.Story", "StatusId");
            CreateIndex("dbo.Story", "EpicId");
            AddForeignKey("dbo.Story", "EpicId", "dbo.Epic", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Story", "EpicId", "dbo.Epic");
            DropIndex("dbo.Story", new[] { "EpicId" });
            DropIndex("dbo.Story", new[] { "StatusId" });
            DropIndex("dbo.Story", new[] { "PriorityId" });
            AlterColumn("dbo.Story", "ReleaseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Story", "EpicId", c => c.Int(nullable: false));
            AlterColumn("dbo.Story", "SprintsId", c => c.Int(nullable: false));
            AlterColumn("dbo.Story", "StatusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Story", "PriorityId", c => c.String());
            RenameColumn(table: "dbo.Story", name: "StatusId", newName: "StatusOfStoriesTasks_Id");
            RenameColumn(table: "dbo.Story", name: "PriorityId", newName: "Priority_Id");
            AddColumn("dbo.Story", "StatusId", c => c.Int(nullable: false));
            AddColumn("dbo.Story", "PriorityId", c => c.String());
            CreateIndex("dbo.Story", "StatusOfStoriesTasks_Id");
            CreateIndex("dbo.Story", "Priority_Id");
            CreateIndex("dbo.Story", "EpicId");
            AddForeignKey("dbo.Story", "EpicId", "dbo.Epic", "Id", cascadeDelete: true);
        }
    }
}
