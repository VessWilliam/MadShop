var builder = WebApplication.CreateBuilder(args);

//Add service to the container  

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run();
