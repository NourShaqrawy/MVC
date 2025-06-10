using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcTodoApp.Controllers
{
    public class HomeController : Controller
    {
    
        private static List<TaskItem> tasks =
        [
            new() { Id = 1, Title = "Learn MVC Design Pattern", IsComplete = false },
            new() { Id = 2, Title = "Learn N-tier Architecture", IsComplete = false },
            new() { Id = 3, Title = "Study git", IsComplete = false },
        ];

        public IActionResult Index()
        {
            return View(tasks);
        }

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

        [HttpPost]
        public IActionResult CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                task.IsComplete = true;
            return RedirectToAction("Index");
        }

       
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
   
        return StatusCode(500, $"An error occurred: {ex.Message}");
    }
}

    }
}