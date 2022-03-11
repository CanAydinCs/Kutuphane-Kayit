using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane_Otomasyonu.Sayfalar
{
    public partial class Kitap_Ekle : Form
    {
        public Kitap_Ekle()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Kitaplar.mdb");


        private void Button1_Click(object sender, EventArgs e)
        {
            //id'yi otomatik çekme
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from KitapListesi";
            int id = 0;
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                id++;
            }
            baglanti.Close();

            //kitap kayıt işlemi
            baglanti.Open();
            int sayfa_sayisi = Convert.ToInt32(numericUpDown1.Value);
            OleDbCommand kom = new OleDbCommand("insert into KitapListesi(id,Kitap_Adi,Yazar,Sayfa_Sayisi,Alindi,Alan_Öğrencinin_Adı_Soyadı,Alan_Öğrencinin_Numarası,Alan_Öğrencinin_Sınıfı) values(" + id + ",'" + textBox1.Text + "','" + textBox2.Text + "'," + sayfa_sayisi + ",'Hayır','-','-','-')", baglanti);
            kom.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kayıt Başarılı");

            textBox1.Clear();
            textBox2.Clear();
            numericUpDown1.Value = 0;
        }

        private void Kitap_Ekle_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Boss x = new Boss();
            x.Show();
            this.Hide();
        }
    }
}
