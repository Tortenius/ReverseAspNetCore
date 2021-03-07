using Microsoft.AspNetCore.Mvc;
using ReverseAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReverseAspNetCore.Controllers
{
    public class BinaryCalcController : Controller
    {
        private Calculator calculator;

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult postBinaryCalc()
        {
            if (HttpContext.Request.Form["selectCalcType"] == "isDec")
            {
                calculator = new Calculator(HttpContext.Request.Form["inputI"], "decimal");
            }
            else if (HttpContext.Request.Form["selectCalcType"] == "isOct")
            {
                calculator = new Calculator(HttpContext.Request.Form["inputI"], "octa");             
            }
            else if (HttpContext.Request.Form["selectCalcType"] == "isBin")
            {
                calculator = new Calculator(HttpContext.Request.Form["inputI"], "binary");
            }
            else if (HttpContext.Request.Form["selectCalcType"] == "isHex")
            {
                calculator = new Calculator(HttpContext.Request.Form["inputI"], "hex");
            }
            else
            {
                //this.output = HttpContext.Request.Form["inputI"];
            }

            ViewData["outputOct"] = this.calculator.oct;
            ViewData["outputBin"] = this.calculator.bin;
            ViewData["outputDec"] = this.calculator.dec;
            ViewData["outputHex"] = this.calculator.hex;

            return View("Index");
        }
    }
}
