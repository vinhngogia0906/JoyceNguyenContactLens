using BackendApplication.Data;
using BackendApplication.Schema.Mutation;
using BackendApplication.Schema.Query;
using BackendApplication.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContactLensDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IContactLensRepository, ContactLensRepository>();

builder.Services.AddGraphQLServer().AddQueryType<ContactLensQuery>().AddMutationType<ContactLensMutation>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ContactLensDbContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});



app.Run();
