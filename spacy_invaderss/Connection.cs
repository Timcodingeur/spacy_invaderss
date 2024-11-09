using System.Data;
using MySql.Data.MySqlClient;
namespace enemie
{
    public class Connection
    {
        /// <summary>
        /// s'occupe de la connection si on veux voir les score
        /// </summary>
        public static void Connect()
        {
            //defini les paramètre de connection
            string server = "db";
            string database = "db_space_invaders";
            string user = "root";
            string password = "root";
            string port = "3306";
           
            Console.Clear();
            //créé la variable de connection
            string connString = String.Format("server={0};port={1};user id={2}; password={3}; database={4};", server, port, user, password, database);
            MySqlConnection conn = new(connString);
            //verifie la connection
            try
            {
                conn.Open();

                Console.WriteLine(" ");

                conn.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message + connString);
            }
            //fait le select de nombre de point et joueur dans la db
            string createUserQuery = $"SELECT jouPseudo, jouNombrePoints FROM `t_joueur` order by jouNombrePoints desc limit 5;";

            // Assurez-vous que la connexion est ouverte.
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            MySqlCommand createUserCommand = new(createUserQuery, conn);
            MySqlDataReader reader = createUserCommand.ExecuteReader();
            Console.WriteLine("Pseudo: NB Point");
            while (reader.Read())
            {
                Console.WriteLine(reader["jouPseudo"].ToString() + " : " + reader["jouNombrePoints"].ToString());
            }
            reader.Close();
            // Fermez la connexion après avoir terminé.
            conn.Close();
            Console.Write("apuiller sur une touche");
            Console.ReadLine();
            GameEngine.ShowMainMenu();

        }
    }
}


