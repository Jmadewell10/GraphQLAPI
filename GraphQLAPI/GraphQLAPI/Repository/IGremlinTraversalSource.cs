using Gremlin.Net.Driver;

namespace GraphQLAPI.Repository
{
    public interface IGremlinTraversalSource
    {
        Task<ResultSet<dynamic>?> GetPeopleAsync();
        Task<ResultSet<dynamic>?> GetSongsAsync();
        Task<ResultSet<dynamic>?> SubmitAsync(string query);
    }
}