using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosUserData
    {
        private readonly CosmosDbOptions _cosmosDb;
        private readonly Container _client;

        public CosmosUserData(IOptions<CosmosDbOptions> options)
        {
            _cosmosDb = options.Value;
        }
    }
}
