using System;
using System.Collections.Generic;
using System.Text;

namespace Jobqueue_TicketSystem.scripts.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string password { get; set; }

        public List<Message> inbox { get; set; } = new List<Message>();
    }
}
