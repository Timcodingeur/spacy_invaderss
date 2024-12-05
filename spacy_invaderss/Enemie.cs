using System;

namespace enemie
{
    /// <summary>
    /// Represents an enemy in the game.
    /// </summary>
    public class Enemy
    {
        public int X { get; set; }
        public int Y { get; set; }
        public byte Life { get; set; } = 3;
        public string Skin { get; set; } = "-0_0-";
        private ConsoleColor color = ConsoleColor.Gray;

        /// <summary>
        /// Animates and updates the enemy's appearance and life.
        /// </summary>
        public void Animate()
        {
            Skin = Skin switch
            {
                "-0_0-" => "^0^0^",
                "^0^0^" => "-0_0-",
                ">-_-<" => ">^-^<",
                ">^-^<" => ">-_-<",
                _ => "^0^0^",
            };

            color = Life switch
            {
                3 => ConsoleColor.White,
                2 => ConsoleColor.Green,
                1 => ConsoleColor.Red,
                _ => ConsoleColor.Black,
            };

            if (Life <= 0)
            {
                EnemyManager.MarkEnemyForRemoval(this);
                Console.SetCursorPosition(X - 1, Y);
                Console.Write("       ");
            }
        }

        /// <summary>
        /// Moves the enemy to the right.
        /// </summary>
        public void MoveRight()
        {
            if (X >= 0 && X < Console.WindowWidth - Skin.Length && Y >= 0 && Y < Console.WindowHeight)
            {
                Console.ForegroundColor = color;
                if (X > 0)
                {
                    Console.SetCursorPosition(X - 1, Y);
                    Console.Write(' ');
                }
                Console.CursorLeft = X;
                Console.Write(Skin);
                X++;
                Animate();
            }
        }

        /// <summary>
        /// Moves the enemy to the left.
        /// </summary>
        public void MoveLeft()
        {
            if (X > 0 && Y >= 0 && Y < Console.WindowHeight)
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(X + Skin.Length, Y);
                Console.Write(' ');

                Console.SetCursorPosition(X, Y);
                Console.Write(Skin);
                X--;
                Animate();
            }
        }

        /// <summary>
        /// Clears residual characters when changing direction from right to left.
        /// </summary>
        public void ClearRight()
        {
            Console.SetCursorPosition(X - 1, Y);
            Console.Write("     ");
        }

        /// <summary>
        /// Clears residual characters when changing direction from left to right.
        /// </summary>
        public void ClearLeft()
        {
            Console.SetCursorPosition(X + 1, Y);
            Console.Write("     ");
        }
    }
}
