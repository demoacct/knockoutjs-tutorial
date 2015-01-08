using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace SearchCollection.Models
{
    public class SRUser
    {
        public ObjectId Id { get; set; }
        public string IdStr { get { return this.Id.ToString(); } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateCreatedFormatted { get { return this.DateCreated.ToString("MMMM dd, yyyy hh:mm:ss tt"); } }
        public UserRole Role { get; set; }
        public DateTime LastLogin { get; set; }
        public ActiveStatus Status { get; set; }
        public string StatusStr { get { return this.Status.ToString(); } }
    }
}