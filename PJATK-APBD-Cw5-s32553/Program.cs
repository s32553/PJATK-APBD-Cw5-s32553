var builder = WebApplication.CreateBuilder(args);

// Rejestracja kontrolerów
builder.Services.AddControllers();

// (opcjonalnie, ale często wymagane na zajęciach)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger (do testów)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapowanie kontrolerów
app.MapControllers();

app.Run();