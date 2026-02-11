using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();







app.MapGet("/api/users", () =>
{
    var list = new List<string>() { "A", "B", "C" };

    return Results.Ok(list); // returnerar listan
});








// OLIKA STATUSKODER

// READ
app.MapGet("/api/users", () => 
{
    return Results.Ok(); // returnerar 200 statuskod (allt har gått bra och jag har utfört det jag ska.)
    return Results.NoContent(); // returnerar en 204 statuskod, (jag har utför det jag ska göra, men skickar inte tillbaka nån data)
    return Results.BadRequest(); // returnerar en 400. (när något har gått fel på klient sidan, en användare har matat in FEL information.)
    return Results.Conflict(); // returnerar en 409 statuskod. (t.ex om vi försöker skapa en användare som REDAN finns.)
    return Results.InternalServerError(); // returnerar 500 statuskod. (Ej användaren som gjort fel, utan något i serverkommunikationen har gått fel.)

});










// C R U D

// CREATE
app.MapPost("/api/users", () => { });

// READ
app.MapGet("/api/users", () => { });

// UPDATE
app.MapPut("/api/users", () => { });
app.MapPatch("/api/users", () => { });

// DELETE
app.MapDelete("/api/users", () => { });




app.Run();
