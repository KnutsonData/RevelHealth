using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Forcast
{
    /// <summary>
    /// Class supports business logic to determine recommended messaging format
    /// for a given day based on weather
    /// </summary>
    public class ForcastMessageForDate
    {
        #region Properties

        public string ForcastDate { get; set; }

        public bool IsCloudyForDate { get; set; }

        public bool IsRainyForDate { get; set; }

        private double _maxTempForDate;
        public double MaxTempForDate
        {
            get => _maxTempForDate;
            set
            {
                if (value > _maxTempForDate)
                {
                    _maxTempForDate = value;
                }
            }
        }

        #endregion

        #region Constructor

        public ForcastMessageForDate(string forcastDate)
        {
            ForcastDate = forcastDate;
        }

        #endregion


        /// <summary>
        /// Builds and returns a formated string based on the IsRainyForDate, IsCloudyForDate, and MaxTempForDate.
        /// </summary>
        /// <returns>string in the format: <date> /n <recommendation string> </returns>
        public string MessageForDate()
        {
            string message = ForcastDate + "\n";
            if (IsRainyForDate)
            {
                message += "Phone Call";
            }
            else if ((MaxTempForDate > 75) && (IsCloudyForDate == false))
            {
                message += "Text Message";
            }
            else if (MaxTempForDate > 55)
            {
                message += "Email";
            }
            else
            {
                message += "Phone Call";
            }

            return message;
        }
    }


}
