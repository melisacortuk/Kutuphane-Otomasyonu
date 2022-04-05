using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KutuphaneOtomasyonu
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void btnKitapkayit_Click(object sender, EventArgs e)
        {
            KitapKayit ilac = new KitapKayit();
            ilac.Show();
            this.Hide();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            KitapKayit kitapKayit = new KitapKayit();
            kitapKayit.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OkuyucuListesi okuyucuListesi = new OkuyucuListesi();
            okuyucuListesi.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OkuyucuKayit okuyucuKayit = new OkuyucuKayit();
            okuyucuKayit.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EmanetKayit emanetKayit = new EmanetKayit();
            emanetKayit.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KitapListesi kitapListesi = new KitapListesi();
            kitapListesi.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EmanetListesi emanetListesi = new EmanetListesi();
            emanetListesi.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
           
        }
    }
}
