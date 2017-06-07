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
        public List<Entry> Entries
        {
            get
            {
                if (HttpContext.Current.Application["db"] == null)
                {
                    HttpContext.Current.Application["db"] = new List<Entry>();
                }
                return (List<Entry>) HttpContext.Current.Application["db"];
            }
        }
    }
}