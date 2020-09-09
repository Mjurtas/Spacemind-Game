using Npgsql;
using SUP_G6.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text;

namespace SUP_G6.Other
{
    public static class DataBaseLogic
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbServer"].ConnectionString;

        #region CREATE PLAYER
        public static int AddPlayer(Player player)
        {
            string stmt = "INSERT INTO player(name) values (@name) returning player_id;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("name", player.Name);
                    command.Parameters.AddWithValue("player_id", player.Id);
                    int id = (int)command.ExecuteScalar();
                    return id;
                }
            }
        }
        #endregion

        #region READ PLAYER
        public static Player GetPlayer(int id)
        {
            string stmt = "select player_id, name from player where player_id=@player_id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("player_id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player = new Player
                            {
                                Id = (int)reader["player_id"],
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
            string stmt = "select player_id, name from player";

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
                                Id = (int)reader["player_id"],
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

        #region CREATE GAME RESULT


        public static int AddGameResult(GameResult gameResult)
        {
            string stmt = "INSERT INTO game_result (player_id, time_start, tries, level ) values (@PlayerId, @Time_start, @Tries, @Level ) returning game_id;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("player_id", gameResult.PlayerId);
                    command.Parameters.AddWithValue("time_start", gameResult.Time_start);

                    command.Parameters.AddWithValue("tries", gameResult.Tries);
                    command.Parameters.AddWithValue("player_id", gameResult.Level);
                    int id = (int)command.ExecuteScalar();
                    return id;
                }
            }
        }
        #endregion

        #region READ GAME RESULT

        public static GameResult GetGameResult(int id)
        {
            string stmt = "select game_id, player_id, time_start, time_end, tries, win, level from game_result where game_id=@game_id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                GameResult gameResult  =  null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("game_id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gameResult = new GameResult()
                            {
                                GameId = (int)reader["game_id"],
                                PlayerId = (int)reader["player_id"],
                                Time_start = (DateTime)reader["time_start"],
                                Time_end = (DateTime)reader["time_end"],
                                Tries = (int)reader["tries"],
                                Win = (bool)reader["win"],
                                Level = (string)reader["level"]
                            };
                        }
                    }
                }
                return gameResult;
            }
        }

        public static ObservableCollection<GameResult> GetGameResults()
        {
            string stmt = "select game_id, player_id, time_start, time_end, tries, win, level from game_result where game_id=@game_id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                GameResult gameResult = null;
                ObservableCollection<GameResult> gameResults = new ObservableCollection<GameResult>();
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gameResult = new GameResult()
                            {
                                GameId = (int)reader["game_id"],
                                PlayerId = (int)reader["player_id"],
                                Time_start = (DateTime)reader["time_start"],
                                Time_end = (DateTime)reader["time_end"],
                                Tries = (int)reader["tries"],
                                Win = (bool)reader["win"],
                                Level = (string)reader["level"]
                            };
                            gameResults.Add(gameResult);
                        }
                    }
                }
                return gameResults;
            }
        }
        
        #endregion
    }
}
