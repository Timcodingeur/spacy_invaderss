using System;
using System.Diagnostics;
using System.Threading;

namespace enemie
{
    internal class GameEngine
    {
        private Player player = new();
        private EnemyManager enemyManager = new();
        private const int FrameDelay = 16;

        /// <summary>
        /// Displays the main menu.
        /// </summary>
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("####################################");
            Console.WriteLine("###       SPACY INVADERS         ###");
            Console.WriteLine("####################################");
            Console.WriteLine();
            Console.WriteLine("1. Jouer");
            Console.WriteLine("2. High Scores");
            Console.WriteLine("3. Quitter");
            Console.WriteLine();
            Console.Write("Choisi (1/2/3): ");

            var choice = Console.ReadKey(true).KeyChar;

            switch (choice)
            {
                case '1':
                    Console.Clear();
                    new GameEngine().Start();
                    break;
                case '2':
                    DatabaseHelper.InitializeDatabase();
                    DatabaseHelper.DisplayHighScores();
                    break;
                case '3':
                    QuitGame();
                    break;
                default:
                    Console.WriteLine("Option invalide");
                    Console.ReadKey();
                    ShowMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        private static void QuitGame()
        {
            Console.Clear();
            Console.WriteLine("Merci d'avoir jouer!");
            Environment.Exit(0);
        }

        /// <summary>
        /// Starts the game and maintains the game loop until the player finishes.
        /// </summary>
        public void Start()
        {
            DatabaseHelper.InitializeDatabase();
            enemyManager.SpawnEnemies();
            player.Spawn();

            while (true)
            {
                Console.CursorVisible = false;
                enemyManager.MoveEnemies();
                player.Update();

                Thread.Sleep(FrameDelay);

                if (enemyManager.NumberOfEnemies == 0)
                {
                    EnemyManager.Enemies.Clear();
                    Console.WriteLine("\nBravo, tu a gagner!");
                    AddScore();
                    Console.WriteLine("Apuillez sur une touche pour continuer");
                    Console.ReadKey(true);
                    break;
                }

                if (Player.PlayerLives <= 0)
                {
                    EnemyManager.Enemies.Clear();
                    Console.WriteLine("\nTa perdu (RIP)");
                    AddScore();

                    string relativePath = "../perdu.html";
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string fullPath = Path.GetFullPath(Path.Combine(currentDirectory, relativePath));

                    try
                    {
                        Process.Start("cmd", $"/c start {fullPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    Console.WriteLine("Apuiller sur une touche pour continuer");
                    Console.ReadKey(true);
                    break;
                }
            }
        }

        /// <summary>
        /// Adds the player's score to the database.
        /// </summary>
        private void AddScore()
        {
            Console.Clear();
            Console.Write("Quelle est votre pseudo?: ");
            string pseudo = Console.ReadLine() ?? "Chaipo";
            int score = enemyManager.Score;
            DatabaseHelper.AddScore(pseudo, score);
        }
    }
}
