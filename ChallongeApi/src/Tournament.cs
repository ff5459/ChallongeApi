using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChallongeApi
{
    /// <summary>
    /// The <see cref="Tournament"/> is a <see cref="Type"/>, used for operations with tournaments.
    /// </summary>
    public class Tournament
    {
        /// <summary>
        /// Constructor for <see cref="JsonConvert"/> to work with.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Tournament() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Constructor for initializing <paramref name="user"/>, <paramref name="id"/> and working with tournaments.
        /// <para/>
        /// Note: You do not always need <paramref name="id"/> to be initialized, call <see cref="Tournament(User)"/> instead.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"> Id of scoped tournament.</param>
        public Tournament(User user, string id)
        {
            this.user = user;
            this.id = id;
        }

        /// <summary>
        /// Constructor for initializing <paramref name="user"/> and working with tournaments.
        /// </summary>
        public Tournament(User user)
        {
            this.user = user;
        }

        /// <summary>
        /// Creates a new tournament.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/create">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> Create(Dictionary<string, string> parameters)
        {
            if (!parameters.ContainsKey("tournament[name]") || !parameters.ContainsKey("tournament[url]"))
                throw new ArgumentNullException(nameof(parameters), "Missing tournament[name] and/or tournament[url]");
            return await User.ParseAndFetch(MethodType.POST, TPATH + ".json", parameters);
        }

        /// <summary>
        /// Retrieves a single tournament record created with your account.
        /// </summary>
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/show">See documentation</see>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> Show(Dictionary<string, string>? parameters)
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.GET, $"{TPATH}/{id}.json", parameters);
        }

        /// <summary>
        /// Updates a tournament's attributes.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/update">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> Update(Dictionary<string, string>? parameters)
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.PUT, $"{TPATH}/{id}.json", parameters);
        }

        /// <summary>
        /// Deletes a tournament along with all its associated records. There is no undo, so use with care!
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/destroy">See documentation</see>
        /// </summary>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> Destroy()
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.DELETE, $"{TPATH}/{id}.json", null);
        }

        /// <summary>
        /// Marks participants who have not checked in as inactive.
        /// <br/>
        /// Moves inactive participants to bottom seeds(ordered by original seed).
        /// <br/>
        /// Transitions the tournament state from 'checking_in' to 'checked_in'
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/process_check_ins">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> ProcessCheckIns(Dictionary<string, string>? parameters)
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.POST, $"{TPATH}/{id}/process_check_ins.json", parameters);
        }

        /// <summary>
        /// Makes all participants active and clears their checked_in_at times.
        /// <br/>
        /// Transitions the tournament state from 'checking_in' or 'checked_in' to 'pending'
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/abort_check_in">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> AbortCheckIns(Dictionary<string, string>? parameters)
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.POST, $"{TPATH}/{id}/abort_check_in.json", parameters);
        }

        /// <summary>
        /// Starts a tournament, opening up first round matches for score reporting. The tournament must have at least 2 participants.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/start">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> Start(Dictionary<string, string>? parameters)
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.POST, $"{TPATH}/{id}/start.json", parameters);
        }

        /// <summary>
        /// Finalizes a tournament that has had all match scores submitted, rendering its results permanent.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/finalize">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> Finalize(Dictionary<string, string>? parameters)
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.POST, $"{TPATH}/{id}/finalize.json", parameters);
        }

        /// <summary>
        /// Resets a tournament, clearing all of its scores and attachments.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/reset">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> Reset(Dictionary<string, string>? parameters)
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.POST, $"{TPATH}/{id}/reset.json", parameters);
        }

        /// <summary>
        /// Sets the state of the tournament to start accepting predictions.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/open_for_predictions">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> OpenForPredictions(Dictionary<string, string>? parameters)
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.POST, $"{TPATH}/{id}/open_for_predictions.json", parameters);
        }

        /// <summary>
        /// Retrieves a tournament's participant list.
        /// </summary>
        /// <returns>A set of participants as <see cref="List{Participant}"/></returns>
        public async Task<List<Participant>> GetParticipants()
        {
            List<Participant> ParticipantsList = new List<Participant>();
            var Result = await User.Fetch(MethodType.GET, $"{TPATH}/{id}/participants.json", null);
            JArray Participants = JArray.Parse(Result);
            foreach (JObject participantJObject in Participants)
            {
                string ResultCut = participantJObject.ToString()[20..^1];
                Participant participant = JsonConvert.DeserializeObject<Participant>(ResultCut)!;
                ParticipantsList.Add(participant);
            }
            return ParticipantsList;
        }

        /// <summary>
        /// Adds a participant to a tournament (up until it is started).
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/participants/create">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>Response as <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JObject> AddParticipant(Dictionary<string, string>? parameters)
        {
            if (id is null)
                throw new ArgumentNullException(id, "id is not initialized.");
            return await User.ParseAndFetch(MethodType.POST, $"{TPATH}/{id}/participants.json", parameters);
        }

        [JsonProperty]
        public bool accept_attachments { get; private set; }
        [JsonProperty]
        public bool allow_participant_match_reporting { get; private set; }
        [JsonProperty]
        public bool anonymous_voting { get; private set; }
        [JsonProperty]
        public object? category { get; private set; }
        [JsonProperty]
        public object? check_in_duration { get; private set; }
        [JsonProperty]
        public object? completed_at { get; private set; }
        [JsonProperty]
        public DateTime created_at { get; private set; }
        [JsonProperty]
        public bool created_by_api { get; private set; }
        [JsonProperty]
        public bool credit_capped { get; private set; }
        [JsonProperty]
        public string? description { get; private set; }
        [JsonProperty]
        public int game_id { get; private set; }
        [JsonProperty]
        public bool group_stages_enabled { get; private set; }
        [JsonProperty]
        public bool hide_forum { get; private set; }
        [JsonProperty]
        public bool hide_seeds { get; private set; }
        [JsonProperty]
        public bool hold_third_place_match { get; private set; }
        [JsonProperty]
        public int max_predictions_per_user { get; private set; }
        [JsonProperty]
        public string? name { get; private set; }
        [JsonProperty]
        public bool notify_users_when_matches_open { get; private set; }
        [JsonProperty]
        public bool notify_users_when_the_tournament_ends { get; private set; }
        [JsonProperty]
        public bool open_signup { get; private set; }
        [JsonProperty]
        public int participants_count { get; private set; }
        [JsonProperty]
        public int prediction_method { get; private set; }
        [JsonProperty]
        public object? predictions_opened_at { get; private set; }
        [JsonProperty]
        public bool _private { get; private set; }
        [JsonProperty]
        public int progress_meter { get; private set; }
        [JsonProperty]
        public string? pts_for_bye { get; private set; }
        [JsonProperty]
        public string? pts_for_game_tie { get; private set; }
        [JsonProperty]
        public string? pts_for_game_win { get; private set; }
        [JsonProperty]
        public string? pts_for_match_tie { get; private set; }
        [JsonProperty]
        public string? pts_for_match_win { get; private set; }
        [JsonProperty]
        public bool quick_advance { get; private set; }
        [JsonProperty]
        public string? ranked_by { get; private set; }
        [JsonProperty]
        public bool require_score_agreement { get; private set; }
        [JsonProperty]
        public string? rr_pts_for_game_tie { get; private set; }
        [JsonProperty]
        public string? rr_pts_for_game_win { get; private set; }
        [JsonProperty]
        public string? rr_pts_for_match_tie { get; private set; }
        [JsonProperty]
        public string? rr_pts_for_match_win { get; private set; }
        [JsonProperty]
        public bool sequential_pairings { get; private set; }
        [JsonProperty]
        public bool show_rounds { get; private set; }
        [JsonProperty]
        public object? signup_cap { get; private set; }
        [JsonProperty]
        public object? start_at { get; private set; }
        [JsonProperty]
        public DateTime started_at { get; private set; }
        [JsonProperty]
        public object? started_checking_in_at { get; private set; }
        [JsonProperty]
        public string? state { get; private set; }
        [JsonProperty]
        public int swiss_rounds { get; private set; }
        [JsonProperty]
        public bool teams { get; private set; }
        [JsonProperty]
        public string[]? tie_breaks { get; private set; }
        [JsonProperty]
        public string? tournament_type { get; private set; }
        [JsonProperty]
        public DateTime updated_at { get; private set; }
        [JsonProperty]
        public string? url { get; private set; }
        [JsonProperty]
        public string? description_source { get; private set; }
        [JsonProperty]
        public object? subdomain { get; private set; }
        [JsonProperty]
        public string? full_challonge_url { get; private set; }
        [JsonProperty]
        public string? live_image_url { get; private set; }
        [JsonProperty]
        public object? sign_up_url { get; private set; }
        [JsonProperty]
        public bool review_before_finalizing { get; private set; }
        [JsonProperty]
        public bool accepting_predictions { get; private set; }
        [JsonProperty]
        public bool participants_locked { get; private set; }
        [JsonProperty]
        public string? game_name { get; private set; }
        [JsonProperty]
        public bool participants_swappable { get; private set; }
        [JsonProperty]
        public bool team_convertable { get; private set; }
        [JsonProperty]
        public bool group_stages_were_started { get; private set; }

        /// <summary>
        /// <see cref="user"/> is a property represents <see cref="User"/>
        /// </summary>
        public User user { get; internal set; }

        /// <summary>
        /// <see cref="id"/> is a property represents an id of scoped tournament.
        /// </summary>
        public string? id { get; }

        /// <summary>
        /// <see cref="TPATH"/> is a constant for path.
        /// </summary>
        private const string TPATH = "tournaments";
    }
}
