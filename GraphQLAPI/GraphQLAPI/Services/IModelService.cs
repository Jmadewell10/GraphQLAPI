using GraphQLAPI.Models;

namespace GraphQLAPI.Services
{
    public interface IModelService
    {
        Person AssignToPersonModel(IDictionary<string, string> properties);
        IDictionary<string, string> GetProperties(IDictionary<string, dynamic> data);
        Song AssignToSongModel(IDictionary<string, string> properties);
    }
}