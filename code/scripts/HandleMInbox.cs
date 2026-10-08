using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Jobqueue_TicketSystem.scripts.Data;
using Jobqueue_TicketSystem.scripts.ManagerScripts;
using Jobqueue_TicketSystem.scripts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Jobqueue_TicketSystem.scripts
{
    public class HandleMInbox
    {
        public static void HandleManagerInbox()
        {
            Console.Clear();
            Console.WriteLine("1. Send Message");
            Console.WriteLine("2. View Inbox");
            Console.WriteLine("3. return");
            string input = Console.ReadLine();

            if (input == "1")
            {
                SendMessage();
            }
            else if (input == "2")
            {
                Openinbox();
            }
            else if (input == "3")
            {
                HandleManagerMenu.HandleManagerMainMenu();
                return;
            }
            else
            {
                Console.WriteLine("invalid input 1/2/3");
                Console.ReadLine();
                HandleManagerInbox();
            }
        }
        public static void SendMessage()
        {
            using var db = new AppDbContext();
            Message message = new Message();
            Console.WriteLine("Enter your name");
            message.sender = Console.ReadLine();

            Console.WriteLine("Enter the recievers username");
            string targetusername = Console.ReadLine();

            Console.WriteLine("Message: ");
            message.message = Console.ReadLine();

            var workerlist = db.worker
    .Include(w => w.inbox)
    .ToList();
            foreach (var worker in workerlist)
            {
                if (worker.name == targetusername)
                {
                    worker.inbox.Add(message);
                    db.SaveChanges();

                    Console.WriteLine("sent message!");
                    Console.ReadLine();
                    HandleManagerInbox();
                    return;
                }
            }
            Console.WriteLine("user not found");
            Console.ReadLine();
            HandleManagerInbox();
            return;
        }
        public static void Openinbox()
        {
            using var db = new AppDbContext();


            Console.WriteLine("enter username");
            string name = Console.ReadLine();
            Console.Clear();
            if (name != null)
            {
                var managerlist = db.manager
             .Include(m => m.inbox)
             .ToList();
                foreach (var man in managerlist)
                {
                    if (man.name == name)
                    {
                        Console.Clear();
                        Console.WriteLine("User found");
                        int id = 1;
                        foreach (var message in man.inbox)
                        {
                            message.Id = id;
                            Console.WriteLine("");
                            Console.WriteLine($"ID: {message.Id}");
                            Console.WriteLine($"From: {message.sender}");

                            id++;
                        }
                        Console.WriteLine("1. Open Message");
                        Console.WriteLine("2. Delete Message");
                        Console.WriteLine("any other key to return");
                        string input = Console.ReadLine();
                        if (input == "1")
                        {
                            Console.WriteLine("enter message Id");
                            string Sid = Console.ReadLine();
                            if (int.TryParse(Sid, out int Iid))
                            {
                                foreach (var message in man.inbox)
                                {
                                    if (message.Id == Iid)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("");
                                        Console.WriteLine($"From: {message.sender}");
                                        Console.WriteLine("");
                                        Console.WriteLine($"Message: {message.message}");
                                        Console.ReadLine();
                                        HandleManagerInbox();
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Enter a number - failed to parse OR failed to find right ID");
                                Console.ReadLine();
                                HandleManagerInbox();
                                return;
                            }
                        }
                        else if (input == "2")
                        {
                            Console.WriteLine("message ID");
                            string Sid = Console.ReadLine();
                            if (int.TryParse(Sid, out int Iid))
                            {
                                foreach (var message in man.inbox)
                                {
                                    if (message.Id == Iid)
                                    {
                                        man.inbox.Remove(message);
                                        db.SaveChanges();
                                        Console.WriteLine("message removed");
                                        Console.ReadLine();
                                        HandleManagerInbox();
                                    }
                                }
                            }
                        }
                        else
                        {
                            HandleManagerInbox();
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("user not found - trying agian please wait");
                    }
                }
            }
        }
    }
}
