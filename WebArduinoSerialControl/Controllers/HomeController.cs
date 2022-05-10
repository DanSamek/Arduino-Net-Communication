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
        string logsPath = System.IO.Path.GetFullPath(".") + @"\logs.txt";

        public string recentlyUsed;
        public IActionResult Index()
        {
            string[] ports = SerialPort.GetPortNames();
            string path = System.IO.Path.GetFullPath(".") + @"\options.csv";
            if (!System.IO.File.Exists(path)) System.IO.File.Create(path);
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
            string endString = "";
            List<string> data = new List<string>();

            for (int index = 1; index < separatedInput.Length; index++) {
                if (separatedInput.Length - 1 == index)
                    endString += separatedInput[index];
                else endString += separatedInput[index] +";";
            }
            string response = getResponse(endString, separatedInput);
            data.Add(DateTime.Now.ToString() + ";" + content.execute + ";response-status: " + response);
            System.IO.File.AppendAllLines(logsPath, data);
            return Json(response);
        }

        public string getResponse(string endString, string[] separatedInput)
        {
            SerialPort sp = new SerialPort();
            try
            {
                sp.PortName = separatedInput[0];
                sp.BaudRate = 9600;
                sp.Open();
                Debug.WriteLine(endString);
                sp.WriteLine(endString);
                while(sp.BytesToRead > 0 ) {
                    System.Threading.Thread.Sleep(1);
                }
                var res = sp.ReadLine();
                Debug.WriteLine(res);
                sp.Close();
                return res;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return "err";
        }

        [HttpPost]
        public IActionResult getString([FromBody] Data ćontent) {
            ćontent.execute = ćontent.execute.Remove(ćontent.execute.Length - 1);
            string[] separatedInput = ćontent.execute.Split(";");
            string endString = "";
            List<string> data = new List<string>();

            for (int index = 1; index < separatedInput.Length; index++)
            {
                if (separatedInput.Length - 1 == index)
                    endString += separatedInput[index];
                else endString += separatedInput[index] + ";";
            }
            string response = getResponse(endString, separatedInput);
            return Json(response);
        }


        public IActionResult Logs() {

            List<string> logs = new List<string>();
            string[] data = System.IO.File.ReadAllLines(logsPath);
            foreach (var dataitem in data)
                logs.Add(dataitem);

           
            return View(logs);
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
