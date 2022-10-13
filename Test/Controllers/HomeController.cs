using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Test.Models;
using System.IO;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult About()
        {
            ViewData["Message"] = "Website for ITRW316 Practical Exams";

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Replace()
        {
            return View();
        }

        public IActionResult Queue()
        {
            
            return View();
        }

        public IActionResult Virtual()
        {
            long memKb;
            GetPhysicallyInstalledSystemMemory(out memKb);

            ViewBag.Total = (memKb / 1024 / 1024);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

    }
}
