using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ChallongeApi
{
    /// <summary>
    /// The main class of Challonge Api.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Constructor for initializing <paramref name="Username"/> and <paramref name="Apitoken"/>.
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Apitoken">
        /// Your challonge token.
        /// <para/>
        /// <seealso href="https://challonge.com/settings/developer">You can get one here</seealso>
        /// </param>
        public User(string Username, string Apitoken)
        {
            this.Username = Username;
            this.Apitoken = Apitoken;
            httpClient.BaseAddress = new Uri($"https://{Username}:{Apitoken}@api.challonge.com/v1");
            //Automatic authorization
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(new UTF8Encoding().GetBytes($"{Username}:{Apitoken}")));
        }

        /// <summary>
        /// Retrieves a set of tournaments created with <see cref="User"/> account.
        /// <para/>
        /// <see href="https://api.challonge.com/v1/documents/tournaments/index">See documentation</see>
        /// </summary>
        /// <param name="parameters">A set of key-value pairs.</param>
        /// <returns>A set of tournaments as <see cref="List{Tournament}"/></returns>
        public async Task<List<Tournament>> GetTournaments(Dictionary<string, string>? parameters)
        {
            List<Tournament> TournamentsList = new List<Tournament>();
            var Result = await Fetch(MethodType.GET, "tournaments.json", parameters);
            JArray Tournaments = JArray.Parse(Result);
            foreach (JObject tournamentJObject in Tournaments)
            {
                string ResultCut = tournamentJObject.ToString()[19..^1];
                Tournament tournament = JsonConvert.DeserializeObject<Tournament>(ResultCut)!;
                tournament.user = this;
                TournamentsList.Add(tournament);
            }
            return TournamentsList;
        }

        /// <summary>
        /// The inner method for making a Http request and get result as <see langword="string"/>.
        /// </summary>
        /// <param name="method"> The <see cref="MethodType"/> for request.</param>
        /// <param name="path">URL part of request.</param>
        /// <param name="arguments">URL arguments of request.</param>
        /// <returns><see cref="HttpResponseMessage"/> as <see langword="string"/></returns>
        internal static async Task<string> Fetch(MethodType method, string path, Dictionary<string, string>? arguments)
        {
            arguments ??= new Dictionary<string, string>();

            string fullpath = $"{httpClient.BaseAddress}/{path}";
            string query = "";

            FormUrlEncodedContent content = new FormUrlEncodedContent(arguments);
            HttpResponseMessage response = new HttpResponseMessage();
            switch (method)
            {
                case MethodType.GET:
                    {
                        foreach (var arg in arguments)
                        {
                            query += $"{arg.Key}={arg.Value}&";
                        }
                        response = await httpClient.GetAsync($"{fullpath}?{query}");
                        break;
                    }
                case MethodType.POST:
                    {
                        response = await httpClient.PostAsync(fullpath, content);
                        break;
                    }
                case MethodType.PUT:
                    {
                        response = await httpClient.PutAsync(fullpath, content);
                        break;
                    }
                case MethodType.DELETE:
                    {
                        foreach (var arg in arguments)
                        {
                            query += $"{arg.Key}={arg.Value}&";
                        }
                        response = await httpClient.DeleteAsync($"{fullpath}?{query}");
                        break;
                    }
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        internal static async Task<JObject> ParseAndFetch(MethodType method, string path, Dictionary<string, string>? arguments)
        {
            var result = await Fetch(method, path, arguments);

            if (result.StartsWith("{"))
                return JObject.Parse(result);
            else
                return new JObject(new JProperty(result));
        }

        /// <remarks>
        /// The <see cref="Username"/> is a <see langword="string"/> property,
        /// that you initialize in <see cref="User(string, string)"/> or <see cref="Api(string, string, string)"/>.
        /// <para>
        /// Note that you have to be registered on <see href="https://challonge.com/"></see> 
        /// </para>
        /// </remarks>
        public string Username { get; }

        /// <summary>
        /// The <see cref="Apitoken"/> is a <see langword="string"/> property represents an api key.
        /// <para>
        /// You can get one <see href="https://challonge.com/settings/developer">here</see>
        /// </para>
        /// </summary>
        public string Apitoken { get; }

        /// <remarks>
        /// The <c>httpClient</c> is a private field, which represents an instance of <see cref="HttpClient"/>.
        /// <br/>
        /// Used to establish connection with <see href="https://api.challonge.com/v1">challonge api</see>
        /// </remarks>
        private static HttpClient httpClient { get; } = new HttpClient();
    }
}