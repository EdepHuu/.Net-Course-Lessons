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

    public IActionResult Details(int? id)
    {
        
        if(id==null)
        {
            return RedirectToAction("List","Course");
        }

        var kurs = Repository.GetByID(id);

        return View(kurs);
    }

    public IActionResult List()
    {
        return View("CourseList", Repository.courses);
    }

}