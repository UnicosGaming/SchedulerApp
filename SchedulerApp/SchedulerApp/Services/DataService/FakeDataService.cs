using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.DataService
{
    public class FakeDataService : IDataService
    {
        List<Schedule> items;

        public FakeDataService()
        {
            InitializeFakeData();
        }
        public Task Delete(string id) => throw new NotImplementedException();
        public Task<IEnumerable<Schedule>> Get()
        {
            return Task.FromResult(items as IEnumerable<Schedule>);
        }
        public Task<Schedule> Get(string id) => throw new NotImplementedException();
        public Task<Schedule> Save(Schedule item)
        {
            Debug.WriteLine($"[DATA SERVICE] Save {item.Id}");

            return Task.FromResult(item);
        }

        private void InitializeFakeData()
        {
            items = new List<Schedule>
            {
                new Schedule
                {
                    Id = Guid.NewGuid().ToString(),
                    Competition = "Competition A",
                    Date = new DateTime(2019,9,13, 12,30,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                },
                new Schedule
                {
                    Id = Guid.NewGuid().ToString(),
                    Competition = "Competition B",
                    Date = new DateTime(2019,9,15, 20,0,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                },
                new Schedule
                {
                    Id = Guid.NewGuid().ToString(),
                    Competition = "Competition C",
                    Date = new DateTime(2019,10,6, 16,0,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                },
                new Schedule
                {
                    Id = Guid.NewGuid().ToString(),
                    Competition = "Competition D",
                    Date = new DateTime(2019,9,30, 20,0,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                }
            };
        }
    }
}
