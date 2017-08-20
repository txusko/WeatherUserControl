/**
* WeatherUserControl v1.0
* Weather UserControl - released under MIT License
* Author: Javi Filella <txusko@gmail.com>
* http://github.com/txusko/WeatherUserControl
* Copyright (c) 2017 Javi Filella
*
* Permission is hereby granted, free of charge, to any person
* obtaining a copy of this software and associated documentation
* files (the "Software"), to deal in the Software without
* restriction, including without limitation the rights to use,
* copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following
* conditions:
*
* The above copyright notice and this permission notice shall be
* included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
* OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
* NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
* HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
* FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
* OTHER DEALINGS IN THE SOFTWARE.
*
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Device.Location;
using System.Drawing.Text;
using System.Xml.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace WeatherUserControlPlugin
{

    /// <summary>
    /// Weather UserControl Plugin based on 3rd Party Weather APIs.
    /// Released under MIT License
    /// Author: Javi Filella <txusko@gmail.com>
    /// http://github.com/txusko/YahooWeatherPlugin
    /// Copyright (c) 2017 Javi Filella
    /// </summary>
    public partial class WeatherUserControl : UserControl
    {
        /// <summary>
        /// Weather Info object
        /// </summary>
        private Weather _CurrentWeather = new Weather();

        #region PRIVATE PROPERTIES

        /// <summary>
        /// Weather API
        /// </summary>
        private IWeatherAPI _WeatherAPI = null;

        /// <summary>
        /// Miliseconds for each timer tick
        /// </summary>
        private int _TimerTime = 500;

        /// <summary>
        /// Recover client's geolocation (Windows service)
        /// </summary>
        private static GeoCoordinateWatcher _Watcher = new GeoCoordinateWatcher();

        /// <summary>
        /// Timer for update label's properties
        /// </summary>
        private System.Windows.Forms.Timer _Timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Timer for update weather info
        /// </summary>
        private System.Windows.Forms.Timer _TimerWeather = new System.Windows.Forms.Timer();

        /// <summary>
        /// Custom font (for weather icons according to yahoo weather codes)
        /// </summary>
        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        /// <summary>
        /// Initial Usercontrol Width
        /// </summary>
        private int initialWidth;

        /// <summary>
        /// Initial Usercontrol Height
        /// </summary>
        private int initialHeight;

        /// <summary>
        /// Initial Font Size of the label elements to take into account.
        /// </summary>
        private Dictionary<string, float> initialFontSize = new Dictionary<string, float>();

        /// <summary>
        /// Forze refresh on every Timer_Tick
        /// </summary>
        private bool _Refresh = false;

        #endregion PRIVATE PROPERTIES

        #region CONFIG PROPERTIES

        /// <summary>
        /// Weather automatic service : based on geolocalization
        /// </summary>
        [Description("Automatic set to True retrieve weather based on geolocation. Automatic set to False retrieve weather based on \"Location\" string."),
        Category("Weather Plugin"), DisplayName("Automatic"), DefaultValue(true)]
        public bool Automatic
        {
            get { return this._Automatic; }
            set
            {
                if (this._Automatic != value)
                {
                    this._Automatic = value;
                    if (value)
                    {
                        this._Search.Text = "";
                        _Watcher.Start();
                    }
                    else
                    {
                        _Watcher.Stop();
                    }
                }
            }
        }
        private bool _Automatic = true;

        /// <summary>
        /// Searched string for the weather service When _Automatic is set to False.
        /// </summary>
        [Description("Default location when \"Automatic\" is set to False"),
        Category("Weather Plugin"), DisplayName("Location"), DefaultValue("")]
        public string SearchLocation
        {
            get { return this._SearchLocation; }
            set { this._SearchLocation = value; }
        }
        private string _SearchLocation = "";

        /// <summary>
        /// Type of temperature to show
        /// </summary>
        [Description("Scales of temperature: Celsius or Farhenheit."),
        Category("Weather Plugin"), DisplayName("Scale of temperature"), DefaultValue(TemperatureTypes.Celsius)]
        public TemperatureTypes TemperatureType
        {
            get { return this._TemperatureType; }
            set
            {
                if (this._TemperatureType != value)
                {
                    this._TemperatureType = value;
                }
            }
        }
        private TemperatureTypes _TemperatureType = TemperatureTypes.Celsius;

        /// <summary>
        /// Determines the weather API used
        /// </summary>
        [Description("Determines the Weather API used."),
        Category("Weather Plugin"), 
        DisplayName("Weather API"),
        RefreshProperties(RefreshProperties.All), 
        Browsable(true), DefaultValue(WeatherAPIs.OpenWeatherMap)]
        public WeatherAPIs WeatherAPI
        {
            get { return this._lWeatherAPI; }
            set 
            {
                if (this._lWeatherAPI != value)
                {
                    this._lWeatherAPI = value;
                }
            }
        }
        private WeatherAPIs _lWeatherAPI = WeatherAPIs.OpenWeatherMap;

        /// <summary>
        /// API Key (when WeatherAPI requieres)
        /// </summary>
        [Description("Determines the Weather API Key (if the API requieres)"),
        Category("Weather Plugin"), 
        DisplayName("Weather API Key"), 
        Browsable(true), DefaultValue("")]
        public string APIKey
        {
            get { return this._APIKey; }
            set { this._APIKey = value; }
        }
        private string _APIKey = "";

        /// <summary>
        /// Show info messages
        /// </summary>
        [Description("Show info messages."),
        Category("Weather Plugin"), DisplayName("Show info"), DefaultValue(true)]
        public bool ShowInfo
        {
            get { return this._lShowInfo; }
            set { this._lShowInfo = value; }
        }
        private bool _lShowInfo = true;

        /// <summary>
        /// Refresh interval in milliseconds. Value between 60000 and 3600000
        /// </summary>
        [Description("Weather refresh interval in milliseconds"),
        Category("Weather Plugin"), DisplayName("Refresh interval"), DefaultValue(600000)]
        public int RefreshInterval
        {
            get { return this._RefreshInterval; }
            set
            {
                if (this._RefreshInterval != value)
                {
                    int lcValue = value;
                    if (lcValue < 30000)
                        lcValue = 30000;
                    else if (lcValue > 3600000)
                        lcValue = 3600000;
                    this._RefreshInterval = lcValue;
                }
            }
        }
        private int _RefreshInterval = 60000;

        #endregion CONFIG PROPERTIES

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor
        /// </summary>
        public WeatherUserControl()
        {
            this._Init();
        }

        #endregion CONSTRUCTORS

        #region METHODS

        /// <summary>
        /// Initialize compontents and environement
        /// </summary>
        private void _Init()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OnLoad override
        /// - initialize GeoCoordinateWatcher 
        /// - Initialize timer
        /// - Load weather font
        /// </summary>
        /// <param name="e"></param>
        public void _Start()
        {
            //Load "metocon" font in _Whether label
            _LoadFont(this._Weather);

            //Info
            if (!this.ShowInfo)
            {
                this.tableLayoutPanelWeather.RowStyles[this.tableLayoutPanelWeather.RowCount - 1].Height = 0;
                this.tableLayoutPanelWeather.RowStyles[this.tableLayoutPanelWeather.RowCount - 2].Height = 0;
                this.tableLayoutPanelWeather.RowStyles[this.tableLayoutPanelWeather.RowCount - 3].Height = 0;
            }
            else
            {
                this.tableLayoutPanelWeather.RowStyles[this.tableLayoutPanelWeather.RowCount - 1].Height = 20;
                this.tableLayoutPanelWeather.RowStyles[this.tableLayoutPanelWeather.RowCount - 2].Height = 20;
                this.tableLayoutPanelWeather.RowStyles[this.tableLayoutPanelWeather.RowCount - 3].Height = 5;
            }

            this._ResizeSearchTextBox();

            //Weather API
            LinkLabel.Link loLink = new LinkLabel.Link();
            switch (this.WeatherAPI)
            {
                case WeatherAPIs.Yahoo:
                    this._WeatherAPI = new YahooWeatherAPI(this._CurrentWeather);
                    break;

                //TODO
                //case WeatherAPIs.WeatherUnderground:
                //    this._WeatherAPI = new WeatherUnderground(this._CurrentWeather);
                //    break;
                
                default:
                case WeatherAPIs.OpenWeatherMap:
                    this._WeatherAPI = new OpenWeathwerMap(this._CurrentWeather);
                    break;
            }

            this._Provider.Text = this._WeatherAPI._Provider;
            loLink.LinkData = this._WeatherAPI._ProviderLink;
            this._Provider.Links.Add(loLink);
            this._WeatherAPI.APIKey = this.APIKey;

            if (this._WeatherAPI.CheckService())
            {
                //Geolocation
                _Watcher.StatusChanged += Watcher_StatusChanged;
                if (Automatic)
                    _Watcher.Start();
                else
                    this._RefreshWeatherInfo();

                //Enable timer for the automatic update of the content
                _Timer.Tick += new EventHandler(_Timer_Tick);
                _Timer.Interval = this._TimerTime;
                _Timer.Start();

                //Recover weather info
                _TimerWeather.Tick += new EventHandler(_TimerWeather_Tick);
                _TimerWeather.Interval = this.RefreshInterval;
                _TimerWeather.Start();

                this._Weather.Click += new System.EventHandler(this._Weather_Click);
                this._WeatherInfo.Click += new System.EventHandler(this._Weather_Click);
                this._Temperature.Click += new System.EventHandler(this._Temperature_Click);
                this._Min.Click += new System.EventHandler(this._Temperature_Click);
                this._Max.Click += new System.EventHandler(this._Temperature_Click);
                this._Location.Click += new System.EventHandler(this._Location_Click);
            }
            else 
            {
                this._LastUpdated.Text = this._CurrentWeather._LastUpdatedText;
                this._Weather.Text = "(";
                this._Location.Text = "Unavailable";
            }
        }

        /// <summary>
        /// _Provider : onclick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        /// <summary>
        /// Load "meteocons_webfont" resource and set to _Weather label
        /// </summary>
        private void _LoadFont(Label toLabel)
        {
            int fontLength = global::WeatherUserControlPlugin.Properties.Resources.meteocons_webfont.Length;

            // create a buffer to read in to
            byte[] fontdata = global::WeatherUserControlPlugin.Properties.Resources.meteocons_webfont;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            // pass the font to the font collection
            privateFontCollection.AddMemoryFont(data, fontLength);

            // free up the unsafe memory
            Marshal.FreeCoTaskMem(data);

            //After that we can create font and assign font to label
            toLabel.Font = new Font(privateFontCollection.Families[0], this._Weather.Font.Size);
            toLabel.Text = ")";

            //Load autoresize vars
            _LoadAutoResize();
        }

        /// <summary>
        /// Load autoresize needed vars
        /// </summary>
        private void _LoadAutoResize()
        {
            //We don't need autoscale
            AutoScaleMode = AutoScaleMode.None;

            // Sets the initial size of the usercontrol
            initialWidth = Width;
            initialHeight = Height;

            // Sets the initial size of the labels
            initialFontSize["_Weather"] = this._Weather.Font.Size;
            initialFontSize["_WeatherInfo"] = this._WeatherInfo.Font.Size;
            initialFontSize["_Location"] = this._Location.Font.Size;
            initialFontSize["_Search"] = this._Search.Font.Size;
            initialFontSize["_Temperature"] = this._Temperature.Font.Size;
            initialFontSize["_Min"] = this._Min.Font.Size;
            initialFontSize["_Max"] = this._Max.Font.Size;
            initialFontSize["_LastUpdated"] = this._LastUpdated.Font.Size;
        }

        /// <summary>
        /// Timer tick : update the labels with the recovered info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Timer_Tick(object sender, EventArgs e)
        {
            //Update info
            if (this._LastUpdated.Text != this._CurrentWeather._LastUpdatedText)
            {
                this._LastUpdated.Text = this._CurrentWeather._LastUpdatedText;
                //this.scaleFont(this._LastUpdated);
            }

            //_Wheather : icon
            if (!string.IsNullOrWhiteSpace(this._CurrentWeather._WeatherCode) && (this.panelWeather.Tag == null || this.panelWeather.Tag.ToString() != this._CurrentWeather._WeatherCode))
            {
                this._Weather.Text = this._CurrentWeather._MeteoconCode;
                this._WeatherInfo.Text = this._CurrentWeather._WeatherText;
                this.panelWeather.Tag = this._CurrentWeather._WeatherCode;
                this.scaleFont(this._Weather);
                this.scaleFont(this._WeatherInfo);
            }

            //Current temperature
            if (!string.IsNullOrWhiteSpace(this._CurrentWeather._Temp) && this._Temperature.Text != this._CurrentWeather._Temp)
            {
                this._Temperature.Text = this._CurrentWeather._Temp;
                this.scaleFont(this._Temperature);
            }

            //Today's lowest temperature
            if (!string.IsNullOrWhiteSpace(this._CurrentWeather._MinTemp) 
                && (this._Min.Tag == null || this._Min.Tag.ToString() != this._CurrentWeather._MinTemp || string.IsNullOrWhiteSpace(this._Min.Text)))
            {
                this._Min.Text = "Min: " + this._CurrentWeather._MinTemp + "º";
                this._Min.Tag = this._CurrentWeather._MinTemp;
                this.scaleFont(this._Min);
            }

            //Today's highest temperature
            if (!string.IsNullOrWhiteSpace(this._CurrentWeather._MaxTemp)
                && (this._Max.Tag == null || this._Max.Tag.ToString() != this._CurrentWeather._MaxTemp || string.IsNullOrWhiteSpace(this._Max.Text)))
            {
                this._Max.Text = "Max: " + this._CurrentWeather._MaxTemp + "º";
                this._Max.Tag = this._CurrentWeather._MaxTemp;
                this.scaleFont(this._Max);
            }

            //Searched location
            if (!string.IsNullOrWhiteSpace(this._CurrentWeather._City) && this._Location.Text != this._CurrentWeather._City)
            {
                this._Location.Text = this._CurrentWeather._City;
                this.scaleFont(this._Location);
            }

            //Weather link
            if (this._WeatherLink.Tag != null && this._WeatherLink.Tag.ToString() != this._CurrentWeather._Link
                || this._WeatherLink.Tag == null && !string.IsNullOrWhiteSpace(this._CurrentWeather._Link))
            {
                LinkLabel.Link loLink = new LinkLabel.Link();
                loLink.LinkData = this._CurrentWeather._Link;
                this._WeatherLink.Links.Clear();
                this._WeatherLink.Tag = this._CurrentWeather._Link;
                this._WeatherLink.Text = "More info";
                this._WeatherLink.Links.Add(loLink);
                this.scaleFont(this._WeatherLink);
            }
        }

        /// <summary>
        /// Autoscale font to container
        /// http://stackoverflow.com/questions/9527721/resize-text-size-of-a-label-when-the-text-got-longer-than-the-label-size
        /// </summary>
        /// <param name="toLabel"></param>
        private void scaleFont(Label toLabel)
        {
            while (toLabel.Width < System.Windows.Forms.TextRenderer.MeasureText(toLabel.Text, 
                new Font(toLabel.Font.FontFamily, toLabel.Font.Size, toLabel.Font.Style)).Width)
            {
                toLabel.Font = new Font(toLabel.Font.FontFamily, toLabel.Font.Size - 0.5f, toLabel.Font.Style);
            }
        }

        /// <summary>
        /// Recover weather info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _TimerWeather_Tick(object sender, EventArgs e)
        {
            //Refresh location
            if(_Refresh) this._RefreshWeatherInfo();
        }

        /// <summary>
        /// Watcher_StatusChanged : update the client geolocation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            //Check for Reasy status
            if (e.Status == GeoPositionStatus.Ready)
            {
                //Check for a known location
                if (!_Watcher.Position.Location.IsUnknown)
                {
                    //Store location
                    this._CurrentWeather._Latitude = _Watcher.Position.Location.Latitude.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
                    this._CurrentWeather._Longitude = _Watcher.Position.Location.Longitude.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
                    //Forze to refresh weather info
                    this._CurrentWeather._LastUpdatedDate = null;
                    this._RefreshWeatherInfo();
                }
            }
        }

        /// <summary>
        /// Recover the weather info taking acount of the class params
        /// </summary>
        private void _RefreshWeatherInfo(int toWaitOnError = 1000)
        {
            if (this._CurrentWeather._LastUpdatedDate != null)
            {
                DateTime ldtTmp = ((DateTime)this._CurrentWeather._LastUpdatedDate).AddMilliseconds(1);//.AddMilliseconds(this.RefreshInterval);
                if (DateTime.Now <= ldtTmp)
                {
                    this._CurrentWeather._LastUpdatedText = "Last updated on " + this._CurrentWeather._LastUpdatedDate.ToString();
                    return;
                }
            }

            this._Location.Text = this._CurrentWeather._City = "Searching...";

            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true;

                bool llChanged = false;
                //Automatic mode
                if (this.Automatic && !string.IsNullOrWhiteSpace(this._CurrentWeather._Latitude)
                    && !string.IsNullOrWhiteSpace(this._CurrentWeather._Longitude))
                    llChanged = this._WeatherAPI._GetLocationLatLon(this._CurrentWeather._Latitude, this._CurrentWeather._Longitude, this.TemperatureType);
                //Search mode
                else if (!this.Automatic && !string.IsNullOrWhiteSpace(this.SearchLocation))
                    llChanged = this._WeatherAPI._GetSearch(this.SearchLocation, this.TemperatureType);

                if (!llChanged)
                {
                    this._Refresh = false;
                    this._CurrentWeather._LastUpdatedText = "Error retrieving data. Waiting " + toWaitOnError + "ms.";
                    Thread.Sleep(toWaitOnError);
                    this._RefreshWeatherInfo(toWaitOnError + 1000);
                }
                else
                {
                    this._Refresh = true;
                }

            }).Start();
        }

        /// <summary>
        /// Click event on _Temperature label.
        /// It will toggle the type of temperature shown between Celsius and Farenheit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Temperature_Click(object sender, EventArgs e)
        {
            if (this.TemperatureType == TemperatureTypes.Celsius)
                this.TemperatureType = TemperatureTypes.Farenheit;
            else
                this.TemperatureType = TemperatureTypes.Celsius;
            //Forze to refresh weather info
            this._CurrentWeather._LastUpdatedDate = null;
            this._RefreshWeatherInfo();
        }

        /// <summary>
        /// Click event on _Loaction label.
        /// Show an editable textbox where user can set manually the desired location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Location_Click(object sender, EventArgs e)
        {
            this._Search.Text = this._Location.Text;
            this._Search.SelectAll();
            //Show search textbox
            this._Search.BringToFront();
            this._Location.Visible = false;
            this.panelLocation.BackColor = this._Search.BackColor;
        }

        /// <summary>
        /// Keypress event on _Search textbox.
        /// Enter or Tab : Apply changes
        /// Esc : Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            //On escape, restore previous location
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                this._Location.BringToFront();
                this._Location.Visible = true;
                this.panelLocation.BackColor = this.BackColor;
            }

            //On Enter or Tab, apply the changes
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Tab))
            {
                //Change visibility
                this._Location.BringToFront();
                this._Location.Visible = true;
                this.panelLocation.BackColor = this.BackColor;
                
                //Check for search string
                if (!string.IsNullOrWhiteSpace(this._Search.Text))
                {
                    this._Automatic = false;
                    //Reload content
                    this.SearchLocation = this._Search.Text;
                    //Forze to refresh weather info
                    this._CurrentWeather._LastUpdatedDate = null;
                    this._RefreshWeatherInfo();
                    this._LastUpdated.Text = "Retrieving data for " + this.SearchLocation;
                }
                else if (!this.Automatic)
                {
                    //If no search string is provided and the _Automatic is set to off, reactivate _Automatic
                    this.Automatic = true;
                    //Forze to refresh weather info
                    this._CurrentWeather._LastUpdatedDate = null;
                    this._RefreshWeatherInfo();
                }
                else
                {
                    this._Location.Text = this._CurrentWeather._City;
                }
            }
        }

        /// <summary>
        /// Usercontrol resize : responsive font-size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelFont_Resize(object sender, EventArgs e)
        {
            //Check needed properties
            if (privateFontCollection.Families.Length <= 0 || initialFontSize.Count <= 0)
                return;

            SuspendLayout();

            // Get the proportionality of the resize
            float proportionalNewWidth = (float)Width / initialWidth;
            float proportionalNewHeight = (float)Height / initialHeight;

            // Calculate the current font size for all the labels
            this._Weather.Font = new Font(privateFontCollection.Families[0], initialFontSize["_Weather"] *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                this._Weather.Font.Style);
            this._WeatherInfo.Font = new Font(this._WeatherInfo.Font.Name, initialFontSize["_WeatherInfo"] *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                this._WeatherInfo.Font.Style);
            this._Location.Font = new Font(this._Location.Font.Name, initialFontSize["_Location"] *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                this._Location.Font.Style);
            this._Search.Font = new Font(this._Search.Font.Name, initialFontSize["_Search"] *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                this._Search.Font.Style);
            this._Temperature.Font = new Font(this._Temperature.Font.Name, initialFontSize["_Temperature"] *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                this._Temperature.Font.Style);
            this._Min.Font = new Font(this._Min.Font.Name, initialFontSize["_Min"] *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                this._Min.Font.Style);
            this._Max.Font = new Font(this._Max.Font.Name, initialFontSize["_Max"] *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                this._Max.Font.Style);
            //this._LastUpdated.Font = new Font(this._LastUpdated.Font.Name, initialFontSize["_LastUpdated"] *
            //    (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
            //    this._LastUpdated.Font.Style);

            this._ResizeSearchTextBox();

            ResumeLayout();
        }

        private void _ResizeSearchTextBox()
        {
            this._Search.Width = this.panelLocation.Width;
            this._Search.Height = this._Search.Font.Height;
            this._Search.Top = (this.panelLocation.Height - this._Search.Height) / 2;
        }

        /// <summary>
        /// Click on _Weather or _WeatherInfo label. Toogle between the two labels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Weather_Click(object sender, EventArgs e)
        {
            this._Weather.Visible = !this._Weather.Visible;
            this._WeatherInfo.Visible = !this._WeatherInfo.Visible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lcMessage = "Wheather UserControl Plugin by Javi Filella" + Environment.NewLine;
            lcMessage += "https://github.com/txusko/WeatherUserControl" + Environment.NewLine;

            lcMessage += Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this._WeatherAPI._Provider))
                lcMessage += "Provider : " + this._WeatherAPI._Provider + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this._WeatherAPI._ProviderLink))
                lcMessage += this._WeatherAPI._ProviderLink + Environment.NewLine;

            lcMessage += Environment.NewLine;
            lcMessage += "Log information" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this._CurrentWeather._LastUpdatedText))
                lcMessage += string.Join(Environment.NewLine, this._CurrentWeather._HistoryInformation);

            MessageBox.Show(lcMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion METHODS

    }
}
