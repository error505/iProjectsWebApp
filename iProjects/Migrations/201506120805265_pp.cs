namespace iProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BugReport", "ProjectsId", c => c.Int(nullable: false));
            AddColumn("dbo.BugReport", "Project_ProjectId", c => c.Int());
            AddColumn("dbo.Story", "ProjectsId", c => c.Int(nullable: false));
            AddColumn("dbo.Story", "Project_ProjectId", c => c.Int());
            CreateIndex("dbo.BugReport", "Project_ProjectId");
            CreateIndex("dbo.Story", "Project_ProjectId");
            AddForeignKey("dbo.Story", "Project_ProjectId", "dbo.Project", "ProjectId");
            AddForeignKey("dbo.BugReport", "Project_ProjectId", "dbo.Project", "ProjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BugReport", "Project_ProjectId", "dbo.Project");
            DropForeignKey("dbo.Story", "Project_ProjectId", "dbo.Project");
            DropIndex("dbo.Story", new[] { "Project_ProjectId" });
            DropIndex("dbo.BugReport", new[] { "Project_ProjectId" });
            DropColumn("dbo.Story", "Project_ProjectId");
            DropColumn("dbo.Story", "ProjectsId");
            DropColumn("dbo.BugReport", "Project_ProjectId");
            DropColumn("dbo.BugReport", "ProjectsId");
        }
    }
}
