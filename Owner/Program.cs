Owner.Data.Owners.InitData();

var builder = WebApplication.CreateBuilder(args);

builder
    .AddGraphQL()
    .ModifyRequestOptions(
        o => o.IncludeExceptionDetails =
            builder.Environment.IsDevelopment())
    .AddApolloFederation(FederationVersion.Federation25)
    .AddTypes()
    .InitializeOnStartup();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
