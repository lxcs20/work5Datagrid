using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridview
{
    class ConnectDb
    {
        //string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\AttendanceRecord.mdf;Integrated Security=True;Connect Timeout=30";
        public SqlConnection Conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\AttendanceRecord.mdf;Integrated Security=True;Connect Timeout=30");
        public SqlDataAdapter Adapter;
        public DataSet ds;
        public SqlCommand command;

        public void Query(String sql)
        {

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
            Conn.Open();
            try
            {
                Adapter = new SqlDataAdapter(sql, Conn);
                ds = new DataSet();
                Adapter.Fill(ds, "data");
                Conn.Close();
            }
            catch (Exception)
            {
                
            }
        }
        
    }
}
