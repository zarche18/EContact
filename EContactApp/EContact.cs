using EContactApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContactApp
{
    public partial class formEContact : Form
    {
        public formEContact()
        {
            InitializeComponent();
        }
        EContactModel model = new EContactModel();
        private void formEContact_Load(object sender, EventArgs e)
        {
            // Load data into Grid
            DataTable dt = model.Select();
            grdList.DataSource = dt;
        }       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get value from the input field
            model.FirstName = txtFirstName.Text;
            model.LastName = txtLastName.Text;
            model.ContactNo = txtContactNo.Text;
            model.Address = txtAddress.Text;
            model.Gender = cboGendar.Text;

            bool success = model.Insert(model);
            if(success)
            {
                MessageBox.Show("Insert new contact success!");
                Clear();
            }
            else
            {
                MessageBox.Show("Insert new contact failed!");
            }

            // Load data into Grid
            DataTable dt = model.Select();
            grdList.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get value from the input field
            model.ContactID = Convert.ToInt32(txtContactID.Text);
            model.FirstName = txtFirstName.Text;
            model.LastName = txtLastName.Text;
            model.ContactNo = txtContactNo.Text;
            model.Address = txtAddress.Text;
            model.Gender = cboGendar.Text;
            bool success = model.Update(model);
            if (success)
            {
                MessageBox.Show("Update contact data success!");
                Clear();
            }
            else
            {
                MessageBox.Show("Update contact data failed!");
            }

            // Load data into Grid
            DataTable dt = model.Select();
            grdList.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32(txtContactID.Text);
            bool success = model.Delete(contactID);
            if(success)
            {
                MessageBox.Show("Delete contact data success!");
                DataTable dt = model.Select();
                grdList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Delete contact data failed!");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void pictureExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Clear()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            cboGendar.Text = string.Empty;
            txtContactID.Text = string.Empty;
        }

        private void grdList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = e.RowIndex;
            txtContactID.Text = grdList.Rows[index].Cells[0].Value.ToString();
            txtFirstName.Text = grdList.Rows[index].Cells[1].Value.ToString();
            txtLastName.Text = grdList.Rows[index].Cells[2].Value.ToString();
            txtContactNo.Text = grdList.Rows[index].Cells[3].Value.ToString();
            txtAddress.Text = grdList.Rows[index].Cells[4].Value.ToString();
            cboGendar.Text = grdList.Rows[index].Cells[5].Value.ToString();
        }
        static string myconnectionstring = ConfigurationManager.ConnectionStrings["MyconnectionString"].ConnectionString;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            // step 1: Create connection string
            SqlConnection conn = new SqlConnection(myconnectionstring);
            // step 2: Create adapter with query
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM EContact WHERE Firstname like '%"+searchText+ "%' OR LastName like '%" + searchText + "%' OR Address like '%" + searchText + "%'",conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            grdList.DataSource = dt;
        }
    }
}
