using Microsoft.Owin.Hosting;
using System;
using System.ComponentModel.Design;
using System.Configuration;
using System.Net.Http;

namespace OwinWebApi
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = ConfigurationManager.AppSettings["backendhost"];
            Console.WriteLine("Solarboat WebApi");
            Console.WriteLine("----------------");
            Console.WriteLine("");
            Console.WriteLine($"Start listening on {baseAddress}");
            Console.WriteLine("EndPoints:");
            //Console.WriteLine($"{baseAddress}/api/values : Get all values from the Data table");


            using (WebApp.Start<Startup>(url: baseAddress))
            {
                ConsoleKeyInfo closeAnswer;
                do
                {
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("Do you want to close the application? Press 'y' if yes.");
                    closeAnswer = Console.ReadKey();
                    Console.WriteLine("");
                } while (closeAnswer.Key.ToString().ToLower()!="y");
                
            }
        }
    }
}