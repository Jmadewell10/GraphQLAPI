using GraphQLAPI.Models;
using GraphQLAPI.Repository;
using GraphQLAPI.Services;
using HotChocolate.Data;

namespace GraphQLAPI.GraphQL
{
    public class Query
    {
        private readonly IGremlinTraversalSource _gremlinTraversalSource;
        private readonly IVertexService _vertexService;

        public Query(IGremlinTraversalSource gremlinTraversalSource, IVertexService vertexService)
        {
            _gremlinTraversalSource = gremlinTraversalSource;
            _vertexService = vertexService;
        }
        [UseFiltering]
        [UseSorting]
        public async Task<IList<Person>?> GetPeople()
        {
            var peopleData = await _gremlinTraversalSource.GetPeopleAsync();
            if (peopleData == null)
            {
                return null;
            }
            var people = _vertexService.ProcessPeople(peopleData);
            return people;
        }

        [UseFiltering]
        [UseSorting]
        public async Task<IList<Song>?> GetSongs()
        {
            var songData = await _gremlinTraversalSource.GetSongsAsync();
            if(songData == null)
            {
                return null;
            }
            var songs = _vertexService.ProcessSong(songData);
            return songs;
        }
    }
}
