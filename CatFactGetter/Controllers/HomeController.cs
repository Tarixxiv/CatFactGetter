using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CatFactGetter.Models;
using CatFactGetter.Services;

namespace CatFactGetter.Controllers;

public class HomeController(IApiService apiService, IFileService fileService) : Controller
{
    
    public IActionResult Index(string message = "", string path="")
    {
        var model = new IndexViewModel()
        {
            Message = message,
            Path = path
        };
        return View(model);
    }

    public IActionResult GetFact(string path)
    {
        var message = "Fact saved";
        try
        {
            var fact = apiService.GetFact().Result;
            fileService.AddFactLine(path,fact);
        }
        catch (Exception e)
        {
            message = e.Message;
        }
        return RedirectToAction("Index", new { message, path });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}