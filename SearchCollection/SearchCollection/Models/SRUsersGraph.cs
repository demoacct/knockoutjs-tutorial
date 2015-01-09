using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchCollection.Models
{
    public class SRUsersGraph
    {
        public string id { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public DateTime date { get; set; }
        public List<SRUsersGraphDetail> data { get; set; }
    }

    public class SRUsersGraphDetail
    {
        public string category { get; set; }
        public int value { get; set; }
        public string color { get; set; }
    }
}