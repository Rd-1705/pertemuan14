using MySqlConnector;
using pertemuan14.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pertemuan14
{
    public partial class Form1 : Form
    {
        private MahasiswaController mahasiswaController = new MahasiswaController();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2TxtNIM.MaxLength = 10;
            guna2Txt_Nama.MaxLength = 25;
            showStudent();
        }

        bool showStudent()
        {
            DataGridView_Mahasiswa.DataSource = mahasiswaController.selectStudent(new MySqlCommand("SELECT * FROM mahasiswa"));
            DataGridView_Mahasiswa.RowTemplate.Height = 80;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_Mahasiswa.Columns[3];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            return true;
        }

        private void guna2Btn_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "select Photo(*.jpg;*.png;*gif)|*.jpg;*.png;*.gif;";

            if(opf.ShowDialog() == DialogResult.OK)
                guna2PictureBox1.Image = Image.FromFile(opf.FileName);
        }

        private void guna2Btn_Clear_Click(object sender, EventArgs e)
        {
            guna2TxtNIM.Clear();
            guna2Txt_Nama.Clear();
            guna2DateTimePicker1.Value = DateTime.Now;
            guna2PictureBox1.Image = null;
        }

        bool verify()
        {
            if ((guna2TxtNIM.Text  == "") || (guna2Txt_Nama.Text == "") || (guna2PictureBox1.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void guna2Btn_Save_Click(object sender, EventArgs e)
        {
            int born_year =  guna2DateTimePicker1.Value.Year;   
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) <= 17 || (this_year - born_year) >= 25)
            {
                MessageBox.Show("Umur harus di antara 17-25 tahun", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    MemoryStream memori = new MemoryStream();
                    guna2PictureBox1.Image.Save(memori, guna2PictureBox1.Image.RawFormat);
                    byte[] img = memori.ToArray();
                    mahasiswaController.insertStudent(guna2TxtNIM.Text, guna2Txt_Nama.Text,
                    guna2DateTimePicker1.Value, img);

                    MessageBox.Show("Penambahan data baru berhasil", "Simpan data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showStudent();
                    guna2TxtNIM.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Inputan kosong", "Tambah data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridView_Mahasiswa_Click(object sender, EventArgs e)
        {  
            guna2TxtNIM.Text = DataGridView_Mahasiswa.CurrentRow.Cells[0].Value.ToString();
            guna2Txt_Nama.Text = DataGridView_Mahasiswa.CurrentRow.Cells[1].Value.ToString();
            guna2DateTimePicker1.Value = (DateTime)DataGridView_Mahasiswa.CurrentRow.Cells[2].Value;
            byte[] img = (byte[])DataGridView_Mahasiswa.CurrentRow.Cells[3].Value;
            MemoryStream gambar = new MemoryStream(img);
            guna2PictureBox1.Image = Image.FromStream(gambar);

        }

        private void guna2Btn_Hapus_Click(object sender, EventArgs e)
        {
            if (verify())
            {
                try
                {
                    mahasiswaController.deleteStudent(guna2TxtNIM.Text);
                    showStudent();
                    guna2Btn_Clear.PerformClick();
                    MessageBox.Show("Hapus data berhasil", "Hapus data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    guna2TxtNIM.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Hapus data error", "Hapus data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }

   

}
