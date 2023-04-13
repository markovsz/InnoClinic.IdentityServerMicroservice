using Api.Extensions;
using Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigurePasswordGenerator(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureFilters();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureIdentityServer(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionsHandler>();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
