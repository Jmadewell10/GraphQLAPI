using GraphQLAPI.Models;
using GraphQLAPI.Repository;
using GraphQLAPI.Services;
using System.Text;

namespace GraphQLAPI.GraphQL.Types
{
    public class SongType : ObjectType<Song>
    {
        protected override void Configure(IObjectTypeDescriptor<Song> descriptor)
        {
            descriptor.Description("Represents a song.");
            descriptor.Field(p => p.People)
                .ResolveWith<Resolvers>(p => p.GetPeople(default!, default!, default!));
        }

        private class Resolvers 
        {
            public IList<Person> GetPeople(Song song, [ScopedService] IGremlinTraversalSource gremlinTraversalSource, [ScopedService] IVertexService vertexService)
            {
                StringBuilder stringBuilder = new();
                stringBuilder.AppendFormat("g.V('{0}').hasLabel('Song').inE('HasSong').outV().hasLabel('Person').project('Person').by(valueMap(true)).fold()", song.ID);
                var query = stringBuilder.ToString();
                var result = gremlinTraversalSource.SubmitAsync(query).Result;
                var people = vertexService.ProcessPeople(result!);
                return people;
            }
        }

    }
}
