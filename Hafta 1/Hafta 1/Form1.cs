using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hafta_1
{
    public partial class Form1 : Form
    {
        private DateTime baslangicZaman;   // başlangıç zamanı
        private DateTime hedefZaman;       // sistem saati
        private bool ileriSayim = true;    // İlk aşama ileri sayım
        private bool ustDurdu = false;     // üst durdu mu?
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            hedefZaman = DateTime.Now;

            baslangicZaman = new DateTime(
                hedefZaman.Year, hedefZaman.Month, hedefZaman.Day,
                19, 34, 40, 0 //şuana göre ayarlamak istersen burdan değişcez
            );

            GuncelleLabel(baslangicZaman, label1, label2, label3, label4);

            label5.Text = "00";
            label6.Text = "00";
            label7.Text = "00";
            label8.Text = "00";

            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ustDurdu) return;
            ileriSayim = false;
            timer1.Start();
            button1.Enabled = false;
        }
        private void GuncelleLabel(DateTime zaman, Label lSaat, Label lDakika, Label lSaniye, Label lSalise)
        {
            lSaat.Text = zaman.Hour.ToString("00");
            lDakika.Text = zaman.Minute.ToString("00");
            lSaniye.Text = zaman.Second.ToString("00");
            lSalise.Text = (zaman.Millisecond / 10).ToString("00");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ileriSayim && !ustDurdu)
            {
                // Üst zamanı ileriye at
                baslangicZaman = baslangicZaman.AddMilliseconds(10);
                GuncelleLabel(baslangicZaman, label1, label2, label3, label4);

                // Sistem saatine gelince
                if (baslangicZaman.Hour == hedefZaman.Hour &&
                baslangicZaman.Minute == hedefZaman.Minute &&
                    baslangicZaman.Second == hedefZaman.Second)
                {
                    ustDurdu = true;        // Üst sabitlenecek
                    ileriSayim = false;     // Geri sayım moduna hazır
                    timer1.Stop();

                    // Hem üst hem alt aynı olacak
                    GuncelleLabel(baslangicZaman, label5, label6, label7, label8);

                    // Buton aktif olsun
                    button1.Enabled = true;
                }
            }
            else if (!ileriSayim)
            {
                // Geri sayım aktifse alt geri gitsin
                baslangicZaman = baslangicZaman.AddMilliseconds(-10);
                GuncelleLabel(baslangicZaman, label5, label6, label7, label8);

                // Sıfıra gelince dursun
                if (baslangicZaman.Hour == 0 &&
                baslangicZaman.Minute == 0 &&
                baslangicZaman.Second == 0 &&
                    baslangicZaman.Millisecond == 0)
                {
                    timer1.Stop();
                    MessageBox.Show("Süre bitti!");
                }
            }
        }
    }
}
