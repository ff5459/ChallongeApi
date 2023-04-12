using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChallongeApi
{
    public class Match
    {
        private Match() { }

        /// <summary>
        /// Retrieves a single match record for a tournament.
        /// </summary>
        /// <param name="include_attachments">Includes an array of associated attachment records if set to true.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> Show(bool include_attachments)
        {
            if (include_attachments)
                return await User.ParseAndFetch(MethodType.GET, $"tournaments/{tournament_id}/matches/{id}.json", new Dictionary<string, string> { { "include_attachments", "1" } });

            else
                return await User.ParseAndFetch(MethodType.GET, $"tournaments/{tournament_id}/matches/{id}.json", new Dictionary<string, string> { { "include_attachments", "0" } });
        }

        /// <summary>
        /// Update/submit the score(s) for a match.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/matches/update">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> Update(Dictionary<string, string> parameters)
        {
            return await User.ParseAndFetch(MethodType.PUT, $"tournaments/{tournament_id}/matches/{id}.json", parameters);
        }

        /// <summary>
        /// Reopens a match that was marked completed, automatically resetting matches that follow it.
        /// </summary>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> Reopen()
        {
            return await User.ParseAndFetch(MethodType.POST, $"tournaments/{tournament_id}/matches/{id}/reopen.json", null);
        }

        /// <summary>
        /// Sets "underway_at" to the current time and highlights the match in the bracket.
        /// </summary>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> MarkAsUnderway()
        {
            return await User.ParseAndFetch(MethodType.POST, $"tournaments/{tournament_id}/matches/{id}/mark_as_underway.json", null);
        }

        /// <summary>
        /// Clears "underway_at" and unhighlights the match in the bracket.
        /// </summary>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> UnMarkAsUnderway()
        {
            return await User.ParseAndFetch(MethodType.POST, $"tournaments/{tournament_id}/matches/{id}/unmark_as_underway.json", null);
        }

        /// <summary>
        /// Retrieves a match's attachments.
        /// </summary>
        /// <returns>A set of attachments as <see cref="List{Attachment}"/></returns>
        public async Task<List<Attachment>> GetAttachments()
        {
            List<Attachment> AttachmentList = new List<Attachment>();
            var Result = await User.Fetch(MethodType.GET, $"tournaments/{tournament_id}/matches/{id}/attachments.json", null);
            JArray Attachments = JArray.Parse(Result);
            foreach (JObject AttachmentJObject in Attachments)
            {
                string ResultCut = AttachmentJObject.ToString()[25..^1];
                Attachment attachment = JsonConvert.DeserializeObject<Attachment>(ResultCut)!;
                attachment.tournament_id = tournament_id;
                AttachmentList.Add(attachment);
            }
            return AttachmentList;
        }

        /// <summary>
        /// Adds a file, link, or text attachment to a match. NOTE: The associated tournament's "accept_attachments" attribute must be true for this action to succeed.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/match_attachments/create">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> CreateAttachment(Dictionary<string, string> parameters)
        {
            return await User.ParseAndFetch(MethodType.POST, $"tournaments/{tournament_id}/matches/{id}/attachments.json", parameters);
        }

        [JsonProperty]
        public object? attachment_count { get; private set; }
        [JsonProperty]
        public DateTime created_at { get; private set; }
        [JsonProperty]
        public object? group_id { get; private set; }
        [JsonProperty]
        public bool has_attachment { get; private set; }
        [JsonProperty]
        public int id { get; private set; }
        [JsonProperty]
        public string? identifier { get; private set; }
        [JsonProperty]
        public object? location { get; private set; }
        [JsonProperty]
        public object? loser_id { get; private set; }
        [JsonProperty]
        public int? player1_id { get; private set; }
        [JsonProperty]
        public bool player1_is_prereq_match_loser { get; private set; }
        [JsonProperty]
        public object? player1_prereq_match_id { get; private set; }
        [JsonProperty]
        public object? player1_votes { get; private set; }
        [JsonProperty]
        public int? player2_id { get; private set; }
        [JsonProperty]
        public bool player2_is_prereq_match_loser { get; private set; }
        [JsonProperty]
        public object? player2_prereq_match_id { get; private set; }
        [JsonProperty]
        public object? player2_votes { get; private set; }
        [JsonProperty]
        public int round { get; private set; }
        [JsonProperty]
        public object? scheduled_time { get; private set; }
        [JsonProperty]
        public DateTime? started_at { get; private set; }
        [JsonProperty]
        public string? state { get; private set; }
        [JsonProperty]
        public int tournament_id { get; private set; }
        [JsonProperty]
        public object? underway_at { get; private set; }
        [JsonProperty]
        public DateTime? updated_at { get; private set; }
        [JsonProperty]
        public object? winner_id { get; private set; }
        [JsonProperty]
        public string? prerequisite_match_ids_csv { get; private set; }
        [JsonProperty]
        public string? scores_csv { get; private set; }
    }
}