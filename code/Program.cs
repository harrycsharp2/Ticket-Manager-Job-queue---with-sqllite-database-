using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Jobqueue_TicketSystem.scripts.Data;
namespace Jobqueue_TicketSystem.scripts
{
    public class Program()
    {
        public static void Main()
        {
            Console.WriteLine("start");
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
            Console.WriteLine("Loaded Database!");

            Console.Clear();
            Console.WriteLine("1. Create Manager account - only one - used only if it is first time setting up software");
            Console.WriteLine("2. Login");
            string input = Console.ReadLine();
            if (input == "1")
            {
                HandleManagerCreation.HandleMCMenu();
            }
            else if (input == "2")
            {
                HandleLogin.HandleLoginMenu();
            }
            else
            {
                Console.WriteLine("Invalid responce");
                Console.ReadLine();
                Main();
            }
        }
    }
}

