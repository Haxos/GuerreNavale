namespace GuerreNavale
{
    partial class HomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            this.SizeLabel = new System.Windows.Forms.Label();
            this.sizeTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.smallRadioButton = new System.Windows.Forms.RadioButton();
            this.mediumRadioButton = new System.Windows.Forms.RadioButton();
            this.bigRadioButton = new System.Windows.Forms.RadioButton();
            this.islandLabel = new System.Windows.Forms.Label();
            this.nbIslandComboBox = new System.Windows.Forms.ComboBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.player1TextBox = new System.Windows.Forms.TextBox();
            this.player2TextBox = new System.Windows.Forms.TextBox();
            this.colorLabel = new System.Windows.Forms.Label();
            this.colorP1ComboBox = new System.Windows.Forms.ComboBox();
            this.colorP2ComboBox = new System.Windows.Forms.ComboBox();
            this.beginButton = new System.Windows.Forms.Button();
            this.sizeTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Font = new System.Drawing.Font("Papyrus", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SizeLabel.Location = new System.Drawing.Point(36, 42);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(134, 21);
            this.SizeLabel.TabIndex = 0;
            this.SizeLabel.Text = "Taille de la carte :";
            // 
            // sizeTableLayoutPanel
            // 
            this.sizeTableLayoutPanel.ColumnCount = 3;
            this.sizeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.sizeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.sizeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.sizeTableLayoutPanel.Controls.Add(this.smallRadioButton, 0, 0);
            this.sizeTableLayoutPanel.Controls.Add(this.mediumRadioButton, 1, 0);
            this.sizeTableLayoutPanel.Controls.Add(this.bigRadioButton, 2, 0);
            this.sizeTableLayoutPanel.Location = new System.Drawing.Point(225, 40);
            this.sizeTableLayoutPanel.Name = "sizeTableLayoutPanel";
            this.sizeTableLayoutPanel.RowCount = 1;
            this.sizeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sizeTableLayoutPanel.Size = new System.Drawing.Size(224, 30);
            this.sizeTableLayoutPanel.TabIndex = 1;
            // 
            // smallRadioButton
            // 
            this.smallRadioButton.AutoSize = true;
            this.smallRadioButton.Location = new System.Drawing.Point(3, 3);
            this.smallRadioButton.Name = "smallRadioButton";
            this.smallRadioButton.Size = new System.Drawing.Size(50, 17);
            this.smallRadioButton.TabIndex = 0;
            this.smallRadioButton.Text = "Small";
            this.smallRadioButton.UseVisualStyleBackColor = true;
            this.smallRadioButton.CheckedChanged += new System.EventHandler(this.smallRadioButton_CheckedChanged);
            // 
            // mediumRadioButton
            // 
            this.mediumRadioButton.AutoSize = true;
            this.mediumRadioButton.Checked = true;
            this.mediumRadioButton.Location = new System.Drawing.Point(77, 3);
            this.mediumRadioButton.Name = "mediumRadioButton";
            this.mediumRadioButton.Size = new System.Drawing.Size(62, 17);
            this.mediumRadioButton.TabIndex = 1;
            this.mediumRadioButton.TabStop = true;
            this.mediumRadioButton.Text = "Medium";
            this.mediumRadioButton.UseVisualStyleBackColor = true;
            this.mediumRadioButton.CheckedChanged += new System.EventHandler(this.mediumRadioButton_CheckedChanged);
            // 
            // bigRadioButton
            // 
            this.bigRadioButton.AutoSize = true;
            this.bigRadioButton.Location = new System.Drawing.Point(151, 3);
            this.bigRadioButton.Name = "bigRadioButton";
            this.bigRadioButton.Size = new System.Drawing.Size(40, 17);
            this.bigRadioButton.TabIndex = 2;
            this.bigRadioButton.Text = "Big";
            this.bigRadioButton.UseVisualStyleBackColor = true;
            this.bigRadioButton.CheckedChanged += new System.EventHandler(this.bigRadioButton_CheckedChanged);
            // 
            // islandLabel
            // 
            this.islandLabel.AutoSize = true;
            this.islandLabel.Font = new System.Drawing.Font("Papyrus", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.islandLabel.Location = new System.Drawing.Point(36, 99);
            this.islandLabel.Name = "islandLabel";
            this.islandLabel.Size = new System.Drawing.Size(113, 21);
            this.islandLabel.TabIndex = 3;
            this.islandLabel.Text = "Nombre d\'îles :";
            // 
            // nbIslandComboBox
            // 
            this.nbIslandComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nbIslandComboBox.FormattingEnabled = true;
            this.nbIslandComboBox.Items.AddRange(new object[] {
            "2",
            "3"});
            this.nbIslandComboBox.Location = new System.Drawing.Point(228, 99);
            this.nbIslandComboBox.Name = "nbIslandComboBox";
            this.nbIslandComboBox.Size = new System.Drawing.Size(120, 21);
            this.nbIslandComboBox.TabIndex = 4;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Papyrus", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(36, 152);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(139, 21);
            this.nameLabel.TabIndex = 5;
            this.nameLabel.Text = "Noms des joueurs :";
            // 
            // player1TextBox
            // 
            this.player1TextBox.Location = new System.Drawing.Point(228, 153);
            this.player1TextBox.MaxLength = 10;
            this.player1TextBox.Name = "player1TextBox";
            this.player1TextBox.Size = new System.Drawing.Size(100, 20);
            this.player1TextBox.TabIndex = 6;
            this.player1TextBox.Text = "Player 1";
            // 
            // player2TextBox
            // 
            this.player2TextBox.Location = new System.Drawing.Point(334, 152);
            this.player2TextBox.MaxLength = 10;
            this.player2TextBox.Name = "player2TextBox";
            this.player2TextBox.Size = new System.Drawing.Size(100, 20);
            this.player2TextBox.TabIndex = 7;
            this.player2TextBox.Text = "Player 2";
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Font = new System.Drawing.Font("Papyrus", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorLabel.Location = new System.Drawing.Point(36, 212);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(157, 21);
            this.colorLabel.TabIndex = 8;
            this.colorLabel.Text = "Couleur des joueurs :";
            // 
            // colorP1ComboBox
            // 
            this.colorP1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorP1ComboBox.FormattingEnabled = true;
            this.colorP1ComboBox.Items.AddRange(new object[] {
            "Brique",
            "Gris",
            "Rose",
            "Rouge",
            "Vert"});
            this.colorP1ComboBox.Location = new System.Drawing.Point(228, 212);
            this.colorP1ComboBox.Name = "colorP1ComboBox";
            this.colorP1ComboBox.Size = new System.Drawing.Size(100, 21);
            this.colorP1ComboBox.TabIndex = 9;
            this.colorP1ComboBox.DropDownClosed += new System.EventHandler(this.colorP1ComboBox_DropDownClosed);
            // 
            // colorP2ComboBox
            // 
            this.colorP2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorP2ComboBox.FormattingEnabled = true;
            this.colorP2ComboBox.Items.AddRange(new object[] {
            "Brique",
            "Gris",
            "Rose",
            "Rouge",
            "Vert"});
            this.colorP2ComboBox.Location = new System.Drawing.Point(334, 212);
            this.colorP2ComboBox.Name = "colorP2ComboBox";
            this.colorP2ComboBox.Size = new System.Drawing.Size(100, 21);
            this.colorP2ComboBox.TabIndex = 10;
            this.colorP2ComboBox.DropDownClosed += new System.EventHandler(this.colorP2ComboBox_DropDownClosed);
            // 
            // beginButton
            // 
            this.beginButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.beginButton.Location = new System.Drawing.Point(192, 290);
            this.beginButton.Name = "beginButton";
            this.beginButton.Size = new System.Drawing.Size(100, 23);
            this.beginButton.TabIndex = 12;
            this.beginButton.Text = "Commencer !";
            this.beginButton.UseVisualStyleBackColor = true;
            this.beginButton.Click += new System.EventHandler(this.beginButton_Click);
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.beginButton);
            this.Controls.Add(this.colorP2ComboBox);
            this.Controls.Add(this.colorP1ComboBox);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.player2TextBox);
            this.Controls.Add(this.player1TextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nbIslandComboBox);
            this.Controls.Add(this.islandLabel);
            this.Controls.Add(this.sizeTableLayoutPanel);
            this.Controls.Add(this.SizeLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HomeForm";
            this.Text = "Home";
            this.sizeTableLayoutPanel.ResumeLayout(false);
            this.sizeTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.TableLayoutPanel sizeTableLayoutPanel;
        private System.Windows.Forms.RadioButton smallRadioButton;
        private System.Windows.Forms.RadioButton mediumRadioButton;
        private System.Windows.Forms.RadioButton bigRadioButton;
        private System.Windows.Forms.Label islandLabel;
        private System.Windows.Forms.ComboBox nbIslandComboBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox player1TextBox;
        private System.Windows.Forms.TextBox player2TextBox;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.ComboBox colorP1ComboBox;
        private System.Windows.Forms.ComboBox colorP2ComboBox;
        private System.Windows.Forms.Button beginButton;
    }
}