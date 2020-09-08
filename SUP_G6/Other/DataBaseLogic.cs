using Npgsql;
using SUP_G6.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SUP_G6.Other
{
    class DataBaseLogic
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["sup_db6"].ConnectionString;

        #region CREATE
        public int AddPlayer(Player player)
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
    }
}
