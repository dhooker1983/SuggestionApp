namespace SuggestionAppUI
{
    public static class RegisterServices
    {

        //extension method with use of the 'this' key word.
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();
            
            //cosmosdb
        }
    }
}
