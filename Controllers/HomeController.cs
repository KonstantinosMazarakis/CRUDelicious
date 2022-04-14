using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;


namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.AllDishes = _context.Dishes.OrderByDescending(d => d.UpdatedAt).ToList();
            return View();
        }


        [HttpGet("/new")]
        public IActionResult newDish()
        {
            return View("newDish");
        }


        [HttpPost("/newPost")]
        public IActionResult AddDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
            _context.Add(newDish);
            _context.SaveChanges();
            return Redirect("/");
            }else
            {
                return View("newDish");
            }
        }


        [HttpGet("/{id}")]
        public IActionResult ViewDish(int id)
        {
            ViewBag.OneDish = _context.Dishes.FirstOrDefault(d => id == d.DishId);
            return View("dish");
        }

        [HttpGet("/edit/{id}")]
        public IActionResult editDish(int id)
        {
            Dish OneDish = _context.Dishes.FirstOrDefault(d => id == d.DishId);
            return View("editDish", OneDish);
        }

        [HttpPost("/post/{id}")]
        public IActionResult postEditDish(int id, Dish updatedDish)
        {
            if(ModelState.IsValid)
            {
                Dish oldDish = _context.Dishes.FirstOrDefault(d => id == d.DishId);
                oldDish.ChefName = updatedDish.ChefName;
                oldDish.DishName = updatedDish.DishName;
                oldDish.Calories = updatedDish.Calories;
                oldDish.Tastiness = updatedDish.Tastiness;
                oldDish.Description = updatedDish.Description;
                oldDish.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            return RedirectToAction("Index");
            }else 
            {
                return View("editDish", updatedDish);
            }
        }

        [HttpGet("/delete/{id}")]
        public IActionResult deleteDish(int id)
        {
            Dish removeDish = _context.Dishes.SingleOrDefault(d => id == d.DishId);
            _context.Dishes.Remove(removeDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
