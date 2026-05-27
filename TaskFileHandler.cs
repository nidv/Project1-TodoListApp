using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Project1_TodoListApp
{
    public class TaskFileHandler
    {
        private const string filePath = "tasks.json";

        public TaskFileHandler()
        {
        }

        /// <summary>
        /// Saves the entire list of TaskItems to a file as JSON.
        /// </summary>
        public void Save(List<TaskItem> tasks)
        {
            try
            {
                // WriteIndented makes the JSON file human-readable instead of one long line
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(tasks, options);

                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads the list of TaskItems from the JSON file.
        /// </summary>
        public List<TaskItem> Load()
        {
            // If the file doesn't exist yet, return an empty list so the app doesn't crash
            if (!File.Exists(filePath))
            {
                return new List<TaskItem>();
            }

            try
            {
                string jsonString = File.ReadAllText(filePath);

                // Deserialize back into a List of TaskItems. 
                // If the file is empty, use the ?? operator to return a fresh list.
                return JsonSerializer.Deserialize<List<TaskItem>>(jsonString) ?? new List<TaskItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}. Returning empty list.");
                return new List<TaskItem>();
            }
        }
    }
}