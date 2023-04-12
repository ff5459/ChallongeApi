using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChallongeApi
{
    public class Participant
    {
        private Participant() { }

        /// <summary>
        /// Retrieves a single participant record for a tournament.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/participants/show">See documentation</see>
        /// </summary>
        /// <param name="include_matches"> Includes an array of associated match records if set to true</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> Show(bool include_matches)
        {
            if (include_matches)
                return await User.ParseAndFetch(MethodType.GET, $"tournaments/{tournament_id}/participants/{id}.json", new Dictionary<string, string> { { "include_matches", "1" } });

            else
                return await User.ParseAndFetch(MethodType.GET, $"tournaments/{tournament_id}/participants/{id}.json", new Dictionary<string, string> { { "include_matches", "0" } });
        }

        /// <summary>
        /// Updates the attributes of a tournament participant.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/participants/update">See documentation</see>
        /// </summary>
        /// <param name="parameters">Update parameters</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> Update(Dictionary<string, string>? parameters)
        {
            return await User.ParseAndFetch(MethodType.PUT, $"tournaments/{tournament_id}/participants/{id}.json", parameters);
        }

        /// <summary>
        /// Checks a participant in, setting checked_in_at to the current time.
        /// </summary>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> CheckIn()
        {
            return await User.ParseAndFetch(MethodType.POST, $"tournaments/{tournament_id}/participants/{id}/check_in.json", null);
        }

        /// <summary>
        /// Marks a participant as having not checked in, setting checked_in_at to null.
        /// </summary>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> UndoCheckIn()
        {
            return await User.ParseAndFetch(MethodType.POST, $"tournaments/{tournament_id}/participants/{id}/undo_check_in.json", null);
        }

        /// <summary>
        /// If the tournament has not started, deletes a participant, automatically filling in the abandoned seed number. If tournament is underway, marks a participant inactive, automatically forfeiting his/her remaining matches.
        /// </summary>
        /// <returns>Response as <see cref="JObject"/></returns>
        public async Task<JObject> Destroy()
        {
            return await User.ParseAndFetch(MethodType.DELETE, $"tournaments/{tournament_id}/participants/{id}.json", null);
        }

        [JsonProperty]
        public int id { get; private set; }
        [JsonProperty]
        public int tournament_id { get; private set; }
        [JsonProperty]
        public string? name { get; private set; }
        [JsonProperty]
        public int seed { get; private set; }
        [JsonProperty]
        public bool active { get; private set; }
        [JsonProperty]
        public DateTime created_at { get; private set; }
        [JsonProperty]
        public DateTime updated_at { get; private set; }
        [JsonProperty]
        public object? invite_email { get; private set; }
        [JsonProperty]
        public int? final_rank { get; private set; }
        [JsonProperty]
        public object? misc { get; private set; }
        [JsonProperty]
        public object? icon { get; private set; }
        [JsonProperty]
        public bool on_waiting_list { get; private set; }
        [JsonProperty]
        public int? invitation_id { get; private set; }
        [JsonProperty]
        public object? group_id { get; private set; }
        [JsonProperty]
        public object? checked_in_at { get; private set; }
        [JsonProperty]
        public object? ranked_member_id { get; private set; }
        [JsonProperty]
        public object? custom_field_response { get; private set; }
        [JsonProperty]
        public object? clinch { get; private set; }
        [JsonProperty]
        public object? integration_uids { get; private set; }
        [JsonProperty]
        public string? challonge_username { get; private set; }
        [JsonProperty]
        public int? challonge_user_id { get; private set; }
        [JsonProperty]
        public bool? challonge_email_address_verified { get; private set; }
        [JsonProperty]
        public bool? removable { get; private set; }
        [JsonProperty]
        public bool? participatable_or_invitation_attached { get; private set; }
        [JsonProperty]
        public bool? confirm_remove { get; private set; }
        [JsonProperty]
        public bool? invitation_pending { get; private set; }
        [JsonProperty]
        public string? display_name_with_invitation_email_address { get; private set; }
        [JsonProperty]
        public string? email_hash { get; private set; }
        [JsonProperty]
        public string? username { get; private set; }
        [JsonProperty]
        public string? display_name { get; private set; }
        [JsonProperty]
        public string? attached_participatable_portrait_url { get; private set; }
        [JsonProperty]
        public bool can_check_in { get; private set; }
        [JsonProperty]
        public bool checked_in { get; private set; }
        [JsonProperty]
        public bool reactivatable { get; private set; }
        [JsonProperty]
        public bool check_in_open { get; private set; }
        [JsonProperty]
        public object[]? group_player_ids { get; private set; }
        [JsonProperty]
        public bool has_irrelevant_seed { get; private set; }
        [JsonProperty]
        public string? ordinal_seed { get; private set; }
    }
}