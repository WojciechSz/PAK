using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAKZaliczenieProjekt
{
    public partial class Form1 : Form
    {
        private int valueR1 = 0;
        public Form1()
        {
            InitializeComponent();
            labelR1Value.Text = valueR1.ToString();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //toolStripStatusLabel1.Text = "MouseMove:  X=" + e.X + ", Y=" + e.Y;
            toolStripStatusLabel1.Text = "";
            //if (e.X >= 225 && e.X <= 330 && e.Y >= 25 && e.Y <= 40)
            //{
            //    toolStripStatusLabel2.Text = "R1";
            //}
            //if (e.X >= 196 && e.X <= 210 && e.Y >= 93 && e.Y <= 180)
            //{
            //    toolStripStatusLabel2.Text = "R2";
            //}
            //if (e.X >= 336 && e.X <= 385 && e.Y >= 130 && e.Y <= 144)
            //{
            //    toolStripStatusLabel2.Text = "C";
            //}
            //if (e.X >= 77 && e.X <= 143 && e.Y >= 20 && e.Y <= 32)
            //{
            //    toolStripStatusLabel2.Text = "L";
            //}
            //if (e.X >= 14 && e.X <= 24 && e.Y >= 47 && e.Y <= 238)
            //{
            //    toolStripStatusLabel2.Text = "U1";
            //}
            //if (e.X >= 432 && e.X <= 442 && e.Y >= 47 && e.Y <= 238)
            //{
            //    toolStripStatusLabel2.Text = "U1";
            //}
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelR1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "R1";
        }
        private void panelR2_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "R2";
        }
        private void panelC_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "C";
        }
        private void panelL_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "L";
        }
        private void panelU1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "U1";
        }
        private void panelU2_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "U2";
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void ustawieniaElementuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStripU1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStripU2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void panelR1_DoubleClick(object sender, EventArgs e)
        {
            FormR1 FormR1 = new FormR1();
            FormR1.Show();
        }

        private void własnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormR1 FormR1 = new FormR1();
            FormR1.Show();
        }

        private void innaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormR2 FormR2 = new FormR2();
            FormR2.Show();
        }

        private void panelR1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void wyświetlajOznaczenieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(wyświetlajOznaczenieToolStripMenuItem.Checked == true)
            {
                labelR1.Visible = true;
            }
            else
            {
                labelR1.Visible = false;
            }
        }

        private void wyświetlajRezystancjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wyświetlajRezystancjeToolStripMenuItem.Checked == true)
            {
                labelR1Value.Visible = true;
            }
            else
            {
                labelR1Value.Visible = false;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.valueR1 = 10;
            labelR1Value.Text = valueR1.ToString();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.valueR1 = 50;
            labelR1Value.Text = valueR1.ToString();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.valueR1 = 100;
            labelR1Value.Text = valueR1.ToString();
        }
    }
}
