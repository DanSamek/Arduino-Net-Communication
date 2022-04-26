using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO.Ports;
using System.IO;
using WebArduinoSerialControl.Models;
using System;
using System.Collections.Generic;

namespace WebArduinoSerialControl.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            string[] ports = SerialPort.GetPortNames();
            string path = System.IO.Path.GetFullPath(".") + @"\options.csv";
            if(!System.IO.File.Exists(path)) System.IO.File.Create(path);
            string[] csvData = System.IO.File.ReadAllLines(path);
            List<string> inputs = new List<string>();
            foreach (string option in csvData) {
                inputs.Add(option);
            }

            ViewData["ports"] = ports;
            return View(inputs);
        }

        [HttpPost]
        public IActionResult Execute([FromBody] Data content)
        {
            content.execute = content.execute.Remove(content.execute.Length - 1);
            string[] separatedInput = content.execute.Split(";");
            var response = "";
            string endString = "";
            for (int index = 0; index < separatedInput.Length; index++) {
                if (separatedInput.Length - 1 == index)
                    endString += separatedInput[index];
                else endString += separatedInput[index] +";";
            }
            SerialPort sp = new SerialPort();
            Debug.WriteLine(endString);
            try {
                sp.PortName = separatedInput[0];
                sp.BaudRate = 9600;
                sp.Open();
                sp.WriteLine(endString);
                sp.Close();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            System.Threading.Thread.Sleep(2500);
            try
            {
                sp.PortName = separatedInput[0];
                sp.BaudRate = 9600;
                sp.Open();                
                response = sp.ReadLine();
                sp.Close();
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
            response = "";
            return Json(response);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public class Data
        {
            public string execute { get; set; }

        }
    }
}
