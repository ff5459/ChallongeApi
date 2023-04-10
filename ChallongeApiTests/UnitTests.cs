using ChallongeApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace ChallongeApi.Tests
{
    [TestClass()]
    public class UnitTests
    {
        User user;
        Tournament Tournament;
        string username = File.ReadAllText(AppContext.BaseDirectory + "username.txt");
        string token = File.ReadAllText(AppContext.BaseDirectory + "token.txt");
        public UnitTests()
        {
            user = new User(username, token);
            Tournament = new Tournament(user, $"{new Random(1).Next()}");
        }
        [TestMethod()]
        public void GetTournamentsTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "state", "ended" },
                { "created_after", "2000-10-10" },
                { "created_before", "2069-10-10" },
            };
            Task<List<Tournament>> task = Task.Run(() => user.GetTournaments(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "tournament[name]", "testtourney" },
                { "tournament[url]", Tournament.id! }
            };
            Task<JObject> task = Task.Run(() => Tournament.Create(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }
        [TestMethod()]
        public void ShowTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "include_participants", "1" },
                { "include_matches", "1" }
            };
            Task<JObject> task = Task.Run(() => Tournament.Show(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "tournament[name]", "UpdateTest" },
                { "tournament[tournament_type]", "double elimination" }
            };
            Task<JObject> task = Task.Run(() => Tournament.Update(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void DestroyTest()
        {
            Task<JObject> task = Task.Run(Tournament.Destroy);
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void ProcessCheckInsTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "include_participants", "1" },
                { "include_matches", "1" }
            };
            Task<JObject> task = Task.Run(() => Tournament.ProcessCheckIns(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void AbortCheckInsTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "include_participants", "1" },
                { "include_matches", "1" }
            };
            Task<JObject> task = Task.Run(() => Tournament.AbortCheckIns(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void StartTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "include_participants", "1" },
                { "include_matches", "1" }
            };
            Task<JObject> task = Task.Run(() => Tournament.Start(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void FinalizeTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "include_participants", "1" },
                { "include_matches", "1" }
            };
            Task<JObject> task = Task.Run(() => Tournament.Finalize(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void ResetTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "include_participants", "1" },
                { "include_matches", "1" }
            };
            Task<JObject> task = Task.Run(() => Tournament.Reset(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void OpenForPredictionsTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "include_participants", "1" },
                { "include_matches", "1" }
            };
            Task<JObject> task = Task.Run(() => Tournament.OpenForPredictions(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void GetParticipantsTest()
        {
            Task<List<Participant>> task = Task.Run(Tournament.GetParticipants);
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void AddParticipantTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "participant[name]", "pamparam" },
                { "participant[seed]", "1" }
            };
            Task<JObject> task = Task.Run(() => Tournament.AddParticipant(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void ShowParticipantTest()
        {
            var part = Tournament.GetParticipants().Result;
            var task = Task.Run(() => part.First().Show(true));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void UpdateParticipantTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "participant[name]", "new pamparam" }
            };
            var part = Tournament.GetParticipants().Result.First();
            var task = Task.Run(() => part.Update(parameters));
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod()]
        public void CheckInParticipantTest()
        {
            var part = Tournament.GetParticipants().Result.First();
            var task = Task.Run(part.CheckIn);
            task.Wait();
        }
    }
}