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

namespace WeatherUserControlPlugin
{
    /// <summary>
    /// Yahoo Weather API
    /// https://developer.yahoo.com/weather/
    /// </summary>
    public class YahooWeatherAPI : IWeatherAPI
    {
        #region Yahoo Weather Codes to Meteocons Webfonf

        /// <summary>
        /// Equivalence between Yahoo Weather Codes anad Meteocons_webfont.ttf
        /// Yahoo Weathe Codes : https://developer.yahoo.com/weather/documentation.html#codes
        /// Metacons Webfont : http://www.alessioatzeni.com/meteocons/
        /// </summary>
        public Dictionary<int, string> WeatherCode2Meteocon = new Dictionary<int, string>()
        {
            { 0, "F" },//TORNADO,
            { 1, "F" },//TROPICAL_STORM,
            { 2, "F" },//HURRICANE,
            { 3, "&" },//SEVERE_THUNDERSTOMS,
            { 4, "&" },//THUNDERSTOMS,
            { 5, "#" },//MIXED_RAIN_AND_SNOW,
            { 6, "#" },//MIXED_RAIN_AND_SLEET,
            { 7, "#" },//MIXED_SNOW_AND_SLEET,
            { 8, "Q" },//FREEZING_DRIZZLE,
            { 9, "Q" },//DRIZZLE,
            { 10, "R" },//FREEZING_RAIN,
            { 11, "R" },//SHOWERS1,
            { 12, "R" },//SHOWERS2,
            { 13, "X" },//SNOW_FLURRIES,
            { 14, "X" },//LIGHT_SNOW_SHOWERS,
            { 15, "X" },//BLOWING_SNOW,
            { 16, "X" },//SNOW,
            { 17, "$" },//HAIL,
            { 18, "8" },//SLEET,
            { 19, "M" },//DUST,
            { 20, "L" },//FOGGY,
            { 21, "A" },//HAZE,
            { 22, "L" },//SMOKY,
            { 23, "F" },//BLUSTERY,
            { 24, "F" },//WINDY,
            { 25, "G" },//COLD,
            { 26, "N" },//CLOUDY,
            { 27, "I" },//MOSTLY_CLOUDY_NIGHT,
            { 28, "H" },//MOSTLY_CLOUDY_DAY,
            { 29, "4" },//PARTLY_CLOUDY_NIGHT,
            { 30, "3" },//PARTLY_CLOUDY_DAY,
            { 31, "C" },//CLEAR_NIGHT,
            { 32, "B" },//SUNNY,
            { 33, "2" },//FAIR_NIGHT,
            { 34, "1" },//FAIR_DAY,
            { 35, "!" },//MIXED_RAIN_AND_HAIL,
            { 36, "'" },//HOT,
            { 37, "6" },//ISOLATED_THUNDERSTORMS,
            { 38, "6" },//SCATTERED_THUNDERSTORMS1,
            { 39, "6" },//SCATTERED_THUNDERSTORMS2,
            { 40, "8" },//SCATTERED_SHOWERS,
            { 41, "$" },//HEAVY_SNOW1,
            { 42, "$" },//SCATTERD_SNOW_SHOWERS,
            { 43, "$" },//HEAVY_SNOW2,
            { 44, "%" },//PARTLY_CLOUDY,
            { 45, "&" },//THUNDERSHOWERS,
            { 46, "$" },//SNOW_SHOWERS,
            { 47, "&" },//ISOLATED_THUNDERSHOWERS,
            { 3200, ")" }//NOT_AVAILABLE = 3200
        };

        #endregion Yahoo Weather Codes to Meteocons Webfonf

        /// <summary>
        /// Api key not needed
        /// </summary>
        public string APIKey
        {
            get { return ""; }
            set {}
        }

        /// <summary>
        /// Provider name
        /// </summary>
        public string _Provider
        {
            get { return "Yahoo Weather"; }
        }

