var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Car> db = new List<Car>();

app.MapGet("/cars", () =>
{
    if (db.Count == 0)
    {
        return Results.NotFound();
    }
    return Results.Ok(db);
});

app.MapGet("/cars/{id}", (int id) =>
{
    if (db.Count == 0 || db[id] == null)
    {
        return Results.NotFound();
    }
    var car = db[id];
    return Results.Ok(car);
});

app.MapPost("/cars", (Car toAdd) =>
{
    db.Add(toAdd);
    return Results.Created();
});

app.Run();

public record Car(string Brand, string Model, int HorsePower);