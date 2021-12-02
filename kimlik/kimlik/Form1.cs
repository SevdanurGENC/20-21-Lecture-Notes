using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace kimlik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbCommand komut;
        OleDbConnection baglanti;
        OleDbDataAdapter da;
        DataSet ds;
        OleDbDataReader dr;
        OleDbCommandBuilder cb;
        DataTable dt;
        void datadoldur()
        {
            baglanti = new OleDbConnection(" Provider = Microsoft.ACE.Oledb.12.0; Data Source = MEMUR.accdb ");
              da = new OleDbDataAdapter("select * from TABLO ", baglanti);
            ds = new DataSet();
            baglanti.Open();
            da = new OleDbDataAdapter("select * from TABLO ", baglanti);
            DataTable tablo = new DataTable();
           da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e) //TABLOYU CAGIRMA 
        {

            baglanti = new OleDbConnection(" Provider = Microsoft.ACE.Oledb.12.0; Data Source = MEMUR.accdb ");
            da = new OleDbDataAdapter("select * from TABLO ", baglanti);
            ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "TABLO");
            dataGridView1.DataSource = ds.Tables["TABLO"];
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)//EKLEME
        {
            string sorgu = "INSERT INTO TABLO (AD,SOYAD,UNVAN ,KURUM ) values (@ad,@soyad,@unvan,@kurum)";
            komut = new OleDbCommand(sorgu, baglanti);

            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            komut.Parameters.AddWithValue("@soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@unvan", textBox3.Text);
            komut.Parameters.AddWithValue("@kurum", textBox4.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            datadoldur();
        }

        private void button4_Click(object sender, EventArgs e)// SILME 
        {
            string sorgu = "DELETE FROM TABLO WHERE SICIL=@sicil";
            komut = new OleDbCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@sicil", dataGridView1.CurrentRow.Cells[4].Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            datadoldur();
        }

        private void button5_Click(object sender, EventArgs e)//GUNCELLEME
        {
             String sorgu = "UPDATE TABLO Set AD=@ad,SOYAD=@soyad,UNAN=@unvan,KURUM,@kurum";
             komut = new OleDbCommand(sorgu, baglanti);
           
            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            komut.Parameters.AddWithValue("@soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@unvan", textBox3.Text);
            komut.Parameters.AddWithValue("@kurum", textBox4.Text);
           baglanti.Open();
           komut.ExecuteNonQuery();
           baglanti.Close();
             datadoldur();

           /// OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            //cb.GetUpdateCommand();
            ///da.Update(tablo);
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            // textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }




        private void button7_Click(object sender, EventArgs e)
        {


        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png |  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            textBox3.Text = dosyayolu;
            pictureBox1.ImageLocation = dosyayolu;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PrintDialog yazıcı = new PrintDialog();
            yazıcı.ShowDialog();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "Ad Like '" + textBox6.Text + "%'";
            dv.RowFilter = string.Format("Ad LIKE '{0}%'", textBox6.Text);
            dataGridView1.DataSource = dv;
        }
    }
}
