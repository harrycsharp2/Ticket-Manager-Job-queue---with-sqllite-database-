using System;
using System.Collections.Generic;
using System.Text;

namespace Jobqueue_TicketSystem.scripts.Models
{
    public class Job
    {
        public int Id { get; set; }
        public int Workerlimit { get; set; }
        public int Iworkersamount { get; set; }
        public List<string> Sworkersname { get; set; } = new List<string>();
        public string jobtype { get; set; }
        public string deadline { get; set; }
        public string name { get; set; }
        public string prioritiy { get; set; }
        public string discription { get; set; }


        

    }
}
