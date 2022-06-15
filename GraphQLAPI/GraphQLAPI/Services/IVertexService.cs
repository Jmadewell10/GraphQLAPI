using GraphQLAPI.Models;

namespace GraphQLAPI.Services
{
    public interface IVertexService
    {
        Person CreatePersonObject(IDictionary<string, dynamic> objects);
        IList<Person> ProcessPeople(IEnumerable<dynamic> peopleData);
        IList<Song> ProcessSong(IEnumerable<dynamic> songData);
    }
}