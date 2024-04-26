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
        kurs.Image = "1.JPG";
        return View(kurs);
    }

    public IActionResult List()
    {
        var kurslar = new List<Course>()
        {
            new Course() {ID = 1 ,Title = "Metehan", Image="1.JPG"},
            new Course() {ID = 2 ,Title = "Betüş", Image="2.JPG"},
            new Course() {ID = 3 ,Title = "Askim", Image="3.JPG"}
        };
        return View("CourseList", kurslar);
    }
}