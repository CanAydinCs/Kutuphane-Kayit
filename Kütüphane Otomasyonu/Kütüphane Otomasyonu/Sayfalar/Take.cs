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
    public partial class Take : Form
    {
        public Take()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Kitaplar.mdb");
        int deg = 0;
        List<string> classlar = new List<string>();

        private void Take_Load(object sender, EventArgs e)
        {
            StreamReader ok = new StreamReader(Application.StartupPath + "\\al.txt");
            deg = Convert.ToInt32(ok.ReadLine());
            ok.Close();

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from KitapListesi";
            OleDbDataReader oku = komut.ExecuteReader();
            int say = 0;
            while (oku.Read())
            {
                if (say == deg)
                {
                    lblKitapAdi.Text = oku["Kitap_Adi"].ToString();
                }
                say++;
            }
            baglanti.Close();

            int sayac = 0;
            int sinif = 9;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (sayac)
                    {
                        case 0:
                            classlar.Add(sinif + "-A");
                            comboBox1.Items.Add(sinif + "-A"); break;
                        case 1:
                            classlar.Add(sinif + "-B");
                            comboBox1.Items.Add(sinif + "-B"); break;
                        case 2:
                            classlar.Add(sinif + "-C");
                            comboBox1.Items.Add(sinif + "-C"); break;
                        case 3:
                            classlar.Add(sinif + "-D");
                            comboBox1.Items.Add(sinif + "-D"); break;
                    }
                    sayac++;
                }
                sinif++;
                sayac = 0;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Student x = new Student();
            x.Show();
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int say = 1;
            foreach (string i in classlar)
            {
                if (comboBox1.Text == i)
                {
                    if (textBox1.Text != "" && comboBox1.Text != "" && numericUpDown1.Value != 0)
                    {
                        baglanti.Open();
                        OleDbCommand komut = new OleDbCommand();
                        komut.Connection = baglanti;
                        int deger = Convert.ToInt32(numericUpDown1.Value);
                        komut.CommandText = $"update KitapListesi set Alindi='Evet',Alan_Öğrencinin_Adı_Soyadı='{textBox1.Text}',Alan_Öğrencinin_Numarası='{deger}',Alan_Öğrencinin_Sınıfı='{comboBox1.Text}' where id={deg}";
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kayıt Başarılı. Ana sayfaya yönlendiriliyorsunuz...");
                        Student x = new Student();
                        x.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Gerekli Alanlar Boş Bırakılmamalıdır!");
                    }
                    break;
                }

                if(say == classlar.Count)
                {
                    MessageBox.Show("Sınıf Kutucuğu Belirtilen Değerlerde Olup Elle Giriş Yapılmamalıdır. Tüm Kayıtlı Sınıfları Görmek İçin Kutucuğun Yanındaki Ok'a tıklayınız.");
                }
                say++;
            }
        }

        private void Take_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
