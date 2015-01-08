using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchCollection.Models.Dao
{
    public class SRUserDao
    {
        private MongoCollection<SRUser> userCollection;

        public SRUserDao()
        {
            var mongoDB = new Mongow();
            var database = mongoDB.TopicFinderDatabase;

            this.userCollection = database.GetCollection<SRUser>("users");
        }

        public bool Create(SRUser model)
        {
            model.DateCreated = DateTime.Now;
            model.LastLogin = DateTime.Now;
            model.Role = UserRole.VISITOR;
            model.Status = ActiveStatus.ACTIVE;

            return this.userCollection.Insert(model).Ok;
        }

        public List<SRUser> Retrieve()
        {
            var all = this.userCollection.FindAll();
            List<SRUser> ret = new List<SRUser>();

            foreach (var item in all)
            {
                ret.Add(item);
            }

            return ret;
        }

        public SRUser Retrieve(string id)
        {
            ObjectId objId = new ObjectId(id);

            var query = Query<SRUser>.EQ(e => e.Id, objId);
            var ret = this.userCollection.FindOne(query);

            return ret;
        }

        public bool Update(SRUser model)
        {
            var query = Query<SRUser>.EQ(e => e.Id, model.Id);
            var update = Update<SRUser>.Set(e => e.FirstName, model.FirstName)
                .Set(e => e.LastName, model.LastName)
                .Set(e => e.Username, model.Username)
                .Set(e => e.Password, model.Password);

            return this.userCollection.Update(query, update).Ok;
        }

        public bool Delete(string id)
        {
            ObjectId objId = new ObjectId(id);

            var query = Query<SRUser>.EQ(e => e.Id, objId);

            return this.userCollection.Remove(query).Ok;
        }
    }
}