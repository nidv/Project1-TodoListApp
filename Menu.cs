using System;
using System.Collections.Generic;
using System.Text;

namespace Project1_TodoListApp
{
    public class Menu
    {
        private readonly TaskFunctions taskFunctions;
        private readonly TaskFileHandler taskFileHandler;
        private bool isRunning;

        public Menu() {
            taskFileHandler = new TaskFileHandler();
            List<TaskItem> tasks = taskFileHandler.Load();
            taskFunctions = new TaskFunctions(tasks);
            isRunning = true;
        }
        public void Start()
        {
            while (isRunning)
            {                
                Show();
                string? choice = Console.ReadLine()?.Trim();
                Console.WriteLine();
                switch (choice)
                {
                    case "1": ViewByDate(); break;
                    case "2": ViewByProject(); break;
                    case "3": AddTask(); break;
                    case "4": EditTask(); break;
                    case "5": MarkDone(); break;
                    case "6": RemoveTask(); break;
                    case "7": SaveAndQuit(); break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  Invalid option. Please enter a number from 1 to 7.\n  Press any key to try again.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            }
        }

        private void Show()
        {
            List<TaskItem> tasks = taskFunctions.GetTasks();
            List<TaskItem> completedTasks = tasks.Where(t => t.Status == true).ToList();
            List<TaskItem> pendingTasks = tasks.Where(t => !t.Status).ToList();

            Console.WriteLine("\nWelcome to TodoList!");
            Console.WriteLine($"You have {pendingTasks.Count} tasks todo and {completedTasks.Count} tasks are done!\n");
            Console.WriteLine("=============================");
            Console.WriteLine("       TODO LIST MENU        ");
            Console.WriteLine("=============================");
            Console.WriteLine("  1. View tasks by due date");
            Console.WriteLine("  2. View tasks by project");
            Console.WriteLine("  3. Add a task");
            Console.WriteLine("  4. Edit a task");
            Console.WriteLine("  5. Mark a task as done");
            Console.WriteLine("  6. Remove a task");
            Console.WriteLine("  7. Save and quit");
            Console.WriteLine("=============================");
            Console.Write("Pick an option: ");
        }


        private void DisplayTasks(List<TaskItem> tasks)
        {
            Console.ResetColor();
            Console.WriteLine();
            if (tasks.Count == 0)
            {
                Console.WriteLine("  (no tasks)");
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"  {"#",-4} {"Status",-6} {"Due Date",-12} {"Project",-15} Title");

            for (int i = 0; i < tasks.Count; i++)
            {
                string status = tasks[i].Status ? "[DONE]" : "[    ]";
                if (tasks[i].Status) Console.ForegroundColor = ConsoleColor.Green;                
                else Console.ResetColor();                
                Console.WriteLine($"  {i + 1,-4} {status,-6} {tasks[i].DueDate:yyyy-MM-dd}   {tasks[i].Project,-15} {tasks[i].Title}");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        // ---- Input helpers ----

        private string PromptString(string label, string? current = null)
        {
            string prompt = current != null ? $"{label} [{current}]: " : $"{label}: ";
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();
            return (!string.IsNullOrWhiteSpace(input)) ? input : (current ?? "");
        }

        private DateTime PromptDate(string label, DateTime? current = null)
        {
            string currentStr = current.HasValue ? current.Value.ToString("yyyy-MM-dd") : "";
            while (true)
            {
                string prompt = current.HasValue ? $"{label} (yyyy-MM-dd) [{currentStr}]: " : $"{label} (yyyy-MM-dd): ";
                Console.Write(prompt);
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input) && current.HasValue)
                    return current.Value;

                if (DateTime.TryParse(input, out DateTime result))
                    return result;

                Console.WriteLine("  Invalid date. Please use yyyy-MM-dd format.");
            }
        }

        public int PromptTaskIndex(string action)
        {
            var tasks = taskFunctions.GetTasks();
            DisplayTasks(tasks);

            if (tasks.Count == 0) return -1;

            while (true)
            {
                Console.Write($"Enter task number to {action} (1-{tasks.Count}): ");
                string? input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out int num) && num >= 1 && num <= tasks.Count)
                    return num - 1; // convert to 0-based

                Console.WriteLine($"  Please enter a number between 1 and {tasks.Count}.");
            }
        }

        public void PrintLine(string message = "")
        {
            Console.WriteLine(message);
        }

        private void ViewByDate()
        {
            Console.WriteLine("--- Tasks sorted by due date ---");
            DisplayTasks(taskFunctions.GetTasksByDate());
            PauseConsole();
        }

        private void ViewByProject()
        {
            Console.WriteLine("--- Tasks sorted by project ---");
            DisplayTasks(taskFunctions.GetTasksByProject());
            PauseConsole();
        }

        private void AddTask()
        {
            Console.WriteLine("--- Add a new task ---");
            string title = PromptString("Title");
            if (string.IsNullOrWhiteSpace(title)) { Console.WriteLine("  Title cannot be empty.\n"); PauseConsole(); return; }

            DateTime dueDate = PromptDate("Due date");
            string project = PromptString("Project");
            if (string.IsNullOrWhiteSpace(project)) { Console.WriteLine("  Project cannot be empty.\n"); PauseConsole(); return; }

            taskFunctions.AddTask(title, dueDate, project);
            Console.WriteLine("  Task added.\n");
            PauseConsole();
        }

        private void EditTask()
        {
            Console.WriteLine("--- Edit a task ---");
            int index = PromptTaskIndex("edit");
            if (index < 0) return;

            var task = taskFunctions.GetTasks()[index];

            Console.WriteLine("  Leave a field blank to keep the current value.");
            string title = PromptString("Title", task.Title);
            DateTime dueDate = PromptDate("Due date", task.DueDate);
            string project = PromptString("Project", task.Project);
            bool status = PromptString("Status, completed? (y/n)", task.Status ? "y" : "n").ToLower() == "y";

            taskFunctions.EditTask(index, title, dueDate, project, status);
            Console.WriteLine("  Task updated.\n");
            PauseConsole();
        }

        private void MarkDone()
        {
            Console.WriteLine("--- Mark a task as done ---");
            int index = PromptTaskIndex("mark done");
            if (index < 0) return;

            bool success = taskFunctions.MarkTaskDone(index);
            Console.WriteLine(success ? "  Task marked as done.\n" : "  Could not mark task.\n");
            PauseConsole();
        }

        private void RemoveTask()
        {
            Console.WriteLine("--- Remove a task ---");
            int index = PromptTaskIndex("remove");
            if (index < 0) return;

            Console.Write("  Are you sure? (y/n): ");
            string? confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm == "y")
            {
                taskFunctions.RemoveTask(index);
                Console.WriteLine("  Task removed.\n");
            }
            else
            {
                Console.WriteLine("  Cancelled.\n");
            }
            PauseConsole();
        }

        private void SaveAndQuit()
        {
            taskFileHandler.Save(taskFunctions.GetTasks());
            Console.WriteLine("  Tasks saved. Goodbye!\n");
            isRunning = false;
        }
        private void PauseConsole()
        {
            Console.WriteLine("Execution step concluded. Press any key to return to main dashboard menu...");
            Console.ReadKey();
        }
    }
}
