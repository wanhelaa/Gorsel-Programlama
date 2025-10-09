using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Haftaa_3
{
    public partial class Form1 : Form
    {
        Button btn1, btn2, btn3, btn4;
        Timer hareketTimer;

        // Hareket yönü: true = köşelere doğru, false = merkeze doğru
        bool aciliyor = true;
        int hiz = 4;
        int centerX, centerY; //konumu başta alıom
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // formun genişlik uzunluğunu rengini ayarladım
            this.Width = 800;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Butonlarım
            btn1 = new Button() { Text = "1.", Width = 50, Height = 50, BackColor = Color.Pink };
            btn2 = new Button() { Text = "2.", Width = 50, Height = 50, BackColor = Color.Pink };
            btn3 = new Button() { Text = "3.", Width = 50, Height = 50, BackColor = Color.Pink };
            btn4 = new Button() { Text = "4.", Width = 50, Height = 50, BackColor = Color.Pink };

            centerX = this.ClientSize.Width / 2 - btn1.Width / 2;  // Merkez koordinatları
            centerY = this.ClientSize.Height / 2 - btn1.Height / 2;

            // Başlangıçta hepsini merkeze koyalım
            btn1.Location = new Point(centerX, centerY);
            btn2.Location = new Point(centerX, centerY);
            btn3.Location = new Point(centerX, centerY);
            btn4.Location = new Point(centerX, centerY);

            // Forma ekleyelim
            Controls.Add(btn1);
            Controls.Add(btn2);
            Controls.Add(btn3);
            Controls.Add(btn4);

            // Timer olsun
            hareketTimer = new Timer();
            hareketTimer.Interval = 10;
            hareketTimer.Tick += HareketTimer_Tick;
            hareketTimer.Start();
        }

        private void HareketTimer_Tick(object sender, EventArgs e)
        {
            int mesafe = 200; // butonların ne kadar uzaklaşacağı 

            if (aciliyor) //açılıosa
            {
                // Dışa 
                btn1.Left -= hiz; // sol
                btn1.Top -= hiz;  // yukarı

                btn2.Left += hiz; // sağ
                btn2.Top -= hiz;  // yukarı

                btn3.Left -= hiz;
                btn3.Top += hiz;

                btn4.Left += hiz;
                btn4.Top += hiz;

                // Ne kadar açıldık?
                if (Math.Abs(btn1.Left - centerX) >= mesafe)
                    aciliyor = false; // fazla uzaklaştıysa geri gel
            }
            else
            {
                // Geri merkeze dön
                if (btn1.Left < centerX) btn1.Left += hiz;
                if (btn1.Top < centerY) btn1.Top += hiz;

                if (btn2.Left > centerX) btn2.Left -= hiz;
                if (btn2.Top < centerY) btn2.Top += hiz;

                if (btn3.Left < centerX) btn3.Left += hiz;
                if (btn3.Top > centerY) btn3.Top -= hiz;

                if (btn4.Left > centerX) btn4.Left -= hiz;
                if (btn4.Top > centerY) btn4.Top -= hiz;

                // hepsi merkeze döndü mü?
                if (Math.Abs(btn1.Left - centerX) <= 2 && Math.Abs(btn1.Top - centerY) <= 2)
                    aciliyor = true; // tekrar açıl
            }
        }
    }
}