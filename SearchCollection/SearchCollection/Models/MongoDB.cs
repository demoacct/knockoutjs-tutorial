using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchCollection.Models
{
    public class Mongow
    {
        public Mongow()
        {
            var connectionString = "mongodb://127.0.0.1";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("topic_mgr_db");

            this.TopicFinderDatabase = database;
        }

        public MongoDatabase TopicFinderDatabase { get; set; }
    }
}