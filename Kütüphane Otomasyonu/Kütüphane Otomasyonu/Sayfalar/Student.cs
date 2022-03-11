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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Kitaplar.mdb");
        int id;

        void IdUpdate()
        {
            //id güncelleme
            id = -1;
            listView1.Items.Clear();
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

        private void Veriler()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from KitapListesi";
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem li = new ListViewItem();
                li.Text = oku["id"].ToString();
                li.SubItems.Add(oku["Kitap_Adi"].ToString());
                li.SubItems.Add(oku["Yazar"].ToString());
                li.SubItems.Add(oku["Sayfa_Sayisi"].ToString());
                li.SubItems.Add(oku["Alindi"].ToString());
                if (oku["Alindi"].ToString() == "Evet")
                {
                    li.SubItems.Add(oku["Alan_Öğrencinin_Adı_Soyadı"].ToString());
                }
                else
                {
                    li.SubItems.Add("-");
                }
                listView1.Items.Add(li);
            }
            baglanti.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Veriler();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //kitap alma işlemleri
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from KitapListesi";
            OleDbDataReader oku = komut.ExecuteReader();
            int deger = Convert.ToInt32(numericUpDown1.Value);
            int say = 0;
            while (oku.Read())
            {
                if(say == deger)
                {
                    if(oku["Alindi"].ToString() == "Hayır")
                    {
                        StreamWriter yaz = new StreamWriter(Application.StartupPath + "\\al.txt");
                        yaz.WriteLine(deger.ToString());
                        yaz.Close();
                        Take x = new Take();
                        x.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bu kitap zaten başka bir öğrenci tarafından alınmış durumda.");
                    }
                }
                say++;
            }
            baglanti.Close();
        }

        private void Student_Load(object sender, EventArgs e)
        {
            IdUpdate();
            numericUpDown1.Maximum = id;
            numericUpDown2.Maximum = id;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //kitap iade için kontrol işlemleri
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from KitapListesi";
            OleDbDataReader oku = komut.ExecuteReader();
            int say = 0; 
            while (oku.Read())
            {
                if (say == Convert.ToInt32(numericUpDown2.Value))
                {
                    if(oku["Alindi"].ToString() == "Evet")
                    {
                        StreamWriter yaz = new StreamWriter(Application.StartupPath + "\\takip.txt");
                        yaz.WriteLine(say.ToString());
                        yaz.Close();

                        Return x = new Return();
                        x.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bu Kitap Daha Birisi Tarafından Alınmamış!");
                    }
                    break;
                }
                say++;
            }
            baglanti.Close();
        }

        private void Student_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form1 x = new Form1();
            x.Show();
            this.Hide();
        }
    }
}
