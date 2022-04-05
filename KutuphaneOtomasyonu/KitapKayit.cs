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
    public partial class KitapKayit : Form
    {
        public KitapKayit()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=Kutuphane.accdb");
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Close();
        }
        public void yenile()
        {
            DataTable dt = new DataTable();
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT BarkodNo,KitapAdi,YazarAdi,YayinEvi,KitapTuru,TeminTarihi,StokSayisi,HasarDurumu from KitapKayit", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        bool x;
        void kontrol()
        {
            baglanti.Open();
            OleDbCommand kullan = new OleDbCommand("SELECT * FROM Kullanici WHERE kullanici_adi=@P1", baglanti);
            kullan.Parameters.AddWithValue("@p1", textBox1.Text);
            OleDbDataReader okut = kullan.ExecuteReader();

            if (okut.Read())
            {
                x = false;
            }
            else
                x = true;
            baglanti.Close();
        }
        public void yoket()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Text = "";
            MiktarNUD.Text = "";
            comboBox2.Text = "";
            textBox5.Text = "";
        }
        private void KitapKayit_Load(object sender, EventArgs e)
        {
            yenile();
            comboBox1.Items.Add("Roman");
            comboBox1.Items.Add("Hikaye");
            comboBox1.Items.Add("Şiir");
            comboBox1.Items.Add("Kişisel Gelişim");
            comboBox1.Items.Add("Biyografi");
            comboBox1.Items.Add("Anı");
            comboBox1.Items.Add("Bilgi");
            comboBox1.Items.Add("Çocuk");
            comboBox1.Items.Add("Gezi");
            comboBox1.Items.Add("Din");
            comboBox2.Items.Add("Hasarsız");
            comboBox2.Items.Add("Hasarlı");
            comboBox2.Items.Add("Kayıp");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Dikkat Tüm Verileriniz Silinebilir.", MessageBoxButtons.YesNo);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE  FROM  KitapKayit WHERE BarkodNo='" + textBox5.Text + "'", baglanti);
                    baglanti.Open();
                    sil.ExecuteNonQuery();
                    baglanti.Close();
                    yenile();
                    MessageBox.Show("Kaydınız Silinmiştir.");

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
            }
            else if (cevap == DialogResult.No)
            {
                MessageBox.Show("İşleminiz İptal Edildi.");
            }
            else
                MessageBox.Show("İşlemi Sonlandırdınız.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand db = new OleDbCommand("UPDATE  KitapKayit  Set BarkodNo = '" + textBox1.Text + "',AdiSoyadi= '" + textBox2.Text + "',YazarAdi= '" + textBox3.Text + "',YayinEvi= '" + textBox4.Text + "' ,KitapTuru= '" + comboBox1.Text + "',TeminTarihi= '" + dateTimePicker1.Text + "',StokSayisi= '" + MiktarNUD.Text + "',HasarDurumu= '" + comboBox2.Text + "' WHERE BarkodNo='" + textBox5.Text + "'", baglanti);
                baglanti.Open();
                db.ExecuteNonQuery();
                baglanti.Close();
                yenile();
                MessageBox.Show("Güncellenmiştir.");

                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (Controls[i] is TextBox) Controls[i].Text = "";
                }
                yoket();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
                baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                kontrol();
                if (x == true)
                {
                    OleDbCommand ekle = new OleDbCommand("insert into KitapKayit(BarkodNo,KitapAdi,YazarAdi,YayinEvi,KitapTuru,TeminTarihi,StokSayisi,HasarDurumu)" +
                    "values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + MiktarNUD.Text + "','" + comboBox2.Text + "')", baglanti);

                    baglanti.Open();
                    ekle.ExecuteNonQuery();
                    baglanti.Close();


                    yenile();
                    MessageBox.Show("Veriler Eklenmiştir.");

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                    yoket();
                }
                else
                    MessageBox.Show("Bu Veri Zaten Kayıtlı.");
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
                baglanti.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int getir = dataGridView1.SelectedCells[0].RowIndex;
            textBox5.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[getir].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[getir].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[getir].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[getir].Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[getir].Cells[5].Value.ToString();
            MiktarNUD.Text = dataGridView1.Rows[getir].Cells[6].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[getir].Cells[7].Value.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }