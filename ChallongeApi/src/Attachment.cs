using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChallongeApi
{
    public class Attachment
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Attachment() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Retrieves a single match attachment record.
        /// </summary>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> Show()//test
        {
            return await User.ParseAndFetch(MethodType.GET, $"tournaments/{tournament_id}/matches/{match_id}/attachments/{id}.json", null);
        }

        /// <summary>
        /// Updates the attributes of a match attachment.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/match_attachments/update">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> Update(Dictionary<string, string> parameters)//test
        {
            return await User.ParseAndFetch(MethodType.PUT, $"tournaments/{tournament_id}/matches/{match_id}/attachments/{id}.json", parameters);
        }

        /// <summary>
        /// Deletes a match attachment.
        /// </summary>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> Destroy()//test
        {
            return await User.ParseAndFetch(MethodType.DELETE, $"tournaments/{tournament_id}/matches/{match_id}/attachments/{id}.json", null);
        }

        public int tournament_id { get; internal set; }

        [JsonProperty]
        public int id { get; private set; }
        [JsonProperty]
        public int match_id { get; private set; }
        [JsonProperty]
        public int user_id { get; private set; }
        [JsonProperty]
        public object description { get; private set; }
        [JsonProperty]
        public string url { get; private set; }
        [JsonProperty]
        public object original_file_name { get; private set; }
        [JsonProperty]
        public DateTime created_at { get; private set; }
        [JsonProperty]
        public DateTime updated_at { get; private set; }
        [JsonProperty]
        public object asset_file_name { get; private set; }
        [JsonProperty]
        public object asset_content_type { get; private set; }
        [JsonProperty]
        public object asset_file_size { get; private set; }
        [JsonProperty]
        public object asset_url { get; private set; }
    }
}