using System;
using System.Collections.Generic;
using System.Text;

namespace Project1_TodoListApp
{
    public class TaskItem
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; } // false = not done, true = done
        public string Project { get; set; }

        public TaskItem(string title, DateTime dueDate, string project, bool status = false)
        {
            Title = title;
            DueDate = dueDate;
            Project = project;
            Status = status;
        }
        public void MarkDone()
        {
            Status = true;
        }
        public void Edit(string title, DateTime dueDate, string project, bool status)
        {
            Title = title;
            DueDate = dueDate;
            Project = project;
            Status = status;
        }
    }
}
