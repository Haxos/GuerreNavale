/// ETML
/// Auteur : Adrian MAYO CARTES
/// Date : 23.11.2016
/// Description : Contient toutes les informations relatives aux navires
///

/// Version : 1.0.1
/// Auteur : Adrian MAYO CARTES
/// Date : 30.11.2016
/// Description : Modification des noms des méthodes et ajout de 2 propriétés
/// 

/// Version : 1.0.2
/// Auteur : Adrian MAYO CARTES
/// Date : 14.12.2016
/// Description : - Création des variables "cbRegistredAttackDisplayFunction" et "maxAttackDistance"
///               - Création des méthodes "RegisterAttackDisplayFunction" et "UnregisterAttackDisplayFunction"
///               - Création de la méthode "ShowAttack"
/// 
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuerreNavale
{
    public class Ship
    {
        /*--------ENUMERATION--------*/
        //catégorie du bateau
        //public enum SizeShip { Light = 1, Medium = 2, Heavy = 3 }

        /*--------CONSTANTES--------*/

        /*---------VARIABLES--------*/
        private bool isAlive = false;   //état du bateau (vivant ou mort)
        private int nbHealthPoint = 0;  //nombre de point de vie
        private int x = 0;  //position x du navire (dans quelle colonne il se trouve)
        private int y = 0;  //position y du navire (dans quelle ligne il se trouve)
        private int maxDistancePerMove = 5; //distance maximum du navire
        private Action cbRegistredFireFunction; //permet de concaténer une méthode dans cette variable. Permet de tirer
        private Action<int, int, int> cbRegistredDisplayMoveFunction; //permet de concaténer une méthode dans cette variable. Permet d'afficher les potentiels mouvement
        //private Action<int, int> cbRegistredMoveFunction; //permet de concaténer une méthode dans cette variable. Permet de bouger
        private Action<int, int, int> cbRegistredAttackDisplayFunction; //permet de concaténer une méthode dans cette variable. Permet d'afficher les cibles
        private int maxAttackDistance = 5; //distance maximale de tir
        private Map gameMap;    //Contient en mémoire la map du jeu
        private bool isSelected = false;

        /*--------PROPRIETES--------*/
        //retourne l'état du bateau, le set est protégé
        public bool IsAlive
        {
            get { return isAlive; }
            protected set { }
        }

        //retourne le nombre de point de vie du bateau, le set est protégé
        public int NbHealthPoint
        {
            get { return nbHealthPoint; }
            protected set { }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public int X
        {
            get { return x; }
            protected set { }
        }

        public int Y
        {
            get { return y; }
            protected set { }
        }
        /*-------CONSTRUCTEURS------*/
        /// <summary>
        /// Configuration par défaut
        /// </summary>
        public Ship(int x, int y, Map gameMap)
        {
            this.x = x;
            this.y = y;

            //l'état du bateau passe à vivant
            isAlive = true;

            //nbHealthPoint = (int)SizeShip.Light;

            //initialise le nombre de point de vie
            nbHealthPoint = 1;

            //initialise la map du jeu
            this.gameMap = gameMap;
        }

        /// <summary>
        /// Configuration personalisé
        /// </summary>
        /// <param name="size"></param>
        /*public Ship(SizeShip size)
        {
            isAlive = true;
            nbHealthPoint = (int)size;
        }*/

        /*---------METHODES---------*/
        /// <summary>
        /// Permet de faire subir des dégâts au navire
        /// </summary>
        public void Hit()
        {
            //le nombre de point de vie est diminué de 1
            nbHealthPoint--;

            //si le nombre de PV est en dessous de 0, alors son état (isAlive) est changé
            if (nbHealthPoint <= 0)
            {
                isAlive = false;
            }
        }

        /// <summary>
        /// Permet de faire tirer le bateau
        /// </summary>
        public void Fire()
        {
            //si on a enregistrer une méthode de tire dans cet object
            if (cbRegistredFireFunction != null)
            {
                cbRegistredFireFunction();
            }
        }

        /// <summary>
        /// Permet d'afficher les cibles potentielles
        /// </summary>
        public void ShowAttack()
        {
            if (cbRegistredAttackDisplayFunction != null)
            {
                cbRegistredAttackDisplayFunction(x, y, maxAttackDistance + 1);
            }
        }

        /// <summary>
        /// Permet de faire bouger le bateau
        /// </summary>
        public void ShowMove()
        {
            //si on a enregistrer une méthode de mouvement dans cet object
            if (cbRegistredDisplayMoveFunction != null)
            {
                cbRegistredDisplayMoveFunction(x, y, maxDistancePerMove + 1);
            }
        }

        ///// <summary>
        ///// Permet de bouger le navire dans une autre case
        ///// </summary>
        //public void Move(int newX, int newY)
        //{
        //    if(cbRegistredMoveFunction != null)
        //    {
        //        cbRegistredMoveFunction(newX, newY);
        //    }
        //}

        /// <summary>
        /// Change les coordonnées du navire
        /// </summary>
        /// <param name="newX">nouvelle position en X</param>
        /// <param name="newY">nouvelle position en Y</param>
        public void ChangeCoordonnates(int newX, int newY)
        {
            x = newX;
            y = newY;
        }

        /// <summary>
        /// Permet d'enregistrer une méthode dans une variable local à la classe
        /// La méthode servira à tirer avec le bateau
        /// </summary>
        /// <param name="callback">Méthode à injecter</param>
        public void RegisterFireFunction(Action callback)
        {
            cbRegistredFireFunction += callback;
        }

        /// <summary>
        /// Permet de désenregistrer une méthode dans une variable local à la classe
        /// </summary>
        /// <param name="callback">Méthode à injecter</param>
        public void UnregisterFireFunction(Action callback)
        {
            cbRegistredFireFunction -= callback;
        }

        /// <summary>
        /// Permet d'enregistrer une méthode dans une variable local à la classe
        /// La méthode servira à faire bouger le bateau
        /// </summary>
        /// <param name="callback">Méthode à injecter</param>
        public void RegisterDisplayMoveFunction(Action<int, int, int> callback)
        {
            cbRegistredDisplayMoveFunction += callback;
        }

        /// <summary>
        /// Permet de désenregistrer une méthode dans une variable local à la classe
        /// </summary>
        /// <param name="callback">Méthode à injecter</param>
        public void UnregisterDisplayMoveFunction(Action<int, int, int> callback)
        {
            cbRegistredDisplayMoveFunction -= callback;
        }


        public void RegisterAttackDisplayFunction(Action<int, int, int> callback)
        {
            cbRegistredAttackDisplayFunction += callback;
        }

        public void UnregisterAttackDisplayFunction()
        {
            cbRegistredAttackDisplayFunction = null;
        }
    }
}