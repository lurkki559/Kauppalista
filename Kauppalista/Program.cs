using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Kauppalista.Data;

var builder = WebApplication.CreateBuilder(args);

//Maanantaiaamu alkaa sprintin suunnittelupalaverilla ja jokainen aamu dailylla. Perjantaina review ja retro. 
//Tekemisessä käytetään GitHubia niin, että jokaiselle 
//muutokselle on syynä joko taski joka on luotu jo sprintin suunnittelussa, tai issue, joka on luotu viikon aikana.

//(Keskivaikea) Kauppalista (Web-applikaatio joka muistaa mitä kauppalistaan on lisätty)

//Todo:
//Välilehti, jossa on kauppalista. Voidaan lisätä tuotteita listalle, myös poistaa. Extra: hakee tuotteiden hintoja netistä, esim Prisma.
//Product -luokka, jossa nimi, hinta ja määrä.
//ShoppingList -luokka, jossa metodit tuotteen lisäämiselle ja poistamiselle.
//Lista pitää pystyä tallentamaan json -tiedostoon.
//Jos tulee jotain omia ajatuksia tai muutosehdotuksia, sitä ennen githubiin ISSUE tai TASK, koska näin on käsketty toimia.


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<ShoppingListService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
