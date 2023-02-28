using MongoDB.Driver;

namespace HiringChallange.Application.Interfaces.Contract
{
    public interface IMongoConnect
    {
        public IMongoCollection<T> ConnectToMongo<T>(string collection);
    }
}
