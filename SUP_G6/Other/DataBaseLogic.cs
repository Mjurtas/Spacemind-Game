using Npgsql;
using SUP_G6.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SUP_G6.Other
{
    public static class DataBaseLogic
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbLocal"].ConnectionString;

        #region CREATE
        public static int AddPlayer(Player player)
        {
            string stmt = "INSERT INTO player(name) values (@name) returning id;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("name", player.Name);
                    command.Parameters.AddWithValue("id", player.Id);
                    int id = (int)command.ExecuteScalar();
                    return id;
                }
            }
        }
        #endregion

        #region READ
        public static Player GetPlayer(int id)
        {
            string stmt = "select id, name from player where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player = new Player
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"]
                            };
                        }
                    }
                }
                return player;
            }
        }

        public static IEnumerable<Player> GetPlayers()
        {
            string stmt = "select id, name from player";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = null;
                List<Player> players = new List<Player>();
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player = new Player
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"]
                            };
                            players.Add(player);
                        }
                    }
                }
                return players;
            }
        }
        #endregion
    }
}
