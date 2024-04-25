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
        var kurslar = new List<Course>()
        {
            new Course() {ID = 1 ,Title = "Metehan"},
            new Course() {ID = 2 ,Title = "Betüş"},
            new Course() {ID = 3 ,Title = "Tufan"}
        };
        return View("CourseList", kurslar);
    }
}