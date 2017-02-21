namespace iProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sd : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TasksToDoApplicationUser", newName: "ApplicationUserTasksToDo");
            DropForeignKey("dbo.ProjectFilesDb", "Story_StoryId", "dbo.Story");
            DropForeignKey("dbo.ProjectFilesDb", "TasksToDo_Id", "dbo.TasksToDo");
            DropForeignKey("dbo.ProjectFilesDb", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectFilesDb", new[] { "UserId" });
            DropIndex("dbo.ProjectFilesDb", new[] { "Story_StoryId" });
            DropIndex("dbo.ProjectFilesDb", new[] { "TasksToDo_Id" });
            DropPrimaryKey("dbo.ApplicationUserTasksToDo");
            AddPrimaryKey("dbo.ApplicationUserTasksToDo", new[] { "ApplicationUser_Id", "TasksToDo_Id" });
            DropColumn("dbo.ProjectFilesDb", "ProjectsId");
            DropColumn("dbo.ProjectFilesDb", "StoriesId");
            DropColumn("dbo.ProjectFilesDb", "TasksId");
            DropColumn("dbo.ProjectFilesDb", "UserId");
            DropColumn("dbo.ProjectFilesDb", "Story_StoryId");
            DropColumn("dbo.ProjectFilesDb", "TasksToDo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectFilesDb", "TasksToDo_Id", c => c.Int());
            AddColumn("dbo.ProjectFilesDb", "Story_StoryId", c => c.Int());
            AddColumn("dbo.ProjectFilesDb", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.ProjectFilesDb", "TasksId", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectFilesDb", "StoriesId", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectFilesDb", "ProjectsId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.ApplicationUserTasksToDo");
            AddPrimaryKey("dbo.ApplicationUserTasksToDo", new[] { "TasksToDo_Id", "ApplicationUser_Id" });
            CreateIndex("dbo.ProjectFilesDb", "TasksToDo_Id");
            CreateIndex("dbo.ProjectFilesDb", "Story_StoryId");
            CreateIndex("dbo.ProjectFilesDb", "UserId");
            AddForeignKey("dbo.ProjectFilesDb", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ProjectFilesDb", "TasksToDo_Id", "dbo.TasksToDo", "Id");
            AddForeignKey("dbo.ProjectFilesDb", "Story_StoryId", "dbo.Story", "StoryId");
            RenameTable(name: "dbo.ApplicationUserTasksToDo", newName: "TasksToDoApplicationUser");
        }
    }
}
