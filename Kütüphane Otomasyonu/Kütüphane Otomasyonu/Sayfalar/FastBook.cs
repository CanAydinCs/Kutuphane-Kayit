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
    public partial class FastBook : Form
    {
        public FastBook()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Kitaplar.mdb");
        int id;

        void IdUpdate()
        {
            id = 0;
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from KitapListesi";
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                id++;
            }
            baglanti.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string[] kitap = richTextBox1.Text.Split(Convert.ToChar("\n"));
            string[] yazar = richTextBox2.Text.Split(Convert.ToChar("\n"));
            string[] sayfa_sayisi = richTextBox3.Text.Split(Convert.ToChar("\n"));

            baglanti.Open();
            OleDbCommand kom;

            for (int i = 0; i < kitap.Length; i++)
            {
                kom = new OleDbCommand("insert into KitapListesi(id,Kitap_Adi,Yazar,Sayfa_Sayisi,Alindi,Alan_Öğrencinin_Adı_Soyadı,Alan_Öğrencinin_Numarası,Alan_Öğrencinin_Sınıfı) values(" + (id + i) + ",'" + kitap[i] + "','" + yazar[i] + "'," + sayfa_sayisi[i] + ",'Hayır','-','-','-')", baglanti);
            }

            kom.ExecuteNonQuery();
            baglanti.Close();
        }

        private void FastBook_Load(object sender, EventArgs e)
        {
            IdUpdate();
        }
    }
}
