using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Question6Elevraad.Models;

namespace Question6Elevraad.Controllers;

public class HomeController : Controller{
    private readonly ILogger<HomeController> _logger;
    
    List<User> users = new List<User>();

    public HomeController(ILogger<HomeController> logger){
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Index(){
        return View();
    }

    [HttpPost]
    public IActionResult Index(string name, string studentClass, int age){
        var user = new User(name, studentClass, age);
        users.Add(user);

        foreach (var p in users){
            Console.WriteLine(p.Name);
        }

        return View();
    }
    
    public IActionResult Privacy(){
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}