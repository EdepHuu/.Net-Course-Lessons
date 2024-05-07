using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using basics.Models;

/* 
*Localhost               => home/index
*Localhost/home          => home/index
*Localhost/home/index    => home/index
*/
namespace basics.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(Repository.courses);
    }

    public IActionResult Contact()
    {
        return View();
    }
}
