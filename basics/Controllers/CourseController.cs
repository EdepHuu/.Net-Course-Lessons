using System.Net.Http.Headers;
using basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace basics.Controllers;

public class CourseController : Controller
{
    public IActionResult Index()
    {
        var kurs = new Course();
        
        kurs.ID = 1;
        kurs.Title = "METEHAN";
        return View(kurs);
    }

    public IActionResult List()
    {
        return View("CourseList");
    }
}