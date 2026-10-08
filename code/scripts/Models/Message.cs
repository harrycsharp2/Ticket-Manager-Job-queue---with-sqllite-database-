using System;
using System.Collections.Generic;
using System.Text;

namespace Jobqueue_TicketSystem.scripts.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string sender { get; set; }
        public string message { get; set; }
    }
}
