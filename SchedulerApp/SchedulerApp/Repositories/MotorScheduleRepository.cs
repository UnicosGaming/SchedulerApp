﻿using SchedulerApp.Models;
using SchedulerApp.Services.DataService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public class MotorScheduleRepository : IScheduleRepository<MotorSchedule>
    {
        private readonly ISqlDataService _sqlDataService;

        public MotorScheduleRepository(ISqlDataService sqlDataService)
        {
            _sqlDataService = sqlDataService;
        }

        public async Task Save(MotorSchedule schedule)
        {
            var pId = new SqlParameter("@id", schedule.Id);
            var pIdTeam = new SqlParameter("@id_team", schedule.Team.Id);
            var pCodeTeam = new SqlParameter("@code_team", schedule.Team.Code);
            var pCompetition = new SqlParameter("@competition", schedule.Competition);
            var pStream = new SqlParameter("@stream", schedule.Stream);
            var pDate = new SqlParameter("@date", schedule.Date);
            var pCar = new SqlParameter("@car", schedule.Car);
            var pTrack = new SqlParameter("@track", schedule.Track);

            try
            {
                await _sqlDataService.ExecuteNonQueryStoredProcedure("sp_InsertMotorSchedule", new[] { pId, pIdTeam, pCodeTeam, pCompetition, pStream, pDate, pCar, pTrack });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
