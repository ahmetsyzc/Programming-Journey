using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap_Makinesi_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "9";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "0";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + ",";
        }

        private void bolmebuton_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "/";
        }

        private void carpmabutton_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "*";
        }

        private void cikarmabuton_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "-";
        }

        private void toplamabutton_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "+";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                richTextBox1.Text = richTextBox1.Text.Substring(0, richTextBox1.Text.Length - 1);
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
            }
        }

        private void sonucbuton_Click(object sender, EventArgs e)
        {
            try
            {
                string ifade = richTextBox1.Text.Replace(",", ".");

                DataTable dt = new DataTable();
                var sonuc = dt.Compute(ifade, "");

                richTextBox1.Text = sonuc.ToString().Replace(".", ",");
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
            }
            catch
            {
                MessageBox.Show("Geçersiz işlem!");
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Sayılar
            if (keyData == Keys.D0 || keyData == Keys.NumPad0) { button0.PerformClick(); return true; }
            if (keyData == Keys.D1 || keyData == Keys.NumPad1) { button1.PerformClick(); return true; }
            if (keyData == Keys.D2 || keyData == Keys.NumPad2) { button2.PerformClick(); return true; }
            if (keyData == Keys.D3 || keyData == Keys.NumPad3) { button3.PerformClick(); return true; }
            if (keyData == Keys.D4 || keyData == Keys.NumPad4) { button4.PerformClick(); return true; }
            if (keyData == Keys.D5 || keyData == Keys.NumPad5) { button5.PerformClick(); return true; }
            if (keyData == Keys.D6 || keyData == Keys.NumPad6) { button6.PerformClick(); return true; }
            if (keyData == Keys.D7 || keyData == Keys.NumPad7) { button7.PerformClick(); return true; }
            if (keyData == Keys.D8 || keyData == Keys.NumPad8) { button8.PerformClick(); return true; }
            if (keyData == Keys.D9 || keyData == Keys.NumPad9) { button9.PerformClick(); return true; }

            // Ondalık
            if (keyData == Keys.Oemcomma || keyData == Keys.Decimal) { button12.PerformClick(); return true; }

            // İşlemler
            if (keyData == Keys.Add || keyData == (Keys.Shift | Keys.Oemplus)) { toplamabutton.PerformClick(); return true; }
            if (keyData == Keys.Subtract || keyData == Keys.OemMinus) { cikarmabuton.PerformClick(); return true; }
            if (keyData == Keys.Multiply) { carpmabutton.PerformClick(); return true; }
            if (keyData == Keys.Divide || keyData == Keys.OemQuestion) { bolmebuton.PerformClick(); return true; }

            // Sonuç
            if (keyData == Keys.Enter || keyData == Keys.Return) { sonucbuton.PerformClick(); return true; }

            // Sil
            if (keyData == Keys.Back) { deletebutton.PerformClick(); return true; }

            // Temizle
            if (keyData == Keys.Delete || keyData == Keys.Escape) { button10.PerformClick(); return true; }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = richTextBox1;
        }
    }
}
