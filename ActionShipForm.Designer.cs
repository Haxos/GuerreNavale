namespace GuerreNavale
{
    partial class ActionShipForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionShipForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.attackButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.errorMoveLabel = new System.Windows.Forms.Label();
            this.errorAttackLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(105, 205);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // attackButton
            // 
            this.attackButton.Location = new System.Drawing.Point(12, 120);
            this.attackButton.Name = "attackButton";
            this.attackButton.Size = new System.Drawing.Size(90, 23);
            this.attackButton.TabIndex = 1;
            this.attackButton.Text = "Attaquer";
            this.attackButton.UseVisualStyleBackColor = true;
            this.attackButton.Click += new System.EventHandler(this.attackButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(12, 35);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(90, 23);
            this.moveButton.TabIndex = 2;
            this.moveButton.Text = "Déplacer";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // errorMoveLabel
            // 
            this.errorMoveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorMoveLabel.Location = new System.Drawing.Point(127, 40);
            this.errorMoveLabel.Name = "errorMoveLabel";
            this.errorMoveLabel.Size = new System.Drawing.Size(145, 40);
            this.errorMoveLabel.TabIndex = 3;
            this.errorMoveLabel.Text = "error move";
            // 
            // errorAttackLabel
            // 
            this.errorAttackLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorAttackLabel.Location = new System.Drawing.Point(127, 125);
            this.errorAttackLabel.Name = "errorAttackLabel";
            this.errorAttackLabel.Size = new System.Drawing.Size(145, 40);
            this.errorAttackLabel.TabIndex = 4;
            this.errorAttackLabel.Text = "error attack";
            // 
            // ActionShipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.errorAttackLabel);
            this.Controls.Add(this.errorMoveLabel);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.attackButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActionShipForm";
            this.Text = "Action depuis un bateau";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button attackButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Label errorMoveLabel;
        private System.Windows.Forms.Label errorAttackLabel;
    }
}