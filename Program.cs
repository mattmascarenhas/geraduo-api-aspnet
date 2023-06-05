using geraduo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers(); //usar rotas de controllers
builder.Services.AddScoped<Database, Database>();//nosso data context
builder.Services.AddTransient<Database, Database>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();


app.UseRouting();//usar rotas

//usando o cors para poder liberar acesso ao front-end
app.UseCors(builder =>
    builder.WithOrigins("http://localhost:3000")
           .AllowAnyHeader()
           .AllowAnyMethod()
);


// Configure the HTTP request pipeline.

//utilizando rotas do controller
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});


app.Run();
