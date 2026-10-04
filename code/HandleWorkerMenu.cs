using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Jobqueue_TicketSystem.scripts.Data;
using Jobqueue_TicketSystem.scripts.Models;
using Microsoft.EntityFrameworkCore;

namespace Jobqueue_TicketSystem.scripts
{
    public class HandleWorkerMenu
    {
        public static string currerntusername = "";
        public static void HandleWorkerMenuM()
        {
            Console.Clear();
            Console.WriteLine("1. view all tasks");
            Console.WriteLine("2. select/replace task");
            Console.WriteLine("3. view/manage current task");
            Console.WriteLine("4. return");
            string input = Console.ReadLine();
            if (input == "1")
            {
                ViewAllTasks();
            }
            else if (input == "2")
            {
                ChooseTask();
            }
            else if (input == "3")
            {
                ManageCurrentTask();
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
                HandleWorkerMenuM();
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
            HandleWorkerMenuM();
            return;
        }
        public static void ChooseTask()
        {
            using var db = new AppDbContext();
            Console.WriteLine("Task Name");
            string Itaskname = Console.ReadLine();
            var workerlist = db.worker.ToList();
            
            var tasklist = db.job.ToList();
           
            
            foreach (var task in tasklist)
            {
                //found task 
                if (task.name == Itaskname)
                {
                    //find worker in database
                    foreach (var worker in workerlist)
                    {
                        if (currerntusername == worker.name)
                        {
                            worker.currentjob = task.name;
                            worker.hasjob = true;
                            task.Iworkersamount++;
                            task.Sworkersname.Add($" + {worker.name}");
                            db.SaveChanges();

                            Console.WriteLine("assigned task");
                            Console.ReadLine();
                            HandleWorkerMenuM();
                            return;
                        }
                    }
                }
            }

        }
        public static void ManageCurrentTask()
        {
            using var db = new AppDbContext();
            string currenttask = "";
            Console.Clear();
            //reminder -----------------
            //find current worker
            //then display current task
            //then give opitions to - finish task / leave task
            var workerlist = db.worker.ToList();
            foreach (var worker in workerlist)
            {
                if (worker.name == currerntusername)
                {
                    currenttask = worker.currentjob;
                }
            }
            var tasklist = db.job.ToList();
            foreach (var task in tasklist)
            {
                if (task.name == currenttask)
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
            }
            Console.WriteLine("1. leave task");
            Console.WriteLine("2. Finish task");
            Console.WriteLine("3. return");
            string input1 = Console.ReadLine();

            if (input1 == "1")
            {
                foreach (var task in tasklist)
                {
                    if (task.name == currenttask)
                    {
                        task.Iworkersamount--;
                        task.Sworkersname.Remove($" + {currerntusername}");
                        db.SaveChanges();

                    }
                }
                foreach (var worker in workerlist)
                {
                    if (worker.name == currerntusername)
                    {
                        worker.currentjob = "none";
                        worker.hasjob = false;
                        db.SaveChanges();
                    }
                }
                Console.WriteLine("DONE");
                Console.ReadLine();
                HandleWorkerMenuM();
                return;
            }
            else if (input1 == "2")
            {
                foreach (var task in tasklist)
                {
                    if (task.name == currenttask)
                    {
                        db.job.Remove(task);
                        db.SaveChanges();
                        Console.WriteLine("DONE");
                        Console.ReadLine();
                        HandleWorkerMenuM();
                        return;
                    }
                }

            }
            else if (input1 == "3")
            {
                HandleWorkerMenuM();
            }

        }
    }
}
