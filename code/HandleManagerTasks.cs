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
            Console.WriteLine("5. return");
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
            Console.WriteLine("Task priority /low/medium/high");
            task.prioritiy = Console.ReadLine();
            Console.WriteLine("Task deadline");
            task.deadline = Console.ReadLine();
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
            foreach (var task in tasklist)
            {

                Console.WriteLine($"ID: #{task.Id}");
                Console.WriteLine($"");
                Console.WriteLine($"Name: {task.name}  :::  Task type: {task.jobtype}");
                Console.WriteLine($"");
                Console.WriteLine($"Worker Amount: {task.Iworkersamount}  ::: Deadline: {task.deadline}");
                Console.WriteLine($"");
                Console.WriteLine($"Priority: {task.prioritiy}  :::  Discipition: {task.discription}");
                foreach (var name in task.Sworkersname)
                {
                    Console.WriteLine($"Worker: {name}");
                }
            }
            Console.ReadLine();
            HandleMtasksmenu();
            return;
        }
    }
}
