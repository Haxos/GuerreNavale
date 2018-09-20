/// ETML
/// Auteur : Adrian MAYO CARTES
/// Date : 05.10.2016
/// Description : Classe qui permet de généré une iles avec toutes ses propriétés.
///     La génération de l'ile se fait de manière procédurale
/// 

/// Version : 1.0.1
/// Auteur : Adrian MAYO CARTES
/// Date : 16.11.2016
/// Description : - mise en place de la classe Tile en lieu et place de l'ancien système en (enum) TileType
///               - ajout de la méthode InitialiseTabTile(int width, int height)
///

/// Version : 1.0.2
/// Auteur : Adrian MAYO CARTES
/// Date : 30.11.2016
/// Description : - modification de la méthode GetTile, retourne la tuile et plus une valeur numérique
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuerreNavale
{
    class Island
    {

        /*--------ENUMERATION--------*/

        /*enum de différents type de parcelle utilisée(ici du 2x2)

        CH = parcelle
        0 = mer
        1 = terre
        l'ordre des nombres est comme suit
        |n°1|n°2|
        |n°3|n°4|
            */
        private enum Chunk
        {
            CH_1_1_1_1 = 41, //parcelle plein 4x4
            CH_1_1_1_0 = 31, CH_1_0_1_1 = 32, CH_0_1_1_1 = 33, CH_1_1_0_1 = 34, //parcelle à 3/4 plein 
            CH_1_1_0_0 = 21, CH_1_0_1_0 = 22, CH_0_0_1_1 = 23, CH_0_1_0_1 = 24, //parcelle à moité plein
            CH_1_0_0_0 = 11, CH_0_0_1_0 = 12, CH_0_0_0_1 = 13, CH_0_1_0_0 = 14,  //parcelle à 1/4 plein
            CH_0_0_0_0 = 0 //parcelle vide
        }

        /*--------CONSTANTES--------*/
        private const int WIDTH_CHUNK = 2;          //largeur d'un chunk
        private const int HEIGHT_CHUNK = 2;         //hauteur d'un chunk
        private const int COEFF_CHUNK_FULL = 10;     //coefficient pour les parcelles pleines 4x4
        private const int COEFF_CHUNK_3_QUARTER = 6;//coefficient pour les parcelles à 3/4 plein 
        private const int COEFF_CHUNK_HALF = 6;     //coefficient pour les parcelles à moitié plein
        private const int COEFF_CHUNK_QUARTER = 1;  //coefficient pour les parcelles bloc à 1/4 plein
        private const int COEFF_CHUNK_EMPTY = 3;    //coefficient pour les parcelles vide

        /*---------VARIABLES--------*/
        private int islandSurfaceMin;   //surface minimum de l'ile
        private int islandSurfaceMax;   //surface maximum de l'ile
        private int islandSurface = 0;  //surface  de l'ile
        private int width;              //largeur max de l'ile
        private int height;             //hauteur max de l'ile
        private Tile[,] tab_tiles;  //tableau des tuiles qui seront utilisé en jeu
        private Chunk[,] tab_chunks;    //tableau des parcelles pour la génération de l'ile
        Random rnd = new Random();      //variable aléatoire

        /*--------PROPRIETES--------*/
        //retourne la surface minimum de l'ile, le set est protégé
        public int IslandSurfaceMin
        {
            get
            {
                return islandSurfaceMin;
            }
            protected set { }
        }

        //retourne la surface maximum de l'ile, le set est protégé
        public int IslandSurfaceMax
        {
            get
            {
                return islandSurfaceMax;
            }
            protected set { }
        }

        //retourne la surface actuelle de l'ile, le set est protégé
        public int IslandSurface
        {
            get
            {
                return islandSurface;
            }
            protected set { }
        }

        //retourne la largeur de l'ile, le set est protégé
        public int Width
        {
            get
            {
                return width;
            }
            protected set { }
        }

        //retourne la hauteur de l'ile, le set est protégé
        public int Height
        {
            get
            {
                return height;
            }
            protected set { }
        }

        /*-------CONSTRUCTEURS------*/
        /// <summary>
        /// Constructeur de la classe Island
        /// </summary>
        /// <param name="width">Largeur max de l'ile</param>
        /// <param name="height">Hauteur max de l'ile</param>
        /// <param name="islandSurfaceMin">Supérficie min de l'ile</param>
        /// <param name="islandSurfaceMax">Supérficie max de l'ile</param>
        /// <param name="generateIsland">True = on génère l'ile dès que l'on initialise l'ile (false par défaut)</param>
        public Island(int width, int height, int islandSurfaceMin, int islandSurfaceMax, bool generateIsland = true)
        {
            this.width = width;
            this.height = height;
            this.islandSurfaceMin = islandSurfaceMin;
            this.islandSurfaceMax = islandSurfaceMax;

            //lance-t-on la génération de l'ile dès l'inisialisation de la classe
            if (generateIsland)
            {
                this.GenerateIsland();
            }
        }

        /*---------METHODES---------*/
        /// <summary>
        /// Génére l'ile selon les paramètres entrés dans le constructeur 
        /// </summary>
        public void GenerateIsland()
        {
            while (islandSurface < islandSurfaceMin || islandSurface > islandSurfaceMax)
            {

                islandSurface = 0;  //remet la surface de l'ile à 0

                int widthChunk = width / WIDTH_CHUNK;       //met à l'inférieur en cas de nombre à décimal
                int heightChunk = height / HEIGHT_CHUNK;    //met à l'inférieur en cas de nombre à décimal

                //si le tableau de parcelles (tab_chunks) ou si le tableau de tuile (tab_tiles) ne sont pas initialisés,
                //on les initialise
                if (tab_tiles == null || tab_chunks == null)
                {
                    InitialiseTabTile(width, height);
                    tab_chunks = new Chunk[widthChunk, heightChunk];
                }

                //passe dans tous les emplacements du tableau de parcelles (tab_chunks)
                //et y met une parcelle (chunk)
                for (int i = 0; i < widthChunk; i++)
                {
                    for (int j = 0; j < heightChunk; j++)
                    {
                        tab_chunks[i, j] = GetNextTile();

                        islandSurface += ((int)tab_chunks[i, j] / 10);
                    }
                }

                //converti les parcelles en tuiles
                ConvertChunkToTile(widthChunk, heightChunk);

                //assècheles lacs intérieur de 2X2
                DryInlandLac(2, 2);

                //assècheles lacs intérieur de 1X2
                DryInlandLac(1, 2);

                //assècheles lacs intérieur de 2X1
                DryInlandLac(2, 1);

                //assècheles lacs intérieur de 1X1
                DryInlandLac(1, 1);

                //retire les potentielles tuiles de terre des coins
                SmoothEdge();
            }
        }

        /// <summary>
        /// Retourne une tuile selon les coefficients pour chaque chunk défini en constantes
        /// </summary>
        /// <returns>un chunk</returns>
        private Chunk GetNextTile()
        {
            //valeur généré aléatoirement
            int randomValue = rnd.Next(COEFF_CHUNK_EMPTY + COEFF_CHUNK_QUARTER + COEFF_CHUNK_HALF + COEFF_CHUNK_3_QUARTER + COEFF_CHUNK_FULL);

            //retourne une parcelle vide
            if (randomValue < COEFF_CHUNK_EMPTY)
            {
                return Chunk.CH_0_0_0_0;
            }
            //retourne une parcelle à 1/4 plein
            else if (randomValue < (COEFF_CHUNK_EMPTY + COEFF_CHUNK_QUARTER))
            {
                //  /!\ écrit en dure !!!
                //retourne une tuile à 1/4 plein parmis les 4 qui existe
                switch (rnd.Next(4))
                {
                    case 0:
                        return Chunk.CH_0_0_0_1;

                    case 1:
                        return Chunk.CH_0_0_1_0;

                    case 2:
                        return Chunk.CH_0_1_0_0;

                    default:
                        return Chunk.CH_1_0_0_0;

                }
            }
            //retourne une parcelle à moitié pleine
            else if (randomValue < (COEFF_CHUNK_EMPTY + COEFF_CHUNK_QUARTER + COEFF_CHUNK_HALF))
            {
                //  /!\ écrit en dure !!!
                //retourne une tuile à moitié plein parmis les 4 qui existe
                switch (rnd.Next(4))
                {
                    case 0:
                        return Chunk.CH_0_0_1_1;

                    case 1:
                        return Chunk.CH_0_1_0_1;

                    case 2:
                        return Chunk.CH_1_0_1_0;

                    default:
                        return Chunk.CH_1_1_0_0;

                }
            }
            //retorune une parcelle à 3/4 plein
            else if (randomValue < (COEFF_CHUNK_EMPTY + COEFF_CHUNK_HALF + COEFF_CHUNK_3_QUARTER))
            {
                //  /!\ écrit en dure !!!
                //retourne une tuile à 3/4 plein parmis les 4 qui existe
                switch (rnd.Next(4))
                {
                    case 0:
                        return Chunk.CH_0_1_1_1;

                    case 1:
                        return Chunk.CH_1_0_1_1;

                    case 2:
                        return Chunk.CH_1_1_0_1;

                    default:
                        return Chunk.CH_1_1_1_0;

                }
            }
            //retourne une parcelle pleine
            else
            {
                return Chunk.CH_1_1_1_1;
            }

        }

        /// <summary>
        /// Converti le tableau de parcelle (tab_chunks) en tableau de tuile (tab_tiles)
        /// </summary>
        /// <param name="widthChunk">nombre de parcelle en largeur</param>
        /// <param name="heightChunk">nombre de parcelle en hauteur</param>
        private void ConvertChunkToTile(int widthChunk, int heightChunk)
        {
            //permet de receuillir les informations de la parcelle
            string[] subStringTemp;

            //passe dans tous les emplacement du tableau de parcelle (2x2 tuiles) et converti en tuile
            for (int i = 0; i < widthChunk; i++)
            {
                for (int j = 0; j < heightChunk; j++)
                {
                    //divise le nom de la parcelle en information
                    //ex: CH_1_0_1_0 => tab_string = {CH, 1, 0, 1, 0}
                    subStringTemp = (tab_chunks[i, j].ToString()).Split('_');

                    //se référé au fonctionnement d'une parcelle en haut du code
                    tab_tiles[i * WIDTH_CHUNK, j * HEIGHT_CHUNK].SetNewTileType((Tile.TileType)(Convert.ToInt16(subStringTemp[1])));           //emplacement n°1
                    tab_tiles[(i * WIDTH_CHUNK) + 1, j * HEIGHT_CHUNK].SetNewTileType((Tile.TileType)(Convert.ToInt16(subStringTemp[2])));     //emplacement n°2
                    tab_tiles[i * WIDTH_CHUNK, (j * HEIGHT_CHUNK) + 1].SetNewTileType((Tile.TileType)(Convert.ToInt16(subStringTemp[3])));     //emplacement n°3
                    tab_tiles[(i * WIDTH_CHUNK) + 1, (j * HEIGHT_CHUNK) + 1].SetNewTileType((Tile.TileType)(Convert.ToInt16(subStringTemp[4])));//emplacement n°4
                }
            }
        }

        /// <summary>
        /// Permet de supprimer les "petits" lacs interieurs de l'ile (valeurs conseillées 1 ou 2),
        /// /!\ La longueur et la hauteur indique un rectangle constitué que d'eau
        /// </summary>
        /// <param name="width">largeur (maximum ?) des lacs à supprimer</param>
        /// <param name="height">hauteur (maximum ?) des lacs à supprimer</param>
        public void DryInlandLac(int widthLac, int heightLac)
        {
            bool isALac;        //intique si la zone analysé est un lac ou pas
            int contour = 1;    //défini un contour dans lequel on ne vas pas y toucher

            //passe dans tous les emplacements de l'ile mais laisse le contour (de 1 case) intact
            for (int i = contour; i < (this.width - contour - widthLac + 1); i++)
            {
                for (int j = contour; j < (this.height - contour - heightLac + 1); j++)
                {
                    isALac = true;

                    for (int k = 0; k < widthLac; k++)
                    {
                        for (int l = 0; l < heightLac; l++)
                        {
                            //si il y a une case au moins qui n'est pas d'eau (TileType.Sea) dans le rectangle
                            if (tab_tiles[(k + i), (l + j)].Type != Tile.TileType.Sea)
                            {
                                isALac = false;
                            }
                            //permet de controler si les cases au dessus et en dessous du rectangle que l'on verifie sont de l'eau
                            //Indique que ce n'est pas un lac interieur et qu'il est connecté à la mer
                            if (((tab_tiles[(k + i), (l + j) - 1].Type == Tile.TileType.Sea) && (l == 0)) || ((tab_tiles[(k + i), (l + j) + 1].Type == Tile.TileType.Sea) && (l + 1 == heightLac)))
                            {
                                isALac = false;
                            }
                            //permet de controler si les cases à droite et à gauche du rectangle que l'on verifie sont de l'eau
                            //Indique que ce n'est pas un lac interieur et qu'il est connecté à la mer
                            if (((tab_tiles[(k + i) - 1, (l + j)].Type == Tile.TileType.Sea) && (k == 0)) || ((tab_tiles[(k + i) + 1, (l + j)].Type == Tile.TileType.Sea) && (k + 1 == widthLac)))
                            {
                                isALac = false;
                            }
                        }
                    }

                    //si c'est un lac, on convertit les cases eau en sol
                    if (isALac == true)
                    {
                        for (int k = 0; k < widthLac; k++)
                        {
                            for (int l = 0; l < heightLac; l++)
                            {
                                tab_tiles[(k + i), (l + j)].SetNewTileType(Tile.TileType.Land);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adouci les coins l'ile
        /// </summary>
        public void SmoothEdge()
        {
            int length = 3;
            int countUpWidth = 0;
            int countUpHeight = 0;

            //permet de ronger les 4 coins suppérieur
            for (int i = 0; i < length; i++)
            {
                countUpHeight = 0;
                for (int j = 0; j + countUpWidth < length; j++)
                {
                    //ronge le coin supérieur gauche
                    tab_tiles[i, j].SetNewTileType(Tile.TileType.Sea);

                    //ronge le coin supérieur droit
                    tab_tiles[(width - countUpWidth - 1), j].SetNewTileType(Tile.TileType.Sea);

                    //ronge le coin inférieur gauche
                    tab_tiles[i, (height - countUpHeight - 1)].SetNewTileType(Tile.TileType.Sea);

                    //ronge le coin inférieur droit
                    tab_tiles[(width - countUpWidth - 1), (height - countUpHeight - 1)].SetNewTileType(Tile.TileType.Sea);

                    countUpHeight++;
                }
                countUpWidth++;
            }
        }

        /// <summary>
        /// Retourne la tuile
        /// </summary>
        /// <param name="column">colonne</param>
        /// <param name="row">ligne</param>
        /// <returns>retourne la tuile</returns>
        public Tile GetTile(int column, int row)
        {
            return tab_tiles[column, row];
        }

        /// <summary>
        /// Initialise le tableau de tuile
        /// </summary>
        /// <param name="width">Largeur du tableau</param>
        /// <param name="height">Hauteur du tableau</param>
        private void InitialiseTabTile(int width, int height)
        {
            tab_tiles = new Tile[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tab_tiles[i, j] = new Tile(i, j, Tile.TileType.Sea);
                }
            }
        }
    }
}
