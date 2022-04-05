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
    public partial class OkuyucuKayit : Form
    {
        public OkuyucuKayit()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=Kutuphane.accdb");
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
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT TcKimlik,AdiSoyadi,DogumTarihi,DogumYeri,Telefon,Eposta,UyelikTarihi,Cinsiyet,Adres from OkuyucuKayit", baglanti);
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
            maskedTextBox1.Text = "";
            textBox3.Text = "";
            maskedTextBox2.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Text = "";
            comboBox2.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                kontrol();
                if (x == true)
                {
                    OleDbCommand ekle = new OleDbCommand("insert into OkuyucuKayit(TcKimlik,AdiSoyadi,DogumTarihi,DogumYeri,Telefon,Eposta,UyelikTarihi,Cinsiyet,Adres)" +
                    "values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + maskedTextBox1.Text + "', '" + textBox3.Text + "','" + maskedTextBox2.Text + "','" + textBox4.Text + "','" + dateTimePicker1.Text + "','" + comboBox2.Text + "','" + textBox5.Text + "')", baglanti);

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

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Dikkat Tüm Verileriniz Silinebilir.", MessageBoxButtons.YesNo);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE  FROM  OkuyucuKayit WHERE TcKimlik='" + textBox6.Text + "'", baglanti);
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
                OleDbCommand db = new OleDbCommand("UPDATE  OkuyucuKayit  Set TcKimlik = '" + textBox1.Text + "',AdiSoyadi= '" + textBox2.Text + "',DogumTarihi= '" + maskedTextBox1.Text + "',DogumYeri= '" + textBox3.Text + "' ,Telefon= '" + maskedTextBox2.Text + "',EPosta= '" + textBox4.Text + "',UyelikTarihi= '" + dateTimePicker1.Text + "',Cinsiyet= '" + comboBox2.Text + "',Adres= '" + textBox5.Text + "' WHERE TcKimlik='" + textBox6.Text + "'", baglanti);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int getir = dataGridView1.SelectedCells[0].RowIndex;
            textBox6.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[getir].Cells[1].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[getir].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[getir].Cells[3].Value.ToString();
            maskedTextBox2.Text = dataGridView1.Rows[getir].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.Rows[getir].Cells[5].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[getir].Cells[6].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[getir].Cells[7].Value.ToString();
            textBox5.Text = dataGridView1.Rows[getir].Cells[8].Value.ToString();
        }

        private void OkuyucuKayit_Load(object sender, EventArgs e)
        {
            yenile();
            comboBox2.Items.Add("Kadın");
            comboBox2.Items.Add("Erkek");
        }
    }
    }