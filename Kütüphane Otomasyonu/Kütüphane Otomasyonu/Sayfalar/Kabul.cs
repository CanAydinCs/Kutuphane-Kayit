using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane_Otomasyonu.Sayfalar
{
    public partial class Kabul : Form
    {
        public Kabul()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "1234")
            {
                MessageBox.Show("Doğru Şifre. Sayfaya Yönlendiriliyorsunuz...");
                Boss x = new Boss();
                x.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Şifre!");
            }
        }

        private void Kabul_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form1 x = new Form1();
            x.Show();
            this.Hide();
        }
    }
}
