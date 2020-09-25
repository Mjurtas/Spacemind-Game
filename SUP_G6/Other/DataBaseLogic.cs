using Npgsql;
using SUP_G6.DataTypes;
using SUP_G6.Interface;
using SUP_G6.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SUP_G6.Other
{
    public static class DataBaseLogic
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbServer"].ConnectionString;
#region CREATE
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

        #region CREATE GAME RESULT

        /* Adds gameresult by when game is finished*/

        public static int AddGameResult(GameResult gameResult)
        {
            string stmt = $"INSERT INTO game_result (player_id, tries, win, level, time, score ) values (@Id, @Tries, @Win, @Level, @Time, @score) returning game_id;";


            using (var conn = new NpgsqlConnection(connectionString))


            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {

                    conn.Open();
                    conn.TypeMapper.MapEnum<Level>("level");
                    command.Parameters.AddWithValue("Id", gameResult.PlayerId);
                    command.Parameters.AddWithValue("Tries", gameResult.Tries);
                    command.Parameters.AddWithValue("Win", gameResult.Win);
                    command.Parameters.AddWithValue("level", gameResult.Level);
                    command.Parameters.AddWithValue("time", gameResult.ElapsedTimeInSeconds);
                    command.Parameters.AddWithValue("score", gameResult.Score);
                    int id = (int)command.ExecuteScalar();
                    return id;
                }
            }
        }
        #endregion
        #endregion

        #region READ
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
        public static ObservableCollection<Player> GetDiligentPlayers()
        {
            string stmt = "SELECT game_result.player_id, player.name, COUNT(game_result.player_id) FROM game_result INNER JOIN player ON game_result.player_id = player.player_id GROUP BY game_result.player_id, player.name ORDER BY COUNT DESC LIMIT 3; ";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = null;
                ObservableCollection<Player> diligentPlayers = new ObservableCollection<Player>();

                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player = new Player()
                            {
                                Id = (int)reader["player_id"],
                                Name = (string)reader["name"],
                                NumberOfGamesPlayed = (Int64)reader["COUNT"]
                            };
                            diligentPlayers.Add(player);
                        }
                    }
                }
                return new ObservableCollection<Player>(diligentPlayers);
            }
        }
        public static ObservableCollection<Player> GetDiligentPlayersOnLevel(Level level)
        {
            string stmt = "SELECT game_result.player_id, player.name, COUNT(game_result.player_id) FROM game_result INNER JOIN player ON game_result.player_id = player.player_id WHERE game_result.level = @level GROUP BY game_result.player_id, player.name, game_result.level ORDER BY COUNT DESC LIMIT 3;";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = null;
                ObservableCollection<Player> diligentPlayers = new ObservableCollection<Player>();

                conn.Open();
                conn.TypeMapper.MapEnum<Level>("level");
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("level", level);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player = new Player()
                            {
                                Id = (int)reader["player_id"],
                                DisplayName = (string)reader["name"],
                                DisplayCount = (Int64)reader["COUNT"]
                            };
                            diligentPlayers.Add(player);
                        }
                    }
                }
                return new ObservableCollection<Player>(diligentPlayers);
            }
        }
        #endregion



        #region READ GAME RESULT




        public static ObservableCollection<IExistInDatabase> GetGameResults()
        {
            string stmt = "select game_id, player.player_id, player.name, tries, win, level from game_result inner join player ON game_result.player_id=player.player_id where win = true";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                GameResult gameResult = null;
                ObservableCollection<IExistInDatabase> gameResults = new ObservableCollection<IExistInDatabase>();

                conn.Open();
                conn.TypeMapper.MapEnum<Level>("level");
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
                                PlayerName = (string)reader["name"],
                                //ElapsedTimeInSeconds = (double)reader["time"],
                                Tries = (int)reader["tries"],
                                Win = (bool)reader["win"],
                                Level = (Level)reader["level"],
                                //Score = (int)reader["score"]
                            };
                            gameResults.Add(gameResult);
                        }
                    }
                }
                return gameResults;
            }
        }

        public static ObservableCollection<GameResult> GetGameResultsBy(Level level, string sort)
        {
            string stmt = "";
            if (sort == "time")
            {
                stmt = "SELECT player.name, tries, time FROM game_result INNER JOIN player ON game_result.player_id=player.player_id WHERE win = true AND level = @level ORDER BY time ASC, tries ASC LIMIT 3;";
            }
            else if (sort == "tries")
            {
                stmt = "SELECT player.name, tries, time FROM game_result INNER JOIN player ON game_result.player_id=player.player_id WHERE win = true AND level = @level ORDER BY tries ASC, time ASC LIMIT 3;";
            }
            using (var conn = new NpgsqlConnection(connectionString))
            {
                GameResult gameResult = null;
                ObservableCollection<GameResult> gameResults = new ObservableCollection<GameResult>();

                conn.Open();
                conn.TypeMapper.MapEnum<Level>("level");
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("level", level);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gameResult = new GameResult()
                            {
                                DisplayName = (string)reader["name"],
                                DisplayCount = (int)reader["tries"],
                                ElapsedTimeInSeconds = (double)reader["time"]
                            };
                            gameResults.Add(gameResult);
                        }
                    }
                }
                return gameResults;
            }
        }
        #endregion
        #endregion
    }
}
