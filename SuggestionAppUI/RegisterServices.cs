﻿namespace SuggestionAppUI
{
    public static class RegisterServices
    {

        //extension method with use of the 'this' key word.
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddMemoryCache();

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
