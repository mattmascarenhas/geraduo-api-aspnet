using geraduo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); //usar rotas de controllers
builder.Services.AddScoped<Database, Database>();//nosso data context
builder.Services.AddTransient<Database, Database>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();//usar rotas


// Configure the HTTP request pipeline.

//utilizando rotas do controller
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});


app.Run();
