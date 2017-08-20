namespace WeatherUserControlPlugin
{
    partial class WeatherUserControl
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelWeather = new System.Windows.Forms.TableLayoutPanel();
            this.panelWeather = new System.Windows.Forms.Panel();
            this._Weather = new System.Windows.Forms.Label();
            this._WeatherInfo = new System.Windows.Forms.Label();
            this.panelTemperature = new System.Windows.Forms.Panel();
            this.tableLayoutPanelTemperature = new System.Windows.Forms.TableLayoutPanel();
            this._Temperature = new System.Windows.Forms.Label();
            this._Min = new System.Windows.Forms.Label();
            this._Max = new System.Windows.Forms.Label();
            this.panelLocation = new System.Windows.Forms.Panel();
            this._Location = new System.Windows.Forms.Label();
            this._Search = new System.Windows.Forms.TextBox();
            this.panelHorizontalSeparator = new System.Windows.Forms.Panel();
            this.panelVerticalSeparator = new System.Windows.Forms.Panel();
            this._Provider = new System.Windows.Forms.LinkLabel();
            this.panelVerticalSeparator2 = new System.Windows.Forms.Panel();
            this._LastUpdated = new System.Windows.Forms.Label();
            this._WeatherLink = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelWeather.SuspendLayout();
            this.panelWeather.SuspendLayout();
            this.panelTemperature.SuspendLayout();
            this.tableLayoutPanelTemperature.SuspendLayout();
            this.panelLocation.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelWeather
            // 
            this.tableLayoutPanelWeather.ColumnCount = 3;
            this.tableLayoutPanelWeather.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelWeather.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanelWeather.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelWeather.Controls.Add(this.panelWeather, 0, 0);
            this.tableLayoutPanelWeather.Controls.Add(this.panelTemperature, 2, 0);
            this.tableLayoutPanelWeather.Controls.Add(this.panelLocation, 0, 2);
            this.tableLayoutPanelWeather.Controls.Add(this.panelHorizontalSeparator, 1, 0);
            this.tableLayoutPanelWeather.Controls.Add(this.panelVerticalSeparator, 0, 1);
            this.tableLayoutPanelWeather.Controls.Add(this._Provider, 0, 4);
            this.tableLayoutPanelWeather.Controls.Add(this.panelVerticalSeparator2, 0, 3);
            this.tableLayoutPanelWeather.Controls.Add(this._LastUpdated, 0, 5);
            this.tableLayoutPanelWeather.Controls.Add(this.panel1, 2, 4);
            this.tableLayoutPanelWeather.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelWeather.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelWeather.Name = "tableLayoutPanelWeather";
            this.tableLayoutPanelWeather.RowCount = 6;
            this.tableLayoutPanelWeather.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanelWeather.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanelWeather.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelWeather.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanelWeather.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelWeather.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelWeather.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelWeather.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelWeather.Size = new System.Drawing.Size(260, 200);
            this.tableLayoutPanelWeather.TabIndex = 1;
            // 
            // panelWeather
            // 
            this.panelWeather.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelWeather.Controls.Add(this._Weather);
            this.panelWeather.Controls.Add(this._WeatherInfo);
            this.panelWeather.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWeather.Location = new System.Drawing.Point(3, 3);
            this.panelWeather.Name = "panelWeather";
            this.panelWeather.Size = new System.Drawing.Size(121, 91);
            this.panelWeather.TabIndex = 0;
            // 
            // _Weather
            // 
            this._Weather.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Weather.Font = new System.Drawing.Font("Microsoft Sans Serif", 68.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Weather.ForeColor = System.Drawing.Color.Black;
            this._Weather.Location = new System.Drawing.Point(0, 0);
            this._Weather.Margin = new System.Windows.Forms.Padding(0);
            this._Weather.Name = "_Weather";
            this._Weather.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this._Weather.Size = new System.Drawing.Size(121, 91);
            this._Weather.TabIndex = 0;
            this._Weather.Text = "#";
            this._Weather.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _WeatherInfo
            // 
            this._WeatherInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._WeatherInfo.Font = new System.Drawing.Font("Segoe Condensed", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._WeatherInfo.ForeColor = System.Drawing.Color.Black;
            this._WeatherInfo.Location = new System.Drawing.Point(0, 0);
            this._WeatherInfo.Margin = new System.Windows.Forms.Padding(0);
            this._WeatherInfo.Name = "_WeatherInfo";
            this._WeatherInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this._WeatherInfo.Size = new System.Drawing.Size(121, 91);
            this._WeatherInfo.TabIndex = 1;
            this._WeatherInfo.Text = "Weather info";
            this._WeatherInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._WeatherInfo.Visible = false;
            // 
            // panelTemperature
            // 
            this.panelTemperature.Controls.Add(this.tableLayoutPanelTemperature);
            this.panelTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTemperature.Location = new System.Drawing.Point(135, 3);
            this.panelTemperature.Name = "panelTemperature";
            this.panelTemperature.Size = new System.Drawing.Size(122, 91);
            this.panelTemperature.TabIndex = 4;
            // 
            // tableLayoutPanelTemperature
            // 
            this.tableLayoutPanelTemperature.ColumnCount = 2;
            this.tableLayoutPanelTemperature.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelTemperature.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelTemperature.Controls.Add(this._Temperature, 0, 0);
            this.tableLayoutPanelTemperature.Controls.Add(this._Min, 0, 1);
            this.tableLayoutPanelTemperature.Controls.Add(this._Max, 1, 1);
            this.tableLayoutPanelTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTemperature.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTemperature.Name = "tableLayoutPanelTemperature";
            this.tableLayoutPanelTemperature.RowCount = 2;
            this.tableLayoutPanelTemperature.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanelTemperature.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelTemperature.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelTemperature.Size = new System.Drawing.Size(122, 91);
            this.tableLayoutPanelTemperature.TabIndex = 0;
            // 
            // _Temperature
            // 
            this.tableLayoutPanelTemperature.SetColumnSpan(this._Temperature, 2);
            this._Temperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Temperature.Font = new System.Drawing.Font("Segoe Condensed", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Temperature.ForeColor = System.Drawing.Color.Black;
            this._Temperature.Location = new System.Drawing.Point(3, 0);
            this._Temperature.Name = "_Temperature";
            this._Temperature.Size = new System.Drawing.Size(116, 68);
            this._Temperature.TabIndex = 1;
            this._Temperature.Text = "0ºC";
            this._Temperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _Min
            // 
            this._Min.AutoSize = true;
            this._Min.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Min.Font = new System.Drawing.Font("Segoe Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Min.ForeColor = System.Drawing.Color.Black;
            this._Min.Location = new System.Drawing.Point(3, 68);
            this._Min.Name = "_Min";
            this._Min.Size = new System.Drawing.Size(55, 23);
            this._Min.TabIndex = 2;
            this._Min.Text = "Min: 0º";
            this._Min.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _Max
            // 
            this._Max.AutoSize = true;
            this._Max.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Max.Font = new System.Drawing.Font("Segoe Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Max.ForeColor = System.Drawing.Color.Black;
            this._Max.Location = new System.Drawing.Point(64, 68);
            this._Max.Name = "_Max";
            this._Max.Size = new System.Drawing.Size(55, 23);
            this._Max.TabIndex = 3;
            this._Max.Text = "Max: 0º";
            this._Max.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLocation
            // 
            this.tableLayoutPanelWeather.SetColumnSpan(this.panelLocation, 3);
            this.panelLocation.Controls.Add(this._Location);
            this.panelLocation.Controls.Add(this._Search);
            this.panelLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLocation.Location = new System.Drawing.Point(3, 105);
            this.panelLocation.Name = "panelLocation";
            this.panelLocation.Size = new System.Drawing.Size(254, 46);
            this.panelLocation.TabIndex = 3;
            // 
            // _Location
            // 
            this._Location.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Location.Font = new System.Drawing.Font("Segoe Condensed", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Location.ForeColor = System.Drawing.Color.Black;
            this._Location.Location = new System.Drawing.Point(0, 0);
            this._Location.Name = "_Location";
            this._Location.Size = new System.Drawing.Size(254, 46);
            this._Location.TabIndex = 0;
            this._Location.Text = "Searching...";
            this._Location.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _Search
            // 
            this._Search.BackColor = System.Drawing.Color.White;
            this._Search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._Search.Font = new System.Drawing.Font("Segoe Condensed", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Search.Location = new System.Drawing.Point(0, 0);
            this._Search.Margin = new System.Windows.Forms.Padding(0);
            this._Search.Multiline = true;
            this._Search.Name = "_Search";
            this._Search.Size = new System.Drawing.Size(254, 46);
            this._Search.TabIndex = 1;
            this._Search.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._Search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._Search_KeyPress);
            // 
            // panelHorizontalSeparator
            // 
            this.panelHorizontalSeparator.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelHorizontalSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHorizontalSeparator.Location = new System.Drawing.Point(130, 3);
            this.panelHorizontalSeparator.Name = "panelHorizontalSeparator";
            this.panelHorizontalSeparator.Size = new System.Drawing.Size(1, 91);
            this.panelHorizontalSeparator.TabIndex = 1;
            // 
            // panelVerticalSeparator
            // 
            this.panelVerticalSeparator.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tableLayoutPanelWeather.SetColumnSpan(this.panelVerticalSeparator, 3);
            this.panelVerticalSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVerticalSeparator.Location = new System.Drawing.Point(3, 100);
            this.panelVerticalSeparator.Name = "panelVerticalSeparator";
            this.panelVerticalSeparator.Size = new System.Drawing.Size(254, 1);
            this.panelVerticalSeparator.TabIndex = 2;
            // 
            // _Provider
            // 
            this._Provider.AutoSize = true;
            this._Provider.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Provider.Location = new System.Drawing.Point(3, 159);
            this._Provider.Name = "_Provider";
            this._Provider.Size = new System.Drawing.Size(121, 20);
            this._Provider.TabIndex = 8;
            this._Provider.TabStop = true;
            this._Provider.Text = "Provider";
            this._Provider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Provider.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._Label_LinkClicked);
            // 
            // panelVerticalSeparator2
            // 
            this.panelVerticalSeparator2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tableLayoutPanelWeather.SetColumnSpan(this.panelVerticalSeparator2, 3);
            this.panelVerticalSeparator2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVerticalSeparator2.Location = new System.Drawing.Point(3, 157);
            this.panelVerticalSeparator2.Name = "panelVerticalSeparator2";
            this.panelVerticalSeparator2.Size = new System.Drawing.Size(254, 1);
            this.panelVerticalSeparator2.TabIndex = 10;
            // 
            // _LastUpdated
            // 
            this.tableLayoutPanelWeather.SetColumnSpan(this._LastUpdated, 3);
            this._LastUpdated.Location = new System.Drawing.Point(3, 179);
            this._LastUpdated.Name = "_LastUpdated";
            this._LastUpdated.Size = new System.Drawing.Size(254, 20);
            this._LastUpdated.TabIndex = 5;
            this._LastUpdated.Text = "Info messages";
            this._LastUpdated.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // _WeatherLink
            // 
            this._WeatherLink.Dock = System.Windows.Forms.DockStyle.Right;
            this._WeatherLink.Location = new System.Drawing.Point(-14, 0);
            this._WeatherLink.Name = "_WeatherLink";
            this._WeatherLink.Size = new System.Drawing.Size(121, 14);
            this._WeatherLink.TabIndex = 9;
            this._WeatherLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._WeatherLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._Label_LinkClicked);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(107, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(15, 14);
            this.button1.TabIndex = 11;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._WeatherLink);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(135, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(122, 14);
            this.panel1.TabIndex = 12;
            // 
            // WeatherUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelWeather);
            this.MinimumSize = new System.Drawing.Size(130, 100);
            this.Name = "WeatherUserControl";
            this.Size = new System.Drawing.Size(260, 200);
            this.Resize += new System.EventHandler(this.LabelFont_Resize);
            this.tableLayoutPanelWeather.ResumeLayout(false);
            this.tableLayoutPanelWeather.PerformLayout();
            this.panelWeather.ResumeLayout(false);
            this.panelTemperature.ResumeLayout(false);
            this.tableLayoutPanelTemperature.ResumeLayout(false);
            this.tableLayoutPanelTemperature.PerformLayout();
            this.panelLocation.ResumeLayout(false);
            this.panelLocation.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWeather;
        private System.Windows.Forms.Panel panelWeather;
        private System.Windows.Forms.Label _Weather;
        private System.Windows.Forms.Label _WeatherInfo;
        private System.Windows.Forms.Panel panelTemperature;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTemperature;
        private System.Windows.Forms.Label _Max;
        private System.Windows.Forms.Label _Temperature;
        private System.Windows.Forms.Label _Min;
        private System.Windows.Forms.Panel panelLocation;
        private System.Windows.Forms.TextBox _Search;
        private System.Windows.Forms.Panel panelHorizontalSeparator;
        private System.Windows.Forms.Panel panelVerticalSeparator;
        private System.Windows.Forms.Label _LastUpdated;
        private System.Windows.Forms.Label _Location;
        private System.Windows.Forms.LinkLabel _Provider;
        private System.Windows.Forms.LinkLabel _WeatherLink;
        private System.Windows.Forms.Panel panelVerticalSeparator2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}
