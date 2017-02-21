namespace iProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserTasksToDo", newName: "TasksToDoApplicationUser");
            DropPrimaryKey("dbo.TasksToDoApplicationUser");
            AddColumn("dbo.ProjectFilesDb", "ProjectsId", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectFilesDb", "StoriesId", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectFilesDb", "TasksId", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectFilesDb", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.ProjectFilesDb", "Story_StoryId", c => c.Int());
            AddColumn("dbo.ProjectFilesDb", "TasksToDo_Id", c => c.Int());
            AddPrimaryKey("dbo.TasksToDoApplicationUser", new[] { "TasksToDo_Id", "ApplicationUser_Id" });
            CreateIndex("dbo.ProjectFilesDb", "UserId");
            CreateIndex("dbo.ProjectFilesDb", "Story_StoryId");
            CreateIndex("dbo.ProjectFilesDb", "TasksToDo_Id");
            AddForeignKey("dbo.ProjectFilesDb", "Story_StoryId", "dbo.Story", "StoryId");
            AddForeignKey("dbo.ProjectFilesDb", "TasksToDo_Id", "dbo.TasksToDo", "Id");
            AddForeignKey("dbo.ProjectFilesDb", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectFilesDb", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectFilesDb", "TasksToDo_Id", "dbo.TasksToDo");
            DropForeignKey("dbo.ProjectFilesDb", "Story_StoryId", "dbo.Story");
            DropIndex("dbo.ProjectFilesDb", new[] { "TasksToDo_Id" });
            DropIndex("dbo.ProjectFilesDb", new[] { "Story_StoryId" });
            DropIndex("dbo.ProjectFilesDb", new[] { "UserId" });
            DropPrimaryKey("dbo.TasksToDoApplicationUser");
            DropColumn("dbo.ProjectFilesDb", "TasksToDo_Id");
            DropColumn("dbo.ProjectFilesDb", "Story_StoryId");
            DropColumn("dbo.ProjectFilesDb", "UserId");
            DropColumn("dbo.ProjectFilesDb", "TasksId");
            DropColumn("dbo.ProjectFilesDb", "StoriesId");
            DropColumn("dbo.ProjectFilesDb", "ProjectsId");
            AddPrimaryKey("dbo.TasksToDoApplicationUser", new[] { "ApplicationUser_Id", "TasksToDo_Id" });
            RenameTable(name: "dbo.TasksToDoApplicationUser", newName: "ApplicationUserTasksToDo");
        }
    }
}
