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
    public partial class Boss : Form
    {
        public Boss()
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

        private void Button1_Click(object sender, EventArgs e)
        {
            Kitap_Ekle x = new Kitap_Ekle();
            x.Show();
            this.Hide();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            Veriler();
        }

        private void Boss_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value != -1)
            {
                listView1.Items.Clear();

                int deger = Convert.ToInt32(numericUpDown1.Value);

                //numericupdown için yeni üst limiti ayarlama
                numericUpDown1.Maximum -= 1;

                //listedeki kitapları yeniden numaralandırma (eğer ortadan kitap silinirse diye.)
                List<string> kitap_adi = new List<string>();
                List<string> yazar = new List<string>();
                List<string> sayfa_sayisi = new List<string>();
                List<string> alindi = new List<string>();
                List<string> ad_soyad = new List<string>();
                List<string> numarası = new List<string>();
                List<string> sinifi = new List<string>();
                int say = id;

                baglanti.Open();
                OleDbCommand kom = new OleDbCommand();
                kom.Connection = baglanti;
                kom.CommandText = "select * from KitapListesi";
                OleDbDataReader oku = kom.ExecuteReader();
                while (oku.Read())
                {
                    kitap_adi.Add(oku["Kitap_Adi"].ToString());
                    yazar.Add(oku["Yazar"].ToString());
                    sayfa_sayisi.Add(oku["Sayfa_Sayisi"].ToString());
                    alindi.Add(oku["Alindi"].ToString());
                    ad_soyad.Add(oku["Alan_Öğrencinin_Adı_Soyadı"].ToString());
                    numarası.Add(oku["Alan_Öğrencinin_Numarası"].ToString());
                    sinifi.Add(oku["Alan_Öğrencinin_Sınıfı"].ToString());
                    say--;
                }
                baglanti.Close();

                kitap_adi.RemoveAt(deger);
                yazar.RemoveAt(deger);
                sayfa_sayisi.RemoveAt(deger);
                ad_soyad.RemoveAt(deger);
                numarası.RemoveAt(deger);
                sinifi.RemoveAt(deger);

                baglanti.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = baglanti;
                for (int i = 0; i <= id; i++)
                {
                    komut.CommandText = "delete from KitapListesi where id=" + i + "";
                    komut.ExecuteNonQuery();
                }
                baglanti.Close();

                baglanti.Open();
                for (int i = 0; i < id; i++)
                {
                    OleDbCommand komu = new OleDbCommand("insert into KitapListesi(id,Kitap_Adi,Yazar,Sayfa_Sayisi,Alindi,Alan_Öğrencinin_Adı_Soyadı,Alan_Öğrencinin_Numarası,Alan_Öğrencinin_Sınıfı) values(" + i + ",'" + kitap_adi[i] + "','" + yazar[i] + "','" + sayfa_sayisi[i] + "','" + alindi[i] + "','" + ad_soyad[i] + "','" + numarası[i] + "','" + sinifi[i] + "')", baglanti);
                    komu.ExecuteNonQuery();
                }
                baglanti.Close();
                IdUpdate();

                Veriler();
            }
            else
            {
                MessageBox.Show("Kitap Listeniz Boş.");
            }
        }

        private void Boss_Load(object sender, EventArgs e)
        {
            IdUpdate();

            //numericupdown için id'ye göre maksimim değeri ayarlama
            numericUpDown1.Maximum = id;
            numericUpDown2.Maximum = id;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from KitapListesi";
            OleDbDataReader oku = komut.ExecuteReader();
            int deger = Convert.ToInt32(numericUpDown2.Value);
            int say = 0;
            while (oku.Read())
            {
                if(say == deger)
                {
                    if (oku["Alindi"].ToString() == "Evet")
                    {
                        StreamWriter id_no = new StreamWriter(Application.StartupPath + "\\takip.txt");
                        id_no.WriteLine(deger.ToString());
                        id_no.Close();
                        Studentinformation x = new Studentinformation();
                        x.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bu kitap hiçbir öğrenci tarafından alınmamış");
                    }
                    break;
                }
                say++;
            }
            baglanti.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form1 x = new Form1();
            x.Show();
            this.Hide();
        }
    }
}
