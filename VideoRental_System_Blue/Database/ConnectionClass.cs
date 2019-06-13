using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace VideoRental_System_Blue.Database
{
    interface database {
        void sqlInject(String query);
        DataTable sqlSrch(String query);
    }
    public class ConnectionClass: database
    {
        private SqlConnection sqlconn;
        private String Loc = "Data Source=LAPTOP-UNJTSKGF\\SQLEXPRESS;Initial Catalog=VideoRental_System_Blue;Integrated Security=True";
        private SqlCommand sqlcmd;
        private SqlDataReader sqlreader;

        public void sqlInject(String query) {
            sqlconn = new SqlConnection(Loc);
            sqlconn.Open();
            sqlcmd = new SqlCommand(query,sqlconn);
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
        }

        // user define method that is used to get the record from the table
        public DataTable sqlSrch(String query)
        {
            DataTable sqltbl = new DataTable();


            sqlconn= new SqlConnection(Loc);

            sqlconn.Open();
            sqlcmd = new SqlCommand(query,sqlconn);

            sqlreader =sqlcmd.ExecuteReader();

            sqltbl.Load(sqlreader);

            sqlconn.Close();

            return sqltbl;
        }

        // count the no of rows in the tables that are matching the query
        public int CountDuplicate(String qry)
        {
            sqlconn= new SqlConnection(Loc);

            sqlconn.Open();
            sqlcmd = new SqlCommand(qry,sqlconn);

            return Convert.ToInt32(sqlcmd.ExecuteScalar().ToString());
        }
        // get the Record from the Rental table and return to the Data grid View 
        public DataTable RentalRecord() {

            DataTable Rental_tbl = new DataTable();

            sqlconn = new SqlConnection(Loc);

            sqlconn.Open();

            sqlcmd = new SqlCommand("Select * from Rental_Record", sqlconn);

            sqlreader = sqlcmd.ExecuteReader();

            Rental_tbl.Load(sqlreader);

            sqlconn.Close();

            return Rental_tbl;

        }

        // get the Record from the Video table and return to the Data grid View 
        public DataTable VideoRecord()
        {

            DataTable Video_tbl = new DataTable();

            sqlconn = new SqlConnection(Loc);

            sqlconn.Open();

            sqlcmd = new SqlCommand("Select * from Video_Record", sqlconn);

            sqlreader = sqlcmd.ExecuteReader();

            Video_tbl.Load(sqlreader);

            sqlconn.Close();

            return Video_tbl;

        }

        // get the Record from the Customer table and return to the Data grid View 
        public DataTable CustomerRecord()
        {

            DataTable Customer_tbl = new DataTable();

            sqlconn = new SqlConnection(Loc);

            sqlconn.Open();

            sqlcmd = new SqlCommand("Select * from Customer_Record", sqlconn);

            sqlreader = sqlcmd.ExecuteReader();

            Customer_tbl.Load(sqlreader);

            sqlconn.Close();

            return Customer_tbl;

        }

        public DataTable srchTopCustomer() {
            DataTable tb = new DataTable();
            String qry = "select Customer_ID,Return_Date, COUNT(*) FROM Rental_Record GROUP BY Customer_ID,Return_Date HAVING COUNT(*)>=1";
            
            tb =sqlSrch(qry);

            return tb;
        }

        public DataTable srchTopMovie() {
            DataTable tb = new DataTable();
            String qry = "select Movie_ID,Return_Date, COUNT(*) FROM Rental_Record GROUP BY Movie_ID,Return_Date HAVING COUNT(*)>=1";

            tb = sqlSrch(qry);

            return tb;
        }





    }
}
