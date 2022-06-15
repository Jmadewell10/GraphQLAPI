using GraphQLAPI.GraphQL;
using GraphQLAPI.GraphQL.Types;
using GraphQLAPI.Repository;
using GraphQLAPI.Services;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using HotChocolate.Data;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGremlinClient>((ServiceProvider) =>
{
    string containerLink = "/dbs/" + builder.Configuration["DatabaseName"] + "/colls/" + builder.Configuration["ContainerName"];
            GremlinServer gremlinServer = new GremlinServer(
                hostname: builder.Configuration["Hostname"],
                port: 443,
                enableSsl: true,
                username: containerLink,
                password: builder.Configuration["PrimaryKey"]);
    
    ConnectionPoolSettings connectionPoolSettings = new ConnectionPoolSettings()
    {
        MaxInProcessPerConnection = 10,
        PoolSize = 30,
    };

    var webSocketConfiguration = new Action<ClientWebSocketOptions>(options =>
    {
        options.KeepAliveInterval = TimeSpan.FromSeconds(10);
    });

    var graphSonReader = new GraphSON2Reader();
    var graphSonWriter = new GraphSON2Writer();
    var mimeType = "application/vnd.gremlin-v2.0+json";

    return new GremlinClient(
        gremlinServer: gremlinServer,
        graphSONReader: graphSonReader,
        graphSONWriter: graphSonWriter,
        mimeType: mimeType,
        connectionPoolSettings: connectionPoolSettings,
        webSocketConfiguration: webSocketConfiguration);
});
builder.Services.AddScoped<IGremlinTraversalSource, GremlinTraversalSource>((ServiceProvider) =>
{
    return new GremlinTraversalSource(ServiceProvider.GetRequiredService<IGremlinClient>());
});
builder.Services.AddScoped<IVertexService, VertexService>((ServiceProvider) =>
{
    return new VertexService(ServiceProvider.GetRequiredService<IModelService>(), ServiceProvider.GetRequiredService<IGremlinTraversalSource>());
});
builder.Services.AddScoped<IModelService, ModelService>((ServiceProvider) =>
{
    return new ModelService();
});
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<PersonType>()
    .AddFiltering()
    .AddSorting();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();
