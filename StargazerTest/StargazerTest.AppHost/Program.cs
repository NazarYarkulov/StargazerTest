var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("rediscache");

var apiService = builder.AddProject<Projects.StargazerTest_ApiService>("apiservice");


builder.AddProject<Projects.StargazerTest_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WithReference(cache);


builder.Build().Run();

