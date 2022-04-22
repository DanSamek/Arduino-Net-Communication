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
            Debug.WriteLine(content.execute);
            return Redirect("/");
        }

        public class Data {
            public string execute { get; set; }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*
           string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
                Console.WriteLine(i+"->"+ports[i]);

            Console.WriteLine("select port [only number]: ");
            var indexOfPort = 0;
            try { Int32.TryParse(Console.ReadLine(), out indexOfPort); }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Selected port: " + ports[indexOfPort]);
            SerialPort sp = new SerialPort(ports[indexOfPort]);
            try {
                sp.PortName = ports[indexOfPort];
                sp.BaudRate = 9600;
                sp.Open();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            bool err = false;
            do
            {
                Console.WriteLine("Read or send (0,1): ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":{ 
                        Console.WriteLine("Send data:");
                        sp.WriteLine(Console.ReadLine());
                        break;
                    }
                    case "0": {
                        Console.WriteLine("Reading data..");
                        Console.WriteLine(sp.ReadLine());
                        break;
                    }
                    default:
                        err = true;
                        break;
                }
            } while (!err);
        }
         */
    }
}
