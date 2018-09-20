/// ETML
/// Auteur : Adrian MAYO CARTES
/// Date : 23.11.2016
/// Description : Classe gérant l'ensemble des méthodes et objets utilisés. C'est le programme principal.
///

/// Version : 1.0.1
/// Auteur : Adrian MAYO CARTES
/// Date : 30.11.2016
/// Description : Ajout de la capture des bases
/// 

/// Version : 1.0.2
/// Auteur : Jérémy DELAY
/// Date : 30.11.2016
/// Description : Modification de la méthode startGame afin de pouvoir récupérer la taille de la carte
///

/// Version : 1.0.3
/// Auteur : Adrian MAYO CARTES
/// Date : 14.12.2016
/// Description : - Création de la méthode de tir (AttackShipDisplay)
///               - Modification de la méthode CreateShip
///               - Création de la constante BUTTON_DISPLAY_ATTACK
///               - Modification de la constante BUTTON_DISPLAY_MOVE
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GuerreNavale
{
    class GameManager : Form
    {
        /*-----------DEBUG----------*/
        private const bool DEBUG_DEFAULT_VALUE_PLAYERS = false;

        /*--------CONSTANTES--------*/
        public const int SHIP_PER_BASE = 3;
        private const int BEGIN_BASE_PER_PLAYER = 3;

        // Tailles des différentes cartes
        public const int SMALL_MAP = 0;
        public const int SMALL_MAP_HEIGHT = 30;
        public const int SMALL_MAP_WIDTH = 40;
        public const int MEDIUM_MAP = 1;
        public const int MEDIUM_MAP_HEIGHT = 40;
        public const int MEDIUM_MAP_WIDTH = 60;
        public const int BIG_MAP = 2;
        public const int BIG_MAP_HEIGHT = 60;
        public const int BIG_MAP_WIDTH = 80;

        // Tailles des îles en fonction de la carte choisie
        private const int SMALL_PRINCIPAL_ISLAND_HEIGHT = 24;
        private const int SMALL_PRINCIPAL_ISLAND_WIDTH = 8;
        private const int SMALL_CENTRAL_ISLAND_HEIGHT = 8;
        private const int SMALL_CENTRAL_ISLAND_WIDTH = 5;
        private const int MEDIUM_PRINCIPAL_ISLAND_HEIGHT = 30;
        private const int MEDIUM_PRINCIPAL_ISLAND_WIDTH = 8;
        private const int MEDIUM_CENTRAL_ISLAND_HEIGHT = 10;
        private const int MEDIUM_CENTRAL_ISLAND_WIDTH = 8;
        private const int BIG_PRINCIPAL_ISLAND_HEIGHT = 46;
        private const int BIG_PRINCIPAL_ISLAND_WIDTH = 15; //21
        private const int BIG_CENTRAL_ISLAND_HEIGHT = 15;
        private const int BIG_CENTRAL_ISLAND_WIDTH = 10;

        // Nombre d'îles en fonction de la taille de la carte
        public const int ALL_MAP_MIN_ISLAND = 2;
        public const int SMALL_MAP_MAX_ISLAND = 3;
        public const int MEDIUM_MAP_MAX_ISLAND = 4;
        public const int BIG_MAP_MAX_ISLAND = 4;

        public const string BUTTON_DIPLAYED_MOVE = "isDisplayedMove";
        public const string BUTTON_DISPLAY_ATTACK = "isDisplayedAttack";


        /*---------VARIABLES--------*/
        private Map gameMap;    //map (carte) du jeu
        //private Ship[,] tab_ships;  //contient tous les bateaux potentiels de 2 joueur. Première entrée du tableau = J1 ou J2 et seconde entrée = bateaux potentiels (qu'il soit créée ou pas)
        private Player player1;     //contient les valeurs pour le joueur 1
        private Player player2;     //contient les valeurs pour le joueur 2
        private NavalWarForm navalWarDisplayed;
        private HomeForm homeInfoEntered;


        /*--------PROPRIETES--------*/
        public bool ValuesIsEntered
        {
            get { return homeInfoEntered.ValuesIsEntered; }
            protected set { }

        }

        
        /*-------CONSTRUCTEURS------*/
        public GameManager()
        {

            if (DEBUG_DEFAULT_VALUE_PLAYERS)
            {
                int nbBaseBeginPlayerIsland = 3;
                this.player1 = new Player("P1", Color.Blue, (MEDIUM_MAP_MAX_ISLAND + (2 * nbBaseBeginPlayerIsland)) * SHIP_PER_BASE);
                this.player2 = new Player("P2", Color.Red, (MEDIUM_MAP_MAX_ISLAND + (2 * nbBaseBeginPlayerIsland)) * SHIP_PER_BASE);
            }

            //player1.IsTurn = true;

        }

        /*public GameManager(Map gameMap, int nbCentralIsland, int nbBaseBeginPlayerIsland = 3)
        {
            this.gameMap = gameMap;
            if (DEBUG_DEFAULT_VALUE_PLAYERS)
            {
                this.player1 = new Player("P1", Color.Blue, (nbBaseBeginPlayerIsland + (2 * nbBaseBeginPlayerIsland)) * SHIP_PER_BASE);
                this.player2 = new Player("P2", Color.Red, (nbBaseBeginPlayerIsland + (2 * nbBaseBeginPlayerIsland)) * SHIP_PER_BASE);
            }
            player1.IsTurn = true;

        }*/

        /*---------METHODES---------*/

        /// <summary>
        /// Permet de créer les navires
        /// </summary>
        /// <param name="player"></param>
        /// <param name="tile"></param>
        /// <param name="selectedPictureBox"></param>
        public void CreateShip(Player player, Tile tile, PictureBox selectedPictureBox)
        {
            /*if (player1.IsTurn)
            {
                player1.CreateShip((x, y, maxDistance) => { DisplayPotentialMove(x, y, maxDistance, navalWarDisplayed.TilesDisplayed); }, gameMap);
            }
            else
            {
                player2.CreateShip((x, y, maxDistance) => { DisplayPotentialMove(x, y, maxDistance, navalWarDisplayed.TilesDisplayed); }, gameMap);
            }*/

            Ship ship = player.CreateShip(
                (x, y, maxDistance) => { DisplayPotentialMove(x, y, maxDistance, navalWarDisplayed.TilesDisplayed); },
                (x, y, maxAttackDistance) => { AttackShipDisplay(player, x, y, maxAttackDistance, navalWarDisplayed.TilesDisplayed); },
                tile.X,
                tile.Y,
                gameMap
                );
            selectedPictureBox.Image = Bitmap.FromFile("../../img/shipOnWater.png");
            selectedPictureBox.BackColor = player.Color;

            tile.ShipOverTile = ship;
        }

        //private void MoveShip(Ship ship, int newX, int newY, object sender, EventArgs e)
        //{
        //    ship.ChangeCoordonnates(newX, newY);
        //    ((PictureBox)sender).Image = Bitmap.FromFile("../../img/possibleMove.jpg");
        //}

        /// <summary>
        /// Permet d'afficher dans quelles cases ils peuvent
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="maxDistance"></param>
        /// <param name="tab_buttonsDisplayed"></param>
        private void DisplayPotentialMove(int x, int y, int maxDistance, PictureBox[,] tab_buttonsDisplayed)
        {
            //List<int[]> tab_coordinateSelectionableCases = new List<int[]>(); //contient les coordonées des cases séléctionnable

            //parcourt un carré autour du navire pour voir quelle cases sont séléctonnable
            for (int i = 0 - maxDistance; i <= maxDistance; i++)
            {
                for (int j = 0 - maxDistance; j <= maxDistance; j++)
                {
                    //si le point en question n'est pas (0;0)
                    //et que l'on n'est pas en dehors de la map
                    //et que le bateau peut parcourir cette distance
                    //et que le type de la case n'est pas de la terre
                    //et qu'il n'y a pas de navire sur la case
                    if (!(i == 0 && j == 0)
                        && (x + i) >= 0 && (x + i) < gameMap.Width
                        && (y + j) >= 0 && (y + j) < gameMap.Height
                        && Math.Abs(i) + Math.Abs(j) < maxDistance
                        && gameMap.GetTile(x + i, y + j).Type != Tile.TileType.Land
                        && gameMap.GetTile(x + i, y + j).ShipOverTile == null)
                    {
                        ///*mettre ici ce qui concerne l'affichage que la case est séléctionnable*/

                        //remplace l'image par celle de mouvement possible
                        tab_buttonsDisplayed[x + i, y + j].Image = Bitmap.FromFile("../../img/possibleMove.jpg");

                        tab_buttonsDisplayed[x + i, y + j].Tag = BUTTON_DIPLAYED_MOVE;

                        ////Ajoute l'événement de déplacement de navire
                        //tab_buttonsDisplayed[x + i, y + j].Click += (sender, e) => gameMap.GetTile(x + i, y + j).ShipOverTile.Move(x + i, y + j);

                        //ajoute cette coordonée à la liste
                        //tab_coordinateSelectionableCases.Add(new int[] { (x + i), (j + i) });

                        //si le type de la case n'est pas de la terre
                        //et que le bateau peut parcourir cette distance
                        //if (gameMap.GetTile(x + i, y + j).Type != Tile.TileType.Land
                        //    || Math.Abs(i) + Math.Abs(j) > maxDistance)
                        //{
                        //    /*mettre ici ce qui concerne le fait de déselectionner*/
                        //    //tab_coordinateSelectionableCases.RemoveAt(tab_coordinateSelectionableCases.Count - 1);
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// Permet de conquérir une base
        /// </summary>
        /// <param name="player">Joueur qui capture</param>
        /// <param name="tile">Tuile capturée</param>
        /// <param name="pictureBox">Tuile affichée</param>
        public void ConquestBase(Player player, Tile tile, PictureBox pictureBox)
        {
            //le joueur capture la base
            tile.CaptureBase(player);
        }

        /// <summary>
        /// Permet d'afficher les cibles potentielles
        /// </summary>
        /// <param name="ownerShip">Propriétaire du navire</param>
        /// <param name="x">Position X du navire</param>
        /// <param name="y">Position Y du navire</param>
        /// <param name="maxAttackDistance">Distance maximale d'attaque</param>
        /// <param name="tab_buttonsDisplayed">Le tableau de boutons qui sert de carte</param>
        public void AttackShipDisplay(Player ownerShip, int x, int y, int maxAttackDistance, PictureBox[,] tab_buttonsDisplayed)
        {
            bool canAttackAShip = false; //variable de contrôle

            //parcourt un carré autour du navire pour voir quelle cases sont séléctonnable
            for (int i = 0 - maxAttackDistance; i <= maxAttackDistance; i++)
            {
                for (int j = 0 - maxAttackDistance; j <= maxAttackDistance; j++)
                {
                    //si le point en question n'est pas (0;0)
                    //et que l'on n'est pas en dehors de la map
                    //et que le bateau peut parcourir cette distance d'attaque
                    //et que le type de la case n'est pas de la terre
                    //et qu'il y a  de navire sur la case
                    /*//et que le navire n'est pas à nous*/
                    if (!(i == 0 && j == 0)
                        && (x + i) > 0 && (x + i) < gameMap.Width
                        && (y + j) > 0 && (y + j) < gameMap.Height
                        && Math.Abs(i) + Math.Abs(j) < maxAttackDistance
                        && gameMap.GetTile(x + i, y + j).Type != Tile.TileType.Land
                        && gameMap.GetTile(x + i, y + j).ShipOverTile != null
                        /*&& !Array.Exists(ownerShip.Tab_Ships, element => element == gameMap.GetTile(x + i, y + j).ShipOverTile)*/)
                    {
                        //remplace l'image par celle de la zone d'attaque possible
                        tab_buttonsDisplayed[x + i, y + j].Image = Bitmap.FromFile("../../img/attack.png");

                        tab_buttonsDisplayed[x + i, y + j].Tag = BUTTON_DISPLAY_ATTACK;

                        canAttackAShip = true;
                    }
                }
            }

            if (!canAttackAShip)
            {
                MessageBox.Show("Aucun navire à portée !\nLa distance de tir maximale est de " + maxAttackDistance + " cases !");
            }
        }


        /// <summary>
        /// Permet de démarrer le jeu
        /// </summary>
        public void StartGame()
        {

            // Récupère les bonnes infos en fonction de la taille de la carte
            switch (homeInfoEntered.SizeMap)
            {
                case SMALL_MAP:
                    gameMap = new GuerreNavale.Map(player1, player2, SMALL_MAP_WIDTH, SMALL_MAP_HEIGHT, homeInfoEntered.NbCentralsIslands, SMALL_CENTRAL_ISLAND_WIDTH, SMALL_CENTRAL_ISLAND_HEIGHT, SMALL_PRINCIPAL_ISLAND_WIDTH, SMALL_PRINCIPAL_ISLAND_HEIGHT);

                    break;
                case MEDIUM_MAP:

                    gameMap = new GuerreNavale.Map(player1, player2, MEDIUM_MAP_WIDTH, MEDIUM_MAP_HEIGHT, homeInfoEntered.NbCentralsIslands, MEDIUM_CENTRAL_ISLAND_WIDTH, MEDIUM_CENTRAL_ISLAND_HEIGHT, MEDIUM_PRINCIPAL_ISLAND_WIDTH, MEDIUM_PRINCIPAL_ISLAND_HEIGHT);
                    break;
                case BIG_MAP:

                    gameMap = new GuerreNavale.Map(player1, player2, BIG_MAP_WIDTH, BIG_MAP_HEIGHT, homeInfoEntered.NbCentralsIslands, BIG_CENTRAL_ISLAND_WIDTH, BIG_CENTRAL_ISLAND_HEIGHT, BIG_PRINCIPAL_ISLAND_WIDTH, BIG_PRINCIPAL_ISLAND_HEIGHT);
                    break;
            }



            navalWarDisplayed = new NavalWarForm(player1, player2, gameMap, homeInfoEntered.SizeMap);

            navalWarDisplayed.UnregisterCreateShipFunction(CreateShip);
            navalWarDisplayed.RegisterCreateShipFunction(CreateShip);

            navalWarDisplayed.ShowDialog();
        }

        /// <summary>
        /// Permet d'afficher le pop-up de saisie d'informations
        /// </summary>
        public void GetInfo()
        {
            homeInfoEntered = new HomeForm();
            homeInfoEntered.ShowDialog();

            //on crée les joueur par rapport au informations collecté
            player1 = new Player(homeInfoEntered.NameP1, homeInfoEntered.ColorP1, (homeInfoEntered.NbCentralsIslands + (2 * BEGIN_BASE_PER_PLAYER)) * SHIP_PER_BASE);
            player2 = new Player(homeInfoEntered.NameP2, homeInfoEntered.ColorP2, (homeInfoEntered.NbCentralsIslands + (2 * BEGIN_BASE_PER_PLAYER)) * SHIP_PER_BASE);

            player1.IsTurn = true;
        }

        /// <summary>
        /// Permet d'avoir le GameManager caché
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            this.Visible = false;
        }


    }
}
