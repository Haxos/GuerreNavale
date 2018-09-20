namespace GuerreNavale
{
    partial class ActionBaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionBaseForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.conquestButton = new System.Windows.Forms.Button();
            this.createShipButton = new System.Windows.Forms.Button();
            this.errorConquestLabel = new System.Windows.Forms.Label();
            this.errorShipLabel = new System.Windows.Forms.Label();
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
            // conquestButton
            // 
            this.conquestButton.Location = new System.Drawing.Point(12, 35);
            this.conquestButton.Name = "conquestButton";
            this.conquestButton.Size = new System.Drawing.Size(90, 23);
            this.conquestButton.TabIndex = 1;
            this.conquestButton.Text = "Coloniser";
            this.conquestButton.UseVisualStyleBackColor = true;
            this.conquestButton.Click += new System.EventHandler(this.conquestButton_Click);
            // 
            // createShipButton
            // 
            this.createShipButton.Location = new System.Drawing.Point(12, 120);
            this.createShipButton.Name = "createShipButton";
            this.createShipButton.Size = new System.Drawing.Size(90, 23);
            this.createShipButton.TabIndex = 2;
            this.createShipButton.Text = "Créer un navire";
            this.createShipButton.UseVisualStyleBackColor = true;
            this.createShipButton.Click += new System.EventHandler(this.createShipButton_Click);
            // 
            // errorConquestLabel
            // 
            this.errorConquestLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorConquestLabel.Location = new System.Drawing.Point(127, 40);
            this.errorConquestLabel.Name = "errorConquestLabel";
            this.errorConquestLabel.Size = new System.Drawing.Size(145, 40);
            this.errorConquestLabel.TabIndex = 3;
            this.errorConquestLabel.Text = "error conquest";
            // 
            // errorShipLabel
            // 
            this.errorShipLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorShipLabel.Location = new System.Drawing.Point(127, 125);
            this.errorShipLabel.Name = "errorShipLabel";
            this.errorShipLabel.Size = new System.Drawing.Size(145, 40);
            this.errorShipLabel.TabIndex = 4;
            this.errorShipLabel.Text = "error ship";
            // 
            // ActionBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.errorShipLabel);
            this.Controls.Add(this.errorConquestLabel);
            this.Controls.Add(this.createShipButton);
            this.Controls.Add(this.conquestButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActionBaseForm";
            this.Text = "Action depuis une base";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button conquestButton;
        private System.Windows.Forms.Button createShipButton;
        private System.Windows.Forms.Label errorConquestLabel;
        private System.Windows.Forms.Label errorShipLabel;
    }
}