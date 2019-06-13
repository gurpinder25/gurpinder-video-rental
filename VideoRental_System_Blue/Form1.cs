using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRental_System_Blue
{
    public partial class Form1 : Form
    {
        //object of the database class using the concept of the package
        Database.ConnectionClass Conn = new Database.ConnectionClass();

        Database.bill billConn = new Database.bill();

        //global variable for database queries 
        String query = "";
        int video_ID = 0, Rental_ID = 0, Customer_ID = 0,Rent=0;
        int pecies = 0, cost=0;

        private void btnVideoDelete_Click(object sender, EventArgs e)
        {
            if (video_ID > 0)
            {
                query = "";
                query = "select count(*) from Rental_Record where Movie_ID=" + Convert.ToInt32(txtMovie_ID.Text.ToString()) + " And  Return_Date='borrow'";
                int count = Conn.CountDuplicate(query);
                MessageBox.Show(""+count);
                if (count == 0)
                {
                    query = "";
                    //deleting the record from the table after verifying from the Datagrid view
                    query = "delete from Video_Record where Video_id=" + Convert.ToInt32(txtMovie_ID.Text.ToString()) + "";
                    Conn.sqlInject(query);
                    MessageBox.Show("Movie Record is deleted from the DataBase Record");
                }
                else {
                    MessageBox.Show("Movie is Already on Rent so thus can't be Delete the Movie you can delete the movie if its return");
                }
            }
            else {
                MessageBox.Show("You must have to choose the video before deleting the Movie");
            }

            clearAll();
        }

        private void btnVideoUpdate_Click(object sender, EventArgs e)
        {
            if (video_ID > 0)
            {
                if (!txtVideoRatting.Text.ToString().Equals("") && !txtVideoTitle.Text.ToString().Equals("") && !txtVideoGenre.Text.ToString().Equals("") && !txtVideoPlot.Text.ToString().Equals("") && nmVideoCopies.Value > 0 && nmVideoCost.Value > 0 && nmVideoYear.Value > 0)
                {
                    query = "";
                    //deleting the record from the table after verifying from the Datagrid view
                    query = "update Video_Record set Video_Ratting='"+txtVideoRatting.Text.ToString()+ "',Video_Title='"+txtVideoTitle.Text.ToString()+ "',Video_Year="+nmVideoYear.Value+ " ,Video_Copies="+nmVideoCopies.Value+ " ,Video_Plot='"+txtVideoPlot.Text.ToString()+ "',Video_Genre='"+txtVideoGenre.Text.ToString()+ "',Video_Cost="+nmVideoCost.Value+" where Video_id=" + Convert.ToInt32(txtMovie_ID.Text.ToString()) + "";
                    Conn.sqlInject(query);
                    MessageBox.Show("Movie Record is Updated from the DataBase Record");

                }
                else {
                    MessageBox.Show("Fill all the Record first and then you can update the Record of the Movie");
                }
                
            }
            else
            {
                MessageBox.Show("You must have to choose the video before Updating the Movie");
            }

            clearAll();
        }

        private void nmVideoCopies_ValueChanged(object sender, EventArgs e)
        {

            //dislay the cost of the price of the video after adding the year of the video
            DateTime sqldate = DateTime.Now;

            int sqlyear = sqldate.Year;

            int sqldiff = sqlyear - Convert.ToInt32(nmVideoYear.Value);

            // MessageBox.Show(diff.ToString());
            if (sqldiff >= 5)
            {
                nmVideoCost.Text = "2";
            }
            if (sqldiff >= 0 && sqldiff < 5)
            {
                nmVideoCost.Text = "5";
            }

        }

        private void btnCustomerInsert_Click(object sender, EventArgs e)
        {
            //check and save the record of the customer permanantly
            if (!txtCustomerName.Text.ToString().Equals("") && !txtCustomerAddress.Text.ToString().Equals("") && nmCustomerPh.Value > 0)
            {
                query = "";
                query = "insert into Customer_Record(Customer_Name,Customer_Address,Customer_Ph) values ('" + txtCustomerName.Text.ToString() + "','" + txtCustomerAddress.Text.ToString() + "','" + nmCustomerPh.Value.ToString() + "')";
                Conn.sqlInject(query);
                MessageBox.Show("Customer Record is saved in the Database Record");
            }
            else {
                MessageBox.Show("Fill all the Details of the customer Before saving the Record Permanently");
            }
            clearAll();
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            //check and delete the record of the customer permanantly
            if (Customer_ID>0)
            {
                query = "";
                query = "select count(*) from Rental_Record where Customer_ID=" + Convert.ToInt32(txtCusomer_ID.Text.ToString())+ " And  Return_Date='borrow'";
                int count = Conn.CountDuplicate(query);
                if (count == 0)
                {
                    query = "";
                    query = "delete from Customer_Record where Customer_ID=" + Convert.ToInt32(txtCusomer_ID.Text.ToString()) + "";
                    Conn.sqlInject(query);
                    MessageBox.Show("Customer Record is deleted from  the Database Record");
                }
                else {
                    MessageBox.Show("You have already  movie on rent so thus u can't delete this record");
                }
            }
            else
            {
                MessageBox.Show("Select the Details of the customer Before deleting  the Record Permanently");
            }

            clearAll();
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            if (Customer_ID > 0)
            {

                if (!txtCustomerName.Text.ToString().Equals("") && !txtCustomerAddress.Text.ToString().Equals("") && nmCustomerPh.Value > 0 )
                {
                    query = "";
                    query = "delete from Customer_Record  Customer_Name='"+txtCustomerName.Text.ToString()+ "',Customer_Address='"+txtCustomerAddress.Text.ToString()+ "',Customer_Ph='"+nmCustomerPh.Value.ToString()+"' where Customer_ID=" + Convert.ToInt32(txtCusomer_ID.Text.ToString()) + "";
                    Conn.sqlInject(query);
                    MessageBox.Show("Customer Record is Updated from  the Database Record");
                }
                else {
                    MessageBox.Show("Fill all the Record of the Cusotmer Before Updating him");
                }       

            }
            else
            {
                MessageBox.Show("Select the Details of the customer Before Updating  the Record Permanently");
            }

            clearAll();

        }

        private void btnMovieRent_Click(object sender, EventArgs e)
        {
            //check and give the movie on rent to the customer for particular day
            if (!txtMovie_ID.Text.ToString().Equals("") && !txtCusomer_ID.Text.ToString().Equals(""))
            {
                query = "";
                query = "select count(*)  from Rental_Record where Customer_ID=" + Convert.ToInt32(txtCusomer_ID.Text.ToString())+ " and Return_Date='borrow'";
                int count = Conn.CountDuplicate(query);
               // MessageBox.Show(""+count);

                if (count < 2)
                {
                    query = "select count(*) from Rental_Record where Movie_ID=" + Convert.ToInt32(txtMovie_ID.Text.ToString())+ " and Return_Date='borrow'";
                    int Booked= Conn.CountDuplicate(query);
                    //MessageBox.Show("" + Booked);
                    if (Booked < pecies)
                    {
                        query = "";
                        query = "insert into Rental_Record(Movie_ID,Customer_ID,Rent_Date,Return_Date) values(" + Convert.ToInt32(txtMovie_ID.Text.ToString()) + "," + Convert.ToInt32(txtCusomer_ID.Text.ToString()) + ",'" + MovieRentDate.Text.ToString() + "','borrow')";
                        Conn.sqlInject(query);
                        MessageBox.Show("Movie is Given on rent to the Registered Customer");
                    }
                    else {
                        MessageBox.Show("No More Videos are available for Rent all has gone on rent ");
                    }
                }
                else {
                    MessageBox.Show("You already have 2 movie on rent so thus we can't give any more");
                }
            }
            else {
                MessageBox.Show("Check the Record before giving the movie on rent");
            }

            clearAll();
        }

        private void btnRentalMovieDelete_Click(object sender, EventArgs e)
        {
            if (Rental_ID > 0)
            {
                query = "";
                query = "select count(*) from Rental_Record where Rental_ID=" + Rental_ID+ " And Return_Date='borrow'";
                int count = Conn.CountDuplicate(query);
                if (count == 0)
                {


                    query = "";
                    query = "delete from Rental_Record where Rental_ID=" + Rental_ID + " and Return_Date!='borrow'";
                    Conn.sqlInject(query);
                    MessageBox.Show("Rental Record is Deleted from the Record ");
                }
                else {
                    MessageBox.Show("This movie is on rent so thus u can't delete this record");
                }
            }
            else {
                MessageBox.Show("Check the Record of the Rental Movie before Deleteing");
            }

            clearAll();
        }

        private void btnMovieReturn_Click(object sender, EventArgs e)
        {
            if (Rental_ID > 0)
            {
                query = "";
                query = "select * from Video_Record where Video_ID=" + Convert.ToInt32(txtMovie_ID.Text.ToString()) + "";
                DataTable tblVideo = new DataTable();
                tblVideo = Conn.sqlSrch(query);
                cost = Convert.ToInt32(tblVideo.Rows[0]["Video_Cost"].ToString());

                int TotalCharges=billConn.totalBill(MovieRentDate.Text.ToString(),cost);
                query = "";
                query = "update Rental_Record set Movie_ID=" + Convert.ToInt32(txtMovie_ID.Text.ToString()) + ",Customer_ID=" + Convert.ToInt32(txtCusomer_ID.Text.ToString()) + ",Rent_Date='" + MovieRentDate.Text.ToString() + "',Return_Date='" + MovieReturnDate.Text.ToString() + "'where Rental_ID=" + Rental_ID + "";
                Conn.sqlInject(query);
                MessageBox.Show("Movie is return to the Company and Your total charges are ===$"+TotalCharges);

            }
            else {
                MessageBox.Show("Check the Record before Returning the Movie");
            }
            clearAll();
        }

        private void RentalRecord_Click(object sender, EventArgs e)
        {
            // get the record from the Table and  display in the Data grid view
            Details.DataSource = Conn.RentalRecord();
            Rent = 1;
            Customer_ID = 0;
            video_ID = 0;


        }

        private void VideoRecord_Click(object sender, EventArgs e)
        {
            // get the record from the Table and  display in the Data grid view
            Details.DataSource = Conn.VideoRecord();

            Rent = 0;
            Customer_ID = 0;
            video_ID = 1;

        }

        private void CustomerRecord_Click(object sender, EventArgs e)
        {
            // get the record from the Table and  display in the Data grid view
            Details.DataSource = Conn.CustomerRecord();
            Rent=0;
            Customer_ID = 1;
            video_ID = 0;


        }

        private void RattingMovie_Click(object sender, EventArgs e)
        {
            // check the top most rated Movies which have sent  on rent 
            Details.DataSource = Conn.srchTopMovie();

            int greater = 0, topID = 0; ;
            for (int y = 0; y < Details.Rows.Count - 1; y++)
            {
                if (greater < Convert.ToInt32(Details.Rows[y].Cells[2].Value))
                {
                    greater = Convert.ToInt32(Details.Rows[y].Cells[2].Value);
                    topID = Convert.ToInt32(Details.Rows[y].Cells[0].Value);
                }

            }
            Details.DataSource = "";
            MessageBox.Show("Movie ID is==" + topID + " went  Mostly on Rent ==" + greater);

        }

        private void RattingCustomer_Click(object sender, EventArgs e)
        {
            // check the top most rated customer who have the most of movies on rent 
            Details.DataSource = Conn.srchTopCustomer();

            int greater = 0, topID = 0; ;
            for (int y = 0; y < Details.Rows.Count - 1; y++)
            {
                if (greater < Convert.ToInt32(Details.Rows[y].Cells[2].Value))
                {
                    greater = Convert.ToInt32(Details.Rows[y].Cells[2].Value);
                    topID = Convert.ToInt32(Details.Rows[y].Cells[0].Value);
                }

            }
            Details.DataSource = "";
            MessageBox.Show("Customer ID is==" + topID + " Having Most Video==" + greater);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Details_DoubleClick(object sender, EventArgs e)
        {
            if (Customer_ID>0) {
                txtCusomer_ID.Text = Details.CurrentRow.Cells[0].Value.ToString();
                txtCustomerName.Text= Details.CurrentRow.Cells[1].Value.ToString();
                txtCustomerAddress.Text= Details.CurrentRow.Cells[2].Value.ToString();
                String phone=Details.CurrentRow.Cells[3].Value.ToString();
                nmCustomerPh.Value = Convert.ToInt64(phone);
            }

            if (video_ID>0) {
                txtMovie_ID.Text= Details.CurrentRow.Cells[0].Value.ToString();
                txtVideoRatting.Text= Details.CurrentRow.Cells[1].Value.ToString();
                txtVideoTitle.Text= Details.CurrentRow.Cells[2].Value.ToString();
                nmVideoYear.Value = Convert.ToInt32(Details.CurrentRow.Cells[3].Value.ToString());

                nmVideoCopies.Value=Convert.ToInt32(Details.CurrentRow.Cells[4].Value.ToString());
                pecies = Convert.ToInt32(nmVideoCopies.Value);

                txtVideoPlot.Text= Details.CurrentRow.Cells[5].Value.ToString();
                txtVideoGenre.Text= Details.CurrentRow.Cells[6].Value.ToString();
                nmVideoCost.Value = Convert.ToInt32(Details.CurrentRow.Cells[7].Value.ToString());
                cost= Convert.ToInt32(Details.CurrentRow.Cells[7].Value.ToString()); 


            }

            if (Rent>0) {
                Rental_ID= Convert.ToInt32(Details.CurrentRow.Cells[0].Value.ToString());
                txtMovie_ID.Text= Details.CurrentRow.Cells[1].Value.ToString();
                txtCusomer_ID.Text= Details.CurrentRow.Cells[2].Value.ToString();
                MovieRentDate.Text= Details.CurrentRow.Cells[3].Value.ToString();

            }

        }
        public void clearAll() {
            txtCusomer_ID.Text = "";
            txtCustomerName.Text = "";
            txtCustomerAddress.Text = "";
            txtMovie_ID.Text = "";
            txtVideoGenre.Text = "";
            txtVideoPlot.Text = "";
            txtVideoRatting.Text = "";
            txtVideoTitle.Text = "";

            nmCustomerPh.Value = 0;
            nmVideoCopies.Value = 0;
            nmVideoCost.Value = 0;
            nmVideoYear.Value = 0;

            Details.DataSource = "";
            Rental_ID = 0;

        }

        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnVideoInsert_Click(object sender, EventArgs e)
        {
            //check the values of all the textbox and numeric updown before inserting the record in the database 
            if (!txtVideoRatting.Text.ToString().Equals("") && !txtVideoTitle.Text.ToString().Equals("") && !txtVideoPlot.Text.ToString().Equals("") && !txtVideoGenre.Text.ToString().Equals("") && nmVideoCopies.Value > 0 && nmVideoCost.Value > 0 && nmVideoYear.Value > 0)
            {
                query = "insert into Video_Record(Video_Ratting,Video_Title,Video_Year,Video_Copies,Video_Plot,Video_Genre,Video_Cost) values('" + txtVideoRatting.Text.ToString() + "','" + txtVideoTitle.Text.ToString() + "'," + nmVideoYear.Value + "," + nmVideoCopies.Value + ",'" + txtVideoPlot.Text.ToString() + "','" + txtVideoGenre.Text.ToString() + "'," + nmVideoCost.Value + ")";
                Conn.sqlInject(query);

                MessageBox.Show("Record is Saved of Movie");
            }
            else {
                MessageBox.Show("FIll all the Video Properly to save the Record of the Video for Rent");
            }
            clearAll();
        }
    }
}
