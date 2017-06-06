using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRwebsite.App_Code
{
    public class Entry
    {
        public Guid MessageId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string ChatId { get; set; }
        public bool IsApproved { get; set; }
    }

    public class Database
    {
        public Database()
        {
            this.Entries = new List<Entry>();
        }

        public List<Entry> Entries { get; set; }
    }
}