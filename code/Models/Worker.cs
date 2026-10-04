using System;
using System.Collections.Generic;
using System.Text;

namespace Jobqueue_TicketSystem.scripts.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public bool hasjob { get; set; }
        public string currentjob { get; set; }
    }
}
