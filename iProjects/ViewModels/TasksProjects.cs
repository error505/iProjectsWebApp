using System;
using System.Collections.Generic;
using iProjects.Models;
using System.Linq;
using System.Web;

namespace iProjects.ViewModels
{
    public class TasksProjects
    {
        public IEnumerable<Project> Projects { get; set; }

        public IEnumerable<TasksToDo> TasksToDoes { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}