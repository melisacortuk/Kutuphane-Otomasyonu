using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace KutuphaneOtomasyonu
{
    public partial class OkuyucuListesi : Form
    {
        public OkuyucuListesi()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=Kutuphane.accdb");
        private void button3_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OkuyucuListesi_Load(object sender, EventArgs e)
        {
                DataTable dt = new DataTable();
                baglanti.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT TcKimlik,AdiSoyadi,DogumTarihi,DogumYeri,Telefon,Eposta,UyelikTarihi,Cinsiyet,Adres from OkuyucuKayit", baglanti);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
        }
    }
}
