using MongoDB.Driver;
using MongoDB.Bson;
using System;

namespace Airbnb_sample
{
    class Program
    {
        static void Main(string[] args)
        {
           MongoCrud db = new MongoCrud("sample-airbnb");
           // db.IdsForListingsInPortugal();
            db.ListingsWithMoreThanFourBdrooms();
           Console.ReadLine();
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
        public void DocumentsCount()
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
         var count= collection.EstimatedDocumentCount();
            Console.WriteLine(count);
        }
        public void ListingWithPropertyTypeHouse()
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
            var filter = Builders<BsonDocument>.Filter.Gt("bedrooms", 4);
            var results = collection.Find(filter).ToList();
            Console.WriteLine(results);
        }
       public void ListingsWithMoreThanFourBdrooms() 
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
            var filter = Builders<BsonDocument>.Filter.Gt("bedrooms", 4);
            var results = collection.Find(filter).ToList();
            Console.WriteLine(results);
        }
       public void ListingsInPortugal() 
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
          var filter = Builders<BsonDocument>.Filter.Eq("address.street", "porto,porto,portugal");
            var results = collection.Find(filter).ToList();
            Console.WriteLine(results);

        }
        public void IdsForListingsInPortugal () 
        {
            var collection = db.GetCollection<BsonDocument>("listingsAndReviews");
            var filter = Builders<BsonDocument>.Filter.Lt("address.street", "porto,porto,portugal");
            var projection = Builders<BsonDocument>.Projection.Include("_id");
            var result = collection.Find(filter).Project(projection).ToList();
            Console.WriteLine(result);
        }
        
    }
}
