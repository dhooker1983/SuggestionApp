using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Rewrite;
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

//these two are added after app.UseRouting. Authentication first!!
app.UseAuthentication();
app.UseAuthorization();

//this overrides the default redirect to the sign out page, instead you are signed out and redirected to home page.
app.UseRewriter(
    new RewriteOptions().Add(
        context =>
        {
            if (context.HttpContext.Request.Path == "/MicrosoftIdentity/Account/SignedOut")
            {
                context.HttpContext.Response.Redirect("/");
            }
        }));

//these are the MVC controllers, needed as the authentication system uses MVC style controllers, therefore have to add 
//routing for them.
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
