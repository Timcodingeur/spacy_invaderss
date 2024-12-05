namespace enemie
{
    /// <summary>
    /// Manages the shots of both the player and the enemies.
    /// </summary>
    public class Shot
    {
        public bool IsPlayerShot { get; set; }
        private readonly string skin = "|";
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Moves the shot on the screen.
        /// </summary>
        public void Move()
        {
            if (IsPlayerShot)
            {
                Console.SetCursorPosition(X + 2, Y);
                Console.Write(" ");
                Y--;
                Console.SetCursorPosition(X + 2, Y);
                Console.Write(skin);
            }
            else
            {
                if (Y < Console.WindowHeight - 1)
                {
                    Console.SetCursorPosition(X, Y);
                    Console.Write(" ");
                    Y++;
                    Console.SetCursorPosition(X, Y);
                    Console.Write(skin);
                }
                else
                {
                    Console.SetCursorPosition(X, Y);
                    Console.Write(" ");
                }
            }
        }
    }
}
