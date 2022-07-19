using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridview
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ConnectDb Q = new ConnectDb();
        private SqlCommand command = new SqlCommand();
        private string sql;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT*FROM tbEmployees";
            Q.Query(sql);

            dtgEmp.DataSource = Q.ds.Tables["data"];
            HeaderDtg();
        }

        private void HeaderDtg()
        {
            dtgEmp.Columns["emID"].HeaderText = "ລະຫັດ";
            dtgEmp.Columns["emFirstName"].HeaderText = "ຊື່";
            dtgEmp.Columns["emLastName"].HeaderText = "ນາມສະກຸນ";
            dtgEmp.Columns["birthDay"].HeaderText = "ວັນເກີດ";
            dtgEmp.Columns["emPhone"].HeaderText = "ເບີໂທ";
            dtgEmp.Columns["email"].HeaderText = "ອີເມວ";
            dtgEmp.Columns["startWork"].HeaderText = "ວັນເລີ່ມວຽກ";
            dtgEmp.Columns["position"].HeaderText = "ຕໍາແໜ່ງ";
            dtgEmp.Columns["emAddress"].HeaderText = "ທີ່ຢູ່";
        }

        private void dtgEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow Row = dtgEmp.Rows[e.RowIndex];
                txtID.Text = Row.Cells[0].Value.ToString();
                txtName.Text = Row.Cells[1].Value.ToString();
                txtLast.Text = Row.Cells[2].Value.ToString();
                txtBirth.Text = Row.Cells[3].Value.ToString();
                txtPhone.Text = Row.Cells[4].Value.ToString();
                txtEmail.Text = Row.Cells[5].Value.ToString();
                txtStart.Text = Row.Cells[6].Value.ToString();
                txtPosition.Text = Row.Cells[7].Value.ToString();
                txtAddress.Text = Row.Cells[8].Value.ToString();
            }
        }

        private void Insert()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("ຜິດພາດ", "ຜົນລັບ");
                return;
            }
            Q.Conn.Open();
            sql = @"INSERT INTO tbEmployees(emFirstname, emLastname, emPhone, email, birthDay, emAddress, position, startWork, photo)
                    VALUES(@Name, @Last, @phone, @mail, @Bd, N'@Addre', @position, @Stwork)";
            command.Parameters.Clear();
            command.CommandText = sql;
            command.Parameters.AddWithValue("Name", txtName.Text);
            command.Parameters.AddWithValue("Last", txtLast.Text);
            command.Parameters.AddWithValue("phone", txtPhone.Text);
            command.Parameters.AddWithValue("mail", txtEmail.Text);
            command.Parameters.AddWithValue("Bd", txtBirth);
            command.Parameters.AddWithValue("Addre", txtAddress.Text);
            command.Parameters.AddWithValue("position", txtPosition.Text);
            command.Parameters.AddWithValue("Stwork", txtStart);
            //command.Parameters.AddWithValue("pic", ConPictoAr(picbox.Image));

            int result = command.ExecuteNonQuery();
            if (result == -1)
            {
                MessageBox.Show("ເກີດຂໍ້ຜິດພາດ", "ຜົນລັບ");
                ClearText();
            }
            else
            {
                MessageBox.Show("ບັນທຶກຂໍ້ມູນສຳເລັດ", "ຜົນລັບ");
            }
        }
        private void ClearText()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtLast.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtBirth.Text = "";
            txtAddress.Text = "";
            txtPosition.Text = "";
            txtStart.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            ClearText();
        }

        private void UpdateDb()
        {
            sql = @"UPDATE tbEmployees SET emFirstname = @name, emLastname = @last, emPhone = @phone, 
            email = @mail, birthDay = @bd, emAddress = @Addr, position = @posit, startWork = @start
            where emID = @ID";

            command.Parameters.Clear();
            command.CommandText = sql;
            command.Parameters.AddWithValue("ID", txtID.Text);
            command.Parameters.AddWithValue("name", txtName.Text);
            command.Parameters.AddWithValue("last", txtLast.Text);
            command.Parameters.AddWithValue("phone", txtPhone.Text);
            command.Parameters.AddWithValue("mail", txtEmail.Text);
            command.Parameters.AddWithValue("bd", txtBirth);
            command.Parameters.AddWithValue("Addr", txtAddress.Text);
            command.Parameters.AddWithValue("posit", txtPosition.Text);
            command.Parameters.AddWithValue("start", txtStart);
            //command.Parameters.AddWithValue("pic", ConPictoAr(picbox.Image));



            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("ບັນທຶກການປ່ຽນແປງ", "ຜົນລັບ");

            }
            else
            {
                MessageBox.Show("ເກີດຂໍ້ຜິດພາດ", "ຜົນລັບ");
                ClearText();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtID.Text == "")
            {
                Insert();
            }
            else
            {
                UpdateDb();
            }
        }
    }
}
