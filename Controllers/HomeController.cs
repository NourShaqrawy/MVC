using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcTodoApp.Controllers
{
    public class HomeController : Controller
    {
        // قائمة المهام الثابتة (في الذاكرة)
        private static List<TaskItem> tasks =
        [
            new() { Id = 1, Title = "Learn MVC Design Pattern", IsComplete = false },
            new() { Id = 2, Title = "Learn N-tier Architecture", IsComplete = false },
            new() { Id = 3, Title = "Study git", IsComplete = false },
        ];

        /// <summary>
        /// Displays the list of tasks.
        /// </summary>
        public IActionResult Index()
        {
            return View(tasks);
        }

        /// <summary>
        /// Adds a new task.
        /// </summary>
        [HttpPost]
        public IActionResult AddTask(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                int newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
                var newTask = new TaskItem { Id = newId, Title = title, IsComplete = false };
                tasks.Add(newTask);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Marks a task as completed.
        /// </summary>
        [HttpPost]
        public IActionResult CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                task.IsComplete = true;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edits the title of a task.
        /// </summary>
        /// <param name="id">The ID of the task to edit</param>
        /// <param name="newTitle">The new title for the task</param>
        [HttpPost]
      public IActionResult EditTask(int id, string newTitle)
{
    try
    {
        var task = tasks.SingleOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound($"Task with ID {id} not found.");
        }

        if (string.IsNullOrWhiteSpace(newTitle))
        {
            return BadRequest("New title cannot be empty.");
        }

        task.Title = newTitle;

        return RedirectToAction("Index");
    }
    catch (Exception ex)
    {
        // Log the exception (if necessary)
        return StatusCode(500, $"An error occurred: {ex.Message}");
    }
}

    }
}