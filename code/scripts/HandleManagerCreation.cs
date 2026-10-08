using System;
using System.Collections.Generic;
using System.Text;
using Jobqueue_TicketSystem.scripts.Data;
using Jobqueue_TicketSystem.scripts.Models;

namespace Jobqueue_TicketSystem.scripts
{
    public class HandleManagerCreation
    {
        public static void HandleMCMenu()
        {
            using var db = new AppDbContext();

            var managerlist = db.manager;
            if (!managerlist.Any())
            {
                var manager = new Manager();

                Console.WriteLine("Manager Name: ");
                manager.name = Console.ReadLine();

                Console.WriteLine("Managers Password: ");
                manager.password = Console.ReadLine();

                db.manager.Add(manager);
                db.SaveChanges();
                var managerlist2 = db.manager.ToList();
                foreach (var man in managerlist2)
                {
                    Console.WriteLine($"Name: {man.name}");
                    Console.WriteLine($"Password: {man.password}");
                }
                Console.ReadLine();
                Program.Main();
                return;

            }
            else
            {
                
                Console.WriteLine("There is alreaddy a manager in the database");
                Console.ReadLine();
                Program.Main();
                return;
            }
        }
    }
}
