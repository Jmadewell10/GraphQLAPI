using GraphQLAPI.Models;
using GraphQLAPI.Repository;
using GraphQLAPI.Services;
using System.Text;

namespace GraphQLAPI.GraphQL.Types
{
    public class PersonType : ObjectType<Person>
    {
        protected override void Configure(IObjectTypeDescriptor<Person> descriptor)
        {
            descriptor.Description("Represents a person.");
            descriptor.Field(p => p.Songs)
                .ResolveWith<Resolvers>(s => s.GetSongs(default!, default!, default!))
                .Description("List of Songs owned by Person");

        }
        private class Resolvers 
        {
            public IList<Song> GetSongs(Person person, [ScopedService] IGremlinTraversalSource gremlinTraversalSource, [ScopedService] IVertexService vertexService)
            {
                StringBuilder stringBuilder = new();
                stringBuilder.AppendFormat("g.V('{0}').hasLabel('Person').outE('HasSong').inV().hasLabel('Song').project('Song').by(valueMap(true)).fold()", person.ID);
                var query = stringBuilder.ToString();
                var result = gremlinTraversalSource.SubmitAsync(query).Result;
                var songs = vertexService.ProcessSong(result!);
                return songs;
            }
        }

    }
}
