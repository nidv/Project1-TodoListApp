using System;
using System.Collections.Generic;
using System.Text;

namespace Project1_TodoListApp
{
    public class TaskFunctions
    {
        
        public List<TaskItem> tasks;
        public List<TaskItem> GetTasks() => tasks;
        public int Count => tasks.Count;
        private bool IsValidIndex(int index) => index >= 0 && index < tasks.Count;

        public TaskFunctions()
        {
        }

        public TaskFunctions(List<TaskItem> tasks)
        {
            this.tasks = tasks;
        }
        public void AddTask(string title, DateTime dueDate, string project)
        {
            tasks.Add(new TaskItem(title, dueDate, project));
        }

        public bool EditTask(int index, string title, DateTime dueDate, string project, bool status)
        {
            if (!IsValidIndex(index)) return false;
            tasks[index].Edit(title, dueDate, project, status);
            return true;
        }

        public bool MarkTaskDone(int index)
        {
            if (!IsValidIndex(index)) return false;
            tasks[index].MarkDone();
            return true;
        }

        public bool RemoveTask(int index)
        {
            if (!IsValidIndex(index)) return false;
            tasks.RemoveAt(index);
            return true;
        }

        public List<TaskItem> GetTasksByDate()
        {
            return tasks.OrderBy(t => t.DueDate).ToList();
        }

        public List<TaskItem> GetTasksByProject()
        {
            return tasks.OrderBy(t => t.Project).ThenBy(t => t.DueDate).ToList();
        }
    }
}

