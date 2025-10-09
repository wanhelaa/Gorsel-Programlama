using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hafta_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool surukleniyor = false;
        Point baslangicNoktasi = new Point(0, 0);

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Sol tuş ile sürükleme başlasın
            {
                surukleniyor = true;
                baslangicNoktasi = e.Location; // tıklandığı noktayı kaydet
            }
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (surukleniyor)
            {
                button1.Left += e.X - baslangicNoktasi.X;
                button1.Top += e.Y - baslangicNoktasi.Y;
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            surukleniyor = false; // sürükleme bitti
        }
    }
}
