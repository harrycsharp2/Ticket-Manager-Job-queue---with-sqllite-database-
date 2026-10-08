using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Jobqueue_TicketSystem.scripts.Data;
using Jobqueue_TicketSystem.scripts.ManagerScripts;
using Microsoft.EntityFrameworkCore;

namespace Jobqueue_TicketSystem.scripts
{

    public class HandleLogin
    {
        
        public static void HandleLoginMenu()
        {
            using var db = new AppDbContext();
            Console.Clear();
            Console.WriteLine("1. Manager Login");
            Console.WriteLine("2. Worker Login");
            string input = Console.ReadLine();
            if (input == "1")
            {
                ManagerLogin();
            }
            else if (input == "2")
            {
                WorkerLogin();
            }
            else
            {
                Console.WriteLine("Invalid Responce");
                Console.ReadLine();
                HandleLoginMenu();
            }
        }
        public static void ManagerLogin()
        {
            using var db = new AppDbContext();
            var managerlogin = db.manager.ToList();
            Console.Clear();
            Console.WriteLine("Name: ");
            string Iname = Console.ReadLine();

            Console.WriteLine("Password: ");
            string Ipassword = Console.ReadLine();

            foreach (var man in managerlogin)
            {
                if (Iname == man.name && Ipassword == man.password)
                {
                    HandleManagerMenu.haslogedi = true;
                    HandleManagerMenu.HandleManagerMainMenu();
                    return;
                }
            }
            return;
        }
        public static void WorkerLogin()
        {
            using var db = new AppDbContext();
            Console.Clear();
            Console.WriteLine("Name:");
            string Iname = Console.ReadLine();

            Console.WriteLine("Password:");
            
            string Ipassword = Console.ReadLine();
            var workerlist = db.worker.ToList();
            foreach (var worker in workerlist)
            {
                if (worker.name == Iname && worker.password == Ipassword)
                {
                    HandleWorkerMenu.currerntusername = Iname;
                    HandleWorkerMenu.HandleWorkerMenuM();
                    return;
                }
            }
            return;
        }
    }
}
