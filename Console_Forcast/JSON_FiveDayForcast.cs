using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Console_Forcast
{

    public class JSON_FiveDayForcast
    {
        #region Root Properties
        /// <summary>
        /// API
        /// </summary>
        public string cod { get; set; }
        /// <summary>
        /// API
        /// </summary>
        public int message { get; set; }
        /// <summary>
        /// Number of lines returned by this API call 
        /// </summary>
        public int cnt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public City city { get; set; }

        #endregion

        #region JSON SubClasses
        public class ListItem
        {
            /// <summary>
            /// Time of data forecasted, unix, UTC
            /// </summary>
            public int dt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Main main { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<WeatherItem> weather { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Clouds clouds { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Wind wind { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Sys sys { get; set; }
            /// <summary>
            /// Time of data forecasted, ISO, UTC
            /// </summary>
            public string dt_txt { get; set; }
        }


        public class Main
        {
            /// <summary>
            /// Temperature
            /// </summary>
            public double temp { get; set; }
            /// <summary>
            /// Human perception of Temperature
            /// </summary>
            public double feels_like { get; set; }
            /// <summary>
            /// Minimum temperature at the moment of calculation.
            /// </summary>
            public double temp_min { get; set; }
            /// <summary>
            /// Maximum temperature at the moment of calculation.
            /// </summary>
            public double temp_max { get; set; }
            /// <summary>
            /// Atmospheric pressure on the sea level by default, hPa
            /// </summary>
            public int pressure { get; set; }
            /// <summary>
            /// Atmospheric pressure on the sea level by default, hPa
            /// </summary>
            public int sea_level { get; set; }
            /// <summary>
            /// Atmospheric pressure on the ground level, hPa
            /// </summary>
            public int grnd_level { get; set; }
            /// <summary>
            /// Humidity %
            /// </summary>
            public int humidity { get; set; }
            /// <summary>
            /// API param
            /// </summary>
            public double temp_kf { get; set; }
        }


        public class WeatherItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// Weather condition id
            /// </summary>
            public string main { get; set; }
            /// <summary>
            /// Group of weather parameters (Rain, Snow, Extreme etc.)
            /// </summary>
            public string description { get; set; }
            /// <summary>
            /// Weather condition within the group
            /// </summary>
            public string icon { get; set; }
        }


        public class Clouds
        {
            /// <summary>
            /// Cloudiness, %
            /// </summary>
            public int all { get; set; }
        }


        public class Wind
        {
            /// <summary>
            /// Wind speed. miles/hour
            /// </summary>
            public double speed { get; set; }
            /// <summary>
            /// Wind direction - degrees
            /// </summary>
            public int deg { get; set; }
        }


        public class Sys
        {
            /// <summary>
            /// API
            /// </summary>
            public string pod { get; set; }
        }



        public class Coord
        {
            /// <summary>
            /// City geo location, latitude
            /// </summary>
            public double lat { get; set; }
            /// <summary>
            /// City geo location, longitude
            /// </summary>
            public double lon { get; set; }
        }


        public class City
        {
            /// <summary>
            /// City ID
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// City name 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Coord coord { get; set; }
            /// <summary>
            /// Country code (GB, JP etc.)
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// population
            /// </summary>
            public int population { get; set; }
            /// <summary>
            /// Shift in seconds from UTC
            /// </summary>
            public int timezone { get; set; }
            /// <summary>
            /// Sun rise time unix, UTC
            /// </summary>
            public int sunrise { get; set; }
            /// <summary>
            /// Sun set time unix, UTC
            /// </summary>
            public int sunset { get; set; }
        }

        #endregion

    }
}
 