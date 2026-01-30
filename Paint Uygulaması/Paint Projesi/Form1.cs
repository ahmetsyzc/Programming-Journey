using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Ekran titremesini azaltır
            this.Paint += Form1_Paint;
        }

        enum Arac { Kalem, Kare, Daire, Cizgi }
        Arac seciliArac = Arac.Kalem;

        bool ciziliyor = false;
        bool silgiModu = false;

        int baslangicX, baslangicY;
        int kalemKalinlik = 3;
        int silgiKalinlik = 3;

        ColorDialog renkSecici = new ColorDialog();
        Bitmap tuval;
        Graphics cizim;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (tuval != null)
                e.Graphics.DrawImage(tuval, 0, 0);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ciziliyor = true;
            baslangicX = e.X;
            baslangicY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!ciziliyor) return;

            if (seciliArac == Arac.Kalem)
            {
                if (silgiModu)
                {
                    cizim.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    using (Pen p = new Pen(Color.Transparent, silgiKalinlik))
                    {
                        p.StartCap = p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        cizim.DrawLine(p, baslangicX, baslangicY, e.X, e.Y);
                    }
                }
                else
                {
                    cizim.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    using (Pen p = new Pen(renkSecici.Color, kalemKalinlik))
                    {
                        p.StartCap = p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        cizim.DrawLine(p, baslangicX, baslangicY, e.X, e.Y);
                    }
                }

                baslangicX = e.X;
                baslangicY = e.Y;
                this.Invalidate();
            }
            else
            {
                this.Invalidate(); // Önceki önizlemeyi temizle

                using (Graphics g = this.CreateGraphics())
                {
                    Pen p = new Pen(renkSecici.Color, kalemKalinlik);

                    int x = Math.Min(baslangicX, e.X);
                    int y = Math.Min(baslangicY, e.Y);
                    int genislik = Math.Abs(e.X - baslangicX);
                    int yukseklik = Math.Abs(e.Y - baslangicY);

                    if (seciliArac == Arac.Kare)
                        g.DrawRectangle(p, x, y, genislik, yukseklik);
                    else if (seciliArac == Arac.Daire)
                        g.DrawEllipse(p, x, y, genislik, yukseklik);
                    else if (seciliArac == Arac.Cizgi)
                        g.DrawLine(p, baslangicX, baslangicY, e.X, e.Y);
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (ciziliyor && seciliArac != Arac.Kalem)
            {
                using (Pen p = new Pen(renkSecici.Color, kalemKalinlik))
                {
                    int x = Math.Min(baslangicX, e.X);
                    int y = Math.Min(baslangicY, e.Y);
                    int genislik = Math.Abs(e.X - baslangicX);
                    int yukseklik = Math.Abs(e.Y - baslangicY);

                    if (seciliArac == Arac.Kare)
                        cizim.DrawRectangle(p, x, y, genislik, yukseklik);
                    else if (seciliArac == Arac.Daire)
                        cizim.DrawEllipse(p, x, y, genislik, yukseklik);
                    else if (seciliArac == Arac.Cizgi)
                        cizim.DrawLine(p, baslangicX, baslangicY, e.X, e.Y);
                }

                this.Invalidate();
            }

            ciziliyor = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            seciliArac = Arac.Kalem;
            silgiModu = false;
            renkSecici.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            seciliArac = Arac.Kalem;
            silgiModu = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            silgiModu = true;
            seciliArac = Arac.Kalem;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cizim.Clear(Color.Transparent); // Sadece çizimleri siler
            this.Invalidate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            seciliArac = Arac.Kare;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            seciliArac = Arac.Daire;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            seciliArac = Arac.Cizgi;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Dosyası|*.png|JPG Dosyası|*.jpg";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap cikti = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                Graphics g = Graphics.FromImage(cikti);

                g.Clear(this.BackColor);

                // Arka plan resmi varsa onu da çiz
                if (this.BackgroundImage != null)
                    g.DrawImage(this.BackgroundImage, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

                // Çizimleri ekle
                g.DrawImage(tuval, 0, 0);

                cikti.Save(sfd.FileName);
                g.Dispose();
                cikti.Dispose();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            kalemKalinlik = Convert.ToInt32(comboBox1.SelectedItem);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            silgiKalinlik = Convert.ToInt32(comboBox2.SelectedItem);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog arkaPlanRenk = new ColorDialog();
            if (arkaPlanRenk.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = arkaPlanRenk.Color;
                this.BackgroundImage = null; // Sadece arka plan resmini siler
                this.Invalidate();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosyaAc = new OpenFileDialog();
            dosyaAc.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";

            if (dosyaAc.ShowDialog() == DialogResult.OK)
            {
                Image yuklenenResim = Image.FromFile(dosyaAc.FileName);
                this.BackgroundImage = new Bitmap(yuklenenResim, this.ClientSize);
                this.BackgroundImageLayout = ImageLayout.Stretch;
                this.Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            object[] kalinliklar = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29 };
            comboBox1.Items.AddRange(kalinliklar);
            comboBox2.Items.AddRange(kalinliklar);

            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 1;

            tuval = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            cizim = Graphics.FromImage(tuval);
            cizim.Clear(Color.Transparent);
        }
    }
}
