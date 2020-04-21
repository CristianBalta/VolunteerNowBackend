using backendapi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendapi.Services
{
    public class DonationsService
    {
        private readonly IMongoCollection<Donations> _donations;

        public DonationsService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _donations = database.GetCollection<Donations>(settings.DonationsCollectionName);
        }

        public List<Donations> Get() =>
           _donations.Find(donation => true).ToList();

    }
}
