using WorkoutNest.IDP;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseSuccessEvents = true;
  
    })
    .AddTestUsers(Config.Users)
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients);



var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/", () => "Hello World!");
app.UseIdentityServer();
app.Run();

