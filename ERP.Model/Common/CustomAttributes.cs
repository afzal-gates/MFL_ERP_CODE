using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Common
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute()
            : base(typeof(DateTime), DateTime.Now.ToShortDateString(), "31/Dec/" + DateTime.Now.Year.ToString() )
        {
        }

        public DateRangeAttribute(string minimumValue)
            : base(typeof(DateTime), minimumValue,DateTime.Now.ToShortDateString())
        {
        }
        
        public DateRangeAttribute(string minimumValue, string maximumValue)
            : base(typeof(DateTime), minimumValue, maximumValue)
        {
        }
    }

    //public class 
   

}