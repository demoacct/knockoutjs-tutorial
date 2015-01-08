using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchCollection.Models
{
    public class Topic
    {
        public MongoDB.Bson.ObjectId Id { get; set; }
        public string IdStr { get { return Id.ToString(); } }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateCreatedFormatted { get { return this.DateCreated.ToString("MMMM dd, yyyy hh:mm:ss tt"); } }
        public bool Enable { get; set; }
        public string Link { get; set; }
    }
}