using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace pertemuan14.Controller
{
    internal class MahasiswaController:Model.Connection
    {
        public DataTable selectStudent(MySqlCommand cmd)
        {
            DataTable date = new DataTable();
            try
            {
                string tampil = "SELECT * FROM mahasiswa";
                da = new MySqlConnector.MySqlDataAdapter(tampil, GetConn());
                da.Fill(date);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return date;
        }

        public void insertStudent(string nim, string name, DateTime tanggalLahir, byte[] photo)
        {
            string add = "INSERT INTO mahasiswa VALUES (" + "@NIM,@Nama,@Tanggal_lahir,@Photo)";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(add, GetConn());
                cmd.Parameters.Add("@NIM", MySqlConnector.MySqlDbType.VarChar).Value = nim;
                cmd.Parameters.Add("@Nama", MySqlConnector.MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@Tanggal_lahir", MySqlConnector.MySqlDbType.Date).Value = tanggalLahir;
                cmd.Parameters.Add("@Photo", MySqlConnector.MySqlDbType.Blob).Value = photo;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Add Date Failed!!" + ex.Message);
            }
        }

        public void deleteStudent(string NIM)
        {
            string delete = "DELETE FROM mahasiswa WHERE NIM=" + NIM;
            try
            {
                cmd = new MySqlConnector.MySqlCommand(delete, GetConn());
                cmd.Parameters.Add("@NIM", MySqlConnector.MySqlDbType.VarChar).Value = NIM;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete failed!!" + ex.Message);
            }
        }
    }
}
