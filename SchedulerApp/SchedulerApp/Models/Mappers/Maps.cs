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
                        Name = reader["name"].ToString()
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
    }
}
