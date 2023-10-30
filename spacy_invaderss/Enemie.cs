namespace enemie
{


    /// <summary>
    /// class pour les enemie, permet des gerer leurs mouvement, leurs mort etc etc
    /// </summary>
    public class Enemie
    {
        private List<Tirs> tirsEnemi = new();//créé une liste avec les tirs des enemie, permet de les fair bouger et les supprimer quand il le faut
        public bool titre = false; //titre permet de definir si c'est le joueur ou l'enemie, ses l'enemie dans se cas (ses pour simplifier la methode de tirs)
        ConsoleColor color = ConsoleColor.Gray;
        public static List<Enemie> enemies = new(); //créé une liste avec les enemie dedans, permet de verifie leurs attribus a chacun
        static List<Enemie> enemiesToRemove = new(); //defini quand il y'aurra un enemie a enlever   
        public int x = 0; //pour definir ou est le tirs ou l'enemie dans la largeur
        public int y=1; //pour definir ou est le tirs ou l'enemie dans la hauteur
        public int NumberEnemy = 20;
        string skin = "-0_0-";
        public byte life = 3;
        public int score = 0;
        char difficulté ='e';
        /// <summary>
        /// encapsule la liste des Tirs enemie
        /// </summary>
        public List<Tirs> TirsEnemi { get => tirsEnemi; set => tirsEnemi = value; }
        /// <summary>
        /// permet d'update de score par rapport a la vie, il fait vie*1000 a chaque fois pour le scrore gagné
        /// </summary>
        public void UpdateScore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            score += 1000*Joueur.jouLife;
            Console.SetCursorPosition(10, 0);
            Console.WriteLine("score: "+score);

        }
        /// <summary>
        /// methode qui permet au enemie d'allez a droite
        /// </summary>
        public void MoveRight()
        {
            // Vérifier si x et y sont dans les limites
            if (x >= 0 && x < Console.WindowWidth - skin.Length && y >= 0 && y < Console.WindowHeight)
            {
                Console.ForegroundColor = color;
                // Vérifiez si x-1 est à l'intérieur des limites avant de définir la position du curseur

                if (x > 0)  // Ajout de cette vérification
                {
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(' ');
                }
                Console.CursorLeft = x;
                Console.Write(skin);
                x++;
                Mouvement();
                
            }
        }
        /// <summary>
        /// methode qui s'ccupe du mouvement du enemie a gauche
        /// </summary>
        public void MoveLeft()
        {
            if (x > 0 && y >= 0 && y < Console.WindowHeight)
            {
                Console.ForegroundColor = color;

                // Efface l'ancienne position
                Console.SetCursorPosition(x + skin.Length, y);
                Console.Write(' ');

                Console.SetCursorPosition(x, y);
                Console.Write(skin);
                x--;
                Mouvement();         
            }
        }
       //methode qui s'occupe de la petite animation des enemie et en paralèle de la vie
        public void Mouvement()
        {

            //fait le petit mouvement des enemie
            skin = skin switch
            {
                "-0_0-" => "^0^0^",
                "^0^0^" => "-0_0-",
                ">-_-<" => ">^-^<",
                ">^-^<" => ">-_-<",
                _ => "^0^0^",
            };
            //s'occupe du system de vie des enemie
            switch (life)
            { 
                case 3:
                    color = ConsoleColor.White;
                    break;
                case 2:
                    color = ConsoleColor.Green;
                    break;
                case 1:
                    color = ConsoleColor.Red;
                    break;
                case 0:
                    
                    enemiesToRemove.Add(this);
                    Console.SetCursorPosition(x-1, y);
                    Console.Write("       ");
                    
                    break;
                default:
                    //logique de suppression des enemie, sa met des espace, sa l'ajoute a la liste enemie remove qui va le supp de la liste ensuite
                    enemiesToRemove.Add(this);
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write("       ");
                    break;
            }

        }
        /// <summary>
        /// permet de supprimer les résidut quand il change de direction (il change de droite pour la gauche)
        /// </summary>
        public void ClearPositionRight()
        {
            Console.SetCursorPosition(x-1, y);
            Console.Write("     ");
        }
        /// <summary>
        /// permet de supprimer les résidut quand il change de direction (il change de gauche pour la droite
        /// </summary>
        public void ClearPositionLeft()
        {
            Console.SetCursorPosition(x+1, y);
            Console.Write("     ");
        }
        static int direction = 1; // 1 pour droite, -1 pour gauche
        static int movesBeforeDrop = Console.WindowWidth - 40; //calcule le nombre de mouvement des enemie aven de changer de sens
        static int currentMoves = 0;//compte les mouvement
        static int tick = 0;

        /// <summary>
        /// fait apparaitre les enemie au début
        /// </summary>
        public void LancerEn()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("a quelle difficulté voulez vous jouer?");
                Console.WriteLine("0= trop facile");
                Console.WriteLine("1= facile");
                Console.WriteLine("2= moyen");
                Console.WriteLine("3= difficile");
                Console.WriteLine("4= très difficile");
                Console.WriteLine("5= impossible");
                Console.Write("Choix: ");
                difficulté = Console.ReadKey(true).KeyChar;
                Console.Clear();
            } while (difficulté != '0' && difficulté !='1' &&difficulté !='2' && difficulté != '3' && difficulté != '4' && difficulté != '5');
            //fait une instance de enemie pour pouvoir utiliser score
            Enemie enemie = new();

            Console.SetCursorPosition(10, 0);
            Console.WriteLine("score: " + enemie.score);
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    Enemie en = new();
                    if (j % 2 == 0)
                    {
                        en.skin = "-0_0-";
                    }
                    else
                    {
                        en.skin = ">-_-<";
                    }

                    en.y = j * 2+1;
                    en.x = i * 8;
                    en.MoveRight();

                    enemies.Add(en);
                }
            }
            //reset ses valeur quand on recommence le jeux
            direction = 1; 
            movesBeforeDrop = Console.WindowWidth - 40;
            currentMoves = 0;
            tick = 0;
        }
        

        /// <summary>
        /// s'occupe du mouvement des enemie
        /// </summary>
        public void Moove()
        {
            
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(" vies: "+Joueur.jouLife);
            Random Tir_enemie = new();
            int tirus;
            switch (difficulté)
            {
                case '0':
                    tirus = Tir_enemie.Next(1, 35);
                    break;
                case '1':
                    tirus = Tir_enemie.Next(1,20);
                    break;
                case '2':
                    tirus = Tir_enemie.Next(1, 13);
                    break;
                case '3':
                    tirus = Tir_enemie.Next(1, 7);
                    break;
                case '4':
                    tirus = Tir_enemie.Next(1, 3);
                    break;
                case '5':
                    tirus = Tir_enemie.Next(1,2);
                    break;
                default: tirus = Tir_enemie.Next(20);
                    break;
            }
            

            // Sélectionne un ennemi au hasard pour tirer
            Enemie? randomEnemy = null;
            if (enemies.Count > 0)
            {
                randomEnemy = enemies[Tir_enemie.Next(enemies.Count)];
            }

            tick++;

            if (tick >= 20)
            {
                if (direction == 1)
                {
                    foreach (var en in enemies)
                    {
                        en.MoveRight();
                    }
                }
                else if (direction == -1)
                {
                    foreach (var en in enemies)
                    {
                        en.MoveLeft();
                    }
                }
               


                tick = 0;
                currentMoves++;
            }
            //enleve les enemie a la fin du mouvement pour eviter les bug
            foreach (var enemyToRemove in Enemie.enemiesToRemove)
            {
                NumberEnemy--;

                UpdateScore();

                Enemie.enemies.Remove(enemyToRemove);
            }
            Enemie.enemiesToRemove.Clear();  // Videz la liste temporaire
            //logique aléatoir pour les tirs des enemie
            if (tirus == 1 && randomEnemy != null)
            {
                try
                {
                    Tirs tirer = new() { titre = false, Y = randomEnemy.y, X = randomEnemy.x };
                    TirsEnemi.Add(tirer);
                }
                catch
                {
                    
                }
            }
            for (int i = 0; i < TirsEnemi.Count; i++)
            {
                TirsEnemi[i].Tir();

                // Si le tir est hors de l'écran, supprimez-le de la liste
                if (TirsEnemi[i].Y >= Console.WindowHeight)
                {
                    Console.SetCursorPosition(TirsEnemi[i].X - 1, TirsEnemi[i].Y);
                    Console.Write("    ");
                    TirsEnemi.RemoveAt(i);
                    i--; 
                }
            }            
            for (int i = 0; i < TirsEnemi.Count; i++)
            {
                Tirs t = TirsEnemi[i];
                //fait un constructeur avec joueur pour verifier si les tir touche ou non
                Joueur currentPlayer = Joueur.CurrentPlayer!;               
                bool hit = false;

               //calcule si il c'est fait toucher
                for (int j = 0; j < Joueur.playerDesign.Length && !hit; j++)
                {
                   
                    for (int k = 0; k < Joueur.playerDesign[j].Length; k++)
                    {
                        if (t.Y == currentPlayer.y + j && t.X == currentPlayer.x + k && Joueur.playerDesign[j][k] != ' ')
                        {
                            hit = true;
                            break;
                        }
                    }
                }
                if (hit)
                {
                    Joueur.jouLife--; 

                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(" vies: " + Joueur.jouLife);  // Affichez la vie du joueur en utilisant 'currentPlayer.jouLife'.

                    TirsEnemi.RemoveAt(i); //enleve le tirs de la liste
                    i--;
                    Console.SetCursorPosition(t.X, t.Y);
                    Console.Write(" ");
                }
            }
            //regarde par rapport au nombre de mouvement des enemie si il doit changer de direction
            if (currentMoves >= movesBeforeDrop)
            {
                foreach (var en in enemies)
                {
                    if (direction == 1)
                    {
                        en.ClearPositionRight();
                    }
                    else
                    {
                        en.ClearPositionLeft();
                    }
                    en.y++;
                }

                direction = -direction;
                currentMoves = 0; //remet les mouvment a 0 pour fair le compt dans l'autre sens
            }
        }
    }
}



