using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using Jobqueue_TicketSystem.scripts.Data;
using Jobqueue_TicketSystem.scripts.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Jobqueue_TicketSystem.scripts
{
    public class HandleWinbox
    {
        public static void HandleWorkerInboxMenu()
        {
            using var db = new AppDbContext();

            Console.Clear();
            Console.WriteLine("1. Send message");
            Console.WriteLine("2. View Inbox");
            Console.WriteLine("any other key to return");
            string input = Console.ReadLine();
            if (input == "1")
            {
                SendMessage();
            }
            else if (input == "2")
            {
                ViewInbox();
            }
            else
            {
                HandleWorkerMenu.HandleWorkerMenuM();
            }
        }
        public static void SendMessage()
        {
            Console.Clear();
            using var db = new AppDbContext();
            Message message = new Message();
            Console.WriteLine("Enter your username");
            message.sender = Console.ReadLine();

            Console.WriteLine("Enter the recievers username");
            string targetusername = Console.ReadLine();

            Console.WriteLine("Enter message");
            message.message = Console.ReadLine();

            var workerlist = db.worker.ToList();
            var managerlist = db.manager.ToList();
            foreach (var worker in workerlist)
            {
                if (worker.name == targetusername)
                {
                    worker.inbox.Add(message);
                    db.SaveChanges();
                    Console.WriteLine("message sent");
                    Console.ReadLine();
                    HandleWorkerInboxMenu();
                    return;
                }
            }
            foreach (var manager in managerlist)
            {
                if (manager.name == targetusername)
                {
                    manager.inbox.Add(message);
                    db.SaveChanges();
                    Console.WriteLine("message sent");
                    Console.ReadLine();
                    HandleWorkerInboxMenu();
                    return;
                }
            }
            Console.WriteLine("user not found");
            Console.ReadLine();
            HandleWorkerInboxMenu();
            return;
        }
        public static void ViewInbox()
        {
            Console.Clear();
            using var db = new AppDbContext();
            Console.WriteLine("enter username");
            string username = Console.ReadLine();
            Console.Clear();
            var workerlist = db.worker
             .Include(w => w.inbox)
             .ToList();
            foreach (var worker in workerlist)
            {
                if (worker.name == username)
                {
                    Console.WriteLine($"Messages: {worker.inbox.Count}");

                    int id = 1;
                    foreach (var message in worker.inbox)
                    {
                        message.Id = id;
                        Console.WriteLine($"ID: {message.Id}");
                        Console.WriteLine($"Sender: {message.sender}");
                        id++;
                    }
                    Console.WriteLine("1. Open Message");
                    Console.WriteLine("2. Delete Message");
                    Console.WriteLine("any other key to return");
                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        Console.WriteLine("enter message id");
                        string Sid = Console.ReadLine();
                        if (int.TryParse(Sid, out int Iid))
                        {
                            foreach (var message in worker.inbox)
                            {
                                if (message.Id == Iid)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Sender: {message.sender}");
                                    Console.WriteLine($"Message: {message.message}");
                                    Console.ReadLine();
                                    HandleWorkerInboxMenu();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("enter a number - invalid id format");
                            Console.ReadLine();
                            HandleWorkerInboxMenu();
                            return;
                        }
                    }
                    else if (input == "2")
                    {
                        Console.WriteLine("enter message id");
                        string Sid = Console.ReadLine();
                        if (int.TryParse(Sid, out int Iid))
                        {
                            foreach (var message in worker.inbox)
                            {
                                if (message.Id == Iid)
                                {
                                    worker.inbox.Remove(message);
                                    db.SaveChanges();
                                    HandleWorkerInboxMenu();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("enter a number - invalid id format");
                            Console.ReadLine();
                            HandleWorkerInboxMenu();
                            return;
                        }
                    }
                    else
                    {
                        HandleWorkerInboxMenu();
                        return;
                    }
                }
            }
            Console.WriteLine("couldnt find username");
            Console.ReadLine();
            HandleWorkerInboxMenu();
            return;
        }
    }
}
