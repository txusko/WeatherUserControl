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
using System.ComponentModel;
using System.Reflection;

namespace WeatherUserControlPlugin
{

    /// <summary>
    /// Temperature types : Celsius or Farenheit
    /// </summary>
    public enum TemperatureTypes
    {
        Celsius,
        Farenheit
    }

    /// <summary>
    /// Weather APIs
    /// </summary>
    public enum WeatherAPIs
    {
        /// <summary>
        /// Open Weather Map API
        /// </summary>
        OpenWeatherMap,
        /// <summary>
        /// Yahoo API
        /// </summary>
        Yahoo,
        ///// <summary>
        ///// Weather Underground
        ///// </summary>
        //WeatherUnderground
    }

    /// <summary>
    /// Interface for Weather provider APIs
    /// </summary>
    public interface IWeatherAPI
    {
        /// <summary>
        /// API Key
        /// </summary>
        string APIKey { get; set; }

        /// <summary>
        /// Provider name
        /// </summary>
        string _Provider { get; }

        /// <summary>
        /// Provider link
        /// </summary>
        string _ProviderLink { get; }

        /// <summary>
        /// Current weather object
        /// </summary>
        Weather _CurrentWeather { get; set; }

        /// <summary>
        /// Check service
        /// </summary>
        /// <returns></returns>
        bool CheckService();

        /// <summary>
        /// Recover weather info based on Lattitude + Longitude from Yahoo Weather API
        /// </summary>
        /// <param name="tcLat">Latitude to search</param>
        /// <param name="tcLon">Longitude to search</param>
        /// <param name="TemperatureType">Type of temperature to search</param>
        bool _GetLocationLatLon(string tcLat, string tcLon, TemperatureTypes TemperatureType);
        
        /// <summary>
        /// Recover weather info based on a search string from Yahoo Weather API
        /// </summary>
        /// <param name="tcSearch">string to search</param>
        /// <param name="TemperatureType">Type of temperature to search</param>
        bool _GetSearch(string tcSearch, TemperatureTypes TemperatureType);
        
    }

    /// <summary>
    /// Weather information class
    /// </summary>
    public class Weather
    {

        #region WEATHER PROPERTIES

        /// <summary>
        /// Longitude
        /// </summary>
        public string _Longitude = "";

        /// <summary>
        /// Latitude
        /// </summary>
        public string _Latitude = "";

        /// <summary>
        /// Current weather code
        /// </summary>
        public string _WeatherCode = "";

        /// <summary>
        /// Meteocon current weather code
        /// </summary>
        public string _MeteoconCode = "";

        /// <summary>
        /// Yahoo current weather text
        /// </summary>
        public string _WeatherText = "";

        /// <summary>
        /// Current temperature
        /// </summary>
        public string _Temp = "";

        /// <summary>
        /// Current location
        /// </summary>
        public string _City = "";

        /// <summary>
        /// Today's lower temperature
        /// </summary>
        public string _MinTemp = "";

        /// <summary>
        /// Today's higher temperature
        /// </summary>
        public string _MaxTemp = "";

        /// <summary>
        /// Weather link
        /// </summary>
        public string _Link = "";

        /// <summary>
        /// Info message
        /// </summary>
        public string _LastUpdatedText
        {
            get { return this.__LastUpdatedText; }
            set 
            { 
                this.__LastUpdatedText = value;
                string lcKey = DateTime.Now.ToString("yyyy MM dd HH:mm:ss ffffff");
                if (!_HistoryInformation.ContainsKey(lcKey))
                    _HistoryInformation.Add(lcKey, this.__LastUpdatedText);
            }
        }
        private string __LastUpdatedText = "";

        /// <summary>
        /// Log information
        /// </summary>
        public Dictionary<string, string> _HistoryInformation = new Dictionary<string, string>();

        /// <summary>
        /// Last updated date
        /// </summary>
        public DateTime? _LastUpdatedDate = null;

        /// <summary>
        /// Temperature type
        /// </summary>
        public TemperatureTypes _TemperatureType = TemperatureTypes.Celsius;

        #endregion WEATHER PROPERTIES

    }
}
