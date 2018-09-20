/// ETML
/// Auteur : Adrian MAYO CARTES
/// Date : 23.11.2016
/// Description : Classe gérant les informations d'une case comme s'il y a un bateau sur cette dernière, 
///                 ses coordonnées, son type de tuile, etc...
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuerreNavale
{
    public class Tile
    {
        /*--------ENUMERATION--------*/

        //enum de différents type de tuiles
        public enum TileType { Sea, Land, Base };

        /*--------CONSTANTES--------*/

        /*---------VARIABLES--------*/
        private int posX = -1;      //dans quelle colonne il se trouve
        private int posY = -1;      //dans quelle ligne il se trouve
        private TileType tileType;  //quel type de tuile est-ce
        private Ship shipOverTile = null;   //quel bateau se trouve sur cette tuile
        private Player ownerBase = null;    //qui possède la tuile

        /*--------PROPRIETES--------*/
        //retourne dans quelle colonne se trouve cette tuile, le set est protégé
        public int X
        {
            get { return posX; }
            protected set { }
        }

        //retourne dans quelle ligne se trouve cette tuile, le set est protégé
        public int Y
        {
            get { return posY; }
            protected set { }
        }

        //retourne le type de cette tuile, le set est protégé
        public TileType Type
        {
            get { return tileType; }
            protected set { }
        }

        //retourne le propriétaire de la tuile, le set est protégé
        public Player Owner
        {
            get { return ownerBase; }
            protected set { }
        }

        //retourne le navire qui est sur cette tuile
        public Ship ShipOverTile
        {
            get { return shipOverTile; }
            set { shipOverTile = value; }
        }
        /*-------CONSTRUCTEURS------*/
        ///// <summary>
        ///// Constructeur de la classe
        ///// </summary>
        ///// <param name="posX">dans quelle colonne il se trouve</param>
        ///// <param name="posY">dans quelle ligne il se trouve</param>
        //public Tile(int posX, int posY)
        //{
        //    this.posX = posX;
        //    this.posY = posY;

        //}

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="posX">dans quelle colonne il se trouve</param>
        /// <param name="posY">dans quelle ligne il se trouve</param>
        /// <param name="type">quel type de tuile est-ce</param>
        public Tile(int posX, int posY, TileType type)
        {
            this.posX = posX;
            this.posY = posY;
            this.tileType = type;

        }

        /*---------METHODES---------*/
        /// <summary>
        /// Permet d'attribuer un nouveau type à cette tuile
        /// </summary>
        /// <param name="type">nouveau type de la tuile</param>
        /// <param name="player"></param>
        public void SetNewTileType(TileType type, Player player = null)
        {
            //si le nouveau type est différent de l'ancien
            if (tileType != type)
            {
                //change le type de la tuile
                tileType = type;

                //si ce type est une base alors il y attribue un propriétaire
                if (tileType == TileType.Base)
                {
                    ownerBase = player;
                }
            }
        }

        /// <summary>
        /// Permet de réassigner les coordonnées
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetNewCoordonnates(int x, int y)
        {
            this.posX = x;
            this.posY = y;
        }

        /// <summary>
        /// Si cette tuile est une base alors le joueur en question le capture
        /// </summary>
        /// <param name="player">Le joueur qui capture</param>
        public void CaptureBase(Player player)
        {
            //si cette tuile est une base alors le propriétaire de la tuile devient le joueur
            if (tileType == TileType.Base)
            {
                ownerBase = player;
            }
        }

        /// <summary>
        /// Utilisé pour mettre un bateau sur cette tuile
        /// </summary>
        /// <param name="ship">Le navire qui est sur cette tuile</param>
        public void EnterShipOnThisTile(Ship ship)
        {
            if (shipOverTile == null
                && tileType != TileType.Land)
            {
                shipOverTile = ship;
            }
        }

        /// <summary>
        /// Utiliser pour retirer le navire de cette tuile
        /// </summary>
        public void ExitShipOfThisTile()
        {
            shipOverTile = null;
        }
    }
}
