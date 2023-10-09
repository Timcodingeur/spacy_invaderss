using MySql;
using System;
using MySql.Data;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
namespace enemie
{
    internal class GameEngine
    {
        //initialise l'enemie et le joueur pour les var non static
        Joueur jeux = new(); //fait un constructeur avec joueur, permet d'utiliser les différente methode des joueur dans le code
        Enemie enemie = new();//fait un constructeur avec enemie, permet d'utiliser les différente methode des enemie dans le code
        int fpscal = 16; //petit system pour les fps
        public int difficulté;
        /// <summary>
        /// lance le menu
        /// </summary>
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("####################################");
            Console.WriteLine("###       SPACE INVADERS         ###");
            Console.WriteLine("####################################");
            Console.WriteLine();
            Console.WriteLine("1. Jouer");
            Console.WriteLine("2. High Score");
            Console.WriteLine("3. Quitter");
            Console.WriteLine();
            Console.Write("Choisissez une option (1/2/3) : ");


            var choice = Console.ReadKey(true).KeyChar;


            switch (choice)
            {
                case '1':
                    Console.Clear();
                    new GameEngine().Start();
                    break;
                case '2':
                    Connection.Connect();
                    break;
                case '3':
                    QuitGame();
                    break;
                default:
                    Console.WriteLine("Option non valide. Appuyez sur une touche pour revenir au menu principal.");
                    Console.ReadKey();
                    ShowMainMenu();
                    break;
            }

        }
        /// <summary>
        /// quitte le jeux
        /// </summary>
        private static void QuitGame()
        {
            //quitte le jeux
            Console.Clear();
            Console.WriteLine("Merci d'avoir joué !");
            Environment.Exit(0);
        }
        /// <summary>
        /// lance le jeux et le mientien dans une boule jusque a ce que je joueur finisse
        /// </summary>
        public void Start()
        {
           
            enemie.LancerEn();
            jeux.Apparait();

            while (true)
            {
                
                Console.CursorVisible = false;
                enemie.Moove();
                jeux.Jouer();

                Thread.Sleep(fpscal);
                //si il y'a plus d'enemie on gagne (si vous gagner vous etes fort
                if (enemie.NumberEnemy == 0)
                {
                    Enemie.enemies.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("Bravo vous avez gagner");
                    Ajout();
                    Console.WriteLine("cliquer sur une touche pour continuer");
                    Console.ReadKey(true);
                    break;
                }//si la vie est a 0 le joueur perd, et cela ramène a une petite page internet de troll
                if (Joueur.jouLife <= 0)
                {
                    Enemie.enemies.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("tu a perdu (RIP)");
                    Ajout();


                    string relativePath = "../perdu.html";
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string fullPath = Path.GetFullPath(Path.Combine(currentDirectory, relativePath));

                    try
                    {
                        Process.Start("cmd", $"/c start {fullPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Une erreur est survenue: {ex.Message}");
                    }
                    Console.WriteLine("cliquer sur une touche pour continuer");
                    Console.ReadKey(true);
                    break;
                }


            }

        }
        /// <summary>
        /// permet l'ajout du score dans la db
        /// </summary>
        private void Ajout()
        { 
            Console.Clear();
            //definit les paramètre (j'ai mis comme sa comme sa ses plus facile a fair des modif
            string server = "localhost";
            string database = "db_space_invaders";
            string user = "root";
            string password = "root";
            string port = "6033";

            //créé la variable de connection
            string connString = String.Format("server={0};port={1};user id={2}; password={3}; database={4};", server, port, user, password, database);

            Console.Write("Quelle est votre pseudo?: ");

            string pseudo = Console.ReadLine()!;

            int score = enemie.score;
            using MySqlConnection conn = new(connString);
            try
            {
                conn.Open();
                Console.WriteLine(" ");

                string createUserQuery = "INSERT INTO t_joueur (jouPseudo, jouNombrePoints) VALUES (@pseudo, @score);";

                MySqlCommand createUserCommand = new(createUserQuery, conn);

                createUserCommand.Parameters.AddWithValue("@pseudo", pseudo);
                createUserCommand.Parameters.AddWithValue("@score", score);

                createUserCommand.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
