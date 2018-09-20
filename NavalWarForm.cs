/// ETML
/// Auteur : Jérémy DELAY
/// Date : 30.11.2016
/// Description : Crée le formulaire de jeu.
///

/// Version : 1.0.1
/// Auteur : Adrian MAYO CARTES
/// Date : 30.11.2016
/// Description : split dans le autres fichiers
/// 

/// Version : 1.0.2
/// Auteur : Adrian MAYO CARTES
/// Date : 30.11.2016
/// Description : ajout du déplacement
/// 

/// Version : 1.0.3
/// Auteur : Jérémy DELAY
/// Date : 30.11.2016
/// Description : Création d'un deuxième constructeur afin de pouvoir récupérer la taille de la carte
///               Remplacement du TLP contenant les PictBox par un simple panel pour un gain de temps non négligeable
/// 

/// Version : 1.0.4
/// Auteur : Adrian MAYO CARTES
/// Date : 30.11.2016
/// Description : Ajout de 2 méthodes de reset, pour remmetre les tuiles dans leurs états d'origine.
///               Correction du problème du double pop-up quand un bateau était dans une base
/// 
/// Bug découvert : Si l'on souhaite déplacer un bateau, puis que l'on change d'avis pour en déplacer un autre, 
///                 c'est le premier bateau qui se déplace quand même
/// 

/// Version : 1.0.5
/// Auteur : Adrian MAYO CARTES
/// Date : 30.11.2016
/// Description : Bug découvert en 1.0.4 corrigé
/// 

/// Version : 1.0.6
/// Auteur : Jérémy DELAY
/// Date : 14.12.2016
/// Description : Ajout de la décrémentation du nombre d'actions restantes
/// 
/// Bug découvert : On peut sélectionner les navires adverses. On ne peut pas les déplacer par contre, 
///                 un message d'erreur apparaît.
///

/// Version : 1.0.7
/// Auteur : Adrian MAYO CARTES
/// Date : 14.12.2016
/// Description : - Ajout de la condition  "else if (pictureBoxClicked.Tag.ToString() == GameManager.BUTTON_DISPLAY_ATTACK)" dans 
///                 la méthode "pictureBox_Click"
///               - Modification de la méthode "ResetDisplayedTile"
///               - Ajout des méthodes pour enregistrer la méthode d'attaque dans "pictureBox_Click"
/// 
/// 
/// Bug découvert : - On peut sélectionner les navires adverses. On ne peut pas les déplacer par contre, 
///                   un message d'erreur apparaît.
///                 - Le compteur d'action descend de 2 cran lors de la création d'un bateau
///

/// Version : 1.0.8
/// Auteur : Adrian MAYO CARTES
/// Date : 18.12.2016
/// Description : - Ajout de la condition SHIP_IS_NOT_MINE dans la méthode "pictureBox_Click"
///               - Changement des conditions "if(actionLeft == 0)" en plus pettit que 0
///               - Ajout de "CG.Collect()" dans "ResetDisplayedTiles"
///               - Ajout de la réduction d'action quand on attaque
///               - Remplacement de "player" en "player.Name" dans les Name des objets WinForm quand on les crée
///               - Modification de la méthode "RegisterCreateShipFunction"
///               
/// 
/// Correction : - Mise en condition d'un doublon qui fessait que quand on créait un bateau cela couter 2 au lieu de 1
///              - On peut plus séléctionner un navire adverse grâce à l'ajout de la condition SHIP_IS_NOT_MINE
///              
/// Optimisation : - Ajout de la méthode "loadImage" et du dictionnaire "imagesRessources" pour éviter de recharge les images depuis les fichiers
/// 

/// Version : 1.0.9
/// Auteur : Jérémy DELAY
/// Date : 19.12.2016
/// Description : - Ajout de la demande à l'utilisateur avant de fermer le formulaire
///               - Ajout de la colorisation en bas en fonction du joueur qui doit jouer
///               - Ajout de l'image pour le bateau attaquable 
/// 

