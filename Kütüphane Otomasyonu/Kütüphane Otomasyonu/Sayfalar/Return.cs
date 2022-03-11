using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace Kütüphane_Otomasyonu.Sayfalar
{
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        int deger, studentno;
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Kitaplar.mdb");


        private void Return_Load(object sender, EventArgs e)
        {
            StreamReader oku = new StreamReader(Application.StartupPath + "\\takip.txt");
            deger = Convert.ToInt32(oku.ReadLine());
            oku.Close();

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from KitapListesi";
            OleDbDataReader ok = komut.ExecuteReader();
            int say = 0;
            while (ok.Read())
            {
                if(say == deger)
                {
                    studentno = Convert.ToInt32(ok["Alan_Öğrencinin_Numarası"]);
                    break;
                }
                say++;
            }
            baglanti.Close();
        }

        private void Return_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Student x = new Student();
            x.Show();
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(studentno == Convert.ToInt32(numericUpDown1.Value))
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = baglanti;
                komut.CommandText = $"update KitapListesi set Alindi='Hayır',Alan_Öğrencinin_Adı_Soyadı='-',Alan_Öğrencinin_Numarası='-',Alan_Öğrencinin_Sınıfı='-' where id={deger}";
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Düzenleme Başarılı! Ana Sayfaya Yönlendiriliyorsunuz!");
                Student x = new Student();
                x.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Girilen Öğrenci Numarası Veri Tabanınkiyle Uyuşmuyor. Tekrar Deneyiniz.");
            }
        }
    }
}
