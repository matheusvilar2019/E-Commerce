using E_Commerce.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ECommerceDataContext>();
var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
