using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReverseAspNetCore.Controllers
{
    public class ReverseOutputController : Controller
    {
        private string input;
        private string path = @Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ReverseTest.txt";
        private string output;

        public IActionResult ReverseOutput()
        {
            ViewData["output"] = this.output;

            return View();
        }

        [HttpPost]
        public IActionResult postReverseOutput()
        {
            this.input = HttpContext.Request.Form["inputI"];

            if (HttpContext.Request.Form["isReverse"] == "on")
            {
                this.output = this.Reverse(HttpContext.Request.Form["inputI"]);
            }
            else
            {
                this.output = HttpContext.Request.Form["inputI"];
            }
            //this.save(this.output);

            ViewData["output"] = this.output;

            return View("ReverseOutput");
        }

        private string Reverse(string reverse)
        {      
            char[] cArray = reverse.ToCharArray();
            string result = String.Empty;

            for (int i = cArray.Length - 1; i > -1; i--)
            {
                result += cArray[i];
            };

            return result;
        }
    }
}
