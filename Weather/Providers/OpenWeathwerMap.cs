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
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.ComponentModel;

namespace WeatherUserControlPlugin
{
    /// <summary>
    /// Open Weather Map
    /// https://openweathermap.org/api
    /// </summary>
    public class OpenWeathwerMap : IWeatherAPI
    {

        #region Open Weather Map codes to Meteocons Webfont

        /// <summary>
        /// General icon codes
        /// </summary>
        public Dictionary<string, string> OpenWeatherMapIcons2Meteocon = new Dictionary<string, string>()
        {
            { "01d", "B" },
            { "01n", "C" },
        
            { "02d", "H" },
            { "02n", "I" },
        
            { "03d", "N" },
            { "03n", "N" },
        
            { "04d", "Y" },
            { "04n", "Y" },
        
            { "09d", "Q" },
            { "09n", "Q" },
        
            { "10d", "R" },
            { "10n", "R" },
        
            { "11d", "0" },
            { "11n", "0" },
        
            { "13d", "W" },
            { "13n", "W" },
        
            { "50d", "J" },
            { "50n", "K" }
        };
        
        /// <summary>
        /// All weather codes (TODO)
        /// </summary>
        public Dictionary<int, string> OpenWeatherMapCode2Meteocon = new Dictionary<int, string>()
        {
            //Group 2xx: Thunderstorm
            { 200, "" },// 	thunderstorm with light rain 	11d
            { 201, "" },// 	thunderstorm with rain 	11d
            { 202, "" },// 	thunderstorm with heavy rain 	11d
            { 210, "" },// 	light thunderstorm 	11d
            { 211, "" },// 	thunderstorm 	11d
            { 212, "" },// 	heavy thunderstorm 	11d
            { 221, "" },// 	ragged thunderstorm 	11d
            { 230, "" },// 	thunderstorm with light drizzle 	11d
            { 231, "" },// 	thunderstorm with drizzle 	11d
            { 232, "" },// 	thunderstorm with heavy drizzle 	11d
            //Group 3xx: Drizzle
            { 300, "" },// 	light intensity drizzle 	09d
            {301, "" },// 	drizzle 	09d
            { 302, "" },// 	heavy intensity drizzle 	09d
            { 310, "" },// 	light intensity drizzle rain 	09d
            { 311, "" },// 	drizzle rain 	09d
            { 312, "" },// 	heavy intensity drizzle rain 	09d
            { 313, "" },// 	shower rain and drizzle 	09d
            { 314, "" },// 	heavy shower rain and drizzle 	09d
            { 321, "" },// 	shower drizzle 	09d
            //Group 5xx: Rain
            { 500, "" },// 	light rain 	10d
            { 501, "" },// 	moderate rain 	10d
            { 502, "" },// 	heavy intensity rain 	10d
            { 503, "" },// 	very heavy rain 	10d
            { 504, "" },// 	extreme rain 	10d
            { 511, "" },// 	freezing rain 	13d
            { 520, "" },// 	light intensity shower rain 	09d
            { 521, "" },// 	shower rain 	09d
            { 522, "" },// 	heavy intensity shower rain 	09d
            { 531, "" },// 	ragged shower rain 	09d
            //Group 6xx: Snow
            { 600, "" },// 	light snow 	13d
            { 601, "" },// 	snow 	13d
            { 602, "" },// 	heavy snow 	13d
            { 611, "" },// 	sleet 	13d
            { 612, "" },// 	shower sleet 	13d
            { 615, "" },// 	light rain and snow 	13d
            { 616, "" },// 	rain and snow 	13d
            { 620, "" },// 	light shower snow 	13d
            { 621, "" },// 	shower snow 	13d
            { 622, "" },// 	heavy shower snow 	13d
            //Group 7xx: Atmosphere
            { 701, "" },// 	mist 	50d
            { 711, "" },// 	smoke 	50d
            { 721, "" },// 	haze 	50d
            { 731, "" },// 	sand, dust whirls 	50d
            { 741, "" },// 	fog 	50d
            { 751, "" },// 	sand 	50d
            { 761, "" },// 	dust 	50d
            { 762, "" },// 	volcanic ash 	50d
            { 771, "" },// 	squalls 	50d
            { 781, "" },// 	tornado 	50d
            //Group 800: Clear
            { 800, "" },// 	clear sky 	01d 01n
            //Group 80x: Clouds
            { 801, "" },// 	few clouds 	02d 02n
            { 802, "" },// 	scattered clouds 	03d 03n
            { 803, "I" },// 	broken clouds 	04d 03d
            { 804, "" },// 	overcast clouds 	04d 04d
            //Group 90x: Extreme
            { 900, "" },// 	tornado
            { 901, "" },// 	tropical storm
            { 902, "" },// 	hurricane
            { 903, "" },// 	cold
            { 904, "" },// 	hot
            { 905, "" },// 	windy
            { 906, "" },// 	hail
            //Group 9xx: Additional
            { 951, "" },// 	calm
            { 952, "" },// 	light breeze
            { 953, "" },// 	gentle breeze
            { 954, "" },// 	moderate breeze
            { 955, "" },// 	fresh breeze
            { 956, "" },// 	strong breeze
            { 957, "" },// 	high wind, near gale
            { 958, "" },// 	gale
            { 959, "" },// 	severe gale
            { 960, "" },// 	storm
            { 961, "" },// 	violent storm
            { 962, "" }// 	hurricane 
        };
        
