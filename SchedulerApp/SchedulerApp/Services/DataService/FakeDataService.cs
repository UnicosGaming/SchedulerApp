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
        List<Schedule> items;

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
        public Task<IEnumerable<Schedule>> Get()
        {
            var results = items.FindAll(x => x.Date >= DateTime.UtcNow);
            return Task.FromResult( results as IEnumerable<Schedule>);
        }
        public Task<Schedule> Get(string id) => throw new NotImplementedException();
        public Task<Schedule> Save(Schedule schedule)
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
            items = new List<Schedule>
            {
                new Schedule(Guid.NewGuid().ToString())
                {
                    Competition = "Competition A",
                    Date = new DateTime(2019,9,13, 12,30,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                },
                new Schedule(Guid.NewGuid().ToString())
                {
                    Competition = "Competition B",
                    Date = new DateTime(2020,9,15, 20,0,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                },
                new Schedule(Guid.NewGuid().ToString())
                {
                    Competition = "Competition C",
                    Date = new DateTime(2019,10,6, 16,0,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                },
                new Schedule(Guid.NewGuid().ToString())
                {
                    Competition = "Competition D",
                    Date = new DateTime(2020,12,30, 20,0,0),
                    Stream = "https://twitch.tv/UnicosGaming"
                }
            };
        }
    }
}
