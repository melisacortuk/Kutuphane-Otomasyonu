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
    public partial class EmanetKayit : Form
    {
        public EmanetKayit()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=Kutuphane.accdb");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                kontrol();
                if (x == true)
                {
                    OleDbCommand ekle = new OleDbCommand("insert into EmanetKayit(TcKimlik,AdiSoyadi,BarkodNo,TeminTarihi,TeslimTarihi,HasarDurumu,EmanetDurumu)" +
                    "values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + dateTimePicker1.Text + "','" + maskedTextBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "')", baglanti);

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
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT TcKimlik,AdiSoyadi,BarkodNo,TeminTarihi,TeslimTarihi,HasarDurumu,EmanetDurumu from EmanetKayit", baglanti);
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
            dateTimePicker1.Text = "";
            maskedTextBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox4.Text = "";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Dikkat Tüm Verileriniz Silinebilir.", MessageBoxButtons.YesNo);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE  FROM  EmanetKayit WHERE BarkodNo='" + textBox4.Text + "'", baglanti);
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

        private void EmanetKayit_Load(object sender, EventArgs e)
        {
            yenile();
            comboBox1.Items.Add("Hasarsız");
            comboBox1.Items.Add("Hasarlı");
            comboBox1.Items.Add("Kayıp");
            comboBox2.Items.Add("Teslim Edildi");
            comboBox2.Items.Add("Teslim Edilmedi");
            comboBox2.Items.Add("Geç Teslim Edecek");
            comboBox2.Items.Add("Geç Teslim Edildi");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand db = new OleDbCommand("UPDATE  EmanetKayit  Set TcKimlik = '" + textBox1.Text + "',AdiSoyadi= '" + textBox2.Text + "',BarkodNo= '" + textBox3.Text + "',TeminTarihi= '" + dateTimePicker1.Text + "' ,TeslimTarihi= '" + maskedTextBox1.Text + "',HasarDurumu= '" + comboBox1.Text + "',EmanetDurumu= '" + comboBox2.Text + "' WHERE BarkodNo='" + textBox4.Text + "'", baglanti);
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
            yenile();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int getir = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[getir].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[getir].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[getir].Cells[3].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[getir].Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[getir].Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[getir].Cells[6].Value.ToString();
            textBox4.Text = dataGridView1.Rows[getir].Cells[2].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    }
