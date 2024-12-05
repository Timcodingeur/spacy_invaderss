using MySql.Data.MySqlClient;
using System;

namespace enemie
{
    public static class DatabaseHelper
    {
        private static string server = "localhost";
        private static string database = "db_space_invaders";
        private static string user = "root";
        private static string password = "root";
        private static string port = "6033";

        private static string ConnectionStringWithoutDatabase =>
            $"server={server};port={port};user id={user}; password={password};";

        private static string ConnectionStringWithDatabase =>
            $"server={server};port={port};user id={user}; password={password}; database={database};";

        /// <summary>
        /// Initialise la base de données et les tables si elles n'existent pas.
        /// </summary>
        public static void InitializeDatabase()
        {
            using MySqlConnection conn = new(ConnectionStringWithoutDatabase);
            try
            {
                conn.Open();

                // Crée la base de données si elle n'existe pas
                string createDbQuery = $"CREATE DATABASE IF NOT EXISTS {database};";
                MySqlCommand createDbCommand = new(createDbQuery, conn);
                createDbCommand.ExecuteNonQuery();

                // Utilise la base de données
                conn.ChangeDatabase(database);

                // Crée la table `t_joueur` si elle n'existe pas
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS t_joueur (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        jouPseudo VARCHAR(50) NOT NULL,
                        jouNombrePoints INT NOT NULL
                    );";
                MySqlCommand createTableCommand = new(createTableQuery, conn);
                createTableCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erreur lors de l'initialisation de la base de données : {ex.Message}");
            }
        }

        /// <summary>
        /// Ajoute le score du joueur à la base de données.
        /// </summary>
        /// <param name="pseudo">Pseudo du joueur.</param>
        /// <param name="score">Score du joueur.</param>
        public static void AddScore(string pseudo, int score)
        {
            using MySqlConnection conn = new(ConnectionStringWithDatabase);
            try
            {
                conn.Open();

                string insertQuery = "INSERT INTO t_joueur (jouPseudo, jouNombrePoints) VALUES (@pseudo, @score);";

                MySqlCommand insertCommand = new(insertQuery, conn);
                insertCommand.Parameters.AddWithValue("@pseudo", pseudo);
                insertCommand.Parameters.AddWithValue("@score", score);

                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Erreur lors de l'ajout du score : {e.Message}");
            }
        }

        /// <summary>
        /// Affiche les meilleurs scores.
        /// </summary>
        public static void DisplayHighScores()
        {
            using MySqlConnection conn = new(ConnectionStringWithDatabase);
            try
            {
                conn.Open();

                string selectQuery = "SELECT jouPseudo, jouNombrePoints FROM t_joueur ORDER BY jouNombrePoints DESC LIMIT 5;";
                MySqlCommand selectCommand = new(selectQuery, conn);

                using MySqlDataReader reader = selectCommand.ExecuteReader();

                Console.Clear();
                Console.WriteLine("Pseudo : NB Point");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["jouPseudo"]} : {reader["jouNombrePoints"]}");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Erreur lors de la récupération des scores : {e.Message}");
            }
            finally
            {
                Console.Write("Appuyez sur une touche pour continuer...");
                Console.ReadLine();
                GameEngine.ShowMainMenu();
            }
        }
    }
}
