using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EContactApp.Model
{
    public class EContactModel
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender  { get; set; }
        static string myConnectionstring = ConfigurationManager.ConnectionStrings["MyconnectionString"].ConnectionString;

        //Select data from database
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myConnectionstring);
            DataTable dt = new DataTable();

            try
            {
                // Step 1: Writing SQL Query
                string sql = "Select * From EContact";
                // Step 2: Creating cmd using SQL and conn
                SqlCommand cmd = new SqlCommand(sql,conn);
                // Step 3: Creating SQL Adapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Step 4: open connection
                conn.Open();
                // Step 5: Fill the data to adapter
                adapter.Fill(dt);
            }
            catch
            {

            }
            finally
            {
                // Step 6: close connnection
                conn.Close();
            }
            return dt;
        }

        //Insert data into database
        public bool Insert(EContactModel contactModel)
        {
            bool isSuccess = false;

            // Step 1: Connect Database
            SqlConnection conn = new SqlConnection(myConnectionstring);
            try
            {
                // Step 2: Create a SQL Query to insert Data
                string sqlquery = "INSERT INTO EContact (FirstName,LastName,ContactNo,Address,Gender)Values(@FirstName,@LastName,@ContactNo,@Address,@Gender)";

                //Step 2: Create SQL Command using sql and conn
                SqlCommand command = new SqlCommand(sqlquery,conn);
                // Step 3: Creaet Parameters to add data
                command.Parameters.AddWithValue("@FirstName", contactModel.FirstName);
                command.Parameters.AddWithValue("@LastName", contactModel.LastName);
                command.Parameters.AddWithValue("@ContactNo", contactModel.ContactNo);
                command.Parameters.AddWithValue("@Address", contactModel.Address);
                command.Parameters.AddWithValue("@Gender", contactModel.Gender);
                // Step 4: Connection open 
                conn.Open();
                //If the query is success then the value will be greater than 0.
                int row = command.ExecuteNonQuery();
                if(row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                //step 5: close connection
                conn.Close();
            }
            finally
            {

            }

            return isSuccess;
        }

        //Update data into database
        public bool Update(EContactModel contactModel)
        {
            bool isSuccess = false;
            //step 1: Connect Database
            SqlConnection conn = new SqlConnection(myConnectionstring);
            try
            {
                //step 2: Create a SQL Query to update Data
                string sqlQuery = "UPDATE EContact SET FirstName= @FirstName,LastName = @LastName, ContactNo = @ContactNo, Address =@Address, Gender = @Gender WHERE ContactID = @ContactID";
                //step 3: Create SQL Command using sql and conn
                SqlCommand command = new SqlCommand(sqlQuery,conn);
                //step 4: Creaet Parameters to add data
                command.Parameters.AddWithValue("@ContactID", contactModel.ContactID);
                command.Parameters.AddWithValue("@FirstName", contactModel.FirstName);
                command.Parameters.AddWithValue("@LastName", contactModel.LastName);
                command.Parameters.AddWithValue("@ContactNo", contactModel.ContactNo);
                command.Parameters.AddWithValue("@Address", contactModel.Address);
                command.Parameters.AddWithValue("@Gender", contactModel.Gender);
                //step 5: Open Connection
                conn.Open();
                int rowNum = command.ExecuteNonQuery();

                if(rowNum > 0)
                { isSuccess = true; }
                else
                { isSuccess = false; }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                //step 6:  Close Connection
                conn.Close();
            }
            return isSuccess;
        }

        //Method to data data from database
        public bool Delete (int id)
        {
            bool isSuccess = false;
            //Step 1: Connect Database
            SqlConnection conn = new SqlConnection(myConnectionstring);
            try
            {
                //Step 2: Create a SQL Query to delete Data
                string sqlQuery = "DELETE EContact WHERE ContactID = @ContactID";
                //Step 3: Create SQL Command using sql and conn
                SqlCommand command = new SqlCommand(sqlQuery,conn);
                //Step 4: Creaet Parameters to add data
                command.Parameters.AddWithValue("@ContactID",id);
                //Step 5: Connection open 
                conn.Open();
                //If the query is success then the value will be greater than 0.
                int row = command.ExecuteNonQuery();
                if(row > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch
            {

            }
            finally
            {
                //Step 1: Connect Database
                conn.Close();
            }
            return isSuccess;
        }
    }    
}
