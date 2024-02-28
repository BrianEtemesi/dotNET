using HotChoco.Dapper.API.MutationResolver;
using HotChoco.Dapper.API.QueryResolver;
using HotChoco.Dapper.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddType<UserQueryResolver>()
    .AddMutationType(m => m.Name("Mutation"))
    .AddType<UserMutationResolver>();





builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGraphQL();
app.MapControllers();

app.Run();
