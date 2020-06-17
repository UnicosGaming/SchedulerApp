using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.DataService
{
    public class FakeDataService : IDataService
    {
        List<TeamSchedule> items;

        public FakeDataService()
        {
            InitializeFakeData();
        }
        public void Delete(string id)
        {
            var item = items.Find(x => x.Id == id);

            if (item != null)
            {
                items.Remove(item);
            }
        }
        public Task<IEnumerable<TeamSchedule>> Get()
        {
            var results = items.FindAll(x => x.Date >= DateTime.UtcNow);
            return Task.FromResult(results as IEnumerable<TeamSchedule>);
        }
        public Task<TeamSchedule> Get(string id) => throw new NotImplementedException();
        public Task<TeamSchedule> Save(TeamSchedule schedule)
        {
            Debug.WriteLine($"[DATA SERVICE] Save {schedule.Id}");

            var item = items.Find(x => x.Id == schedule.Id);

            if (item == null)
                items.Add(schedule);
            //else
            //    schedule.CopyTo(item);

            return Task.FromResult(schedule);
        }

        private void InitializeFakeData()
        {
            items = new List<TeamSchedule>
            {
                new TeamSchedule()
                {
                    Competition = "Competition A",
                    Opponent = "Opponent A",
                    Date = new DateTime(2019,9,13, 12,30,0),
                    Stream = "https://twitch.tv/UnicosGaming",
                },
                new TeamSchedule()
                {
                    Competition = "Competition B",
                    Opponent = "Opponent B",
                    Date = new DateTime(2020,9,15, 20,0,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                },
                new TeamSchedule()
                {
                    Competition = "Competition C",
                    Opponent = "Opponent C",
                    Date = new DateTime(2019,10,6, 16,0,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                },
                new TeamSchedule()
                {
                    Competition = "Competition D",
                    Opponent = "Opponent D",
                    Date = new DateTime(2020,12,30, 20,0,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                }
            };
        }
    }
}
