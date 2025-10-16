using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hafta_4_2
{
    public partial class Form1 : Form
    {
        GroupBox oyunAlani;
        Button btnBasla, btnBitir;
        Label lblSure;
        ListBox lstSecilenler;
        List<Button> sayiButonlari = new List<Button>();
        Random rnd = new Random();
        Timer hareketTimer, oyunTimer;
        int sure = 60;
        List<int> dogruSiraliSayilar = new List<int>();
        bool oyunAktif = false;
        public Form1()
        {
            InitializeComponent();
            FormOlustur();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void FormOlustur()
        {
           
            this.Text = "Çift Sayı Sıralama Oyunu";
            this.Size = new Size(800, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

           
            oyunAlani = new GroupBox();
        oyunAlani.Text = "Oyun Alanı";
            oyunAlani.BackColor = Color.LightGray;
            oyunAlani.Location = new Point(150, 30);
        oyunAlani.Size = new Size(400, 300);
            this.Controls.Add(oyunAlani);

       
            btnBasla = new Button();
        btnBasla.Text = "OYUNA BAŞLA";
            btnBasla.Size = new Size(100, 50);
        btnBasla.Location = new Point(30, 80);
        btnBasla.Click += BtnBasla_Click;
            this.Controls.Add(btnBasla);

        
            btnBitir = new Button();
        btnBitir.Text = "OYUNU BİTİR";
            btnBitir.Size = new Size(100, 50);
        btnBitir.Location = new Point(600, 80);
        btnBitir.Click += BtnBitir_Click;
            this.Controls.Add(btnBitir);

       
            lblSure = new Label();
        lblSure.Text = "SÜRE: 60 sn";
            lblSure.Font = new Font("Arial", 12, FontStyle.Bold);
        lblSure.Location = new Point(610, 150);
        lblSure.AutoSize = true;
            this.Controls.Add(lblSure);

          
            lstSecilenler = new ListBox();
        lstSecilenler.Location = new Point(600, 200);
        lstSecilenler.Size = new Size(120, 100);
            this.Controls.Add(lstSecilenler);

           
            hareketTimer = new Timer();
        hareketTimer.Interval = 200;
            hareketTimer.Tick += HareketTimer_Tick;

          
            oyunTimer = new Timer();
        oyunTimer.Interval = 1000;
            oyunTimer.Tick += OyunTimer_Tick;
        }

        void BtnBasla_Click(object sender, EventArgs e)
        {
            OyunuBaslat();
        }

        void OyunuBaslat()
        {
            oyunAktif = true;
            sure = 60;
            lblSure.Text = "SÜRE: 60 sn";
            dogruSiraliSayilar.Clear();
            lstSecilenler.Items.Clear();

            foreach (var b in sayiButonlari)
                oyunAlani.Controls.Remove(b);
            sayiButonlari.Clear();

            for (int i = 0; i < 10; i++)
            {
                int sayi = rnd.Next(1, 101);
                Button btn = new Button();
                btn.Text = sayi.ToString();
                btn.Tag = sayi;
                btn.Size = new Size(50, 30);
                btn.BackColor = Color.White;
                btn.Location = new Point(rnd.Next(20, oyunAlani.Width - 60),
                                         rnd.Next(20, oyunAlani.Height - 60));
                btn.Click += SayiButon_Click;
                oyunAlani.Controls.Add(btn);
                sayiButonlari.Add(btn);
            }

            hareketTimer.Start();
            oyunTimer.Start();
        }

        void SayiButon_Click(object sender, EventArgs e)
        {
            if (!oyunAktif) return;

            Button btn = sender as Button;
            int sayi = (int)btn.Tag;

          
            if (sayi % 2 != 0)
            {
                Kaybettin("Tek sayıya bastın! Kaybettin!");
                return;
            }

            
            if (dogruSiraliSayilar.Count == 0 || sayi > dogruSiraliSayilar.Last())
            {
                dogruSiraliSayilar.Add(sayi);
                btn.Enabled = false;
                btn.Visible = false;

                
                lstSecilenler.Items.Clear();
                foreach (var s in dogruSiraliSayilar.OrderBy(x => x))
                    lstSecilenler.Items.Add(s);
            }
            else
            {
                Kaybettin("Yanlış sırada bastın! Kaybettin!");
                return;
            }

         
            if (dogruSiraliSayilar.Count == 5)
            {
                Kazandin();
            }
        }

        void HareketTimer_Tick(object sender, EventArgs e)
        {
            foreach (var btn in sayiButonlari)
            {
                if (!btn.Visible) continue;
                int dx = rnd.Next(-15, 16);
                int dy = rnd.Next(-15, 16);
                int newX = Math.Max(10, Math.Min(oyunAlani.Width - btn.Width - 10, btn.Left + dx));
                int newY = Math.Max(20, Math.Min(oyunAlani.Height - btn.Height - 10, btn.Top + dy));
                btn.Location = new Point(newX, newY);
            }
        }

        void OyunTimer_Tick(object sender, EventArgs e)
        {
            if (!oyunAktif) return;

            sure--;
            lblSure.Text = $"SÜRE: {sure} sn";

            if (sure <= 0)
            {
                Kaybettin("Süre doldu! Kaybettin!");
            }
        }

        void BtnBitir_Click(object sender, EventArgs e)
        {
            if (!oyunAktif)
            {
                MessageBox.Show("Oyun zaten bitmiş!");
                return;
            }

            if (dogruSiraliSayilar.Count == 5)
                Kazandin();
            else
                Kaybettin("Eksik sayıda bastın, kaybettin!");
        }

        void Kazandin()
        {
            oyunAktif = false;
            hareketTimer.Stop();
            oyunTimer.Stop();
            MessageBox.Show($"{sure} saniye kala oyunu doğru bitirdin!", "Tebrikler!");
        }

        void Kaybettin(string mesaj)
        {
            oyunAktif = false;
            hareketTimer.Stop();
            oyunTimer.Stop();
            MessageBox.Show(mesaj, "Oyun Bitti");
        }
    }
}
