using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane_Otomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Sayfalar.Student x = new Sayfalar.Student();
            x.Show();
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Sayfalar.Kabul x = new Sayfalar.Kabul();
            x.Show();
            this.Hide();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Sayfalar.Boss x = new Sayfalar.Boss();
            x.Show();
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Sayfalar.FastBook x = new Sayfalar.FastBook();
            x.Show();
            this.Hide();
        }
    }
}
