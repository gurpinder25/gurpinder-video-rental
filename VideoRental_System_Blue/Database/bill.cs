using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRental_System_Blue.Database
{
    class bill
    {
        int total = 0;
        // total bill calculation method
        public int totalBill(String RentDate, int cost) {

            DateTime Current_date = DateTime.Now;

            //convert the old date from string to Date fromat
            DateTime Old_date = Convert.ToDateTime(RentDate.ToString());


            //get the difference in the days fromat
            String diff = (Current_date - Old_date).TotalDays.ToString();


            // calculate the round off value 
            Double Days = Math.Round(Convert.ToDouble(diff));



            // return the total cost of the Video


            total= cost * Convert.ToInt32(Days);


            return total;
        }

    }
}
