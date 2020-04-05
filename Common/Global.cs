using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
namespace Test.Common
{
    public static class Global
    {
        public static string ConnectionString()
        {
            return @"Data Source=(LocalDb)\MSSQLLocalDB;" +
                "Initial Catalog=BA_Geocoding;Integrated Security=True";
        }
        public static ElasticClient CreateEsClient()
        {
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient;
            StaticConnectionPool connectionPool;
            var nodes = new Uri[]
            {
                new Uri("http://localhost:9200/")
                //new Uri("Add server 2 address")   //Add cluster addresses here
                //new Uri("Add server 3 address")
            };

            connectionPool = new StaticConnectionPool(nodes);
            connectionSettings = new ConnectionSettings(connectionPool);
            elasticClient = new ElasticClient(connectionSettings);
            return elasticClient;
        }
    }
}
