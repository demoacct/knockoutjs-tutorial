using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchCollection.Models.Dao
{
    public class TopicDao
    {
        private MongoCollection<Topic> topicCollection;

        public TopicDao()
        {
            var mongoDB = new Mongow();
            var database = mongoDB.TopicFinderDatabase;

            this.topicCollection = database.GetCollection<Topic>("topics");
        }

        public bool Create(Topic model)
        {
            model.DateCreated = DateTime.Now;
            model.Enable = false;

            return this.topicCollection.Insert(model).Ok;
        }

        public List<Topic> Retrieve()
        {
            var all = this.topicCollection.FindAll();
            List<Topic> ret = new List<Topic>();

            foreach (var item in all)
            {
                ret.Add(item);
            }

            return ret;
        }

        public Topic Retrieve(string id)
        {
            ObjectId objId = new ObjectId(id);

            var query = Query<Topic>.EQ(e => e.Id, objId);
            var ret = this.topicCollection.FindOne(query);

            return ret;
        }

        public bool Update(Topic model)
        {
            var query = Query<Topic>.EQ(e => e.Id, model.Id);
            var update = Update<Topic>.Set(e => e.Title, model.Title)
                .Set(e => e.Description, model.Description)
                .Set(e => e.DateCreated, model.DateCreated);

            return this.topicCollection.Update(query, update).Ok;
        }

        public bool Delete(string id)
        {
            ObjectId objId = new ObjectId(id);

            var query = Query<Topic>.EQ(e => e.Id, objId);

            return this.topicCollection.Remove(query).Ok;
        }
    }
}