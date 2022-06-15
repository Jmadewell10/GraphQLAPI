using Gremlin.Net.Driver;

namespace GraphQLAPI.Repository
{
    public class GremlinTraversalSource : IGremlinTraversalSource
    {
        private readonly IGremlinClient _gremlinClient;

        public GremlinTraversalSource(IGremlinClient gremlinClient)
        {
            _gremlinClient = gremlinClient;
        }

        public async Task<ResultSet<dynamic>?> GetPeopleAsync()
        {
            try
            {
                string query = "g.V().hasLabel('Person').project('Person').by(valueMap(true))";
                return await _gremlinClient.SubmitAsync<dynamic>(query);
            }
            catch (Exception ex)
            {
                return await Task.FromException<ResultSet<dynamic>>(ex);
            }
        }

        public async Task<ResultSet<dynamic>?> GetSongsAsync()
        {
            try
            {
                string query = "g.V().hasLabel('Song').project('Song').by(valueMap(true))";
                return await _gremlinClient.SubmitAsync<dynamic>(query);
            }
            catch (Exception ex)
            {
                return await Task.FromException<ResultSet<dynamic>>(ex);
            }
        }

        public async Task<ResultSet<dynamic>?> SubmitAsync(string query)
        {
            try
            {
                return await _gremlinClient.SubmitAsync<dynamic>(query);
            }
            catch(Exception ex)
            {
                return await Task.FromException<ResultSet<dynamic>>(ex);
            }
        }
    }
}
