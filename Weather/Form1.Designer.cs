namespace WeatherUserControlPlugin
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.weatherUserControl1 = new WeatherUserControlPlugin.WeatherUserControl();
            this.SuspendLayout();
            // 
            // weatherUserControl1
            // 
            this.weatherUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weatherUserControl1.Location = new System.Drawing.Point(0, 0);
            this.weatherUserControl1.MinimumSize = new System.Drawing.Size(20, 10);
            this.weatherUserControl1.Name = "weatherUserControl1";
            this.weatherUserControl1.Size = new System.Drawing.Size(264, 205);
            this.weatherUserControl1.TabIndex = 0;
            this.weatherUserControl1.WeatherAPI = WeatherUserControlPlugin.WeatherAPIs.Yahoo;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 205);
            this.Controls.Add(this.weatherUserControl1);
            this.Name = "Form1";
            this.Text = "Weather UserControl Plugin";
            this.ResumeLayout(false);

        }

        #endregion

        private WeatherUserControl weatherUserControl1;




    }
}

