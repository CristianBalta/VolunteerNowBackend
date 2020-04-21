using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendapi.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UserCollectionName { get; set; }
        public string DonationsCollectionName { get; set; }
        public string NeedCollectionName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string BooksCollectionName { get; set; }

        string UserCollectionName { get; set; }

        string DonationsCollectionName { get; set; }

        string NeedCollectionName { get; set; }

        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
