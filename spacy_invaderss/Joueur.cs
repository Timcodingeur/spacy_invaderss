namespace enemie
{
    public class Player
    {
        public static Player? CurrentPlayer { get; set; }
        public static byte PlayerLives = 5;
        public int X { get; set; }
        public int Y { get; set; } = 20;
        public static string[] Design { get; } = { "  /\\", " /__\\", "[i][i]" };
        private readonly List<Shot> activeShots = new();
        private int shotCooldown = 0;

        /// <summary>
        /// Updates the player: movement and shooting.
        /// </summary>
        public void Update()
        {
            Console.ForegroundColor = ConsoleColor.White;

            HandleCollisions();
            HandleInput();
            UpdateShots();
        }

        /// <summary>
        /// Spawns the player at the beginning of the game.
        /// </summary>
        public void Spawn()
        {
            CurrentPlayer = this;
            X = Console.WindowWidth / 2;

            for (int i = 0; i < Design.Length; i++)
            {
                Console.SetCursorPosition(X, Y + i);
                Console.Write(Design[i]);
            }

            PlayerLives = 5;
        }

        /// <summary>
        /// Handles the player's input for movement and shooting.
        /// </summary>
        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                shotCooldown--;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Move(-1);
                        break;
                    case ConsoleKey.RightArrow:
                        Move(1);
                        break;
                    case ConsoleKey.Spacebar:
                        Shoot();
                        break;
                }
            }
        }

        /// <summary>
        /// Moves the player in the specified direction.
        /// </summary>
        /// <param name="direction">-1 for left, 1 for right.</param>
        private void Move(int direction)
        {
            if ((direction < 0 && X > 0) || (direction > 0 && X < Console.WindowWidth - Design[0].Length))
            {
                Erase();
                X += direction;
                Draw();
            }
        }

        /// <summary>
        /// Handles the player's shooting.
        /// </summary>
        private void Shoot()
        {
            if (shotCooldown <= 0)
            {
                var shot = new Shot { IsPlayerShot = true, Y = Y, X = X };
                activeShots.Add(shot);
                shotCooldown = 10;
            }
        }

        /// <summary>
        /// Updates the positions of the player's shots.
        /// </summary>
        private void UpdateShots()
        {
            for (int i = 0; i < activeShots.Count; i++)
            {
                activeShots[i].Move();

                if (activeShots[i].Y < 1)
                {
                    Console.SetCursorPosition(activeShots[i].X + 2, activeShots[i].Y);
                    Console.Write(" ");
                    activeShots.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Handles collisions between the player's shots and enemies.
        /// </summary>
        private void HandleCollisions()
        {
            for (int i = 0; i < activeShots.Count; i++)
            {
                var shot = activeShots[i];
                bool hit = false;

                for (int j = 0; j < EnemyManager.Enemies.Count && !hit; j++)
                {
                    var enemy = EnemyManager.Enemies[j];
                    if (shot.Y == enemy.Y && (shot.X >= enemy.X - 3 && shot.X <= enemy.X + 2))
                    {
                        enemy.Life--;
                        activeShots.RemoveAt(i);
                        i--;
                        hit = true;
                        Console.SetCursorPosition(shot.X, shot.Y);
                        Console.Write(" ");
                    }
                }

                if (hit) break;
            }
        }

        /// <summary>
        /// Draws the player's ship on the screen.
        /// </summary>
        private void Draw()
        {
            for (int i = 0; i < Design.Length; i++)
            {
                Console.SetCursorPosition(X, Y + i);
                Console.Write(Design[i]);
            }
        }

        /// <summary>
        /// Erases the player's ship from the screen.
        /// </summary>
        private void Erase()
        {
            for (int i = 0; i < Design.Length; i++)
            {
                Console.SetCursorPosition(X, Y + i);
                Console.Write(new string(' ', Design[i].Length));
            }
        }
    }
}
