using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiveHappy.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace LiveHappy.Application.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ChatRoom()
        {
            return View();
        }

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Experiment(string experimentName)
        {
            if (string.IsNullOrWhiteSpace(experimentName))
            {
                var experimentNames = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Views/Experiments/", "*.cshtml", SearchOption.TopDirectoryOnly)
                                                    .Select(Path.GetFileNameWithoutExtension)
                                                    .ToList(); // https://www.c-sharpcorner.com/article/file-providers-in-asp-net-core/

                return View(experimentNames);
            }
            else
            {
                return View($"~/Views/Experiments/{experimentName}.cshtml");
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Blog(string articlePath)
        {
            return View($"~/Views/Experiments/{articlePath}.cshtml");
        }
    }
}
