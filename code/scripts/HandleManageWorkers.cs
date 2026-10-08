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
using Jobqueue_TicketSystem.scripts.ManagerScripts;

namespace Jobqueue_TicketSystem.scripts
{
    public class HandleManageWorkers
    {
        public static void HandleMWMenu()
        {
            using var db = new AppDbContext();

            Console.Clear();
            Console.WriteLine("1. Add worker");
            Console.WriteLine("2. Remove Worker");
            Console.WriteLine("3. View all workers");
            Console.WriteLine("4. Search for worker");
            Console.WriteLine("5. Manage Worker");
            Console.WriteLine("6. return");
            string input = Console.ReadLine();

            if (input == "1")
            {
                Console.Clear();
                Console.WriteLine("Name: ");
                string Iname = Console.ReadLine();

                Console.WriteLine("Password: ");
                string Ipassword = Console.ReadLine();
                var worker = new Worker();
                worker.password = Ipassword;
                worker.name = Iname;
                worker.hasjob = false;
                worker.currentjob = "none";
                db.worker.Add(worker);
                db.SaveChanges();

                Console.Clear();
                Console.WriteLine("Worker Added");
                var workerlist = db.worker.ToList();
                foreach (var Dworker in workerlist)
                {
                    if (Iname == Dworker.name)
                    {
                        Console.WriteLine($"ID: {Dworker.Id}");
                        Console.WriteLine($"Name: {Dworker.name}");
                        Console.WriteLine($"Password: {Dworker.password}");
                        Console.WriteLine("---");
                        Console.WriteLine($"Has task: {Dworker.hasjob} ::: Current task: {Dworker.currentjob}");
                        Console.ReadLine();
                        HandleMWMenu();
                    }
                    
                }
            }
            else if (input == "2")
            {
                Console.Clear();
                Console.WriteLine("Name of worker you wish to remove // 0 to return");
                string input1 = Console.ReadLine();
                if (input1 == "0")
                {
                    HandleMWMenu();
                }
                var workerslist = db.worker.ToList();
                foreach (var worker in workerslist)
                {
                    if (worker.name == input1)
                    {
                        db.Remove(worker);
                        db.SaveChanges();
                        Console.WriteLine("worker removed");
                        Console.ReadLine();
                        HandleMWMenu();
                    }
                }
            }
            else if (input == "3")
            {
                Console.Clear();
                var workerlist = db.worker.ToList();
                foreach (var worker in workerlist)
                {
                    Console.WriteLine("------");
                    Console.WriteLine($"ID: {worker.Id}");
                    Console.WriteLine($"Name: {worker.name}");
                    Console.WriteLine($"Password: {worker.password}");
                    Console.WriteLine($"---");
                    Console.WriteLine($"Has task: {worker.hasjob}  :::  Current task: {worker.currentjob}");
                }
                Console.ReadLine();
                HandleMWMenu();
            }
            else if (input == "4")
            {
                Console.Clear();
                Console.WriteLine("Name of worker // 0 to return");
                string name = Console.ReadLine();
                if (name == "0")
                {
                    HandleMWMenu();
                }
                var workerlist = db.worker.ToList();
                foreach (var worker in workerlist)
                {
                    if (worker.name == name)
                    {
                        Console.Clear();
                        Console.WriteLine("------");
                        Console.WriteLine($"ID: {worker.Id}");
                        Console.WriteLine($"Name: {worker.name}");
                        Console.WriteLine($"Password: {worker.password}");
                        Console.WriteLine($"---");
                        Console.WriteLine($"Has task: {worker.hasjob}  :::  Current task: {worker.currentjob}");
                        Console.ReadLine();
                        HandleMWMenu();
                   
                    }

                }
            }
            else if (input == "5")
            {
                Console.Clear();
                Console.WriteLine("Enter name of worker // 0 to return");
                string name = Console.ReadLine();
                if (name == "0")
                {
                    HandleMWMenu();
                }
                var workerlist = db.worker.ToList();
                foreach (var worker in workerlist)
                {
                    if (worker.name == name)
                    {
                        Console.Clear();
                        Console.WriteLine("------");
                        Console.WriteLine($"ID: {worker.Id}");
                        Console.WriteLine($"Name: {worker.name}");
                        Console.WriteLine($"Password: {worker.password}");
                        Console.WriteLine($"---");
                        Console.WriteLine($"Has task: {worker.hasjob}  :::  Current task: {worker.currentjob}");
                        Console.WriteLine("------");
                        Console.WriteLine("1. assign/replace task");
                        Console.WriteLine("2. change password");
                        Console.WriteLine("3. return");
                        string opition = Console.ReadLine();

                        if (opition == "1")
                        {
                            
                            Console.Clear();
                            Console.WriteLine("Task name: ");
                            string taskname = Console.ReadLine();
                            var tasks = db.job.ToList();
                            foreach (var task in tasks)
                            {
                                if (task.name == taskname && task.isdone == true)
                                {
                                    Console.WriteLine("task is already done");
                                    Console.ReadLine();
                                    HandleMWMenu();
                                }
                                if (task.name == taskname)
                                {
                                    task.Iworkersamount++;
                                    task.Sworkersname.Add($" + {worker.name}");
                                    worker.currentjob = task.name;
                                    worker.hasjob = true;
                                    db.SaveChanges();
                                }
                            }
                            Console.WriteLine("worker task assigned");
                            Console.ReadLine();
                            HandleMWMenu();
                        }
                        else if (opition == "2")
                        {
                            Console.Clear();
                            Console.WriteLine("new password: ");
                            string password = Console.ReadLine();

                            worker.password = password;
                            db.SaveChanges();
                            Console.WriteLine("worker updated");
                            Console.ReadLine();
                            HandleMWMenu();
                        }
                        else if (opition == "3")
                        {
                            HandleMWMenu();

                        }
                        else
                        {
                            HandleMWMenu();
                        }
                    }
                }
            }
            else if (input == "6")
            {
                HandleManagerMenu.HandleManagerMainMenu();
                return;
            }
            else
            {
                Console.WriteLine("Invalid responce");
                Console.ReadLine();
                HandleMWMenu();
            }
        }
    }
}
