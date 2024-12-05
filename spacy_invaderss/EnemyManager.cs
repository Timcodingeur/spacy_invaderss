using System;
using System.Collections.Generic;

namespace enemie
{
    /// <summary>
    /// Manages the enemies: spawning, movement, and interactions.
    /// </summary>
    public class EnemyManager
    {
        public static List<Enemy> Enemies { get; } = new();
        private static readonly List<Enemy> EnemiesToRemove = new();
        private readonly List<Shot> enemyShots = new();
        private static int direction = 1;
        private static int movesBeforeDrop;
        private static int currentMoves = 0;
        private static int tick = 0;
        public int NumberOfEnemies { get; private set; }
        public int Score { get; private set; }
        private char difficulty;

        /// <summary>
        /// Spawns the enemies at the beginning of the game.
        /// </summary>
        public void SpawnEnemies()
        {
            SelectDifficulty();

            Console.SetCursorPosition(10, 0);
            Console.WriteLine("Score: " + Score);
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    var enemy = new Enemy
                    {
                        Skin = j % 2 == 0 ? "-0_0-" : ">-_-<",
                        Y = j * 2 + 1,
                        X = i * 8
                    };
                    enemy.MoveRight();

                    Enemies.Add(enemy);
                }
            }

            NumberOfEnemies = Enemies.Count;
            direction = 1;
            movesBeforeDrop = Console.WindowWidth - 40;
            currentMoves = 0;
            tick = 0;
        }

        /// <summary>
        /// Moves the enemies and handles their actions.
        /// </summary>
        public void MoveEnemies()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(" Vie: " + Player.PlayerLives);
            Random random = new();
            int shotChance = GetShotChance(random);

            Enemy randomEnemy = null;
            if (Enemies.Count > 0)
            {
                randomEnemy = Enemies[random.Next(Enemies.Count)];
            }

            tick++;

            if (tick >= 20)
            {
                MoveAllEnemies();
                tick = 0;
                currentMoves++;
            }

            RemoveDeadEnemies();
            HandleEnemyShooting(shotChance, randomEnemy, random);
            UpdateEnemyShots();
            CheckEnemyShotsCollision();

            if (currentMoves >= movesBeforeDrop)
            {
                MoveEnemiesDown();
                direction = -direction;
                currentMoves = 0;
            }
        }

        /// <summary>
        /// Updates the player's score when an enemy is destroyed.
        /// </summary>
        private void UpdateScore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Score += 1000 * Player.PlayerLives;
            Console.SetCursorPosition(10, 0);
            Console.WriteLine("Score: " + Score);
        }

        /// <summary>
        /// Selects the game difficulty.
        /// </summary>
        private void SelectDifficulty()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("Select your difficulty level:");
                Console.WriteLine("0 = Trops facile");
                Console.WriteLine("1 = Facile");
                Console.WriteLine("2 = Pas trops dur");
                Console.WriteLine("3 = Dur");
                Console.WriteLine("4 = Très dur");
                Console.WriteLine("5 = Impossible");
                Console.Write("choix: ");
                difficulty = Console.ReadKey(true).KeyChar;
                Console.Clear();
            } while (difficulty < '0' || difficulty > '5');
        }

        /// <summary>
        /// Gets the chance of an enemy shooting based on the difficulty.
        /// </summary>
        private int GetShotChance(Random random)
        {
            return difficulty switch
            {
                '0' => random.Next(1, 35),
                '1' => random.Next(1, 20),
                '2' => random.Next(1, 13),
                '3' => random.Next(1, 7),
                '4' => random.Next(1, 3),
                '5' => random.Next(1, 2),
                _ => random.Next(1, 20),
            };
        }

        /// <summary>
        /// Moves all enemies in the current direction.
        /// </summary>
        private void MoveAllEnemies()
        {
            foreach (var enemy in Enemies)
            {
                if (direction == 1)
                    enemy.MoveRight();
                else
                    enemy.MoveLeft();
            }
        }

        /// <summary>
        /// Removes enemies that have been destroyed.
        /// </summary>
        private void RemoveDeadEnemies()
        {
            foreach (var enemy in EnemiesToRemove)
            {
                NumberOfEnemies--;
                UpdateScore();
                Enemies.Remove(enemy);
            }
            EnemiesToRemove.Clear();
        }

        /// <summary>
        /// Handles the shooting of enemies.
        /// </summary>
        private void HandleEnemyShooting(int shotChance, Enemy randomEnemy, Random random)
        {
            if (shotChance == 1 && randomEnemy != null)
            {
                var shot = new Shot { IsPlayerShot = false, Y = randomEnemy.Y, X = randomEnemy.X };
                enemyShots.Add(shot);
            }
        }

        /// <summary>
        /// Updates the positions of enemy shots.
        /// </summary>
        private void UpdateEnemyShots()
        {
            for (int i = 0; i < enemyShots.Count; i++)
            {
                enemyShots[i].Move();

                if (enemyShots[i].Y >= Console.WindowHeight)
                {
                    Console.SetCursorPosition(enemyShots[i].X - 1, enemyShots[i].Y);
                    Console.Write("    ");
                    enemyShots.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Checks for collisions between enemy shots and the player.
        /// </summary>
        private void CheckEnemyShotsCollision()
        {
            for (int i = 0; i < enemyShots.Count; i++)
            {
                var shot = enemyShots[i];
                var currentPlayer = Player.CurrentPlayer!;
                bool hit = false;

                for (int j = 0; j < Player.Design.Length && !hit; j++)
                {
                    for (int k = 0; k < Player.Design[j].Length; k++)
                    {
                        if (shot.Y == currentPlayer.Y + j && shot.X == currentPlayer.X + k && Player.Design[j][k] != ' ')
                        {
                            hit = true;
                            break;
                        }
                    }
                }

                if (hit)
                {
                    Player.PlayerLives--;

                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(" Vie: " + Player.PlayerLives);

                    enemyShots.RemoveAt(i);
                    i--;
                    Console.SetCursorPosition(shot.X, shot.Y);
                    Console.Write(" ");
                }
            }
        }

        /// <summary>
        /// Moves the enemies down when they reach the edge of the screen.
        /// </summary>
        private void MoveEnemiesDown()
        {
            foreach (var enemy in Enemies)
            {
                if (direction == 1)
                    enemy.ClearRight();
                else
                    enemy.ClearLeft();
                enemy.Y++;
            }
        }

        /// <summary>
        /// Adds an enemy to the removal list.
        /// </summary>
        /// <param name="enemy">The enemy to remove.</param>
        public static void MarkEnemyForRemoval(Enemy enemy)
        {
            EnemiesToRemove.Add(enemy);
        }
    }
}
