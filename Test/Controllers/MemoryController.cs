using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using Test.Models;
using System.IO;
using System.Globalization;

namespace Test.Controllers
{
    public class MemoryController : Controller
    {
        [HttpPost]
        public IActionResult Calculate(string osRAM, decimal frames)
        {
            
            decimal memOS = Convert.ToDecimal(osRAM, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            long memKb;
            GetPhysicallyInstalledSystemMemory(out memKb);

            decimal memMb = Convert.ToDecimal(memKb);

            memMb = (memMb / 1024 / 1024);
            
            Memory server = new Memory(memMb, memOS, frames);
            return View(server);
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
        
    }
}