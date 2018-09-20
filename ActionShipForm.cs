/// ETML
/// Auteur : Jérémy DELAY
/// Date : 16.11.2016
/// Description : Crée un formulaire d'action pour les navires
/// 
/// Manque :    action de déplacement
///             action d'attaque
///

/// Version : 1.0.1
/// Auteur : Adrian MAYO CARTES
/// Date : 23.11.2016
/// Description : Ajout graphique de la zone de déplacement
/// 
/// Manque :    action de déplacement
///             action d'attaque
///

/// Version : 1.0.2
/// Auteur : Adrian MAYO CARTES
/// Date : 30.11.2016
/// Description : Ajout graphique de la zone de déplacement
/// 
/// Manque :    action d'attaque
///

/// Version : 1.0.3
/// Auteur : Adrian MAYO CARTES
/// Date : 30.11.2016
/// Description : Ajout d'une propriété ainsi que d'une variable (hasBeenUsed)
/// 
/// Manque :    action d'attaque
///

/// Version : 1.0.4
/// Auteur : Adrian MAYO CARTES
/// Date : 14.12.2016
/// Description : - Ajout de la variable "displayShipAttack"
///               - Ajout des méthodes "RegisterDisplayShipAttackFunction" et "UnregisterDisplayShipAttackFunction"
///               - Modification de la méthode "attackButton_Click"
///

/// Version : 1.0.5
/// Auteur : Adrian MAYO CARTES
/// Date : 18.12.2016
/// Description : - Ajout de la constante SHIP_IS_NOT_MINE
///               - Ajout de la condition SHIP_IS_NOT_MINE dans les 2 switch
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
    public partial class ActionShipForm : Form
    {
        /*----------DEBUG-----------*/

        /*--------CONSTANTES--------*/
        public const int NO_ERROR = 0;

        public const int NO_MOVE_POSSIBLE = 7;

        public const int NO_MORE_MOVE = 6;

        public const int SHIP_IS_NOT_MINE = 8;


        /*---------VARIABLES--------*/
        private Action displayShipMove;
        private Action displayShipAttack;
        private bool hasBeenUsed = false;


        /*--------PROPRIETES--------*/
        public bool HasBeenUsed
        {
            get { return hasBeenUsed; }
            protected set { }
        }


        /*-------CONSTRUCTEURS------*/
        public ActionShipForm(int errorMove, int errorAttack)
        {
            // Initialise le formulaire
            InitializeComponent();

            /* Vérifie la valeur de l'erreur de déplacement et affiche un message,
               désactive le bouton lié si nécessaire */
            switch (errorMove)
            {
                case NO_MOVE_POSSIBLE:
                    moveButton.Enabled = false;
                    errorMoveLabel.Text = "Votre bateau ne peut pas bouger !";
                    errorMoveLabel.ForeColor = Color.Red;
                    break;
                case NO_MORE_MOVE:
                    moveButton.Enabled = false;
                    errorMoveLabel.Text = "Vous n'avez plus d'actions !";
                    errorMoveLabel.ForeColor = Color.Red;
                    break;
                case SHIP_IS_NOT_MINE:
                    moveButton.Enabled = false;
                    errorMoveLabel.Text = "Ce navire ne vous appartient pas !";
                    errorMoveLabel.ForeColor = Color.Red;
                    break;
                default:
                    moveButton.Enabled = true;
                    errorMoveLabel.Text = "Partez naviguer avec votre navire.";
                    errorMoveLabel.ForeColor = Color.Green;
                    break;
            }

            /* Vérifie la valeur de l'erreur d'attaque et affiche un message,
               désactive le bouton lié si nécessaire */
            switch (errorAttack)
            {
                case NO_MORE_MOVE:
                    attackButton.Enabled = false;
                    errorAttackLabel.Text = "Vous n'avez plus d'actions !";
                    errorAttackLabel.ForeColor = Color.Red;
                    break;
                case SHIP_IS_NOT_MINE:
                    attackButton.Enabled = false;
                    errorAttackLabel.Text = "Ce navire ne vous appartient pas !";
                    errorAttackLabel.ForeColor = Color.Red;
                    break;
                default:
                    attackButton.Enabled = true;
                    errorAttackLabel.Text = "À l'assaut !";
                    errorAttackLabel.ForeColor = Color.Green;
                    break;
            }
        }


        /*---------METHODES---------*/
        // Ferme la fenêtre
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Lance le déplacement du bateau
        private void moveButton_Click(object sender, EventArgs e)
        {
            displayShipMove();

            // Garde en mémoire le fait qu'une action a été effectuée
            hasBeenUsed = true;

            this.Close();
        }

        // Lance la phase d'attaque
        private void attackButton_Click(object sender, EventArgs e)
        {
            displayShipAttack();

            // Garde en mémoire le fait qu'une action a été effectuée
            hasBeenUsed = true;

            this.Close();
        }

        //enregistre la méthode d'affichage de mouvement des navires
        public void RegisterdisplayShipMoveFunction(Action callback)
        {
            displayShipMove += callback;
        }

        //enregistre la méthode d'affichage de mouvement des navires
        public void UnregisterdisplayShipMoveFunction(Action callback)
        {
            displayShipMove -= callback;
        }

        public void RegisterDisplayShipAttackFunction(Action callback)
        {
            displayShipAttack += callback;
        }

        public void UnregisterDisplayShipAttackFunction()
        {
            displayShipAttack = null;
        }
    }
}