        /// <summary>
        /// Provider link
        /// </summary>
        public string _ProviderLink
        {
            get { return "https://www.yahoo.com/?ilc=401"; }
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
        public YahooWeatherAPI(Weather toCurrentWeather)
        {
            this._CurrentWeather = toCurrentWeather;
        }

        /// <summary>
        /// Check service
        /// </summary>
        /// <returns></returns>
        public bool CheckService()
        {
            return true;
        }

        /// <summary>
        /// Recover weather info based on Lattitude + Longitude from Yahoo Weather API
        /// </summary>
        /// <param name="tcLat"></param>
        /// <param name="tcLon"></param>
        public bool _GetLocationLatLon(string tcLat, string tcLon, TemperatureTypes TemperatureType)
        {
            if (this._CurrentWeather._LastUpdatedText == "Retrieving data for " + tcLat + "," + tcLon)
                this._CurrentWeather._LastUpdatedText += ".";
            else
                this._CurrentWeather._LastUpdatedText = "Retrieving data for " + tcLat + "," + tcLon;

            // Form Actual URL - REST API call
            StringBuilder sbURL = new StringBuilder();
            sbURL.Append(@"https://query.yahooapis.com/v1/public/yql?q=");
            // YQL 
            sbURL.Append(@"select%20*%20from%20weather.forecast%20where%20woeid%20in%20(SELECT%20woeid%20FROM%20geo.places%20WHERE%20text=%22(" + tcLat + "," + tcLon + ")%22)");
            //Celsius
            sbURL.Append(@" and u='" + (TemperatureType == TemperatureTypes.Celsius ? "c" : "f") + "'");
            // Prevent cross site scripting
            sbURL.Append(@"&diagnostics=true");

            return this._WebClientRequest(sbURL);
        }

        /// <summary>
        /// Recover weather info based on a search string from Yahoo Weather API
        /// </summary>
        /// <param name="tcSearch"></param>
        public bool _GetSearch(string tcSearch, TemperatureTypes TemperatureType)
        {
            if (this._CurrentWeather._LastUpdatedText == "Retrieving data for " + tcSearch)
                this._CurrentWeather._LastUpdatedText += ".";
            else
                this._CurrentWeather._LastUpdatedText = "Retrieving data for " + tcSearch;

            // Form Actual URL - REST API call
            StringBuilder sbURL = new StringBuilder();
            sbURL.Append(@"https://query.yahooapis.com/v1/public/yql?q=");
            // YQL 
            sbURL.Append(@"select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + tcSearch + "%22)");
            //Celsius
            sbURL.Append(@" and u='" + (TemperatureType == TemperatureTypes.Celsius ? "c" : "f") + "'");
            // Prevent cross site scripting
            sbURL.Append(@"&diagnostics=true");

            return this._WebClientRequest(sbURL);
        }

        /// <summary>
        /// WebClient request
        /// </summary>
        /// <param name="toURL"></param>
        /// <returns></returns>
        private bool _WebClientRequest(StringBuilder toURL)
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
                return this._ParseXmlResult(reader.ReadToEnd());
            }
            catch (Exception ex)
            {
                this._CurrentWeather._LastUpdatedText = "Error retriving data : " + ex.ToString();
            }
            finally
            {
                if (data != null) data.Close();
                if (reader != null) reader.Close();
                //Dispose WebClient
                wc.Dispose();
            }

            return false;
        }

        /// <summary>
        /// Parse recovered XML from Yahoo Weather API
        /// </summary>
        /// <param name="XMLresult"></param>
        private bool _ParseXmlResult(string XMLresult)
        {
            //Parsew XML String
            XDocument xdoc = XDocument.Parse(XMLresult);
            XNamespace yWeather = "http://xml.weather.yahoo.com/ns/rss/1.0";
            //Location
            var locationNode = xdoc.Descendants(yWeather + "location").FirstOrDefault();
            if (locationNode != null)
            {
                this._CurrentWeather._City = locationNode.Attribute("city").Value.ToString();
                //Temperature
                locationNode = xdoc.Descendants(yWeather + "condition").FirstOrDefault();
                this._CurrentWeather._Temp = locationNode.Attribute("temp").Value.ToString() + "º";
                this._CurrentWeather._WeatherCode = locationNode.Attribute("code").Value.ToString();
                this._CurrentWeather._MeteoconCode = WeatherCode2Meteocon[Convert.ToInt32(this._CurrentWeather._WeatherCode)].ToString();
                this._CurrentWeather._WeatherText = locationNode.Attribute("text").Value.ToString();
                //Forecast
                locationNode = xdoc.Descendants(yWeather + "forecast").FirstOrDefault();
                this._CurrentWeather._MinTemp = locationNode.Attribute("low").Value.ToString();
                this._CurrentWeather._MaxTemp = locationNode.Attribute("high").Value.ToString();
                //Type
                locationNode = xdoc.Descendants(yWeather + "units").FirstOrDefault();
                this._CurrentWeather._Temp += locationNode.Attribute("temperature").Value.ToString();
                //Link
                this._CurrentWeather._Link = xdoc.Descendants("link").FirstOrDefault().Value.ToString();

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
