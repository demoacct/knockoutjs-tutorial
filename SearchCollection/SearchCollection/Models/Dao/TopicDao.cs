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
            SRUserDao userDao = new SRUserDao();

            foreach (var item in all)
            {
                SRUser user = userDao.Retrieve(item.CreatedBy.ToString());

                if (user != null)
                {
                    item.User = user.FirstName + " "+ user.LastName;
                }
                else
                {
                    item.User = "ADMIN";
                }

                ret.Add(item);
            }

            return ret;
        }

        public List<SRUsersGraph> getUsersTopicGraph()
        {
            List<SRUsersGraph> ret = new List<SRUsersGraph>();
            SRUserDao userDao = new SRUserDao();

            var users = userDao.Retrieve();
            var topics = this.Retrieve();

            foreach (var item in users)
            {
                SRUsersGraph g = new SRUsersGraph
                {
                    id = item.IdStr,
                    name = item.FirstName + " " + item.LastName,
                    value = topics.Where(a=>a.CreatedBy.ToString() == item.IdStr).Count(),
                    data = null
                    //data = topics.Select(a => {
                    //    return new SRUsersGraphDetail
                    //    {
                    //        category = a.DateCreated.ToString("yyyy-MM-dd"),
                    //        value = topics.Where(b=>b.CreatedBy.ToString() == item.IdStr).Count(),
                    //        color = item.Color
                    //    };
                    //}).ToList()
                };

                ret.Add(g);
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