using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyDAL
{
    public class DestinyDailyManager
    {
        private int TimeDifference
        {
            get
            {
                var strTimeDiff = ConfigurationManager.AppSettings["TimeZoneDifference"];
                return Convert.ToInt32(strTimeDiff);
            }
        }

        public DateTime TodayDate => DateTime.Now.AddHours(TimeDifference).AddMinutes(5);
        protected DateTime WeeklyDate
        {
            get
            {
                var today = DateTime.Now.AddHours(TimeDifference).AddMinutes(5);
                while (today.DayOfWeek != DayOfWeek.Tuesday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }

        protected DateTime TrialsDate
        {
            get
            {
                var today = DateTime.Now.AddHours(TimeDifference).AddMinutes(5);
                while (today.DayOfWeek != DayOfWeek.Friday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }

        protected DateTime XurDate
        {
            get
            {
                var today = DateTime.Now.AddHours(TimeDifference).AddMinutes(5);
                while (today.DayOfWeek != DayOfWeek.Friday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }
    }
}
