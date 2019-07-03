using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Service.Helpers
{
    public static class TimeAgoClass
    {
        public static string TimeAgo(this DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("Hace {0} segundos", Math.Abs(timeSpan.Seconds) );
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1
                    ? String.Format("Hace {0} minutos aproximadamente", Math.Abs(timeSpan.Minutes) )
                    : "Hace 1 minuto aproximadamente";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1
                    ? String.Format("Hace {0} horas aproximadamente", Math.Abs(timeSpan.Hours) )
                    : "Hace 1 hora aproximadamente";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ? String.Format("Hace {0} dias aproximadamente", Math.Abs(timeSpan.Days) ) : "Ayer";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30
                    ? String.Format("Hace {0} meses aproximadamente", Math.Abs(timeSpan.Days) / 30)
                    : "Hace 1 mes Aproximadamente";
            }
            else
            {
                result = timeSpan.Days > 365
                    ? String.Format("about {0} years ago", Math.Abs(timeSpan.Days) / 365)
                    : "about a year ago";
            }

            return result;
        }
    }
}