/// Version : 1.1.0
/// Auteur : Adrian MAYO CARTES
/// Date : 19.12.2016
/// Description : - Correction du nombre de bateau reçu par base capturée
///               - Correction du nombre de case dont un bateau se déplacer/attaquer
///               - Correction d'un bug qui empêchait de déplacer les bateaux sur les premières lignes et colonnes
///               - Correction d'un bug. Il manquait le reset des tags avec celui des tuiles.
/// 


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuerreNavale
{
    public partial class NavalWarForm : Form
    {
        /*----------DEBUG-----------*/
        // Affiche les cases en couleurs différentes
        bool DEBUG = false;
        // Permet d'afficher le plan de jeu
        bool CREATE_BOARD_DEBUG = true;
        // Permet de créer une carte 4x6
        bool DEBUG_PICTBOX = false;


        /*--------CONSTANTES--------*/

        private const int INFO_BOARD_LINES = 2;
        private const int INFO_BOARD_COLUMNS = 3;
        private const int INFO_PLAYERS_LINES = 3;

        private const int POS_Y_FOR_PLAYER_INFO = 0;

        private const float PERCENT_SIZE_INFO_BOARD = 90;
        private const float PERCENT_SIZE_FINISH_BUTTON = 10;

        // Positions dans les tableaux
        private const int TOP_POSITION = 11;              // position du panel par rapport au haut de la fenêtre
        private const int LEFT_POSITION = 11;             // position du panel par rapport à gauche de la fenêtre
        private const int TOP_POSITION_INFO_BOARD = 0;    // position de l'infoBoard par rapport au haut du panel
        private const int LEFT_POSITION_INFO_BOARD = 1;   // position de l'infoBoard par rapport à gauche du panel
        private const int TOP_POSITION_INFO = 0;          // position des infos par rapport au haut des cases externes de l'infoBoard
        private const int LEFT_POSITION_INFO = 0;         // position des infos par rapport à gauche des cases externes de l'infoBoard
        private const int TOP_POSITION_BOARD = 0;         // position du board par rapport au haut de la case centrale de l'infoBoard
        private const int LEFT_POSITION_BOARD = 1;        // position du board par rapport à gauche de la case centrale de l'infoBoard

        // Tailles en pixels
        private const int WIDTH_FORM = 1420;                // largeur en px de la fenêtre
        private const int HEIGHT_FORM = 860;                // hauteur en px de la fenêtre
        private const int WIDTH_PANEL = WIDTH_FORM - 38;    // largeur en px du tableau de jeu (38 = bordures + marges)
        private const int HEIGHT_PANEL = HEIGHT_FORM - 60;  // hauteur en px du tableau de jeu (60 = en-tête + bordure + marges)
        private const int WIDTH_INFO_TLPANEL = 150;
        private const int HEIGHT_INFO_TLPANEL = 50;
        private const int TEMP_SIZE_IN_PIXEL = 50;

        private const int MARGIN = 10;                  // marge entre les boutons
        private const float THIRD_IN_PERCENT = 100 / 3;
        private const float HALF_IN_PERCENT = 100 / 2;


        /*---------VARIABLES--------*/
        /* // Variables normalement saisies par l'utilisateur
        private int numberCentralIsland = 0;
        private Color player1Color = Color.LightBlue;
        private Color player2Color = Color.Red;
        private string player1Name = "Player 1";
        private string player2Name = "Player 2"; */
        private int sizeMap = 1;                // 0: small  1: medium  2: big
        private Player player1;
        private Player player2;

        private int nbBaseP1 = 3;
        private int nbBaseP2 = 3;
        private int nbShipP1 = 9;
        private int nbShipP2 = 9;

        Map mapGenerated;

        public int actionLeft = 2;
        private float percentBoard = 0;

        Panel globalPanel = new Panel();        // Panel englobant toute la surface de jeu
        TableLayoutPanel infoBoardTableLayoutPanel = new TableLayoutPanel();        /* TableLayoutPanel séparant les infos des joueurs, 
                                                                                       le plan de jeu et le bouton "fin de tour" */
        TableLayoutPanel infoP1TableLayoutPanel = new TableLayoutPanel();           // TableLayoutPanel donnant les infos du joueur 1
        TableLayoutPanel infoP2TableLayoutPanel = new TableLayoutPanel();           // TableLayoutPanel donnant les infos du joueur 2
        TableLayoutPanel nbActionsEndTableLayoutPanel = new TableLayoutPanel();     /* TableLayoutPanel contenant le nombre d'actions 
                                                                                       restantes et le bouton de fin de tour */
        TableLayoutPanel nbActionsTableLayoutPanel = new TableLayoutPanel();        /* TableLayoutPanel contenant le nombre d'actions 
                                                                                       restantes et un label l'indiquant */
        Label actionLeftLabel = new Label();        // Label contenant le nombre d'actions restantes
        Label actionLeftTextLabel = new Label();    // Label contenant le texte explicatif des actions restantes 
        Button endOfTurnButton = new Button();

        PictureBox[,] boardPictBox;

        int nbPictBoxHeight = 0;
        int nbPictBoxWidth = 0;

        Player currentPlayerTurn;
        Action<Player, Tile, PictureBox> createShipCallback;

        IDictionary<string, Image> imagesRessources = new Dictionary<string, Image>();

        /*--------PROPRIETES--------*/

        //retourne le tableau de pictureBox
        public PictureBox[,] TilesDisplayed
        {
            get
            {
                return boardPictBox;
            }
            protected set { }
        }

        //retourne la carte logique
        public Map Map
        {
            get { return mapGenerated; }
            protected set { }
        }

        /*-------CONSTRUCTEURS------*/
        /*public NavalWarForm()
        {
            InitializeComponent();

            this.player1 = new Player("P1", Color.Blue, (GameManager.MEDIUM_MAP_MAX_ISLAND + (2 * 3)) * 3);
            this.player2 = new Player("P2", Color.Red, (GameManager.MEDIUM_MAP_MAX_ISLAND + (2 * 3)) * 3);

            generateGameBoard();

            this.Width = WIDTH_PANEL + 16 + 22;     // (taille du panel) + (bordures gauche et droite) + (2 * marge de 11)
            this.Height = HEIGHT_PANEL + 38 + 22;   // (taille du panel) +  (en-tête et bordures bas)  + (2 * marge de 11)
        }*/

        public NavalWarForm(Player P1, Player P2, Map map)
        {
            //charge en mémoire les images utiliser sur le plateau jouable
            loadImages();

            this.player1 = P1;
            this.player2 = P2;

            if (player1.IsTurn)
            {
                currentPlayerTurn = player1;
            }
            else
            {
                currentPlayerTurn = player2;
            }

            nbPictBoxWidth = map.Width;
            nbPictBoxHeight = map.Height;

            this.mapGenerated = map;

            InitializeComponent();
            generateGameBoard();
            this.Width = WIDTH_PANEL + 16 + 22;     // (taille du panel) + (bordures gauche et droite) + (2 * marge de 11)
            this.Height = HEIGHT_PANEL + 38 + 22;   // (taille du panel) +  (en-tête et bordures bas)  + (2 * marge de 11)
        }

        public NavalWarForm(Player P1, Player P2, Map map, int sizeMap)
        {
            //charge en mémoire les images utiliser sur le plateau jouable
            loadImages();

            this.player1 = P1;
            this.player2 = P2;
            this.sizeMap = sizeMap;

            if (player1.IsTurn)
            {
                currentPlayerTurn = player1;
            }
            else
            {
                currentPlayerTurn = player2;
            }

            nbPictBoxWidth = map.Width;
            nbPictBoxHeight = map.Height;

            this.mapGenerated = map;

            InitializeComponent();
            generateGameBoard();
            this.Width = WIDTH_PANEL + 16 + 22;     // (taille du panel) + (bordures gauche et droite) + (2 * marge de 11)
            this.Height = HEIGHT_PANEL + 38 + 22;   // (taille du panel) +  (en-tête et bordures bas)  + (2 * marge de 11)
        }
        /*---------METHODES---------*/
        /// <summary>
        /// Génere les graphismes du jeu
        /// </summary>
        private void generateGameBoard()
        {
            // Création d'un panel de base
            globalPanel.Margin = new Padding(0);
            globalPanel.Dock = DockStyle.None;
            globalPanel.Location = new Point(TOP_POSITION, LEFT_POSITION);
            globalPanel.Size = new Size(WIDTH_PANEL, HEIGHT_PANEL);
            globalPanel.Name = "globalPanel";
            globalPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            if (DEBUG)
            {
                globalPanel.BackColor = Color.Red;
            }
            this.Controls.Add(globalPanel);

            /* Création d'un layoutPanel dans le but de créer des zones spécifiques séparées (2 lignes, 3 colonnes)
                ------------------------------------
                | Info J1 | Plan de jeu  | Info J2 |
                |         |              |         |
                ------------------------------------
                |         | Bouton "fin" |         |
                ------------------------------------        */
            infoBoardTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, PERCENT_SIZE_INFO_BOARD));
            infoBoardTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, PERCENT_SIZE_FINISH_BUTTON));
            /*for (int i = 0; i < INFO_BOARD_COLUMNS; i++)
            {
                infoBoardTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            }*/
            percentBoard = WIDTH_PANEL - 2 * WIDTH_INFO_TLPANEL;
            infoBoardTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, WIDTH_INFO_TLPANEL));
            infoBoardTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percentBoard));
            infoBoardTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, WIDTH_INFO_TLPANEL));

            infoBoardTableLayoutPanel.RowCount = INFO_BOARD_LINES;
            infoBoardTableLayoutPanel.ColumnCount = INFO_BOARD_COLUMNS;
            infoBoardTableLayoutPanel.Margin = new Padding(0);
            infoBoardTableLayoutPanel.Dock = DockStyle.None;
            infoBoardTableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            infoBoardTableLayoutPanel.Location = new Point(LEFT_POSITION_INFO_BOARD, TOP_POSITION_INFO_BOARD);
            infoBoardTableLayoutPanel.Size = new Size(globalPanel.Width - (2 * LEFT_POSITION_INFO_BOARD), globalPanel.Height - (2 * TOP_POSITION_INFO_BOARD));
            infoBoardTableLayoutPanel.Name = "infoBoardTableLayoutPanel";
            infoBoardTableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            infoBoardTableLayoutPanel.BackColor = Color.Beige;
            globalPanel.Controls.Add(infoBoardTableLayoutPanel);


            // Création des layoutPanel afin d'afficher les infos des joueurs
            infoP1TableLayoutPanel = createInfoPanel("infoP1TableLayoutPanel", player1.Color, 0, player1);
            infoP2TableLayoutPanel = createInfoPanel("infoP2TableLayoutPanel", player2.Color, 2, player2);

            // Remplissage des TLP avec les infos des joueurs
            infoP1TableLayoutPanel = addInfoPlayer(infoP1TableLayoutPanel, player1);
            infoP2TableLayoutPanel = addInfoPlayer(infoP2TableLayoutPanel, player2);


            // Création d'un TLP contenant le nb d'actions restantes et le bouton de fin de tour
            nbActionsEndTableLayoutPanel = createColumnsTableLayoutPanel(infoBoardTableLayoutPanel, "nbActionsEndTableLayoutPanel", Color.FloralWhite, 1, 1, THIRD_IN_PERCENT);
            (this.Controls.Find("nbActionsEndTableLayoutPanel", true)[0]).BackColor = player1.Color;

            // Création d'un TLP contenant le nb d'actions restantes et et un label l'indiquant
            nbActionsTableLayoutPanel = createColumnsTableLayoutPanel(nbActionsEndTableLayoutPanel, "nbActionsEndTableLayoutPanel", Color.Chartreuse, 1, 0, THIRD_IN_PERCENT * 2);

            // Création du label contenant le texte explicatif des actions restantes
            actionLeftTextLabel.Font = new Font("Papyrus", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            actionLeftTextLabel.Margin = new Padding(0);
            actionLeftTextLabel.TextAlign = ContentAlignment.MiddleRight;
            actionLeftTextLabel.Location = new Point(LEFT_POSITION_INFO, TOP_POSITION_INFO);
            actionLeftTextLabel.Name = "actionLeftLabel";
            actionLeftTextLabel.Size = new Size(1, 80);
            actionLeftTextLabel.TabIndex = 0;
            actionLeftTextLabel.Text = "Action(s) restante(s) :";
            actionLeftTextLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            nbActionsTableLayoutPanel.Controls.Add(actionLeftTextLabel, 0, 0);

            // Création du label contenant le nombre d'actions restantes
            actionLeftLabel.Font = new Font("Papyrus", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            actionLeftLabel.Margin = new Padding(0);
            actionLeftLabel.TextAlign = ContentAlignment.MiddleLeft;
            actionLeftLabel.Location = new Point(LEFT_POSITION_INFO, TOP_POSITION_INFO);
            actionLeftLabel.Name = "actionLeftLabel";
            actionLeftLabel.Size = new Size(1, 80);
            actionLeftLabel.TabIndex = 0;
            actionLeftLabel.Text = Convert.ToString(actionLeft);
            actionLeftLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            nbActionsTableLayoutPanel.Controls.Add(actionLeftLabel, 1, 0);


            // Création du bouton de fin de tour
            endOfTurnButton.Margin = new Padding(0);
            endOfTurnButton.Name = "endOfTurnButton";
            endOfTurnButton.Size = new Size(200, 24);
            endOfTurnButton.TabIndex = 2;
            endOfTurnButton.Text = "Fin du tour";
            endOfTurnButton.UseVisualStyleBackColor = true;
            endOfTurnButton.Anchor = AnchorStyles.None;
            endOfTurnButton.Click += new EventHandler(endOfTurnButton_Click);
            nbActionsEndTableLayoutPanel.Controls.Add(endOfTurnButton, 0, 0);


            // Vérifie si l'on souhaite afficher le plan de jeu
            if (CREATE_BOARD_DEBUG)
            {
                createBoard(infoBoardTableLayoutPanel, sizeMap);
            }

        } // faim de la fonction principale


        /// <summary>
        /// Création du plan de jeu sous forme de pictBox dans un panel
        /// </summary>
        /// <param name="infoBoardTableLayoutPanel"> TLP dans lequel il faut insérer notre panel de jeu </param>
        /// <param name="sizeMap"> Taille de carte </param>
        private void createBoard(TableLayoutPanel infoBoardTableLayoutPanel, int sizeMap)
        {
            double percentSizeHeight = 0;
            double percentSizeWidth = 0;

            int index = 0;

            double panelWidth = (WIDTH_PANEL - (2 * WIDTH_INFO_TLPANEL)) - 2;  // 1080
            double panelHeight = HEIGHT_PANEL * 0.9;                     // 720

            int pictBoxHeight = 0;
            int pictBoxWidth = 0;

            /* /!\ Version abandonnée car le temps de chargement est beaucoup trop long !
            TableLayoutPanel boardTableLayoutPanel = new TableLayoutPanel();
            */

            Panel boardPanel = new Panel();

            if (DEBUG_PICTBOX)
            {
                nbPictBoxHeight = 4;
                nbPictBoxWidth = 6;
            }

            percentSizeHeight = 1.0 / nbPictBoxHeight;
            percentSizeWidth = 1.0 / nbPictBoxWidth;

            // Création d'un tableau de pictureBox
            boardPictBox = new PictureBox[nbPictBoxWidth, nbPictBoxHeight];

            /* /!\ Version abandonnée car le temps de chargement est beaucoup trop long !

            // Création d'un TLP pour le plan de jeu
            boardTableLayoutPanel.RowCount = nbPictBoxHeight;
            boardTableLayoutPanel.ColumnCount = nbPictBoxWidth;
            boardTableLayoutPanel.Margin = new Padding(0);
            boardTableLayoutPanel.Dock = DockStyle.None;
            boardTableLayoutPanel.Location = new Point(LEFT_POSITION_BOARD, TOP_POSITION_BOARD);
            boardTableLayoutPanel.Size = new Size(Convert.ToInt32(tempWidth), Convert.ToInt32(tempHeight));
            boardTableLayoutPanel.Name = "boardTableLayoutPanel";
            boardTableLayoutPanel.Padding = new Padding(0);
            boardTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            //boardTableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            if (DEBUG)
            {
                boardTableLayoutPanel.BackColor = Color.Yellow;
            }
            infoBoardTableLayoutPanel.Controls.Add(boardTableLayoutPanel, 1, 0);
            */

            // Création d'un panel pour le plan de jeu
            boardPanel.Margin = new Padding(0);
            boardPanel.Dock = DockStyle.None;
            boardPanel.Location = new Point(LEFT_POSITION_BOARD, TOP_POSITION_BOARD);
            boardPanel.Size = new Size(Convert.ToInt32(panelWidth), Convert.ToInt32(panelHeight));
            boardPanel.Name = "boardPanel";
            boardPanel.Padding = new Padding(0);

            //boardTableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            if (DEBUG)
            {
                boardPanel.BackColor = Color.Yellow;
            }
            infoBoardTableLayoutPanel.Controls.Add(boardPanel, 1, 0);

            //mapGenerated = new GuerreNavale.Map(nbPictBoxWidth, nbPictBoxHeight);

            pictBoxHeight = Convert.ToInt32(boardPanel.Size.Height * percentSizeHeight);
            pictBoxWidth = Convert.ToInt32(Math.Floor(boardPanel.Size.Width * percentSizeWidth));

            // Création des boutons définissant le plan de jeu
            for (int i = 0; i < nbPictBoxWidth; i++)
            {
                for (int j = 0; j < nbPictBoxHeight; j++)
                {
                    /* /!\ Version abandonnée car le temps de chargement est beaucoup trop long !

                    // Création d'un pictBox du plan de jeu
                    boardPictBox[i, j] = new PictureBox();
                    boardPictBox[i, j].Location = new Point(0, 0);
                    boardPictBox[i, j].Margin = new Padding(0);
                    boardPictBox[i, j].Name = "board_" + i + "_" + j;
                    boardPictBox[i, j].Size = new Size(Convert.ToInt32(boardTableLayoutPanel.Size.Width * percentSizeWidth),
                                                      Convert.ToInt32(boardTableLayoutPanel.Size.Height * percentSizeHeight));
                    boardPictBox[i, j].TabIndex = index;
                    index++;

                    if (mapGenerated.GetTile(i, j).Type == GuerreNavale.Tile.TileType.Sea)
                    {
                        boardPictBox[i, j].Tag = "water.jpg";
                    }
                    else if(mapGenerated.GetTile(i, j).Type == GuerreNavale.Tile.TileType.Base)
                    {
                        boardPictBox[i, j].Tag = "tileBase.jpg";
                    }
                    else
                    {
                        boardPictBox[i, j].Tag = "sand.png";
                    }

                    //boardPictBox[i, j].Tag = "default.png";
                    //boardPictBox[i, j].Image = Bitmap.FromFile("../../img/default.png");
                    if (mapGenerated.GetTile(i, j).Type == GuerreNavale.Tile.TileType.Sea)
                    {
                        boardPictBox[i, j].Image = Bitmap.FromFile("../../img/water.jpg");
                    }
                    else if (mapGenerated.GetTile(i, j).Type == GuerreNavale.Tile.TileType.Base)
                    {
                        boardPictBox[i, j].Image = Bitmap.FromFile("../../img/tileBase.jpg");
                    }
                    else
                    {
                        boardPictBox[i, j].Image = Bitmap.FromFile("../../img/sand.png");
                    }
                    //boardPictBox[i, j].Image = Bitmap.FromFile("../../img/" + Convert.ToString(boardPictBox[i, j].Tag));
                    boardPictBox[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    //boardButton[i, j].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
                    boardPictBox[i, j].Padding = new Padding(0);
                    boardPictBox[i, j].Click += new EventHandler(this.pictureBox_Click);
                    boardPictBox[i, j].Tag = "";

                    boardTableLayoutPanel.Controls.Add(boardPictBox[i, j], i, j);
                    */


                    // Création d'un pictBox du plan de jeu
                    boardPictBox[i, j] = new PictureBox();
                    if (GameManager.BIG_MAP == sizeMap)
                    {
                        boardPictBox[i, j].Location = new Point(i * pictBoxWidth + 20, j * pictBoxHeight);
                    }
                    else
                    {
                        boardPictBox[i, j].Location = new Point(i * pictBoxWidth, j * pictBoxHeight);
                    }
                    boardPictBox[i, j].Margin = new Padding(0);
                    boardPictBox[i, j].Name = "board_" + i + "_" + j;
                    boardPictBox[i, j].Size = new Size(Convert.ToInt32(pictBoxWidth),
                                                      Convert.ToInt32(pictBoxHeight));
                    boardPictBox[i, j].TabIndex = index;
                    index++;

                    //boardPictBox[i, j].Tag = "default.png";
                    //boardPictBox[i, j].Image = Bitmap.FromFile("../../img/default.png");
                    //if (mapGenerated.GetTile(i, j).Type == Tile.TileType.Sea)
                    //{
                    //    boardPictBox[i, j].Image = Bitmap.FromFile("../../img/water.jpg");
                    //}
                    //else if (mapGenerated.GetTile(i, j).Type == Tile.TileType.Base)
                    //{
                    //    boardPictBox[i, j].Image = Bitmap.FromFile("../../img/tileBase.jpg");
                    //}
                    //else
                    //{
                    //    boardPictBox[i, j].Image = Bitmap.FromFile("../../img/sand.png");
                    //}

                    boardPictBox[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    //boardButton[i, j].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
                    boardPictBox[i, j].Padding = new Padding(0);
                    boardPictBox[i, j].Click += new EventHandler(this.pictureBox_Click);
                    boardPictBox[i, j].Tag = "";

                    boardPanel.Controls.Add(boardPictBox[i, j]);
                }
            }

            //permet d'afficher les images en fonction du type de la tuile
            ResetDisplayedTile();
        }

        /// <summary>
        /// Affiche les formulaires d'action lors du clic utilisateur. Vérifie aussi les actions possibles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_Click(object sender, EventArgs e)
        {
            string[] tab_valueRowAndColonm = ((PictureBox)sender).Name.Split('_'); //contient les infos inscrites dans le nom du pictureBox
            int column = Convert.ToInt32(tab_valueRowAndColonm[1]); //contient la valeur de la colonne
            int row = Convert.ToInt32(tab_valueRowAndColonm[2]);    //contient la valeur de la ligne
            int errorConquest = ActionBaseForm.NO_ERROR;
            int errorShip = ActionBaseForm.NO_ERROR;
            int errorMove = ActionShipForm.NO_ERROR;
            int errorAttack = ActionShipForm.NO_ERROR;
            PictureBox pictureBoxClicked = (PictureBox)sender;
            bool shipHasBeenUsed = false;
            bool baseHasBeenUsed = false;

            ResetDisplayedTile();

            if (pictureBoxClicked.Tag.ToString() == GameManager.BUTTON_DIPLAYED_MOVE)
            {
                Ship shipSelected = null;
                for (int i = 0; i < currentPlayerTurn.Tab_Ships.Length && shipSelected == null; i++)
                {
                    if (currentPlayerTurn.Tab_Ships[i].IsSelected)
                    {
                        shipSelected = currentPlayerTurn.Tab_Ships[i];
                    }
                }

                //retire le navire de son ancienne case
                mapGenerated.GetTile(shipSelected.X, shipSelected.Y).ShipOverTile = null;

                ResetDisplayedTile();
                ResetTags();

                //remet les images par défaut
                //for (int i = 0; i < mapGenerated.Width; i++)
                //{
                //    for (int j = 0; j < mapGenerated.Height; j++)
                //    {
                //        if (mapGenerated.GetTile(i, j).ShipOverTile == null || boardPictBox[i, j].Tag.ToString() == GameManager.BUTTON_DIPLAYED_MOVE)
                //        {
                //            if (mapGenerated.GetTile(i, j).Type == GuerreNavale.Tile.TileType.Sea)
                //            {
                //                boardPictBox[i, j].Image = Bitmap.FromFile("../../img/water.jpg");
                //            }
                //            else if (mapGenerated.GetTile(i, j).Type == GuerreNavale.Tile.TileType.Base)
                //            {
                //                boardPictBox[i, j].Image = Bitmap.FromFile("../../img/tileBase.jpg");
                //            }
                //            else
                //            {
                //                boardPictBox[i, j].Image = Bitmap.FromFile("../../img/sand.png");
                //            }
                //            boardPictBox[i, j].Tag = "";
                //        }
                //    }
                //}

                //place le navire dans la nouvelle case
                mapGenerated.GetTile(column, row).ShipOverTile = shipSelected;

                //change les coordonnées du navire
                shipSelected.ChangeCoordonnates(column, row);

                //affiche l'image dans la classe
                pictureBoxClicked.Image = imagesRessources["shipOnWater"];

                //colorie le bateau à la couleur du joueur
                pictureBoxClicked.BackColor = currentPlayerTurn.Color;

                //le navire n'est plus séléctionné
                shipSelected.IsSelected = false;

                shipHasBeenUsed = true;

                // Décrémente le nombre d'actions disponibles et l'affiche
                actionLeft--;
                actionLeftLabel.Text = Convert.ToString(actionLeft);

            }// if (pictureBoxClicked.Tag.ToString() == GameManager.BUTTON_DIPLAYED_MOVE)

            //si la tuile est attaquable
            else if (pictureBoxClicked.Tag.ToString() == GameManager.BUTTON_DISPLAY_ATTACK)
            {
                Random rnd = new Random();

                if (rnd.Next(3) == 2)
                {
                    MessageBox.Show("Touché et coulé !");
                    //supprime le navire de la tuile
                    mapGenerated.GetTile(column, row).ShipOverTile = null;
                }
                else
                {
                    MessageBox.Show("Mince !\nVous avez raté votre tir !");
                }


                ResetDisplayedTile();
                ResetTags();

                shipHasBeenUsed = true;

                // Décrémente le nombre d'actions disponibles et l'affiche
                actionLeft--;
                actionLeftLabel.Text = Convert.ToString(actionLeft);
            }//else if (pictureBoxClicked.Tag.ToString() == GameManager.BUTTON_DISPLAY_ATTACK)

            //si la tuile contient un navire
            else if (mapGenerated.GetTile(column, row).ShipOverTile != null)
            {
                ResetTags();

                // Déselectionne tous les bateaux
                for (int i = 0; i < currentPlayerTurn.Tab_Ships.Length; i++)
                {
                    if (currentPlayerTurn.Tab_Ships[i] != null)
                    {
                        currentPlayerTurn.Tab_Ships[i].IsSelected = false;
                    }
                }

                //si ce bateau ne nous appartient pas
                if(!Array.Exists(currentPlayerTurn.Tab_Ships, element => element == mapGenerated.GetTile(column, row).ShipOverTile))
                {
                    errorMove = ActionShipForm.SHIP_IS_NOT_MINE;
                    errorAttack = ActionShipForm.SHIP_IS_NOT_MINE;
                }
                // Aucun mouvement disponible
                else if (actionLeft <= 0)
                {
                    errorMove = ActionShipForm.NO_MORE_MOVE;
                    errorAttack = ActionShipForm.NO_MORE_MOVE;
                }
                else
                {
                    // Impossible de bouger
                    if (false)
                    {
                        // CONDITION A COMPLETER
                        errorMove = ActionShipForm.NO_MOVE_POSSIBLE;
                    }
                }


                ActionShipForm selectedShip = new ActionShipForm(errorMove, errorAttack);

                //désenregistre puis enregistre la méthode de mouvement
                selectedShip.UnregisterdisplayShipMoveFunction(mapGenerated.GetTile(column, row).ShipOverTile.ShowMove);
                selectedShip.RegisterdisplayShipMoveFunction(mapGenerated.GetTile(column, row).ShipOverTile.ShowMove);

                //désenregistre puis enregistre la méthode d'attaque
                selectedShip.UnregisterDisplayShipAttackFunction();
                selectedShip.RegisterDisplayShipAttackFunction(mapGenerated.GetTile(column, row).ShipOverTile.ShowAttack);

                mapGenerated.GetTile(column, row).ShipOverTile.IsSelected = true;

                
                //affiche la boite de dialogue
                selectedShip.ShowDialog();

                shipHasBeenUsed = selectedShip.HasBeenUsed;
            }//else if (mapGenerated.GetTile(column, row).Ship...

            else
            {
                ResetTags();
            }

            //si la tuile en question est une base
            if (mapGenerated.GetTile(column, row).Type == Tile.TileType.Base && !shipHasBeenUsed)
            {
                ResetTags();

                // Aucun mouvement disponible
                if (actionLeft <= 0)
                {
                    errorConquest = ActionBaseForm.NO_MORE_MOVE;
                    errorShip = ActionBaseForm.NO_MORE_MOVE;
                }
                else
                {
                    // Erreurs concernant la colonisation
                    if (currentPlayerTurn == mapGenerated.GetTile(column, row).Owner)
                    {
                        errorConquest = ActionBaseForm.BASE_IS_MINE;
                    }
                    // pas de bateau à moi dans la base
                    else if (mapGenerated.GetTile(column, row).ShipOverTile == null || !Array.Exists(currentPlayerTurn.Tab_Ships, element => element == mapGenerated.GetTile(column, row).ShipOverTile))
                    {
                        errorConquest = ActionBaseForm.NO_SHIP_IN_BASE;
                    }
                    //base pas à moi mais bateau dans la base
                    else if (mapGenerated.GetTile(column, row).ShipOverTile != null && Array.Exists(currentPlayerTurn.Tab_Ships, element => element == mapGenerated.GetTile(column, row).ShipOverTile))
                    {
                        errorConquest = ActionBaseForm.NO_ERROR;
                    }


                    // Erreurs concernant la création de navire
                    if (currentPlayerTurn != mapGenerated.GetTile(column, row).Owner)
                    {
                        errorShip = ActionBaseForm.BASE_IS_NOT_MINE;
                    }
                    // il y a un bateau dans la base (à moi ou à l'adversaire)
                    else if (mapGenerated.GetTile(column, row).ShipOverTile != null)
                    {
                        errorShip = ActionBaseForm.SHIP_IN_BASE;
                    }
                    // limite de création de bateau atteinte
                    else if (false) 
                    {
                        // CONDITION A COMPLETER
                        errorShip = ActionBaseForm.NO_MORE_SHIP;
                    }
                }

                ActionBaseForm selectOptionInBase = new ActionBaseForm(errorConquest, errorShip);

                //crée les navires
                if (createShipCallback != null)
                {
                    selectOptionInBase.UnregisterCreateShipFunction(() => { createShipCallback(currentPlayerTurn, mapGenerated.GetTile(column, row), (PictureBox)sender); });
                    selectOptionInBase.RegisterCreateShipFunction(() => { createShipCallback(currentPlayerTurn, mapGenerated.GetTile(column, row), (PictureBox)sender); });
                }

                //permet de capturer une base
                if (mapGenerated.GetTile(column, row).ShipOverTile != null)
                {
                    selectOptionInBase.UnregisterCaptureShipFunction();
                    selectOptionInBase.RegisterCaptureShipFunction(() => mapGenerated.GetTile(column, row).CaptureBase(currentPlayerTurn));
                }

                //affiche la boite de dialigue
                selectOptionInBase.ShowDialog();

                baseHasBeenUsed = selectOptionInBase.HasBeenUsed;


                //// Vérifie si la base a été utilisée, le cas échéant décrémente le nombre d'actions disponibles et l'affiche
                //if (baseHasBeenUsed)
                //{
                //    actionLeft--;
                //    actionLeftLabel.Text = Convert.ToString(actionLeft);
                //}

                // Vérifie si la base a été utilisée, le cas échéant décrémente le nombre d'actions disponibles et l'affiche
                if (selectOptionInBase.HasColonised)
                {
                    //Label lb = (Label)(this.Controls.Find("shipP1Label", true)[0]);
                    

                    if (currentPlayerTurn == player1)
                    {
                        nbShipP1 += GameManager.SHIP_PER_BASE;
                        nbBaseP1 += 1;
                        affectValueToLabel(nbShipP1, (Label)(this.Controls.Find("shipP"+currentPlayerTurn.Name + "Label", true)[0]));
                        affectValueToLabel(nbBaseP1, (Label)(this.Controls.Find("baseP"+ currentPlayerTurn.Name + "Label", true)[0]));
                    }
                    else
                    {
                        nbShipP2 += GameManager.SHIP_PER_BASE;
                        nbBaseP2 += 1;
                        affectValueToLabel(nbShipP2, (Label)(this.Controls.Find("shipP" + currentPlayerTurn.Name + "Label", true)[0]));
                        affectValueToLabel(nbBaseP2, (Label)(this.Controls.Find("baseP" + currentPlayerTurn.Name + "Label", true)[0]));
                    }
                }

                // Vérifie si la base a été utilisée, le cas échéant décrémente le nombre d'actions disponibles et l'affiche
                if (baseHasBeenUsed)
                {
                    actionLeft--;
                    actionLeftLabel.Text = Convert.ToString(actionLeft);
                }

            }//if (mapGenerated.GetTile(column, row).Type == Tile.TileType.Base && !shipHasBeenUsed)

            //MessageBox.Show("Rien ne se passe", "Trempette", MessageBoxButtons.OK);
        }

        /// <summary>
        /// Permet de changer de joueur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endOfTurnButton_Click(object sender, EventArgs e)
        {
            //réinitialise le plateau de jeu
            ResetTags();
            ResetDisplayedTile();

            // Vérifie si c'est le tour du joueur 1 et le cas échéant donne le tour au joueur 2, sinon il fait l'inverse
            if (player1.IsTurn)
            {
                player1.IsTurn = false;
                player2.IsTurn = true;
                currentPlayerTurn = player2;

                // Colorie le TLP en bas à la couleur du Joueur 2
                (this.Controls.Find("nbActionsEndTableLayoutPanel", true)[0]).BackColor = player2.Color;

                MessageBox.Show("C'est au tour de " + player2.Name, "Changement de joueur", MessageBoxButtons.OK);
            }
            else
            {
                player1.IsTurn = true;
                player2.IsTurn = false;
                currentPlayerTurn = player1;

                // Colorie le TLP en bas à la couleur du Joueur 1
                (this.Controls.Find("nbActionsEndTableLayoutPanel", true)[0]).BackColor = player1.Color;

                MessageBox.Show("C'est au tour de " + player1.Name, "Changement de joueur", MessageBoxButtons.OK);
            }

            

            // Réinitialise le nombre d'actions restantes pour le prochain joueur et l'affiche
            actionLeft = 2;
            actionLeftLabel.Text = Convert.ToString(actionLeft);
        }

        /// <summary>
        /// Ajoute les informations des joueurs dans leurs champs respectifs.
        /// </summary>
        /// <param name="infoTableLayoutPanel"> TLP dans lequel il faut insérer nos TLP avec les infos des joueurs </param>
        /// <param name="player"> Informations du joueur </param>
        /// <returns> Le TLP créé avec les infos </returns>
        private TableLayoutPanel addInfoPlayer(TableLayoutPanel infoTableLayoutPanel, Player player)
        {
            Label nameLabel = new Label();
            Label nbShipsLabel = new Label();
            Label nbBasesLabel = new Label();

            PictureBox basePictBox = new PictureBox();
            PictureBox shipPictBox = new PictureBox();

            TableLayoutPanel shipsTableLayoutPanel = new TableLayoutPanel();
            TableLayoutPanel basesTableLayoutPanel = new TableLayoutPanel();

            // Création du label contenant le nom du joueur
            nameLabel.Font = new Font("Papyrus", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            nameLabel.Margin = new Padding(0);
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            //nameLabel.BorderStyle = BorderStyle.FixedSingle;
            nameLabel.Location = new Point(LEFT_POSITION_INFO, TOP_POSITION_INFO);
            nameLabel.Name = "nameP" + player.Name + "Label";
            nameLabel.Size = new Size(WIDTH_INFO_TLPANEL, HEIGHT_INFO_TLPANEL);
            nameLabel.TabIndex = 0;
            nameLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            infoTableLayoutPanel.Controls.Add(nameLabel, 0, 0);

            // Ajout du nom en fonction du joueur et ajout d'un TableLayoutPanel pour le décompte des bateaux et des bases
            if (player1 == player)
            {
                nameLabel.Text = player1.Name;

                shipsTableLayoutPanel = createColumnsTableLayoutPanel(infoTableLayoutPanel, "shipsTableLayoutPanelP1", Color.Chocolate, 0, 1);
                basesTableLayoutPanel = createColumnsTableLayoutPanel(infoTableLayoutPanel, "basesTableLayoutPanelP1", Color.Firebrick, 0, 2);

                affectValueToLabel(nbShipP1, nbShipsLabel);
                affectValueToLabel(nbBaseP1, nbBasesLabel);

            }
            else
            {
                nameLabel.Text = player2.Name;

                shipsTableLayoutPanel = createColumnsTableLayoutPanel(infoTableLayoutPanel, "shipsTableLayoutPanelP2", Color.Coral, 0, 1);
                basesTableLayoutPanel = createColumnsTableLayoutPanel(infoTableLayoutPanel, "basesTableLayoutPanelP2", Color.Aquamarine, 0, 2);

                affectValueToLabel(nbShipP2, nbShipsLabel);
                affectValueToLabel(nbBaseP2, nbBasesLabel);
            }

            // Ajout de l'image des bateaux
            shipPictBox.Image = Bitmap.FromFile("../../img/ship.png");
            shipPictBox.Margin = new Padding(0);
            shipPictBox.Location = new Point(LEFT_POSITION_INFO, TOP_POSITION_INFO);
            shipPictBox.Name = "shipP" + player.Name + "PictureBox";
            shipPictBox.Size = new Size(WIDTH_INFO_TLPANEL / 3, WIDTH_INFO_TLPANEL / 3);
            shipPictBox.SizeMode = PictureBoxSizeMode.StretchImage;
            shipPictBox.TabIndex = 1;
            shipPictBox.TabStop = false;
            shipsTableLayoutPanel.Controls.Add(shipPictBox, 0, 0);

            // Création du label contenant le nombre de bateaux du joueur
            nbShipsLabel.Font = new Font("Papyrus", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            nbShipsLabel.Margin = new Padding(0);
            nbShipsLabel.TextAlign = ContentAlignment.MiddleCenter;
            //nbShipsLabel.BorderStyle = BorderStyle.FixedSingle;
            nbShipsLabel.Location = new Point(LEFT_POSITION_INFO, TOP_POSITION_INFO);
            nbShipsLabel.Name = "shipP" + player.Name + "Label";
            nbShipsLabel.Size = new Size(WIDTH_INFO_TLPANEL * 2 / 3, WIDTH_INFO_TLPANEL / 3);
            nbShipsLabel.TabIndex = 0;
            //nbShipsLabel.Text = "09"; // voir au-dessus
            nbShipsLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            shipsTableLayoutPanel.Controls.Add(nbShipsLabel, 1, 0);

            // Ajout de l'image des bases
            basePictBox.Image = Bitmap.FromFile("../../img/base.png");
            basePictBox.Margin = new Padding(0);
            basePictBox.Location = new Point(LEFT_POSITION_INFO, TOP_POSITION_INFO);
            basePictBox.Name = "baseP" + player.Name + "PictureBox";
            basePictBox.Size = new Size(WIDTH_INFO_TLPANEL / 3, WIDTH_INFO_TLPANEL / 3);
            basePictBox.SizeMode = PictureBoxSizeMode.StretchImage;
            basePictBox.TabIndex = 1;
            basePictBox.TabStop = false;
            basesTableLayoutPanel.Controls.Add(basePictBox, 0, 0);

            // Création du label contenant le nombre de bases du joueur
            nbBasesLabel.Font = new Font("Papyrus", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            nbBasesLabel.Margin = new Padding(0);
            nbBasesLabel.TextAlign = ContentAlignment.MiddleCenter;
            //nbBasesLabel.BorderStyle = BorderStyle.FixedSingle;
            nbBasesLabel.Location = new Point(LEFT_POSITION_INFO, TOP_POSITION_INFO);
            nbBasesLabel.Name = "baseP" + player.Name + "Label";
            nbBasesLabel.Size = new Size(WIDTH_INFO_TLPANEL * 2 / 3, WIDTH_INFO_TLPANEL / 3);
            nbBasesLabel.TabIndex = 0;
            //nbBasesLabel.Text = "03"; // voir au-dessus
            nbBasesLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            basesTableLayoutPanel.Controls.Add(nbBasesLabel, 1, 0);

            return infoTableLayoutPanel;
        }

        /// <summary>
        /// Affecte une valeur à un label
        /// </summary>
        /// <param name="valueToAffect"> </param>
        /// <param name="labelToModify"> </param>
        private void affectValueToLabel(int valueToAffect, Label labelToModify)
        {
            // Vérifie si la valeur à affecter est inférieure à 10 afin d'ajouter un "0" d'esthétique si nécessaire
            if (10 > valueToAffect)
            {
                labelToModify.Text = "0" + Convert.ToString(valueToAffect);
            }
            else
            {
                labelToModify.Text = Convert.ToString(valueToAffect);
            }
        }

        /// <summary>
        /// Crée un TLP à deux colonnes et une ligne
        /// </summary>
        /// <param name="previousTableLayoutPanel"> TLP dans lequel il faut insérer notre TLP </param>
        /// <param name="nameOfPanel"> Le nom à donner au TLP </param>
        /// <param name="backgroundColor"> Couleur de fond </param>
        /// <param name="posX"> Position X dans le TLP précédent </param>
        /// <param name="posY"> Position Y dans le TLP précédent </param>
        /// <param name="percentFirstColumn"> Taille en % de la première colonne </param>
        /// <returns> Le TLP créé </returns>
        private TableLayoutPanel createColumnsTableLayoutPanel(TableLayoutPanel previousTableLayoutPanel, string nameOfPanel, Color backgroundColor,
                                                               int posX, int posY, float percentFirstColumn = 0)
        {
            TableLayoutPanel columnsTableLayoutPanel = new TableLayoutPanel();

            if (percentFirstColumn != 0)
            {
                columnsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percentFirstColumn));
                columnsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (100 - percentFirstColumn)));
            }
            columnsTableLayoutPanel.ColumnCount = 2;
            columnsTableLayoutPanel.Margin = new Padding(0);
            columnsTableLayoutPanel.Dock = DockStyle.None;
            columnsTableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            columnsTableLayoutPanel.Location = new Point(LEFT_POSITION_INFO, TOP_POSITION_INFO);
            columnsTableLayoutPanel.Name = nameOfPanel;
            columnsTableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            if (DEBUG)
            {
                columnsTableLayoutPanel.BackColor = backgroundColor;
            }
            else
            {
                columnsTableLayoutPanel.BackColor = Color.Empty;
            }

            previousTableLayoutPanel.Controls.Add(columnsTableLayoutPanel, posX, posY);

            return columnsTableLayoutPanel;
        }

        /// <summary>
        /// Crée un TLP pour qu'il contienne les infos des joueurs
        /// </summary>
        /// <param name="nameOfPanel"> Le nom à donner au TLP </param>
        /// <param name="backgroundColor"> Couleur de fond </param>
        /// <param name="posX"> Position X dans le TLP précédent </param>
        /// <param name="player"> Informations du joueur </param>
        /// <returns> Le TLP créé </returns>
        private TableLayoutPanel createInfoPanel(string nameOfPanel, Color backgroundColor, int posX, Player player)
        {
            TableLayoutPanel infoTableLayoutPanel = new TableLayoutPanel();

            infoTableLayoutPanel.RowCount = INFO_PLAYERS_LINES;
            infoTableLayoutPanel.Margin = new Padding(0);
            infoTableLayoutPanel.Dock = DockStyle.None;
            infoTableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            infoTableLayoutPanel.Location = new Point(LEFT_POSITION_INFO, TOP_POSITION_INFO);
            infoTableLayoutPanel.Size = new Size(WIDTH_INFO_TLPANEL, HEIGHT_INFO_TLPANEL);
            infoTableLayoutPanel.Name = nameOfPanel;
            infoTableLayoutPanel.BackColor = backgroundColor;
            infoBoardTableLayoutPanel.Controls.Add(infoTableLayoutPanel, posX, POS_Y_FOR_PLAYER_INFO);

            if (player1 == player)
            {
                infoTableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            }
            else
            {
                infoTableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
            }

            return infoTableLayoutPanel;
        }

        /// <summary>
        /// Réinitialise les pictureBox 
        /// </summary>
        private void ResetDisplayedTile()
        {

            //remet les images par défaut
            for (int i = 0; i < mapGenerated.Width; i++)
            {
                for (int j = 0; j < mapGenerated.Height; j++)
                {
                    //si il ne contient pas de navires
                    if (mapGenerated.GetTile(i, j).ShipOverTile == null || boardPictBox[i, j].Tag.ToString() == GameManager.BUTTON_DIPLAYED_MOVE)
                    {
                        if (mapGenerated.GetTile(i, j).Type == GuerreNavale.Tile.TileType.Sea)
                        {
                            boardPictBox[i, j].Image = imagesRessources["water"];
                        }
                        else if (mapGenerated.GetTile(i, j).Type == GuerreNavale.Tile.TileType.Base)
                        {
                            boardPictBox[i, j].Image = imagesRessources["base"];

                            if (mapGenerated.GetTile(i, j).Owner != null)
                            {
                                boardPictBox[i, j].BackColor = mapGenerated.GetTile(i, j).Owner.Color;
                            }
                            else
                            {
                                boardPictBox[i, j].BackColor = Color.White;
                            }
                        }
                        else
                        {
                            boardPictBox[i, j].Image = imagesRessources["land"];
                        }
                    }//if (mapGenerated.GetTile(i, j).ShipOverTile

                    else if (mapGenerated.GetTile(i, j).ShipOverTile != null)
                    {
                        boardPictBox[i, j].Image = imagesRessources["shipOnWater"];

                        if (Array.Exists(player1.Tab_Ships, element => element == mapGenerated.GetTile(i, j).ShipOverTile))
                        {
                            boardPictBox[i, j].BackColor = player1.Color;
                        }
                        else if (Array.Exists(player2.Tab_Ships, element => element == mapGenerated.GetTile(i, j).ShipOverTile))
                        {
                            boardPictBox[i, j].BackColor = player2.Color;
                        }
                    }
                }
            }

            //permet de collecter ce qui n'est plus nécessaire
            GC.Collect();
        }

        /// <summary>
        /// réinitialise les tags
        /// </summary>
        private void ResetTags()
        {
            for (int i = 0; i < mapGenerated.Width; i++)
            {
                for (int j = 0; j < mapGenerated.Height; j++)
                {
                    boardPictBox[i, j].Tag = "";
                }
            }
        }

        /// <summary>
        /// Permet de charger une partie des images en mémoire pour ne pas devoir les recharger des fichiers à chaque utilisation
        /// </summary>
        private void loadImages()
        {
            imagesRessources["land"] = Bitmap.FromFile("../../img/sand.png");
            imagesRessources["water"] = Bitmap.FromFile("../../img/water.jpg");
            imagesRessources["shipOnWater"] = Bitmap.FromFile("../../img/shipOnWater.png");
            imagesRessources["base"] = Bitmap.FromFile("../../img/basePictureTransparent.png");
        }

        /// <summary>
        /// Permet d'enregistrer la méthode de création de navire
        /// </summary>
        /// <param name="callback">méthode à enregistrer</param>
        public void RegisterCreateShipFunction(Action<Player, Tile, PictureBox> callback)
        {
            createShipCallback += ((player, tile, pb) => {
                callback(player, tile, pb);
                if (player == player1)
                {
                    nbShipP1--;
                    affectValueToLabel(nbShipP1, (Label)(this.Controls.Find("shipP" + currentPlayerTurn.Name + "Label", true)[0]));
                }
                else
                {
                    nbShipP2--;
                    affectValueToLabel(nbShipP2, (Label)(this.Controls.Find("shipP" + currentPlayerTurn.Name + "Label", true)[0]));
                }
            });
        }

        /// <summary>
        /// Permet de désenregistrer la méthode de création de navire
        /// </summary>
        /// <param name="callback">méthode à désenregistrer</param>
        public void UnregisterCreateShipFunction(Action<Player, Tile, PictureBox> callback)
        {
            createShipCallback -= callback;
        }

        private void NavalWarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DialogResult.No == MessageBox.Show("Voulez-vous vraiment quitter le jeu ?", "Quitter", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                e.Cancel = true;
            }
        }
    }
}
