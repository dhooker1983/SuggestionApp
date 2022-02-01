using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SuggestionAppUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages(); default code
//builder.Services.AddServerSideBlazor(); default code

//this custom extension ConfigureServices class is replacing the default code commented out above that I have moved to the custom
//class. Main reason for this is that lots of services will need to be injected so want to make it more managable in a separate
//class and have one call to that class here.

builder.ConfigureServices();

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
