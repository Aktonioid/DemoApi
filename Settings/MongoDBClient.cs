using MongoDB.Driver;
using dotenv.net;

namespace DemoApi.Settings
{
    public class MongoDBClient //В данном классе только строка подключения и название Базы Данных
    {
        public MongoDBClient()
        {   
            MongoClientSettings settings = new MongoClientSettings();
            var mongoClient = new MongoClient(Connection_string);
            db = mongoClient.GetDatabase(DBName);
        }

        public IMongoDatabase db;

        private static string Host {get { return EnvDict("DB_HOST"); }}
        private static string Port { get { return EnvDict("PORT"); } }
        private static string login{get{return EnvDict("MONGO_INITDB_ROOT_USERNAME");}}
        private static string passw{get{return EnvDict("MONGO_INITDB_ROOT_PASSWORD");}}
        private const string DBName = "Database";
        public string Connection_string = $"mongodb://{login}:{passw}@{Host}:{Port}";
        //testing

        
        private static String EnvDict(String variable)
        {
            return Environment.GetEnvironmentVariable(variable)!;

        }
        
        
    }
}
