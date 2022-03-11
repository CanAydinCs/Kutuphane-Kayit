using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace Kütüphane_Otomasyonu.Sayfalar
{
    public partial class Studentinformation : Form
    {
        public Studentinformation()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Kitaplar.mdb");

        private void Studentinformation_Load(object sender, EventArgs e)
        {
            StreamReader oku = new StreamReader(Application.StartupPath + "\\takip.txt");
            int deger = Convert.ToInt32(oku.ReadLine());
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
                    label5.Text = ok["Alan_Öğrencinin_Adı_Soyadı"].ToString();
                    label6.Text = ok["Alan_Öğrencinin_Numarası"].ToString();
                    label7.Text = ok["Alan_Öğrencinin_Sınıfı"].ToString();
                }
                say++;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Boss x = new Boss();
            x.Show();
            this.Hide();
        }

        private void Studentinformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
