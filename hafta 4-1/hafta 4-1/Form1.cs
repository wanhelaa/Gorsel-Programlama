using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hafta_4_1
{
    public partial class Form1 : Form
    {

        private Button spawnButton; 
        private Random rnd = new Random();
        private int puan = 0;
        private Rectangle kirmiziAlan; //  kırmızı bölge 
        private int spawnSayi; // Butonun üstündeki sayı
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            label1.Text = "Puan: 0";
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Location = new Point(10, 10);

           
            timer1.Interval = 1000; // Her saniye spawn
            timer1.Tick += timer1_Tick;
            timer1.Start();

            
            spawnButton = new Button();
            spawnButton.Size = new Size(50, 50); 
            spawnButton.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            spawnButton.Click += SpawnButton_Click;
            Controls.Add(spawnButton);

            
            int alanBoyut = 200;
            kirmiziAlan = new Rectangle(
                (ClientSize.Width - alanBoyut) / 2,
                (ClientSize.Height - alanBoyut) / 2,
                alanBoyut,
                alanBoyut
            );

            
            this.Resize += (s, ev) =>
            {
                kirmiziAlan = new Rectangle(
                    (ClientSize.Width - alanBoyut) / 2,
                    (ClientSize.Height - alanBoyut) / 2,
                    alanBoyut,
                    alanBoyut
                );
            };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {// Her tick'te buton rastgele konumda spawn olur
            int x = rnd.Next(0, ClientSize.Width - spawnButton.Width);
            int y = rnd.Next(40, ClientSize.Height - spawnButton.Height);

            spawnButton.Location = new Point(x, y);

        
            spawnSayi = rnd.Next(1, 10);
            spawnButton.Text = spawnSayi.ToString();

            
            if (kirmiziAlan.IntersectsWith(spawnButton.Bounds))
            {
                spawnButton.ForeColor = Color.Red;
            }
            else
            {
                spawnButton.ForeColor = Color.Black;
            }
        }

        private void SpawnButton_Click(object sender, EventArgs e)
        {
            if (spawnButton.ForeColor == Color.Red)
            {
                // Kırmızıysa puan artar
                puan += spawnSayi;
            }
            else
            {
                // Siyahsa puan azalır
                puan -= spawnSayi;
            }

            label1.Text = $"Puan: {puan}";

            // Kazanma
            if (puan >= 100)
            {
                timer1.Stop();
                MessageBox.Show("Kazandın! 🎉", "Tebrikler");
            }
        }

    }
}