        #endregion

        /// <summary>
        /// Api key needed for Open Weather Map API requests.
        /// </summary>
        public string APIKey
        {
            get { return this._APIKey; }
            set { this._APIKey = value; }
        }
        private string _APIKey = "";

        /// <summary>
        /// Provider name
        /// </summary>
        public string _Provider
        {
            get { return "Open Weather Map"; }
        }

        /// <summary>
        /// Provider link
        /// </summary>
        public string _ProviderLink
        {
            get { return "https://openweathermap.org/"; }
        }

        /// <summary>
        /// Current weather object
        /// </summary>
        public Weather _CurrentWeather
        {
            get { return this._lCurrentWeather; }
            set { this._lCurrentWeather = value; }
        }
        private Weather _lCurrentWeather = new Weather();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="toCurrentWeather"></param>
        public OpenWeathwerMap(Weather toCurrentWeather)
        {
            this._CurrentWeather = toCurrentWeather;
        }

        /// <summary>
        /// Check service
        /// </summary>
        /// <returns></returns>
        public bool CheckService()
        {
            if (string.IsNullOrWhiteSpace(this._APIKey))
            {
                this._CurrentWeather._LastUpdatedText = "API Key required.";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get weather info based on latitude and longitude
        /// </summary>
        /// <param name="tcLat"></param>
        /// <param name="tcLon"></param>
        /// <param name="TemperatureType"></param>
        /// <returns></returns>
        public bool _GetLocationLatLon(string tcLat, string tcLon, TemperatureTypes TemperatureType)
        {
            if (string.IsNullOrWhiteSpace(this._APIKey))
            {
                this._CurrentWeather._LastUpdatedText += "API Key required.";
                return false;
            }
            if (this._CurrentWeather._LastUpdatedText == "Retrieving data for " + tcLat + "," + tcLon)
                this._CurrentWeather._LastUpdatedText += ".";
            else
                this._CurrentWeather._LastUpdatedText = "Retrieving data for " + tcLat + "," + tcLon;

            string lcSearch = "http://api.openweathermap.org/data/2.5/weather?mode=xml&appid=" + this._APIKey;
            lcSearch += "&lat=" + tcLat + "&lon=" + tcLon;
            lcSearch += "&units=" + (TemperatureType == TemperatureTypes.Celsius ? "metric" : "imperial");

            string lcRet = this._WebClientRequest(lcSearch);
            if (!string.IsNullOrWhiteSpace(lcRet))
            {
                //Parse XML String
                XDocument xdoc = XDocument.Parse(lcRet);
                //Location
                var locationNode = xdoc.Descendants("current").FirstOrDefault();
                if (locationNode != null)
                {
                    return this._ParseXmlResult((XElement)locationNode);
                }
            }

            return false;
        }

        /// <summary>
        /// Get weather info based on a search string
        /// </summary>
        /// <param name="tcSearch"></param>
        /// <param name="TemperatureType"></param>
        /// <returns></returns>
        public bool _GetSearch(string tcSearch, TemperatureTypes TemperatureType)
        {
            if (this._CurrentWeather._LastUpdatedText == "Retrieving data for " + tcSearch)
                this._CurrentWeather._LastUpdatedText += ".";
            else
                this._CurrentWeather._LastUpdatedText = "Retrieving data for " + tcSearch;

            string lcSearch = "http://api.openweathermap.org/data/2.5/find?mode=xml&appid=" + this._APIKey;
            lcSearch += "&q=" + tcSearch;
            lcSearch += "&units=" + (TemperatureType == TemperatureTypes.Celsius ? "metric" : "imperial");

            string lcRet = this._WebClientRequest(lcSearch);
            if (!string.IsNullOrWhiteSpace(lcRet))
            {
                //Parse XML String
                XDocument xdoc = XDocument.Parse(lcRet);
                //Location
                var locationNode = xdoc.Descendants("cities").FirstOrDefault();
                if (locationNode != null && Convert.ToInt32(locationNode.Descendants("count").FirstOrDefault().Value) > 0)
                {
                    locationNode = locationNode.Descendants("list").FirstOrDefault().Descendants("item").FirstOrDefault();
                    return this._ParseXmlResult((XElement)locationNode);
                }
            }

            return false;
        }

        /// <summary>
        /// WebClient request
        /// </summary>
        /// <param name="toURL"></param>
        /// <returns></returns>
        private string _WebClientRequest(string toURL)
        {
            WebClient wc = new WebClient();
            Stream data = null;
            StreamReader reader = null;
            try
            {
                // Initialize Web Client and set its encoding to UTF8
                wc.Proxy = null;
                wc.Encoding = Encoding.UTF8;

                //// Download string (XML data) from REST API response
                //string XMLresult = wc.DownloadString(toURL.ToString());

                // Download string (XML data) from API response
                data = wc.OpenRead(toURL.ToString());
                reader = new StreamReader(data);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                this._CurrentWeather._LastUpdatedText = "Error retriving data : " + ex.Message.ToString();
            }
            finally
            {
                if (data != null) data.Close();
                if (reader != null) reader.Close();
                //Dispose WebClient
                wc.Dispose();
            }

            return "";
        }

        /// <summary>
        /// Parse recovered XML
        /// </summary>
        /// <param name="XMLresult"></param>
        private bool _ParseXmlResult(XElement locationNode = null)
        {
            if (locationNode != null)
            {
                this._CurrentWeather._City = locationNode.Descendants("city").FirstOrDefault().Attribute("name").Value.ToString();
                this._CurrentWeather._City += ", " + locationNode.Descendants("city").FirstOrDefault().Descendants("country").FirstOrDefault().Value.ToString();

                //Temperature
                this._CurrentWeather._Temp = locationNode.Descendants("temperature").FirstOrDefault().Attribute("value").Value.ToString() + "º";
                //this._CurrentWeather._Temp = Convert.ToDecimal(locationNode.Descendants("temperature").FirstOrDefault().Attribute("value").Value).ToString("0.0") + "º";
                this._CurrentWeather._WeatherCode = locationNode.Descendants("weather").FirstOrDefault().Attribute("number").Value.ToString();
                this._CurrentWeather._MeteoconCode = OpenWeatherMapCode2Meteocon[Convert.ToInt32(this._CurrentWeather._WeatherCode)].ToString();
                if (string.IsNullOrWhiteSpace(this._CurrentWeather._MeteoconCode))
                {
                    string lcIcon = locationNode.Descendants("weather").FirstOrDefault().Attribute("icon").Value.ToString();
                    this._CurrentWeather._MeteoconCode = OpenWeatherMapIcons2Meteocon[lcIcon].ToString();
                }

                this._CurrentWeather._WeatherText = locationNode.Descendants("weather").FirstOrDefault().Attribute("value").Value.ToString();
                //Forecast
                this._CurrentWeather._MinTemp = locationNode.Descendants("temperature").FirstOrDefault().Attribute("min").Value.ToString();
                this._CurrentWeather._MaxTemp = locationNode.Descendants("temperature").FirstOrDefault().Attribute("max").Value.ToString();
                //Type
                this._CurrentWeather._TemperatureType = locationNode.Descendants("temperature").FirstOrDefault().Attribute("unit").Value.ToString() == "metric" ? TemperatureTypes.Celsius : TemperatureTypes.Farenheit;
                //Link
                this._CurrentWeather._Link = "https://openweathermap.org/city/" + locationNode.Descendants("city").FirstOrDefault().Attribute("id").Value.ToString();

                this._CurrentWeather._LastUpdatedDate = DateTime.Now;
                this._CurrentWeather._LastUpdatedText = "Last updated on " + this._CurrentWeather._LastUpdatedDate.ToString();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
