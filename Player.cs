/// ETML
/// Auteur : Adrian MAYO CARTES
/// Date : 23.11.2016
/// Description : Classe gérant les informations du joueur ainsi que les bateaux qui lui sont associés 
///

/// Version : 1.0.1
/// Auteur : Adrian MAYO CARTES
/// Date : 14.12.2016
/// Description : Modification de la méthode "CreateShip"
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GuerreNavale
{
    public class Player
    {
        /*--------CONSTANTES--------*/

        /*---------VARIABLES--------*/
        private string name;    //nom du joueur
        private Color color;    //couleur attribué au joueur
        private Ship[] tab_ships;    //tableau contenant les bateaux attribué à ce joueur
        private bool isTurn = false;    //true si c'est son tour de jeux
        //private Base[,] tab_basesOwned;

        /*--------PROPRIETES--------*/
        //retourne si c'est son tour ou pas, on peut modifier cette valeur
        public bool IsTurn
        {
            get { return isTurn; }
            set { isTurn = value; }
        }

        //retourne le nom du joueur, le set est protégé
        public string Name
        {
            get { return name; }
            protected set { }
        }

        //retourne la couleur du joueur, le set est protégé
        public Color Color
        {
            get { return color; }
            protected set { }
        }

        //retourne le tableau des navire des joueurs
        public Ship[] Tab_Ships
        {
            get { return tab_ships; }
            protected set { }
        }
        /*-------CONSTRUCTEURS------*/
        public Player(string name, Color color, int nbShipMax)
        {
            this.name = name;
            this.color = color;
            tab_ships = new Ship[nbShipMax];
        }

        /*---------METHODES---------*/
        public Ship CreateShip(Action<int, int, int> callbackMove, Action<int, int, int> callbackAttack, int x, int y, Map gameMap)
        {
            bool isShipCreated = false;
            Ship shipCreated = null;

            //crée un nouveau bateau au premier emplacement vide
            for (int i = 0; i < tab_ships.Length && !isShipCreated; i++)
            {
                if (tab_ships[i] == null)
                {
                    //crée le navire
                    tab_ships[i] = new Ship(x, y, gameMap);

                    //intègre la méthode de déplacement
                    tab_ships[i].RegisterDisplayMoveFunction(callbackMove);

                    tab_ships[i].RegisterAttackDisplayFunction(callbackAttack);

                    isShipCreated = true;

                    shipCreated = tab_ships[i];
                }
            }

            return shipCreated;

        }
    }
}
