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
    public partial class GirisPaneli : Form
    {
        public GirisPaneli()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=Kutuphane.accdb");
        private void GirisPaneli_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from Kullanici where kullanici_adi=@p1 AND sifre=@P2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            OleDbDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                Anasayfa form = new Anasayfa();
                form.Show();
                this.Hide();
            }
        }
    }
}
