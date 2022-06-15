using GraphQLAPI.Models;

namespace GraphQLAPI.Services
{
    public class ModelService : IModelService
    {
        public IDictionary<string, string> GetProperties(IDictionary<string, dynamic> data)
        {
            IDictionary<string, string> propertiesToReturn = new Dictionary<string, string>();
            foreach (var property in data)
            {
                if (property.Key.Equals("id") || property.Key.Equals("label"))
                {

                    propertiesToReturn.Add(property.Key, property.Value);
                }
                else
                {
                    foreach (var value in property.Value)
                    {
                        propertiesToReturn.Add(property.Key.ToString(), value.ToString());
                    }
                }
            }
            return propertiesToReturn;
        }

        public Person AssignToPersonModel(IDictionary<string, string> properties)
        {
            Person person = new Person();
            person.KnownPeople = new List<Person>();
            person.Songs = new List<Song>();
            if (properties.ContainsKey("id"))
                person.ID = properties["id"];
            if (properties.ContainsKey("Name"))
                person.Name = properties["Name"];
            if (properties.ContainsKey("Email"))
                person.Email = properties["Email"];
            return person;
        }

        public Song AssignToSongModel(IDictionary<string, string> properties)
        {
            Song song = new Song();
            song.People = new List<Person>();
            if(properties.ContainsKey("id"))
                song.ID = properties["id"];
            if(properties.ContainsKey("Name"))
                song.Name = properties["Name"];
            if (properties.ContainsKey("Album"))
                song.Album = properties["Album"];
            return song;
        }
    }
}
