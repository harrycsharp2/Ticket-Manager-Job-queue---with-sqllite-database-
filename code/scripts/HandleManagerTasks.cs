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
    public class HandleManagerTasks
    {
        public static void HandleMtasksmenu()
        {
            using var db = new AppDbContext();
            Console.Clear();
            Console.WriteLine("1. Create task");
            Console.WriteLine("2. Remove task");
            Console.WriteLine("3. View task");
            Console.WriteLine("4. View all tasks");
            Console.WriteLine("5. View/Delete done tasks");
            Console.WriteLine("6. return");
            string input = Console.ReadLine();

            if (input == "1")
            {
                CreateTask();
            }
            else if (input == "2")
            {
                RemoveTask();
            }
            else if (input == "3")
            {
                ViewTask();
            }
            else if (input == "4")
            {
                ViewAllTasks();
            }
            else if (input == "5")
            {
                ViewAllDoneTasks();
            }
            else if (input == "6")
            {
                HandleManagerMenu.HandleManagerMainMenu();
                return;
            }
            else
            {
                HandleMtasksmenu();
            }
        }
        public static void CreateTask()
        {
            using var db = new AppDbContext();
            Console.Clear();
            Job task = new Job();
            Console.WriteLine("Task name");
            task.name = Console.ReadLine();
            Console.WriteLine("Task type");
            task.jobtype = Console.ReadLine();
            Console.WriteLine("Task discription");
            task.discription = Console.ReadLine();
            Console.WriteLine("Task priority /low/med/high   - MUST BE TYPED LIKE ONE OF THESE OR ORDER WILL BE WRONG");
            task.prioritiy = Console.ReadLine();
            Console.WriteLine("Task deadline");
            task.deadline = Console.ReadLine();
            task.isdone = false;
            task.donenote = "";

            Console.WriteLine("worker limit /1/2/3/4/5...");
            string Sworkerlimit = Console.ReadLine();

            task.Iworkersamount = 0;
           
            if (int.TryParse(Sworkerlimit, out int i)) { task.Workerlimit = i; }

            db.job.Add(task);
            db.SaveChanges();
            Console.WriteLine("Created task!");
            Console.ReadLine();
            HandleMtasksmenu();
            return;
            
        }
        public static void RemoveTask()
        {
            using var db = new AppDbContext();
            Console.Clear();
            Console.WriteLine("Task name");
            string taskname = Console.ReadLine();
            var tasklist = db.job.ToList();
            foreach (var task in tasklist)
            {
                if (task.name == taskname)
                {
                    db.job.Remove(task);
                    db.SaveChanges();
                    HandleMtasksmenu();
                    return;
                }
            }
        }
        public static void ViewTask()
        {
            using var db = new AppDbContext();

            Console.Clear();
            Console.WriteLine("Task name");
            string taskname = Console.ReadLine();
            var tasklist = db.job.ToList();

            foreach (var task in tasklist)
            {
                if (task.name == taskname)
                {
                    Console.WriteLine($"ID: #{task.Id}");
                    Console.WriteLine($"");
                    Console.WriteLine($"Name: {task.name}  :::  Task type: {task.jobtype}");
                    Console.WriteLine($"");
                    Console.WriteLine($"Worker Amount: {task.Iworkersamount}  ::: Deadline: {task.deadline}");
                    Console.WriteLine($"");
                    Console.WriteLine($"Priority: {task.prioritiy}  :::  Discipition: {task.discription}");
                    Console.WriteLine($"Is Done: {task.isdone}");
                    foreach (var name in task.Sworkersname)
                    {
                        Console.WriteLine($"Worker: {name}");
                    }
                    Console.ReadLine();
                    HandleMtasksmenu();
                    return;

                }
            }

        }
        public static void ViewAllTasks()
        {
            using var db = new AppDbContext();

            var tasklist = db.job.ToList();
            var sortedtaskslist = tasklist.OrderByDescending(task =>
            task.prioritiy == "high" ? 3 :
            task.prioritiy == "med" ? 2 :
            1);
            foreach (var task in sortedtaskslist)
            {
                Console.WriteLine("");
                Console.WriteLine($"ID: #{task.Id}");
                Console.WriteLine($"");
                Console.WriteLine($"Name: {task.name}  :::  Task type: {task.jobtype}");
                Console.WriteLine($"");
                Console.WriteLine($"Worker Amount: {task.Iworkersamount}  ::: Deadline: {task.deadline}");
                Console.WriteLine($"");
                Console.WriteLine($"Priority: {task.prioritiy}  :::  Discipition: {task.discription}");
                Console.WriteLine($"Is Done: {task.isdone}");
                foreach (var name in task.Sworkersname)
                {
                    Console.WriteLine($"Worker: {name}");
                }
            }
            Console.ReadLine();
            HandleMtasksmenu();
            return;
        }
        public static void ViewAllDoneTasks()
        {
            using var db = new AppDbContext();

            var tasklist = db.job.ToList();
            foreach (var task in tasklist)
            {
                if (task.isdone == true)
                {
                    Console.WriteLine($"ID: #{task.Id}");
                    Console.WriteLine($"");
                    Console.WriteLine($"Name: {task.name}  :::  Task type: {task.jobtype}");
                    Console.WriteLine($"");
                    Console.WriteLine($"Worker Amount: {task.Iworkersamount}  ::: Deadline: {task.deadline}");
                    Console.WriteLine($"");
                    Console.WriteLine($"Priority: {task.prioritiy}  :::  Discipition: {task.discription}");
                    Console.WriteLine($"Is Done: {task.isdone}  ::: Task Note: {task.donenote}");
                    foreach (var name in task.Sworkersname)
                    {
                        Console.WriteLine($"Worker: {name}");
                    }
                    Console.WriteLine("type `1` to delete a done task --- any other key to return");
                    string I = Console.ReadLine();
                    if (I == "1")
                    {
                        Console.WriteLine("Enter task ID (just the number)");
                        string id = Console.ReadLine();
                        if (int.TryParse(id, out int Iid)) ;
                        foreach (var Dtask in tasklist)
                        {
                            if (Dtask.Id == Iid)
                            {
                                db.job.Remove(Dtask);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        HandleMtasksmenu();
                        return;
                    }
                }
            }
            Console.ReadLine();
            HandleMtasksmenu();
            return;
        }
    }
}
