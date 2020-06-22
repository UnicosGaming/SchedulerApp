using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace SchedulerApp.Models.Mappers
{
    public static class Maps
    {
        public static Group ToGroup(SqlDataReader reader)
        {
            if (reader.Read())
            {
                return new Group(reader["id"].ToString())
                {
                    Name = reader["name"].ToString(),
                    IsReadOnly = (bool)reader["isreadonly"]
                };
            }

            throw new Exception("Cannot map To Group");
        }

        public static List<Team> ToTeams(SqlDataReader reader)
        {
            var teams = new List<Team>();

            try
            {
                while (reader.Read())
                {
                    teams.Add(new Team(reader["id"].ToString())
                    {
                        Name = reader["name"].ToString(),
                        Code = reader["code"].ToString()
                    });
                }

                return teams;
            }
            catch (Exception)
            {
                throw new Exception("Cannot map To Teams");
            }
        }

        public static Page ToPage(SqlDataReader reader)
        {
            if (reader.Read())
            {
                return new Page(reader["id"].ToString())
                {
                    Name = reader["name"].ToString()
                };
            }

            throw new Exception("Cannot map To Page");
        }

        public static List<Schedule> ToSchedules(SqlDataReader reader)
        {
            var schedules = new List<Schedule>();

            try
            {
                while (reader.Read())
                {
                    schedules.Add(new Schedule(reader["id"].ToString())
                    {
                        Competition = reader["competition"].ToString(),
                        Stream = reader["stream"].ToString(),
                        Date = Convert.ToDateTime(reader["date"].ToString()),
                        Team = new Team(reader["team_id"].ToString())
                        {
                            Name = reader["team_name"].ToString(),
                            Code = reader["team_code"].ToString(),
                            Page = new Page(reader["page_id"].ToString())
                            {
                                Name = reader["page_name"].ToString()
                            }
                        }
                    });
                }

                return schedules;
            }
            catch (Exception)
            {
                throw new Exception("Cannot map To Schedules");
            }
        }

        public static TeamSchedule ToTeamSchedule(SqlDataReader reader)
        {
            if (reader.Read())
            {
                return new TeamSchedule(reader["id"].ToString())
                {
                    Competition = reader["competition"].ToString(),
                    Stream = reader["stream"].ToString(),
                    Date = Convert.ToDateTime(reader["date"].ToString()),
                    Opponent = reader["opponent"].ToString(),
                    Team = new Team(reader["team_id"].ToString())
                    {
                        Name = reader["team_name"].ToString(),
                        Code = reader["team_code"].ToString(),
                        Page = new Page(reader["page_id"].ToString())
                        {
                            Name = reader["page_name"].ToString()
                        }
                    }
                };
            }

            throw new Exception("Cannot map To Page");
        }

        public static MotorSchedule ToMotorSchedule(SqlDataReader reader)
        {
            if (reader.Read())
            {
                return new MotorSchedule(reader["id"].ToString())
                {
                    Competition = reader["competition"].ToString(),
                    Stream = reader["stream"].ToString(),
                    Date = Convert.ToDateTime(reader["date"].ToString()),
                    Car = reader["car"].ToString(),
                    Track = reader["track"].ToString(),
                    Team = new Team(reader["team_id"].ToString())
                    {
                        Name = reader["team_name"].ToString(),
                        Code = reader["team_code"].ToString(),
                        Page = new Page(reader["page_id"].ToString())
                        {
                            Name = reader["page_name"].ToString()
                        }
                    }
                };
            }

            throw new Exception("Cannot map To Page");
        }

    }
}
