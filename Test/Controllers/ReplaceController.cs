using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Test.Views.Replace
{

    public class ReplaceController : Controller
    {

        public bool InUse = false;
        [System.Runtime.InteropServices.ComVisible(true)]

        [HttpPost]
        public IActionResult Display(string oldS, string newS, IFormFile FileUpload)
        {
            try
            {
                FileStream document = new FileStream(@"Uploads\test.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                StreamReader old = new StreamReader(document);

                string OriginalText = old.ReadToEnd();

                old.Close();
                document.Close();

                if (!OriginalText.Contains(oldS))
                {
                    ViewBag.message = "Word '" + oldS + "' is not in text file";

                    return View();
                }
                else
                {
                    string NewText = Regex.Replace(OriginalText, oldS, newS, RegexOptions.IgnoreCase);
                    FileStream document2 = new FileStream(@"Uploads\test.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    StreamWriter write = new StreamWriter(document2);

                    write.WriteLine(NewText);
                    write.Close();

                    Word temp = new Word
                    {
                        OldString = oldS,
                        NewString = newS
                    };

                    ViewBag.Words = temp;
                    ViewBag.Old = OriginalText;
                    ViewBag.New = NewText;
                    
                    document2.Close();

                    return View();
                }
            }
            catch
            {
                ViewBag.message = "Currently serving other user, try again";
                return View();
            }

            //if (FileUpload != null)
            //{

            //    StreamReader FileRead = new StreamReader(FileUpload.OpenReadStream());
            //    OriginalText = FileRead.ReadToEnd();
            //    FileRead.Close();

            //    string NewText = Regex.Replace(OriginalText, temp.OldString, temp.NewString, RegexOptions.IgnoreCase);

            //    StreamWriter WriteFile = new StreamWriter(@"C:\Users\Phillip - Dekstop\Desktop\BSc. Fisika en Rekenaarwetenskap (N153P)\316Prak\Test\Test\\Uploads\uploaded.txt");
            //    WriteFile.WriteLine(NewText);
            //    WriteFile.Close();

            //    ViewBag.Old = OriginalText;
            //    ViewBag.New = NewText;

            //    InUse = false;

            //}
            //else
            //{
        
        }

        public IActionResult ViewFile()
        {
            FileStream document = new FileStream(@"Uploads\test.txt", FileMode.Open, FileAccess.ReadWrite);

            StreamReader read = new StreamReader(document);

            string text = read.ReadToEnd();

            read.Close();

            document.Close();

            ViewBag.text = text;

            return View();

        }
        
    }
    
}