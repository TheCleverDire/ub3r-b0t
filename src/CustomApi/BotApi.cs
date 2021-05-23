﻿
namespace UB3RB0T
{
    using Newtonsoft.Json;
    using Serilog;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Internal, private API for UB3R-B0T.
    /// TODO: Try to generalize this, notably allow for a dynamic customizeable response so others can tailor for their needs.
    /// </summary>
    public class BotApi
    {
        private readonly Uri apiEndpoint;

        public BotApi(Uri endpoint)
        {
            this.apiEndpoint = endpoint;
        }

        public async Task<BotResponseData> IssueRequestAsync(BotMessageData messageData)
        {
            try
            {
                var response = await new Uri($"{this.apiEndpoint}/{messageData.Command}").PostJsonAsync(messageData);
                return JsonConvert.DeserializeObject<BotResponseData>(await response.GetStringAsync());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to parse {Endpoint}", this.apiEndpoint.ToString());
                return new BotResponseData();
            }
        }
    }
}
