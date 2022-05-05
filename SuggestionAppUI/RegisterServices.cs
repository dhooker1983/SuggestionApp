

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace SuggestionAppUI
{
    public static class RegisterServices
    {

        //extension method with use of the 'this' key word.
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();

            builder.Services.AddMemoryCache();

            //brings in MVC for authentication controllers and the UI pages
            builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();


            //below is available via the two packages installed: Microsoft.Identity.Web and Microsoft.Identity.Web.UI
            //here I am adding the authentication system and we are saying use 'MicrosoftIdentityWebApp' and get the
            //config for that from appsetting.json AzureAdB2C section.
            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

            //this essentially creates a policy. Policy says that the jobTitle claim has to have a value of Admin
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                   policy.RequireClaim(claimType:"jobTitle", allowedValues:"Admin");
                });
            });
                
            builder.Services.Configure<CosmosDbOptions>(builder.Configuration.GetSection("CosmosDb"));

            CosmosClient cosmosClient = new CosmosClient(builder.Configuration.GetValue<string>("CosmosDb:CosmosDbConnection"));
            builder.Services.AddSingleton(cosmosClient);

            builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>();
            builder.Services.AddSingleton<ICategoryData, CosmosCategoryData>();
            builder.Services.AddSingleton<IStatusData, CosmosStatusData>();
            builder.Services.AddSingleton<ISuggestionData, CosmosSuggestionData>();
            builder.Services.AddSingleton<IUserData, CosmosUserData>();
        }
    }
}
