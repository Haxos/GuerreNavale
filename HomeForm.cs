/// ETML
/// Auteur : Jérémy DELAY
/// Date : 23.11.2016
/// Description : Crée un formulaire d'accueil permettant de recueillir les informations saisies par les joueurs
/// 

/// Version : 1.0.1
/// Auteur : Adrian MAYO CARTES
/// Date : 23.11.2016
/// Description : Ajout des propriétés de la classe
/// 

/// Version : 1.0.2
/// Auteur : Jérémy DELAY
/// Date : 19.12.2016
/// Description : Ajout de la demande à l'utilisateur avant de fermer le formulaire
/// 
/// Modification : ANNULÉ - car cela demande confirmation, lorsque l'on souhaite passer à la suite.
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

    public partial class HomeForm : Form
    {
        /*----------DEBUG-----------*/


        /*--------CONSTANTES--------*/
        private const int NB_PLAYER = 2;

        /*---------VARIABLES--------*/
        private string namePlayer1; //contient le nom du joueur 1
        private string namePlayer2; //contient le nom du joueur 2
        private Color colorPlayer1; //contient la couleur utilisée par le joueur 1
        private Color colorPlayer2; //contient la couleur utilisée par le joueur 2
        private int sizeMap;        //taille de la carte choisie
        private int nbCentralIsland;//nb d'ile au centrales
        private bool valuesIsEntered = false;

        /*--------PROPRIETES--------*/

        //retourne le nom choisi par le joueur 1, le set est protégé
        public string NameP1
        {
            get { return namePlayer1; }
            protected set { }
        }

        //retourne le nom choisi par le joueur 2, le set est protégé
        public string NameP2
        {
            get { return namePlayer2; }
            protected set { }
        }

        //retourne la couleur choisi par le joueur 1, le set est protégé
        public Color ColorP1
        {
            get { return colorPlayer1; }
            protected set { }
        }

        //retourne la couleur choisi par le joueur 2, le set est protégé
        public Color ColorP2
        {
            get { return colorPlayer2; }
            protected set { }
        }

        //retourne la taille de la carte choisie, le set est protégé
        public int SizeMap
        {
            get { return sizeMap; }
            protected set { }
        }

        //retourne le nombre d'iles centrales, le set est protégé
        public int NbCentralsIslands
        {
            get { return nbCentralIsland; }
            protected set { }
        }

        public bool ValuesIsEntered
        {
            get { return valuesIsEntered; }
            protected set { }
        }
        /*-------CONSTRUCTEURS------*/


        /*---------METHODES---------*/
        public HomeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Change le nombre d'îles sélectionnables lorsque l'on choisit la petite carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smallRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (true == smallRadioButton.Checked)
            {
                nbIslandComboBox.Items.Clear();
                nbIslandComboBox.Text = "";
                for (int i = GameManager.ALL_MAP_MIN_ISLAND; i <= GameManager.SMALL_MAP_MAX_ISLAND; i++)
                {
                    nbIslandComboBox.Items.Add(i);
                }
            }
        }

        /// <summary>
        /// Change le nombre d'îles sélectionnables lorsque l'on choisit la carte moyenne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediumRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (true == mediumRadioButton.Checked)
            {
                nbIslandComboBox.Items.Clear();
                nbIslandComboBox.Text = "";
                for (int i = GameManager.ALL_MAP_MIN_ISLAND; i <= GameManager.MEDIUM_MAP_MAX_ISLAND; i++)
                {
                    nbIslandComboBox.Items.Add(i);
                }
            }
        }

        /// <summary>
        /// Change le nombre d'îles sélectionnables lorsque l'on choisit la grande carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bigRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (true == bigRadioButton.Checked)
            {
                nbIslandComboBox.Items.Clear();
                nbIslandComboBox.Text = "";
                for (int i = GameManager.ALL_MAP_MIN_ISLAND; i <= GameManager.BIG_MAP_MAX_ISLAND; i++)
                {
                    nbIslandComboBox.Items.Add(i);
                }
            }
        }

        /// <summary>
        /// Efface la couleur choisie par le joueur 1 parmis les choix du joueur 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorP1ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string temp = colorP2ComboBox.Text;    // Variable contenant l'élément choisi de manière temporaire

            // Efface la collection d'éléments
            colorP2ComboBox.Items.Clear();

            // Ajoute tous les éléments pour le joueurs 2, sauf celui choisi par le joueur 1
            if ("Brique" != colorP1ComboBox.Text)
                colorP2ComboBox.Items.Add("Brique");
            if ("Gris" != colorP1ComboBox.Text)
                colorP2ComboBox.Items.Add("Gris");
            if ("Rose" != colorP1ComboBox.Text)
                colorP2ComboBox.Items.Add("Rose");
            if ("Rouge" != colorP1ComboBox.Text)
                colorP2ComboBox.Items.Add("Rouge");
            if ("Vert" != colorP1ComboBox.Text)
                colorP2ComboBox.Items.Add("Vert");

            colorP2ComboBox.Text = temp;
        }

        /// <summary>
        /// Efface la couleur choisie par le joueur 2 parmis les choix du joueur 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorP2ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string temp = colorP1ComboBox.Text;    // Variable contenant l'élément choisi de manière temporaire

            // Efface la collection d'éléments
            colorP1ComboBox.Items.Clear();

            // Ajoute tous les éléments pour le joueurs 1, sauf celui choisi par le joueur 2
            if ("Brique" != colorP2ComboBox.Text)
                colorP1ComboBox.Items.Add("Brique");
            if ("Gris" != colorP2ComboBox.Text)
                colorP1ComboBox.Items.Add("Gris");
            if ("Rose" != colorP2ComboBox.Text)
                colorP1ComboBox.Items.Add("Rose");
            if ("Rouge" != colorP2ComboBox.Text)
                colorP1ComboBox.Items.Add("Rouge");
            if ("Vert" != colorP2ComboBox.Text)
                colorP1ComboBox.Items.Add("Vert");

            colorP1ComboBox.Text = temp;
        }

        /// <summary>
        /// Vérifie l'authenticité des valeurs sélectionnées et, en cas de réussite, les garde en mémoire.
        /// Sinon un message d'erreur s'affiche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginButton_Click(object sender, EventArgs e)
        {
            // Vérifie la validité de infos séléctionnées
            if ("" == nbIslandComboBox.Text ||
               "" == player1TextBox.Text ||
               "" == player2TextBox.Text ||
               "" == colorP1ComboBox.Text ||
               "" == colorP2ComboBox.Text)
            {
                // Il y a une erreur
                MessageBox.Show("Au moins une valeur n'a pas été renseignée !", "Valeurs Incorrectes", MessageBoxButtons.OK);
            }
            else
            {
                string[] tab_color = new string[NB_PLAYER];
                tab_color[0] = colorP1ComboBox.Text;
                tab_color[1] = colorP2ComboBox.Text;

                for (int i = 0; i < NB_PLAYER; i++)
                {
                    switch (tab_color[i])
                    {
                        case "Brique":
                            tab_color[i] = "Firebrick";
                            break;
                        case "Gris":
                            tab_color[i] = "Gray";
                            break;
                        case "Rose":
                            tab_color[i] = "Pink";
                            break;
                        case "Rouge":
                            tab_color[i] = "Red";
                            break;
                        case "Vert":
                            tab_color[i] = "Green";
                            break;
                    }
                }

                namePlayer1 = player1TextBox.Text;
                namePlayer2 = player2TextBox.Text;
                colorPlayer1 = Color.FromName(tab_color[0]);
                colorPlayer2 = Color.FromName(tab_color[1]);
                nbCentralIsland = Convert.ToInt16(nbIslandComboBox.Text);

                if (smallRadioButton.Checked)
                {
                    sizeMap = GameManager.SMALL_MAP;
                }
                else if (mediumRadioButton.Checked)
                {
                    sizeMap = GameManager.MEDIUM_MAP;
                }
                else
                {
                    sizeMap = GameManager.BIG_MAP;
                }

                valuesIsEntered = true;

                this.Close();

            }
        }


    }
}
