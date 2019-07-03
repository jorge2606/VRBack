using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class DateDto
    {
        public int Day { set; get; }
        public int Month { set; get; }
        public int Year { set; get; }

        public DateTime ToDateTime()
        {
            return new DateTime(this.Year, this.Month, this.Day);
        }

        public DateTime ToDateTime(int year, int month, int day)
        {
            return new DateTime(year,month,day);
        }

        public bool AreSame(DateDto param)
        {
            return param.Day == Day && param.Month == Month && param.Year == Year;
        }
    }

    public class TimeDto
    {
        public int Hour { set; get; }
        public int Minute { set; get; }
        public int Second { set; get; }

        public DateTime ToDateTime(DateTime p_date)
        {
            return new DateTime(p_date.Year, p_date.Month, p_date.Day, Hour, Minute, Second);
        }
    }
}
