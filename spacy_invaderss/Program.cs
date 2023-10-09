namespace enemie
{
    internal class Program
    {
        /// <summary>
        /// main, lance le jeux
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)
            {
                GameEngine.ShowMainMenu();
            }
        }
    }
}