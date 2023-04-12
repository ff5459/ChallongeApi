using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

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
        }
        [TestMethod()]
        public void ShowTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "include_participants", "1" },
                { "include_matches", "1" }
            };
            var task = Task.Run(() => Tournament.Show(parameters));
            task.Wait();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "tournament[name]", "UpdateTest" },
                { "tournament[tournament_type]", "double elimination" },
                { "tournament[accept_attachments]", "true" }
            };
            Task<JObject> task = Task.Run(() => Tournament.Update(parameters));
            task.Wait();
        }

        [TestMethod()]
        public void DestroyTest()
        {
            Task<JObject> task = Task.Run(Tournament.Destroy);
            task.Wait();
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
        }

        [TestMethod()]
        public void GetParticipantsTest()
        {
            Task<List<Participant>> task = Task.Run(Tournament.GetParticipants);
            task.Wait();
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
        }

        [TestMethod()]
        public void ShowParticipantTest()
        {
            var part = Tournament.GetParticipants().Result;
            var task = Task.Run(() => part.First().Show(true));
            task.Wait();
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
        }

        [TestMethod()]
        public void CheckInParticipantTest()
        {
            var part = Tournament.GetParticipants().Result.First();
            var task = Task.Run(part.CheckIn);
            task.Wait();
        }

        [TestMethod()]
        public void UndoCheckInTest()
        {
            var part = Tournament.GetParticipants().Result.First();
            var task = Task.Run(part.UndoCheckIn);
            task.Wait();
        }

        [TestMethod()]
        public void DestroyParticipantTest()
        {
            var part = Tournament.GetParticipants().Result.First();
            var task = Task.Run(part.Destroy);
            task.Wait();
        }

        [TestMethod()]
        public void ClearParticipantsTest()
        {
            var task = Task.Run(Tournament.Clear);
            task.Wait();
        }

        [TestMethod()]
        public void RandomizeTest()
        {
            var task = Task.Run(Tournament.Randomize);
            task.Wait();
        }

        [TestMethod()]
        public void GetMatchesTest()
        {
            var task = Task.Run(Tournament.GetMatches);
            task.Wait();
        }

        [TestMethod()]
        public void ShowMatchTest()
        {
            var match = Tournament.GetMatches().Result.First();
            var task = Task.Run(() => match.Show(true));
            task.Wait();
        }

        [TestMethod()]
        public void UpdateMatchTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "match[scores_csv]", "15-0, 10-5, 5-19" }
            };
            var match = Tournament.GetMatches().Result.First();
            var task = Task.Run(() => match.Update(parameters));
            task.Wait();
        }

        [TestMethod()]
        public void ReopenTest()
        {
            var match = Tournament.GetMatches().Result.First(m => m.state == "complete");
            var task = Task.Run(match.Reopen);
            task.Wait();
        }

        [TestMethod()]
        public void MarkAsUnderwayTest()
        {
            var match = Tournament.GetMatches().Result.First();
            var task = Task.Run(match.MarkAsUnderway);
            task.Wait();
        }

        [TestMethod()]
        public void UnMarkAsUnderwayTest()
        {
            var match = Tournament.GetMatches().Result.First();
            var task = Task.Run(match.UnMarkAsUnderway);
            task.Wait();
        }

        [TestMethod()]
        public void GetAttachmentsTest()
        {
            var match = Tournament.GetMatches().Result.First();
            var task = Task.Run(match.GetAttachments);
            task.Wait();
        }

        [TestMethod()]
        public void CreateAttachmentTest()
        {
            var parameters = new Dictionary<string, string>
            {
                { "match_attachment[url]", "https://google.com" }
            };
            var match = Tournament.GetMatches().Result.First();
            var task = Task.Run(() => match.CreateAttachment(parameters));
            task.Wait();
        }

        [TestMethod()]
        public void ShowAttachmentTest()
        {
            var match = Tournament.GetMatches().Result.First();
            var attachment = match.GetAttachments().Result.First();
            var task = Task.Run(attachment.Show);
            task.Wait();
        }

        [TestMethod()]
        public void UpdateAttachmentTest()
        {
            var parameters = new Dictionary<string, string>
            {
                {
                    "match_attachment[url]", "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
                }
            };
            var match = Tournament.GetMatches().Result.First();
            var attachment = match.GetAttachments().Result.First();
            var task = Task.Run(() => attachment.Update(parameters));
            task.Wait();
        }

        [TestMethod()]
        public void DestroyAttachmentTest()
        {
            var match = Tournament.GetMatches().Result.First();
            var attachment = match.GetAttachments().Result.First();
            var task = Task.Run(attachment.Destroy);
            task.Wait();
        }
    }
}