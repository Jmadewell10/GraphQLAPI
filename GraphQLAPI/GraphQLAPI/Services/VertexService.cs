using GraphQLAPI.Extensions;
using GraphQLAPI.Models;
using GraphQLAPI.Repository;
using System.Text;

namespace GraphQLAPI.Services
{
    public class VertexService : IVertexService
    {
        private readonly IModelService _modelService;
        private readonly IGremlinTraversalSource _gremlinTraversalSource;
        public VertexService(IModelService modelService, IGremlinTraversalSource gremlinTraversalSource)
        {
            _modelService = modelService;
            _gremlinTraversalSource = gremlinTraversalSource;
        }
        public IList<Person> ProcessPeople(IEnumerable<dynamic> peopleData)
        {
            IList<Person> people = new List<Person>();
            var dictionaries = peopleData.IterateToDictionaries();
            foreach (var personObject in dictionaries)
            {
                Person person = CreatePersonObject(personObject);
                people.Add(person);
            }
            return people;
        }

        public IList<Song> ProcessSong(IEnumerable<dynamic> songData)
        {
            IList<Song> songs = new List<Song>();
            var dictionaries = songData.IterateToDictionaries();
            foreach(var songObject in dictionaries)
            {
                Song song = CreateSongObject(songObject);
                songs.Add(song);
            }
            return songs;
        }

        public Person CreatePersonObject(IDictionary<string, dynamic> objects)
        {
            Person person = new Person();
            foreach (var key in objects.Keys)
            {
                IDictionary<string, string> properties = _modelService.GetProperties(objects[key]);
                if (properties.Any())
                {
                    person = _modelService.AssignToPersonModel(properties);
                }
            }
            return person;
        }

        public Song CreateSongObject(IDictionary<string, dynamic> objects)
        {
            Song song = new Song();
            foreach(var key in objects.Keys)
            {
                IDictionary<string, string> properties = _modelService.GetProperties(objects[key]);
                song = _modelService.AssignToSongModel(properties);
            }
            return song;
        }

        public string QueryBuilder(string id, string rootObject, string targetObject)
        {
            string query = string.Empty;
            StringBuilder stringBuilder = new();
            bool inE = rootObject.InE(targetObject);
            string edgeName = rootObject.GetEdgeName(targetObject);

            stringBuilder.AppendFormat("g.V('{0}').hasLabel('{1}')", id, rootObject);
            if (!inE)
            {
                if (edgeName.Equals("HasSong"))
                {
                    stringBuilder.AppendFormat(".outE('{0}').inV().project('Song').by(valueMap(true))", edgeName);
                    query = stringBuilder.ToString();
                    return query;
                }                   
                stringBuilder.AppendFormat(".outE('{0}').inV().project('Person').by(valueMap(true))", edgeName);
                query = stringBuilder.ToString();
                return query;
            }
            stringBuilder.AppendFormat(".inE('{0}').outV().project('Person').by(valueMap(true))", edgeName);
            query = stringBuilder.ToString();
            return query;
        }
    }
}
