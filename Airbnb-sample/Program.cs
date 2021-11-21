using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;



namespace Airbnb_sample
{
   public class Program 
    {
        static void Main(string[] args)
        {
            MongoCrud db = new MongoCrud("sample-airbnb");
          var recs= db.IdsForListingsInPortugal();
           foreach(var rec in recs) 
            {
                Console.WriteLine(rec);
            }
           
            
         // db.ListingsWithMoreThanFourBdrooms();
           // db.DocumentsCount();
            //Console.WriteLine("hello world");
        }
    }
    public class MongoCrud
    {
        private IMongoDatabase db;
        public MongoCrud(string database)
        {
            var client = new MongoClient("mongodb+srv://airbnb:airbnb123@cluster0.ax9b0.mongodb.net/sample_airbnb?retryWrites=true&w=majority");
            db = client.GetDatabase(database);
        }
        public void getAttributesOfTheListings () {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
        }
        public long DocumentsCount()
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
         var count= collection.EstimatedDocumentCount();
            return count;
        }
        public  IEnumerable<BsonDocument> ListingWithPropertyTypeHouse()
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
            var filter = Builders<BsonDocument>.Filter.Gt("bedrooms", 4);
            var results = collection.Find(filter).ToList();
            return results;
            
        }
       public IEnumerable<BsonDocument> ListingsWithMoreThanFourBdrooms() 
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
            var filter = Builders<BsonDocument>.Filter.Gt("bedrooms", 4);
            var results = collection.Find(filter).ToList();
            return results;
        }
       public IEnumerable<BsonDocument> ListingsInPortugal() 
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
          var filter = Builders<BsonDocument>.Filter.Eq("address.country", "portugal");
            var results = collection.Find(filter).ToList();
            return results;

        }
        public IEnumerable<BsonDocument> IdsForListingsInPortugal () 
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
            var filter = Builders<BsonDocument>.Filter.Lt("address.country", "portugal");
            var projection = Builders<BsonDocument>.Projection.Include("_id");
            var results = collection.Find(filter).Project(projection).ToList();
            return results;
        }
        
    }
   
}
