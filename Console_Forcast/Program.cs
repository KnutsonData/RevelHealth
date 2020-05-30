using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Console_Forcast
{
    class Program
    {   
        private const string FORCAST_URL = "http://api.openweathermap.org/data/2.5/forecast?q=minneapolis,us&units=imperial&APPID=09110e603c1d5c272f94f64305c09436";
        private static readonly HttpClient client = new HttpClient();


        #region Methods
        /// <summary>
        /// Call weather API and return a deserialized JSON object JSON_FiveDayForcast representing a series of 
        /// time-stamped, point-in-time weather forcasts for CONST FORCAST_URL.
        /// </summary>
        /// <returns>a deserialized JSON object JSON_FiveDayForcast</returns>
        private static async Task<JSON_FiveDayForcast> RetrieveForcasts()
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();

                var streamTask = client.GetStreamAsync(FORCAST_URL);
                var forcast = await JsonSerializer.DeserializeAsync<JSON_FiveDayForcast>(await streamTask);

                return forcast;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw ex;
            }

        }



        /// <summary>
        /// Builds and returns a list of strings.  Each string in the list represents a single date
        /// and its corresponding message type recommendation based on that date's weather forcast.
        /// </summary>
        /// <returns>List of Date strings with Messaging recommendation</returns>
        private static List<string> MessageReccomendations(JSON_FiveDayForcast fileDayForcast)
        {
            try
            {
                //List of strings to be returned
                var dailyForcasts = new List<string>();

                //Dictionary of <date as string, ForcastMessageForDate> to help process forcast list for each date in list
                Dictionary<string, ForcastMessageForDate> dict_ForcastedDates = new Dictionary<string, ForcastMessageForDate>();

                //8 AM to 4PM
                TimeSpan startOfBusiness = new TimeSpan(8, 0, 0);
                TimeSpan endOfBusiness = new TimeSpan(16, 0, 0);


                foreach (var timedForcast in fileDayForcast.list)
                {
                    DateTime ci_Date = DateTime.Parse(timedForcast.dt_txt).AddSeconds(fileDayForcast.city.timezone);

                    //if time of forcast list item is between business hours, evaluate data for date
                    if (ci_Date.TimeOfDay >= startOfBusiness
                        && ci_Date.TimeOfDay <= endOfBusiness)
                    {

                        //start by ensuring a dictionary entry was created for current date
                        if (!dict_ForcastedDates.ContainsKey(ci_Date.ToShortDateString()))
                        {
                            dict_ForcastedDates.Add(ci_Date.ToShortDateString(), new ForcastMessageForDate(ci_Date.ToShortDateString()));
                        }

                        //retrieve dictionary entry for current date
                        ForcastMessageForDate ci_ForcastMessageForDate = dict_ForcastedDates[ci_Date.ToShortDateString()];

                        //evaluate all weather objects, if any contain rain or mist, mark date as "Rainy"
                        foreach (var ci_Weather in timedForcast.weather)
                        {
                            if (ci_Weather.id > 199 && ci_Weather.id < 702)
                            {
                                ci_ForcastMessageForDate.IsRainyForDate = true;
                            }

                        }

                        //Check cloud cover percentage - if over 25%, mark date as "cloudy"
                        if (timedForcast.clouds.all > 25)
                        {
                            ci_ForcastMessageForDate.IsCloudyForDate = true;
                        } 

                        //Pass temperature to ForcastMessageForDate object - property should update if value is higher
                        ci_ForcastMessageForDate.MaxTempForDate = timedForcast.main.temp;

                    }

                }

                //Now use dictionary of ForcastMessageForDate objects to build list of string recommendations
                foreach (var ci_Key in dict_ForcastedDates.Keys)
                {
                    dailyForcasts.Add(dict_ForcastedDates[ci_Key].MessageForDate());
                }

                return dailyForcasts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw ex;
            }
        }
        #endregion

        static async Task Main(string[] args)
        {
            var forcast = await RetrieveForcasts();

            List<string> Recommendations = MessageReccomendations(forcast);

            foreach (var ci_recommendation in Recommendations)
            {
                Console.WriteLine(ci_recommendation);
            }

            //Dispose once all HttpClient calls are complete. 
            // https://stackoverflow.com/questions/15705092/do-httpclient-and-httpclienthandler-have-to-be-disposed-between-requests

            client.Dispose();

        }
    }
}
