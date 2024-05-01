namespace ufficio
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginComponent = new System.Windows.Forms.Integration.ElementHost();
            this.login = new LoginComponent.login();
            this.SuspendLayout();
            // 
            // loginComponent
            // 
            this.loginComponent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginComponent.Location = new System.Drawing.Point(0, 0);
            this.loginComponent.Name = "loginComponent";
            this.loginComponent.Size = new System.Drawing.Size(800, 450);
            this.loginComponent.TabIndex = 0;
            this.loginComponent.Text = "elementHost1";
            this.loginComponent.Child = this.login;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loginComponent);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private LoginComponent.login login1;
        private System.Windows.Forms.Integration.ElementHost loginComponent;
        private LoginComponent.login login;
    }
}

