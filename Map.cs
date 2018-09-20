///ETML
/// Auteur : Adrian MAYO CARTES
/// Date : 05.10.2016
/// Description : Permet de généré une carte (map)
/// 

///Version : 1.0.1
/// Auteur : Adrian MAYO CARTES
/// Date : 02.11.2016
/// Description : - nombre d'iles centrales du constructeur mis au début des valeurs facultatives
///               - temps de pause entre chaque génération d'iles passé de 10 à 100 ms
/// 

///Version : 1.0.2
/// Auteur : Jérémy DELAY
/// Date : 08.11.2016
/// Description : Taille des îles centrales placées avant celles des îles personnelles dans le constructeur
/// 

///Version : 1.0.3
/// Auteur : Adrian MAYO CARTES
/// Date : 09.11.2016
/// Description : - Bug fixé au niveau de la création de la sous-map
/// 

///Version : 1.0.4
/// Auteur : Adrian MAYO CARTES
/// Date : 16.11.2016
/// Description : - mise en place de la classe Tile en lieu et place de l'ancien système en (enum) TileType
///               - ajout de la méthode InitialiseTabTile(int width, int height)
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GuerreNavale
{
    public class Map
    {
        /*--------ENUMERATION--------*/

        /*enumération des différents patterns des iles centrales
        
            PTRN = pattern
            0 = pas d'ile
            1= ile

            l'ordre des nombres est comme suit

            PRTN_abc_def_ghi :
            _____________
            | a | b | c |
            |___|___|___|
            | d | e | f |
            |___|___|___|
            | g | h | i |
            |___|___|___|

        */
        private enum CentralIslandsPattern
        {
            PTRN_010_000_010 = 21, PTRN_100_000_001 = 22, PTRN_001_000_100 = 23,
            PTRN_010_000_101 = 31, PTRN_101_000_010 = 32, PTRN_100_010_001 = 33, PTRN_001_010_100 = 34,
            PTRN_010_101_010 = 41, PTRN_101_000_101 = 42
        }

        /*--------CONSTANTES--------*/
        private const double PORCENT_SURFACE_MIN = 0.7; //pourcentage min de terre dans une ile
        private const double PORCENT_SURFACE_MAX = 0.8; //pourcentage max de terre dans une ile
        private const int COEFF_2_ISLANDS_CENTRAL_COLUMN = 1;   //coefficient d'apparition du pattern où les iles se situent dans la colonne central
        private const int COEFF_2_ISLANDS_DIAG = 1;             //coefficient d'apparition du pattern où les iles se situent dans les 2 coins opposés
        private const int COEFF_3_ISLANDS_V = 1;                //coefficient d'apparition du pattern où les iles forment un V
        private const int COEFF_3_ISLANDS_DIAG = 1;             //coefficient d'apparition du pattern où les iles se situent sur la diagonale
        private const int COEFF_4_ISLANDS_CORNERS = 1;          //coefficient d'apparition du pattern où les iles forme un losange/carré
        private const int COEFF_4_ISLANDS_NOT_CORNERS = 1;      //coefficient d'apparition du pattern où les iles se situent dans les 4 coins
        private const int WIDTH_PATTERN = 3;    //largeur du pattern des iles centrales
        private const int HEGHT_PATTERN = 3;    //hauteur du pattern des iles centrales
        private const double RATIO_CENTRAL_SUBMAP_WIDTH = 0.4;      //ratio de la longeur de la sous-map contenant les iles centrales est de 2/5 de la longueur de la map
        private const double RATIO_CENTRAL_SUBMAP_HEIGHT = 0.75;    //ratio de la hauteur de la sous-map contenant les iles centrales est de 3/4 de la hauteur de la map

        private const int NB_BASE_ON_PLAYER_ISLAND = 3;     //nb de base maritime dans les iles de base des joueurs

        /*---------VARIABLES--------*/
        private int nbCentralIsland;            //nombre d'iles centrales
        private Island[] tab_islands;           //tableau contenant toutes les iles, 0 = ileBase1, 1 = ileBase2, le reste étant les iles centrales
        private int width;                      //largeur du plateau de jeu
        private int height;                     //hauteur du plateau de jeu
        private Tile[,] tab_tiles;    //tuiles(cases) du plateau de jeu
        Player P1;
        Player P2;

        /*--------PROPRIETES--------*/
        //retourne le nombre d'iles centrales, le set est protégé
        public int NbCentralIsland
        {
            get { return nbCentralIsland; }
            protected set { }
        }

        //retourne la largeur de la map, le set est protégé
        public int Width
        {
            get { return width; }
            protected set { }
        }

        //retourne la hauteur de la map, le set est protégé
        public int Height
        {
            get { return height; }
            protected set { }
        }

        /*-------CONSTRUCTEURS------*/
        public Map(Player P1, Player P2, int widthMap, int heightMap, int nbCentralIsland = 3, int widthCentralIsland = 8, int heightCentralIsland = 10, int widthBaseIsland = 8, int heightBaseIsland = 30)
        {
            //initialise les différentes variables 
            this.width = widthMap;
            this.height = heightMap;
            this.nbCentralIsland = nbCentralIsland;

            this.P1 = P1;
            this.P2 = P2;

            //génération de la map
            GenerateMap(widthBaseIsland, heightBaseIsland, widthCentralIsland, heightCentralIsland);
        }

        /*---------METHODES---------*/
        /// <summary>
        /// Permet de généré la map avec les iles dessus
        /// </summary>
        /// <param name="widthBaseIsland">largeur des iles des bases des joueurs</param>
        /// <param name="heightBaseIsland">hauteur des iles de bases des joueurs</param>
        /// <param name="widthCentralIsland">largeur des iles centrales</param>
        /// <param name="heightCentralIsland">hauteur des iles centrales</param>
        private void GenerateMap(int widthBaseIsland, int heightBaseIsland, int widthCentralIsland, int heightCentralIsland)
        {
            int surfaceMinBaseIsland = Convert.ToInt32(widthBaseIsland * heightBaseIsland * PORCENT_SURFACE_MIN);           //surface minimun des iles de bases
            int surfaceMaxBaseIsland = Convert.ToInt32(widthBaseIsland * heightBaseIsland * PORCENT_SURFACE_MAX);           //surface maximum des iles de bases
            int surfaceMinCentralIsland = Convert.ToInt32(widthCentralIsland * heightCentralIsland * PORCENT_SURFACE_MIN);  //surface minimun des iles centrales
            int surfaceMaxCentralIsland = Convert.ToInt32(widthCentralIsland * heightCentralIsland * PORCENT_SURFACE_MAX);  //surface maximun des iles centrales

            //on initialise le tableau en 2 + nb des iles centrales, 2 étant les 2 iles qui serviront de bases
            tab_islands = new Island[2 + nbCentralIsland];

            //on crée l'ile de base du joueur 1
            tab_islands[0] = new Island(widthBaseIsland, heightBaseIsland, surfaceMinBaseIsland, surfaceMaxBaseIsland);

            //pause de 100 millisecondes
            Thread.Sleep(100);

            //on crée l'ile de base du joueur 2
            tab_islands[1] = new Island(widthBaseIsland, heightBaseIsland, surfaceMinBaseIsland, surfaceMaxBaseIsland);

            //on crée l'ile centrales
            for (int i = 0; i < nbCentralIsland; i++)
            {
                tab_islands[2 + i] = new Island(widthCentralIsland, heightCentralIsland, surfaceMinCentralIsland, surfaceMaxCentralIsland);
                Thread.Sleep(100);
            }

            //on place les bases
            PutBases();

            //initialisation du plateau de tuiles
            tab_tiles = InitialiseTabTile(width, height);

            //Créer la sous map contenant les iles centrales
            //BUGFIXED: le calcul pour obtenir était, précedemment, (widthCentralIsland*nbCentralIsland) au lieu de (width*RATIO_CENTRAL_SUBMAP_WIDTH)
            Tile[,] tab_subMap = GenerateSubMapCentralIsland(Convert.ToInt32(width * RATIO_CENTRAL_SUBMAP_WIDTH), Convert.ToInt32(height * RATIO_CENTRAL_SUBMAP_HEIGHT));

            //installation des différentes iles
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //on installe l'ile de base du joueur 1 sur la map
                    if ((j >= ((height - heightBaseIsland) / 2) && j <= (((height - heightBaseIsland) / 2) + tab_islands[0].Height - 1)) && (i >= 1 && i <= (tab_islands[0].Width)))
                    {
                        tab_tiles[i, j] = tab_islands[0].GetTile(i - 1, j - ((height - heightBaseIsland) / 2));
                    }
                    //on installe l'ile de base du joueur 2 sur la map
                    else if ((j >= ((height - heightBaseIsland) / 2) && j <= (((height - heightBaseIsland) / 2) + tab_islands[1].Height - 1)) && (i >= (width - tab_islands[1].Width - 1) && i <= (width - 2)))
                    {
                        tab_tiles[i, j] = tab_islands[1].GetTile(i - (width - tab_islands[1].Width - 1), j - ((height - heightBaseIsland) / 2));
                    }
                    //on injecte la sous-map
                    else if (j == ((height - tab_subMap.GetLength(1)) / 2) && i == ((width - tab_subMap.GetLength(0)) / 2))
                    {
                        InjectSubMapToMap(ref i, ref j, tab_subMap.GetLength(0), tab_subMap.GetLength(1), tab_subMap);
                    }
                }
            }

            //reassignation des coordonnes des tuiles
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tab_tiles[i, j].SetNewCoordonnates(i, j);
                }
            }
        }

        /// <summary>
        /// Retourne l'un des pattern des iles centrales
        /// </summary>
        /// <returns>pattern choisi</returns>
        private CentralIslandsPattern GetPatternCentralIslands()
        {
            CentralIslandsPattern patternChoose;    //pattern choisi qui sera retourné
            Random rnd = new Random();              //variable qui choisi une valeur int de façon random
            int valueChoose;                        //valeur retourné par de random

            //séléctionne le cas selon le nombres d'iles centrales
            switch (nbCentralIsland)
            {
                case 2:
                    //valeur généré aléatoirement
                    valueChoose = rnd.Next(COEFF_2_ISLANDS_CENTRAL_COLUMN + COEFF_2_ISLANDS_DIAG);

                    //le pattern choisi est celui qui contient les iles dans la colonne central
                    if (valueChoose < COEFF_2_ISLANDS_CENTRAL_COLUMN)
                    {
                        patternChoose = CentralIslandsPattern.PTRN_010_000_010;
                    }
                    //le pattern choisi est l'un des 2 qui se situe dans les coins opposés
                    else
                    {
                        switch (rnd.Next(2))
                        {
                            case 0:
                                patternChoose = CentralIslandsPattern.PTRN_001_000_100;
                                break;

                            default:
                                patternChoose = CentralIslandsPattern.PTRN_100_000_001;
                                break;
                        }
                    }
                    break;

                case 3:
                    //valeur généré aléatoirement
                    valueChoose = rnd.Next(COEFF_3_ISLANDS_DIAG + COEFF_3_ISLANDS_V);

                    //le pattern choisi est l'un de ceux qui font la diagonal
                    if (valueChoose < COEFF_3_ISLANDS_DIAG)
                    {
                        switch (rnd.Next(2))
                        {
                            case 0:
                                patternChoose = CentralIslandsPattern.PTRN_100_010_001;
                                break;

                            default:
                                patternChoose = CentralIslandsPattern.PTRN_001_010_100;
                                break;
                        }
                    }
                    //le pattern choisi est l'un de ceux qui font une forme de V
                    else
                    {
                        switch (rnd.Next(2))
                        {
                            case 0:
                                patternChoose = CentralIslandsPattern.PTRN_010_000_101;
                                break;

                            default:
                                patternChoose = CentralIslandsPattern.PTRN_101_000_010;
                                break;
                        }
                    }
                    break;

                default:
                    //valeur généré aléatoirement
                    valueChoose = valueChoose = rnd.Next(COEFF_4_ISLANDS_CORNERS + COEFF_4_ISLANDS_NOT_CORNERS);

                    //le pattern choisi est celui qui a les iles dans les coins
                    if (valueChoose < COEFF_4_ISLANDS_CORNERS)
                    {
                        patternChoose = CentralIslandsPattern.PTRN_101_000_101;
                    }
                    //le pattern choisi est celui en forme de losange/carré
                    else
                    {
                        patternChoose = CentralIslandsPattern.PTRN_010_101_010;
                    }
                    break;
            }

            //retourne le pattern choisi
            return patternChoose;
        }

        /// <summary>
        /// Retourne un tableau des différentes iles centrales qui est un sous tableau de la map global
        /// </summary>
        /// <param name="width">largeur de la sous-map</param>
        /// <param name="height">hauteur de la sous-map</param>
        /// <returns>Un tableau à 2 dimension de type TileType (tableau de tuiles)</returns>
        private Tile[,] GenerateSubMapCentralIsland(int width, int height)
        {
            Tile[,] tab_centralIslands = InitialiseTabTile(width, height);

            int whichIsland = 0;

            //divise le nom du pattern en information de ligne
            //ex: PTRN_101_000_101 => tab_string = {"PTRN", "101", "000", "101"}
            string[] tab_subStringTemp = GetPatternCentralIslands().ToString().Split('_');

            //tableau jaggered de transition
            char[][] tab_values = { tab_subStringTemp[1].ToCharArray(), tab_subStringTemp[2].ToCharArray(), tab_subStringTemp[3].ToCharArray() };

            //passe dans toutes les cases de la sous-map (donc 3x3 carré)
            for (int i = 0; i < tab_values.Length; i++)
            {
                for (int j = 0; j < tab_values[i].Length; j++)
                {
                    //si le carré de la sous-map concerné doit avoir une ile,
                    //alors on ajoute l'ile dans cette case
                    if ((tab_values[j][i]) == '1')
                    {
                        for (int k = 0; k < (width / 3); k++)
                        {
                            for (int l = 0; l < (height / 3); l++)
                            {
                                tab_centralIslands[k + (i * width / 3), l + (j * height / 3)] = tab_islands[2 + whichIsland].GetTile(k, l);
                            }
                        }

                        whichIsland++;
                    }
                }
            }

            return tab_centralIslands;
        }

        /// <summary>
        /// injecte la sous-map dans la map principal
        /// </summary>
        /// <param name="i">colonne de la map principal</param>
        /// <param name="j">ligne de la map principal</param>
        /// <param name="widthSubMap">largeur de la sous-map</param>
        /// <param name="heightSubMap">hauteur de la sous-map</param>
        /// <param name="tab_subMap">tableau de tuile de la sous-map</param>
        private void InjectSubMapToMap(ref int i, ref int j, int widthSubMap, int heightSubMap, Tile[,] tab_subMap)
        {
            int temp = j; //garde en mémoire la valeur de j

            //parcourt toute la sous-map
            for (int k = 0; k < widthSubMap; k++)
            {
                for (int l = 0; l < heightSubMap; l++)
                {
                    tab_tiles[i, j] = tab_subMap[k, l];
                    j++;
                }
                i++;
                j = temp;
            }
        }

        /// <summary>
        /// Permet de placer les bases maritimes
        /// </summary>
        private void PutBases()
        {
            bool isBasePut = false;
            Random rnd = new Random();

            //place les bases sur l'ile du joueur 1
            for (int i = 0; i < tab_islands[0].Height; i++)
            {
                if (i % Convert.ToInt32(Math.Ceiling((double)tab_islands[0].Height / (NB_BASE_ON_PLAYER_ISLAND + 1))) == 0 && i != 0)
                {
                    //à la ligne donnée on vérifie pour chaque case si c'est de la terre
                    //on controle de droite à gauche
                    for (int j = tab_islands[0].Width - 1; !isBasePut && j >= 0; j--)
                    {
                        if (tab_islands[0].GetTile(j, i).Type == Tile.TileType.Land)
                        {
                            tab_islands[0].GetTile(j, i).SetNewTileType(Tile.TileType.Base);
                            tab_islands[0].GetTile(j, i).CaptureBase(P1);

                            isBasePut = true;
                        }
                    }

                }

                isBasePut = false;
            }

            //place les bases sur l'ile du joueur 2
            for (int i = 0; i < tab_islands[1].Height; i++)
            {
                if (i % Convert.ToInt32(Math.Ceiling((double)tab_islands[1].Height / (NB_BASE_ON_PLAYER_ISLAND + 1))) == 0 && i != 0)
                {
                    //à la ligne donnée on vérifie pour chaque case si c'est de la terre
                    //on controle de gauche à droite
                    for (int j = 0; !isBasePut && j < tab_islands[1].Width; j++)
                    {
                        if (tab_islands[1].GetTile(j, i).Type == Tile.TileType.Land)
                        {
                            tab_islands[1].GetTile(j, i).SetNewTileType(Tile.TileType.Base);
                            tab_islands[1].GetTile(j, i).CaptureBase(P2);

                            isBasePut = true;
                        }
                    }

                }

                isBasePut = false;
            }

            //int k = -1;
            //int l = -1;
            int k = 0;
            int l = 0;

            //place les bases sur les iles centrales
            //la variable i commence à 2 car les 2 premières cases du tab_islands correspond au iles de départ des joueurs
            for (int i = 2; i < tab_islands.Length; i++)
            {
                while (!isBasePut)
                {

                    k = rnd.Next(tab_islands[i].Width);
                    l = rnd.Next(tab_islands[i].Height);

                    switch (rnd.Next(4))
                    {
                        case 0:
                            //à la ligne donnée (l) on vérifie pour chaque case si c'est de la terre
                            //on controle de gauche à droite
                            for (int j = 0; !isBasePut && j < tab_islands[i].Width; j++)
                            {
                                if (tab_islands[i].GetTile(j, l).Type == Tile.TileType.Land)
                                {
                                    tab_islands[i].GetTile(j, l).SetNewTileType(Tile.TileType.Base);

                                    isBasePut = true;
                                }
                            }
                            break;
                        case 1:
                            //à la ligne donnée (l) on vérifie pour chaque case si c'est de la terre
                            //on controle de droite à gauche
                            for (int j = tab_islands[i].Width - 1; !isBasePut && j >= 0; j--)
                            {
                                if (tab_islands[i].GetTile(j, l).Type == Tile.TileType.Land)
                                {
                                    tab_islands[i].GetTile(j, l).SetNewTileType(Tile.TileType.Base);

                                    isBasePut = true;
                                }
                            }
                            break;
                        case 2:
                            //à la colonne donnée (k) on vérifie pour chaque case si c'est de la terre
                            //on controle de haut en bas
                            for (int j = 0; !isBasePut && j < tab_islands[i].Height; j++)
                            {
                                if (tab_islands[i].GetTile(k, j).Type == Tile.TileType.Land)
                                {
                                    tab_islands[i].GetTile(k, j).SetNewTileType(Tile.TileType.Base);

                                    isBasePut = true;
                                }
                            }
                            break;
                        default:
                            //à la colonne donnée (k) on vérifie pour chaque case si c'est de la terre
                            //on controle de bas en haut
                            for (int j = tab_islands[i].Height - 1; !isBasePut && j >= 0; j--)
                            {
                                if (tab_islands[i].GetTile(k, j).Type == Tile.TileType.Land)
                                {
                                    tab_islands[i].GetTile(k, j).SetNewTileType(Tile.TileType.Base);

                                    isBasePut = true;
                                }
                            }
                            break;
                    }//switch (rnd.Next(4))

                }//while (!isBasePut)

                isBasePut = false;
            }

        }

        /// <summary>
        /// Retourne la tuile
        /// </summary>
        /// <param name="column">colonne</param>
        /// <param name="row">ligne</param>
        /// <returns>la valeur de la tuile</returns>
        public Tile GetTile(int column, int row)
        {
            return tab_tiles[column, row];
        }

        /// <summary>
        /// Initialise le tableau de tuile
        /// </summary>
        /// <param name="width">Largeur du tableau</param>
        /// <param name="height">Hauteur du tableau</param>
        private Tile[,] InitialiseTabTile(int width, int height)
        {
            Tile[,] tab_tiles = new Tile[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tab_tiles[i, j] = new Tile(i, j, Tile.TileType.Sea);
                }
            }

            return tab_tiles;
        }
    }
}
