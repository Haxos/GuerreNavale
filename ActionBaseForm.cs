/// ETML
/// Auteur : Jérémy DELAY
/// Date : 16.11.2016
/// Description : Crée un formulaire d'action pour les bases
/// 
/// Manque :    action de conquête
///             action création de navire
/// 

/// Version : 1.0.1
/// Auteur : Adrian MAYO CARTES
/// Date : 23.11.2016
/// Description : Ajout de la création d'un navire
/// 
/// Manque :    action de conquête
/// 

/// Version : 1.0.2
/// Auteur : Adrian MAYO CARTES
/// Date : 23.11.2016
/// Description : Ajout d'une variable (captureBaseCallback),
///               création de l'action de conquête et
///               ajout de 2 fonctions de callback
/// 

/// Version : 1.0.3
/// Auteur : Jérémy DELAY
/// Date : 14.12.2016
/// Description : Ajout de la booléenne hasBeenUsed et de sa propriété, ainsi
///               que de son changement de valeur lors d'une action effectuée
/// 

/// Version : 1.0.4
/// Auteur : Jérémy DELAY
/// Date : 19.12.2016
/// Description : - Ajout de l'icone du formulaire (obli remarqué)
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
    public partial class ActionBaseForm : Form
    {
        /*----------DEBUG-----------*/

        /*--------CONSTANTES--------*/
        public const int NO_ERROR = 0;

        public const int BASE_IS_MINE = 1;
        public const int NO_SHIP_IN_BASE = 2;

        public const int BASE_IS_NOT_MINE = 3;
        public const int SHIP_IN_BASE = 4;
        public const int NO_MORE_SHIP = 5;

        public const int NO_MORE_MOVE = 6;


        /*---------VARIABLES--------*/
        private Action createShipCallback;
        private Action captureBaseCallback;
        private bool hasBeenUsed = false;
        private bool hasColonised = false;
        private bool hasCreateShip = false;


        /*--------PROPRIETES--------*/
        public bool HasBeenUsed
        {
            get { return hasBeenUsed; }
            protected set { }
        }

        public bool HasColonised
        {
            get { return hasColonised; }
            protected set { }
        }

        public bool HasCreateShip
        {
            get { return hasCreateShip; }
            protected set { }
        }

        /*-------CONSTRUCTEURS------*/
        public ActionBaseForm(int errorConquest, int errorShip)
        {
            // Initialise le formulaire
            InitializeComponent();

            /* Vérifie la valeur de l'erreur de conquête et affiche un message,
               désactive le bouton lié si nécessaire */
            switch (errorConquest)
            {
                case BASE_IS_MINE:
                    conquestButton.Enabled = false;
                    errorConquestLabel.Text = "Cette base vous appartient déjà !";
                    errorConquestLabel.ForeColor = Color.Red;
                    break;
                case NO_SHIP_IN_BASE:
                    conquestButton.Enabled = false;
                    errorConquestLabel.Text = "Vous n'avez pas de bateau dans cette base !";
                    errorConquestLabel.ForeColor = Color.Red;
                    break;
                case NO_MORE_MOVE:
                    conquestButton.Enabled = false;
                    errorConquestLabel.Text = "Vous n'avez plus d'actions !";
                    errorConquestLabel.ForeColor = Color.Red;
                    break;
                default:
                    conquestButton.Enabled = true;
                    errorConquestLabel.Text = "Vous pouvez coloniser cette base.";
                    errorConquestLabel.ForeColor = Color.Green;
                    break;
            }

            /* Vérifie la valeur de l'erreur de création de bateau et affiche un message,
               désactive le bouton si nécessaire */
            switch (errorShip)
            {
                case BASE_IS_NOT_MINE:
                    createShipButton.Enabled = false;
                    errorShipLabel.Text = "Cette base ne vous appartient pas !";
                    errorShipLabel.ForeColor = Color.Red;
                    break;
                case SHIP_IN_BASE:
                    createShipButton.Enabled = false;
                    errorShipLabel.Text = "Il y a déjà un bateau dans la base !";
                    errorShipLabel.ForeColor = Color.Red;
                    break;
                case NO_MORE_SHIP:
                    createShipButton.Enabled = false;
                    errorShipLabel.Text = "Vous ne pouvez plus créer de bateau !";
                    errorShipLabel.ForeColor = Color.Red;
                    break;
                case NO_MORE_MOVE:
                    createShipButton.Enabled = false;
                    errorShipLabel.Text = "Vous n'avez plus d'actions !";
                    errorShipLabel.ForeColor = Color.Red;
                    break;
                default:
                    createShipButton.Enabled = true;
                    errorShipLabel.Text = "Vous pouvez créer un bateau.";
                    errorShipLabel.ForeColor = Color.Green;
                    break;
            }

        }


        /*---------METHODES---------*/
        // Ferme la fenêtre
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Change le possesseur de la base vers le conquérant
        private void conquestButton_Click(object sender, EventArgs e)
        {
            if (captureBaseCallback != null)
            {
                captureBaseCallback();
            }
            
            // Garde en mémoire le fait qu'une action a été effectuée
            hasBeenUsed = true;

            // Garde en mémoire le fait qu'une base a été colonisée
            hasColonised = true;

            this.Close();
        }

        // Crée un bateau dans la base
        private void createShipButton_Click(object sender, EventArgs e)
        {
            if (createShipCallback != null)
            {
                createShipCallback();
            }

            // Garde en mémoire le fait qu'une action a été effectuée
            hasBeenUsed = true;

            // Garde en mémoire le fait qu'un bateau a été créé
            hasCreateShip = true;

            this.Close();
        }

        public void RegisterCreateShipFunction(Action callback)
        {
            createShipCallback += callback;
        }

        public void UnregisterCreateShipFunction(Action callback)
        {
            createShipCallback -= callback;
        }

        public void RegisterCaptureShipFunction(Action callback)
        {
            captureBaseCallback += callback;
        }

        public void UnregisterCaptureShipFunction()
        {
            captureBaseCallback = null;
        }

    }
}
