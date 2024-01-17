using System.Net.Mime;
using System.Text.Json;
using Catalog.Api.Controllers;
using Catalog.Api.Repositories;
using Catalog.Api.Settings;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IItemsRepository,MongoDbItemsRepository>();
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.DateTime));

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
builder.Services.AddSingleton<IMongoClient>(ServiceProvider => 
{
   
    return new MongoClient(mongoDbSettings?.ConnectionString);
});

//now .net will not remove async suffix in api calls
builder.Services.AddControllers(options => {
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//to add health checks
//ready for database and live for rest service
builder.Services.AddHealthChecks().AddMongoDb(mongoDbSettings.ConnectionString, name:"mongodb", timeout:TimeSpan.FromSeconds(3),tags: new[] {"ready"});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

//ready for mongodb database
app.MapHealthChecks("/health/ready", new HealthCheckOptions{
    Predicate = (check) => check.Tags.Contains("ready"),
    ResponseWriter = async(ctx,rpt) => 
    {
        var result = JsonSerializer.Serialize(new 
        {
            status = rpt.Status,
            checks = rpt.Entries.Select(e=>new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                exception = e.Value.Exception != null ? e.Value.Exception.Message : "none",
                duration = e.Value.Duration.ToString()
            })
        });

        ctx.Response.ContentType = MediaTypeNames.Application.Json;
        await ctx.Response.WriteAsync(result);
    }
});
//live for rest service
app.MapHealthChecks("/health/live", new HealthCheckOptions{
    Predicate = (_) => false
});

app.Run();
