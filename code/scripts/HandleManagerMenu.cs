using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Jobqueue_TicketSystem.scripts.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;
using Jobqueue_TicketSystem.scripts.Models;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Jobqueue_TicketSystem.scripts.ManagerScripts
{
    public class HandleManagerMenu
    {
        public static bool haslogedi = false;
        public static void HandleManagerMainMenu()
        {
            if (!haslogedi)
            {
                Program.Main();
                return;
            }
            Console.Clear();
            Console.WriteLine("Manager Menu");
            Console.WriteLine("1.Manage Workers");
            Console.WriteLine("2.Manage Jobs");
            Console.WriteLine("3.Inbox");
            Console.WriteLine("4. return");
            string input = Console.ReadLine();
            
            if (input == "1")
            {
                HandleManageWorkers.HandleMWMenu();
                return;
            }
            else if (input == "2")
            {
                HandleManagerTasks.HandleMtasksmenu();
            }
            else if (input == "3")
            {
                HandleMInbox.HandleManagerInbox();
            }
            else if (input == "4")
            {
                Program.Main();
                return;
            }
            else
            {
                Console.WriteLine("invalid responce");
                Console.ReadLine();
                HandleManagerMainMenu();
            }
        }
    }
}
