using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace PAKZaliczenieProjekt
{
    public partial class Form1 : Form
    {
        Timer timer1;

        private const int step = 1;

        private const double default1ValueR1 = 10;
        private const double default1ValueR2 = 40;
        private const double default1ValueL = 0.007;
        private const double default1ValueC = 0.00001;
        private const double default1ValueU1 = 130;

        private const double default2ValueR1 = 20;
        private const double default2ValueR2 = 50;
        private const double default2ValueL = 0.008;
        private const double default2ValueC = 0.000011;
        private const double default2ValueU1 = 140;

        private const double default3ValueR1 = 30;
        private const double default3ValueR2 = 60;
        private const double default3ValueL = 0.009;
        private const double default3ValueC = 0.000012;
        private const double default3ValueU1 = 150;

        private const double default1ValueF1 = 0;
        private const double default1ValueF2 = 1000;

        private bool schemeEnabled;
        private bool chartsPrinted;

        private int size;

        private double valueR1;
        private double valueR2;
        private double valueL;
        private double valueC;
        private double valueU1;
        private double valueU2;
        private double valueF1;
        private double valueF2;
        
        private double[] F;
        
        // pulsacja
        private double[] Omega;

        // wartości zespolone
        // źródło zasilania 
        private Complex[] E;
        // impedancje zespolone
        private Complex[] Z1; 
        private Complex[] Z2; 
        private Complex[] Z3; 
        // napięcia zespolone
        private Complex[] U1; 
        private Complex[] U2;
        // prady zespolone
        private Complex[] I1;
        private Complex[] I2;
        private Complex[] I3;
        // wspolczynnik podobieństwa
        private Complex[] k;

        private Complex[] U2ByU1;


        public double ValueR1
        {
            get
            {
                return this.valueR1;
            }
            set
            {
                valueR1 = value;
            }
        }

        public double ValueR2
        {
            get
            {
                return this.valueR2;
            }
            set
            {
                valueR2 = value;
            }
        }

        public double ValueL
        {
            get
            {
                return this.valueL;
            }
            set
            {
                valueL = value;
            }
        }
        public double ValueC
        {
            get
            {
                return this.valueC;
            }
            set
            {
                valueC = value;
            }
        }

        public double ValueU1
        {
            get
            {
                return this.valueU1;
            }
            set
            {
                valueU1 = value;
            }
        }

        public double ValueU2
        {
            get
            {
                return this.valueU2;
            }
            set
            {
                valueU2 = value;
            }
        }

        public double ValueF1
        {
            get
            {
                return this.valueF1;
            }
            set
            {
                valueF1 = value;
            }
        }

        public double ValueF2
        {
            get
            {
                return this.valueF2;
            }
            set
            {
                valueF2 = value;
            }
        }

        public string ValueR1String
        {
            get
            {
                return this.valueR1.ToString();
            }
            set
            {
                double temp;
                if (!double.TryParse(value, out temp))
                {
                    MessageBox.Show("Błędna wartość rezystancji R1", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (temp <= 0)
                {
                    MessageBox.Show("R1 <= 0", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    valueR1 = temp;
                }
            }
        }

        public string ValueR2String
        {
            get
            {
                return this.valueR2.ToString();
            }
            set
            {
                double temp;
                if (!double.TryParse(value, out temp))
                {
                    MessageBox.Show("Błędna wartość rezystancji R2", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (temp <= 0)
                {
                    MessageBox.Show("R2 <= 0", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    valueR2 = temp;
                }
            }
        }

        public string ValueLString
        {
            get
            {
                return this.valueL.ToString();
            }
            set
            {
                double temp;
                if (!double.TryParse(value, out temp))
                {
                    MessageBox.Show("Błędna wartość indukcyjności L", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (temp <= 0)
                {
                    MessageBox.Show("L <= 0", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    valueL = temp;
                }
            }
        }

        public string ValueCString
        {
            get
            {
                return this.valueC.ToString();
            }
            set
            {
                double temp;
                if (!double.TryParse(value, out temp))
                {
                    MessageBox.Show("Błędna wartość pojemności C", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (temp <= 0)
                {
                    MessageBox.Show("C <= 0", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    valueC = temp;
                }
            }
        }

        public string ValueU1String
        {
            get
            {
                return this.valueU1.ToString();
            }
            set
            {
                double temp;
                if (!double.TryParse(value, out temp))
                {
                    MessageBox.Show("Błędna wartość napięcia U1", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (temp <= 0)
                {
                    MessageBox.Show("U1 <= 0", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    valueU1 = temp;
                }
            }
        }

        public string ValueF1String
        {
            get
            {
                return this.valueF1.ToString();
            }
            set
            {
                double temp;
                if (!double.TryParse(value, out temp))
                {
                    MessageBox.Show("Błędna wartość Min częstotliwości F1", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (temp >= this.valueF2)
                {
                    MessageBox.Show("Częstotliwość Min >= Max", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (temp < 0)
                {
                    MessageBox.Show("F1 < 0", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    valueF1 = temp;
                }
            }
        }

        public string ValueF2String
        {
            get
            {
                return this.valueF2.ToString();
            }
            set
            {
                double temp;
                if (!double.TryParse(value, out temp))
                {
                    MessageBox.Show("Błędna wartość Max częstotliwości F2", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (temp <= this.valueF1)
                {
                    MessageBox.Show("Częstotliwość Min >= Max", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (temp <= 0)
                {
                    MessageBox.Show("F2 <= 0", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    valueF2 = temp;
                }
            }
        }

        enum unit
        {
            Ω,
            H,
            F,
            V,
            Hz
        }

        private void labelTextWithUnit(Label label)
        {
            unit unitType;
            double value;
            switch (label.Name)
            {
                case "labelR1Value":
                    unitType = unit.Ω;
                    value = valueR1;
                    break;
                case "labelR2Value":
                    unitType = unit.Ω;
                    value = valueR2;
                    break;
                case "labelLValue":
                    unitType = unit.H;
                    value = valueL;
                    break;
                case "labelCValue":
                    unitType = unit.F;
                    value = valueC;
                    break;
                case "labelU1Value":
                    unitType = unit.V;
                    value = valueU1;
                    break;
                case "labelU2Value":
                    unitType = unit.V;
                    value = valueU2;
                    break;
                case "labelFMin":
                    unitType = unit.Hz;
                    value = valueF1;
                    break;
                case "labelFMax":
                    unitType = unit.Hz;
                    value = valueF2;
                    break;
                default:
                    unitType = unit.Ω;
                    value = valueR1;
                    break;
            }
            label.Text = value.ToString() + unitType;
        }

        public bool Calculations(BackgroundWorker worker, DoWorkEventArgs e)
        {
            size = step * (Convert.ToInt32(valueF2) - Convert.ToInt32(valueF1));
            schemeEnabled = false;
            int percent1 = 0;
            int percent2 = 0;
            int counter;


            double incr = 1.0/step;

            F = new double [size];
            F[0] = valueF1;

            E = new Complex[size];
            Omega = new double[size];
            Z1 = new Complex[size];
            Z2 = new Complex[size];
            Z3 = new Complex[size];
            I1 = new Complex[size];
            I2 = new Complex[size];
            I3 = new Complex[size];
            U1 = new Complex[size];
            U2 = new Complex[size];
            k = new Complex[size];
            U2ByU1 = new Complex[size];

            //używane w testach wątku roboczego
            //int c = 0;

            for (counter = 0; counter < step * (valueF2 - valueF1); counter++)
            {
                //używane w testach wątku roboczego
                //for (int i = 0; i < 10000000; i++)
                //{
                //    c += 1;
                //}

                if (counter == 0)
                {
                    F[counter] = valueF1;
                }
                else
                {
                    F[counter] = F[counter - 1] + incr;
                }
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return false;
                }
                E[counter] = new Complex((valueU1 / Math.Sqrt(2)), 0);
                Omega[counter] = 2 * Math.PI * F[counter];
                Z1[counter] = new Complex(0, Omega[counter] * valueL);
                Z2[counter] = new Complex(valueR1, 0);
                Z3[counter] = new Complex(valueR2, -(1 / Omega[counter] * valueC));

                I3[counter] = new Complex(1, 0);
                U2[counter] = Complex.Multiply(I3[counter], Z3[counter]);
                I2[counter] = Complex.Divide(U2[counter], Z2[counter]);

                I1[counter] = Complex.Add(I2[counter], I3[counter]);

                U1[counter] = Complex.Add(Complex.Multiply(Z1[counter], I1[counter]), U2[counter]);

                k[counter] = Complex.Divide(E[counter], U1[counter]);

                I1[counter] = Complex.Multiply(k[counter], I1[counter]);
                I2[counter] = Complex.Multiply(k[counter], I2[counter]);
                I3[counter] = Complex.Multiply(k[counter], I3[counter]);

                U2[counter] = Complex.Multiply(I3[counter], Z3[counter]);
                U1[counter] = Complex.Add(Complex.Multiply(Z1[counter], I1[counter]), U2[counter]);

                U2ByU1[counter] = Complex.Divide(U2[counter], U1[counter]);

                percent2 = (int)(100 * (F[counter] - valueF1) / (valueF2 - valueF1));
                if (percent2 > percent1)
                {
                    worker.ReportProgress(percent2);
                    percent1 = percent2;
                }
            }

            return true;
        }

        public Form1()
        {
            InitializeComponent();

            schemeEnabled = true;
            buttonStop.Enabled = false;
            chartsPrinted = false;

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            this.ValueR1 = default1ValueR1;
            this.ValueR2 = default1ValueR2;
            this.ValueL = default1ValueL;
            this.ValueC = default1ValueC;
            this.ValueU1 = default1ValueU1;
            this.ValueF1 = default1ValueF1;
            this.ValueF2 = default1ValueF2;

            labelTextWithUnit(labelR1Value);
            labelTextWithUnit(labelR2Value);
            labelTextWithUnit(labelLValue);
            labelTextWithUnit(labelCValue);
            labelTextWithUnit(labelU1Value);
            labelTextWithUnit(labelFMin);
            labelTextWithUnit(labelFMax);

            this.toolStripMenuItem2.Text = default1ValueR1 + "" + unit.Ω;
            this.toolStripMenuItem3.Text = default2ValueR1 + "" + unit.Ω;
            this.toolStripMenuItem4.Text = default3ValueR1 + "" + unit.Ω;

            this.toolStripMenuItem5.Text = default1ValueR2 + "" + unit.Ω;
            this.toolStripMenuItem6.Text = default2ValueR2 + "" + unit.Ω;
            this.toolStripMenuItem7.Text = default3ValueR2 + "" + unit.Ω;

            this.toolStripMenuItem11.Text = default1ValueL + "" + unit.H;
            this.toolStripMenuItem12.Text = default2ValueL + "" + unit.H;
            this.toolStripMenuItem13.Text = default3ValueL + "" + unit.H;

            this.toolStripMenuItem8.Text = default1ValueC + "" + unit.F;
            this.toolStripMenuItem9.Text = default2ValueC + "" + unit.F;
            this.toolStripMenuItem10.Text = default3ValueC + "" + unit.F;

            this.toolStripMenuItem14.Text = default1ValueU1 + "" + unit.V;
            this.toolStripMenuItem15.Text = default2ValueU1 + "" + unit.V;
            this.toolStripMenuItem16.Text = default3ValueU1 + "" + unit.V;

            toolStripStatusLabel1.Text = "Gotowy";
            toolStripStatusLabel3.Text = "     ";

        }

        void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = 
                DateTime.Now.Hour.ToString("0#.") + ":" +
                DateTime.Now.Minute.ToString("0#.") + ":" +
                DateTime.Now.Second.ToString("0#.");
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Schemat ideowy";
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Gotowy";
        }

        private void chart1_MouseEnter(object sender, EventArgs e)
        {
            if (chartsPrinted)
            {
                toolStripStatusLabel1.Text = "Wykres  - " + chart1.Titles[0].Text;
                this.toolTip1.SetToolTip(this.chart1, "Wykres  - " + chart1.Titles[0].Text);
            }
        }

        private void chart1_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Gotowy";
        }

        private void chart2_MouseEnter(object sender, EventArgs e)
        {
            if (chartsPrinted)
            {
                toolStripStatusLabel1.Text = "Wykres  - " + chart2.Titles[0].Text;
                this.toolTip1.SetToolTip(this.chart2, "Wykres  - " + chart2.Titles[0].Text);
            }
        }

        private void chart2_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Gotowy";
        }

        private void chart3_MouseEnter(object sender, EventArgs e)
        {
            if (chartsPrinted)
            {
                toolStripStatusLabel1.Text = "Wykres  - " + chart3.Titles[0].Text;
                this.toolTip1.SetToolTip(this.chart3, "Wykres  - " + chart3.Titles[0].Text);
            }
        }

        private void chart3_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Gotowy";
        }

        private void panelR1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "Rezystor R1 - " + this.ValueR1 + unit.Ω;
        }

        private void panelR2_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "Rezystor R2 - " + this.ValueR2 + unit.Ω;
        }

        private void panelL_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "Cewka L - " + this.ValueL + unit.H;
        }

        private void panelC_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "Kondensator C - " + this.ValueC + unit.F;
        }

        private void panelU1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "Napięcie zasilające U1 - " + this.ValueU1 + unit.V;
        }

        private void panelU2_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "Napięcie U2 - " + this.ValueU2 + unit.V;
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

        private void wyświetlajOznaczenieToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (wyświetlajOznaczenieToolStripMenuItem1.Checked == true)
            {
                labelR2.Visible = true;
            }
            else
            {
                labelR2.Visible = false;
            }
        }

        private void wyświetlajOznaczenieToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (wyświetlajOznaczenieToolStripMenuItem2.Checked == true)
            {
                labelL.Visible = true;
            }
            else
            {
                labelL.Visible = false;
            }
        }

        private void wyświetlajOznaczenieToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (wyświetlajOznaczenieToolStripMenuItem3.Checked == true)
            {
                labelC.Visible = true;
            }
            else
            {
                labelC.Visible = false;
            }
        }

        private void wyświetlajOznaczenieToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (wyświetlajOznaczenieToolStripMenuItem4.Checked == true)
            {
                labelU1.Visible = true;
            }
            else
            {
                labelU1.Visible = false;
            }
        }

        private void wyświetlajOznaczenieToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (wyświetlajOznaczenieToolStripMenuItem5.Checked == true)
            {
                labelU2.Visible = true;
            }
            else
            {
                labelU2.Visible = false;
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

        private void wyświetlajRezystancjeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (wyświetlajRezystancjeToolStripMenuItem1.Checked == true)
            {
                labelR2Value.Visible = true;
            }
            else
            {
                labelR2Value.Visible = false;
            }
        }

        private void wyświetlajIndukcyjnośćToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wyświetlajIndukcyjnośćToolStripMenuItem.Checked == true)
            {
                labelLValue.Visible = true;
            }
            else
            {
                labelLValue.Visible = false;
            }
        }

        private void wyświetlajPojemnośćToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wyświetlajPojemnośćToolStripMenuItem.Checked == true)
            {
                labelCValue.Visible = true;
            }
            else
            {
                labelCValue.Visible = false;
            }
        }

        private void wyświetlajNapięcieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wyświetlajNapięcieToolStripMenuItem.Checked == true)
            {
                labelU1Value.Visible = true;
                labelFMin.Visible = true;
                labelFMax.Visible = true;
                labelMin.Visible = true;
                labelMax.Visible = true;
            }
            else
            {
                labelU1Value.Visible = false;
                labelFMin.Visible = false;
                labelFMax.Visible = false;
                labelMin.Visible = false;
                labelMax.Visible = false;
            }
        }

        private void wyświetlajOznaczeniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wyświetlajOznaczeniaToolStripMenuItem.Checked == true)
            {
                wyświetlajOznaczenieToolStripMenuItem.Checked = true;
                wyświetlajOznaczenieToolStripMenuItem1.Checked = true;
                wyświetlajOznaczenieToolStripMenuItem2.Checked = true;
                wyświetlajOznaczenieToolStripMenuItem3.Checked = true;
                wyświetlajOznaczenieToolStripMenuItem4.Checked = true;
                wyświetlajOznaczenieToolStripMenuItem5.Checked = true;
            }
            else
            {
                wyświetlajOznaczenieToolStripMenuItem.Checked = false;
                wyświetlajOznaczenieToolStripMenuItem1.Checked = false;
                wyświetlajOznaczenieToolStripMenuItem2.Checked = false;
                wyświetlajOznaczenieToolStripMenuItem3.Checked = false;
                wyświetlajOznaczenieToolStripMenuItem4.Checked = false;
                wyświetlajOznaczenieToolStripMenuItem5.Checked = false;
            }
            wyświetlajOznaczenieToolStripMenuItem_Click(sender, e);
            wyświetlajOznaczenieToolStripMenuItem1_Click(sender, e);
            wyświetlajOznaczenieToolStripMenuItem2_Click(sender, e);
            wyświetlajOznaczenieToolStripMenuItem3_Click(sender, e);
            wyświetlajOznaczenieToolStripMenuItem4_Click(sender, e);
            wyświetlajOznaczenieToolStripMenuItem5_Click(sender, e);
        }

        private void wyświetlajWartościParametrówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wyświetlajWartościParametrówToolStripMenuItem.Checked == true)
            {
                wyświetlajRezystancjeToolStripMenuItem.Checked = true;
                wyświetlajRezystancjeToolStripMenuItem1.Checked = true;
                wyświetlajIndukcyjnośćToolStripMenuItem.Checked = true;
                wyświetlajPojemnośćToolStripMenuItem.Checked = true;
                wyświetlajNapięcieToolStripMenuItem.Checked = true;
            }
            else
            {
                wyświetlajRezystancjeToolStripMenuItem.Checked = false;
                wyświetlajRezystancjeToolStripMenuItem1.Checked = false;
                wyświetlajIndukcyjnośćToolStripMenuItem.Checked = false;
                wyświetlajPojemnośćToolStripMenuItem.Checked = false;
                wyświetlajNapięcieToolStripMenuItem.Checked = false;
            }
            wyświetlajRezystancjeToolStripMenuItem_Click(sender, e);
            wyświetlajRezystancjeToolStripMenuItem1_Click(sender, e);
            wyświetlajIndukcyjnośćToolStripMenuItem_Click(sender, e);
            wyświetlajPojemnośćToolStripMenuItem_Click(sender, e);
            wyświetlajNapięcieToolStripMenuItem_Click(sender, e);
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                ValueR1String = toolStripTextBox1.Text;
                labelTextWithUnit(labelR1Value);
                contextMenuStripR1.Close();
            }
        }

        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ValueR2String = toolStripTextBox2.Text;
                labelTextWithUnit(labelR2Value);
                contextMenuStripR2.Close();
            }
        }

        private void toolStripTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ValueLString = toolStripTextBox3.Text;
                labelTextWithUnit(labelLValue);
                contextMenuStripL.Close();
            }
        }

        private void toolStripTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ValueCString = toolStripTextBox4.Text;
                labelTextWithUnit(labelCValue);
                contextMenuStripC.Close();
            }
        }

        private void toolStripTextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ValueU1String = toolStripTextBox5.Text;
                labelTextWithUnit(labelU1Value);
                contextMenuStripU1.Close();
            }
        }

        private void toolStripTextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ValueF1String = toolStripTextBox7.Text;
                labelTextWithUnit(labelFMin);
                contextMenuStripU1.Close();
            }
        }

        private void toolStripTextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ValueF2String = toolStripTextBox8.Text;
                labelTextWithUnit(labelFMax);
                contextMenuStripU1.Close();
            }
        }

        private void pasekStanuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pasekStanuToolStripMenuItem.Checked == true)
            {
                statusStrip1.Visible = true;
            }
            else
            {
                statusStrip1.Visible = false;
            }
        }

        private void panelR1_DoubleClick(object sender, EventArgs e)
        {
            contextMenuStripR1.Show();
        }

        private void panelR2_DoubleClick(object sender, EventArgs e)
        {
            contextMenuStripR2.Show();
        }

        private void panelL_DoubleClick(object sender, EventArgs e)
        {
            contextMenuStripL.Show();
        }

        private void panelC_DoubleClick(object sender, EventArgs e)
        {
            contextMenuStripC.Show();
        }

        private void panelU1_DoubleClick(object sender, EventArgs e)
        {
            contextMenuStripU1.Show();
        }

        private void panelU2_DoubleClick(object sender, EventArgs e)
        {
            contextMenuStripU2.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.valueR1 = default1ValueR1;
            labelTextWithUnit(labelR1Value);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.valueR1 = default2ValueR1;
            labelTextWithUnit(labelR1Value);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.valueR1 = default3ValueR1;
            labelTextWithUnit(labelR1Value);
        }


        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.valueR2 = default1ValueR2;
            labelTextWithUnit(labelR2Value);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.valueR2 = default2ValueR2;
            labelTextWithUnit(labelR2Value);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            this.valueR2 = default3ValueR2;
            labelTextWithUnit(labelR2Value);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            this.valueL = default1ValueL;
            labelTextWithUnit(labelLValue);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            this.valueL = default2ValueL;
            labelTextWithUnit(labelLValue);
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            this.valueL = default3ValueL;
            labelTextWithUnit(labelLValue);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            this.valueC = default1ValueC;
            labelTextWithUnit(labelCValue);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            this.valueC = default2ValueC;
            labelTextWithUnit(labelCValue);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            this.valueC = default3ValueC;
            labelTextWithUnit(labelCValue);
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            this.valueU1 = default1ValueU1;
            labelTextWithUnit(labelU1Value);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            this.valueU1 = default2ValueU1;
            labelTextWithUnit(labelU1Value);
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            this.valueU1 = default3ValueU1;
            labelTextWithUnit(labelU1Value);
        }

        private void własnaToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = this.ValueR1.ToString();

        }

        private void innaToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = this.ValueR2.ToString();

        }

        private void innaToolStripMenuItem2_MouseEnter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = this.ValueL.ToString();

        }

        private void innaToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = this.ValueC.ToString();

        }
        private void inneToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = this.ValueU1.ToString();
        }

        private void minToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripTextBox7.Text = this.ValueF1.ToString();
        }

        private void maxToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripTextBox8.Text = this.ValueF2.ToString();
        }

        private void buttonPrintChart_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            backgroundWorker1.RunWorkerAsync();
            buttonStop.Enabled = true;
            buttonPrintChart.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel3.Text = e.ProgressPercentage.ToString() + "%";
            toolStripStatusLabel1.Text = "Obliczenia";
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            e.Result = Calculations(worker, e);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("Przerwano obliczenia");
                }
                else
                {
                    toolStripStatusLabel3.Text = "100%";
                    toolStripProgressBar1.Value = 100;

                    //Rysowanie wykresów
                    //Wykres 1
                    DataTable chart1DataTable;
                    DataView chart1DataView;

                    chart1DataTable = new DataTable();
                    DataColumn chart1Column; 
                    DataRow chart1Row; 
                    chart1Column = new DataColumn(); 
                    chart1Column.DataType = Type.GetType("System.Double");
                    chart1Column.ColumnName = "Czestotliwosc";
                    chart1DataTable.Columns.Add(chart1Column);

                    chart1Column = new DataColumn();
                    chart1Column.DataType = Type.GetType("System.Double");
                    chart1Column.ColumnName = "Prad";
                    chart1DataTable.Columns.Add(chart1Column);

                    for (int i = 0; i < size; i++)
                    {
                        chart1Row = chart1DataTable.NewRow();
                        chart1Row["Czestotliwosc"] = F[i];
                        chart1Row["Prad"] = I1[i].Magnitude;
                        chart1DataTable.Rows.Add(chart1Row);
                    }

                    chart1DataView = new DataView(chart1DataTable);

                    chart1.Series.Clear();
                    chart1.Titles.Clear();

                    chart1.DataBindTable(chart1DataView, "Czestotliwosc");
                    chart1.Series["Prad"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart1.Series["Prad"].Color = Color.Blue;
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{#0.000}";
                    chart1.ChartAreas[0].BackColor = Color.Azure;
                    chart1.ChartAreas[0].AxisX.LineWidth = 1;
                    chart1.ChartAreas[0].AxisY.LineWidth = 1;
                    chart1.ChartAreas[0].AxisX.Title = "Czestotliwosc [Hz]";
                    chart1.ChartAreas[0].AxisY.Title = "Prąd I1 [A]";
                    chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F, FontStyle.Bold);
                    chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F, FontStyle.Bold);
                    chart1.Titles.Add("Prąd zasilający I1 w funkcji częstotliwości");
                    chart1.Titles[0].Font = new System.Drawing.Font("Times New Roman", 16F, FontStyle.Bold);
                    chart1.Titles[0].ForeColor = Color.Gray;
                    chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                    chart1.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
                    chart1.ChartAreas[0].AxisX.Minimum = this.valueF1;
                    chart1.ChartAreas[0].AxisX.Maximum = this.valueF2;
                    
                    //Wykres 2
                    DataTable chart2DataTable;
                    DataView chart2DataView;

                    chart2DataTable = new DataTable();
                    DataColumn chart2Column; 
                    DataRow chart2Row;
                    chart2Column = new DataColumn(); 
                    chart2Column.DataType = Type.GetType("System.Double");
                    chart2Column.ColumnName = "Czestotliwosc";
                    chart2DataTable.Columns.Add(chart2Column);

                    chart2Column = new DataColumn();
                    chart2Column.DataType = Type.GetType("System.Double");
                    chart2Column.ColumnName = "Faza";
                    chart2DataTable.Columns.Add(chart2Column);

                    for (int i = 0; i < size; i++)
                    {
                        chart2Row = chart2DataTable.NewRow();
                        chart2Row["Czestotliwosc"] = F[i];
                        chart2Row["Faza"] = U2ByU1[i].Phase;
                        chart2DataTable.Rows.Add(chart2Row);
                    }
                    chart2DataView = new DataView(chart2DataTable);
                    chart2.Series.Clear();
                    chart2.Titles.Clear();

                    chart2.DataBindTable(chart2DataView, "Czestotliwosc");
                    chart2.Series["Faza"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart2.Series["Faza"].Color = Color.Red;
                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = "{#0.000}";
                    chart2.ChartAreas[0].BackColor = Color.Azure;
                    chart2.ChartAreas[0].AxisX.LineWidth = 1;
                    chart2.ChartAreas[0].AxisY.LineWidth = 1;
                    chart2.ChartAreas[0].AxisX.Title = "Czestotliwosc [Hz]";
                    chart2.ChartAreas[0].AxisY.Title = "Faza [φ]";
                    chart2.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F, FontStyle.Bold);
                    chart2.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F, FontStyle.Bold);
                    chart2.Titles.Add("Widmo fazowe filtru U2/U1");
                    chart2.Titles[0].Font = new System.Drawing.Font("Times New Roman", 16F, FontStyle.Bold);
                    chart2.Titles[0].ForeColor = Color.Gray;
                    chart2.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                    chart2.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
                    chart2.ChartAreas[0].AxisX.Minimum = this.valueF1;
                    chart2.ChartAreas[0].AxisX.Maximum = this.valueF2;

                    //Wykres 3
                    DataTable chart3DataTable;
                    DataView chart3DataView;

                    chart3DataTable = new DataTable();
                    DataColumn chart3Column;
                    DataRow chart3Row;
                    chart3Column = new DataColumn();
                    chart3Column.DataType = Type.GetType("System.Double");
                    chart3Column.ColumnName = "Czestotliwosc";
                    chart3DataTable.Columns.Add(chart3Column);

                    chart3Column = new DataColumn();
                    chart3Column.DataType = Type.GetType("System.Double");
                    chart3Column.ColumnName = "Amplituda";
                    chart3DataTable.Columns.Add(chart3Column);

                    for (int i = 0; i < size; i++)
                    {
                        chart3Row = chart3DataTable.NewRow();
                        chart3Row["Czestotliwosc"] = F[i];
                        chart3Row["Amplituda"] = U2ByU1[i].Magnitude;
                        chart3DataTable.Rows.Add(chart3Row);
                    }
                    chart3DataView = new DataView(chart3DataTable);
                    chart3.Series.Clear();
                    chart3.Titles.Clear();

                    chart3.DataBindTable(chart3DataView, "Czestotliwosc");
                    chart3.Series["Amplituda"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart3.Series["Amplituda"].Color = Color.Green;
                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = "{#0.000}";
                    chart3.ChartAreas[0].BackColor = Color.Azure;
                    chart3.ChartAreas[0].AxisX.LineWidth = 1;
                    chart3.ChartAreas[0].AxisY.LineWidth = 1;
                    chart3.ChartAreas[0].AxisX.Title = "Czestotliwosc [Hz]";
                    chart3.ChartAreas[0].AxisY.Title = "Amplituda [dB]";
                    chart3.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F, FontStyle.Bold);
                    chart3.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F, FontStyle.Bold);
                    chart3.Titles.Add("Transmitancja filtru U2/U1 w funkcji częstotliwości");
                    chart3.Titles[0].Font = new System.Drawing.Font("Times New Roman", 16F, FontStyle.Bold);
                    chart3.Titles[0].ForeColor = Color.Gray;
                    chart3.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
                    chart3.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
                    chart3.ChartAreas[0].AxisX.Minimum = this.valueF1;
                    chart3.ChartAreas[0].AxisX.Maximum = this.valueF2;

                    chartsPrinted = true;
                }
            }

            toolStripStatusLabel1.Text = "Gotowy";

            schemeEnabled = true;
            buttonStop.Enabled = false;
            buttonPrintChart.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            buttonStop.Enabled = false;
        }

        private void contextMenuStripR1_Opening(object sender, CancelEventArgs e)
        {
            ustawieniaElementuToolStripMenuItem.Enabled = schemeEnabled;
        }

        private void contextMenuStripR2_Opening(object sender, CancelEventArgs e)
        {
            ustawRezzystancjeToolStripMenuItem.Enabled = schemeEnabled;
        }

        private void contextMenuStripL_Opening(object sender, CancelEventArgs e)
        {
            ustawToolStripMenuItem.Enabled = schemeEnabled;
        }

        private void contextMenuStripC_Opening(object sender, CancelEventArgs e)
        {
            ustawPojemnośćCToolStripMenuItem.Enabled = schemeEnabled;
        }

        private void contextMenuStripU1_Opening(object sender, CancelEventArgs e)
        {
            napięcieU1ToolStripMenuItem.Enabled = schemeEnabled;
            zakresCzęstotliwościToolStripMenuItem.Enabled = schemeEnabled;
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ądWydrukuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void drukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void szybkieDrukowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.BackImage = openFileDialog1.FileName;
                chart2.BackImage = openFileDialog1.FileName;
                chart3.BackImage = openFileDialog1.FileName;
                chart1.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void wyświetlPomocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Temat: Filtr pasywny złożony z kombinacji dwóch rezystorów R1, R2, cewki magnetycznej L i kondensatora C."
            + "\n Zakres:"
            + "\n 1. Schemat ideowy;"
            + "\n 2. Opisu elementów filtru;"
            + "\n 3. Edycja wartości parametrów filtru: R1, R2, L i C;"
            + "\n 4. Edycja wartości parametrów napięcia zasilającego U1 (przebieg sinusoidalny) oraz zakresu częstotliwości f;"
            + "\n 5. Prezentacja graficzna transmitancji filtru U2/U1 w funkcji częstotliwości f dla stanu ustalonego przy zasilaniu sinusoidalnym;"
            + "\n 6. Prezentacja graficzna prądu zasilającego I1 w funkcji częstotliwości."
            + "\n 7. Prezentacja widma fazowego filtru U2/U1;"
            + "\n 8. Inne elementy nadające aplikacji atrybuty dydaktyczne i poznawcze.", "RLC - Informacje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormParameters dlgParams = new FormParameters();

            dlgParams.ValueR1 = this.ValueR1;
            dlgParams.ValueR2 = this.ValueR2;
            dlgParams.ValueL = this.ValueL;
            dlgParams.ValueC = this.ValueC;
            dlgParams.ValueU1 = this.ValueU1;
            dlgParams.ValueF1 = this.ValueF1;
            dlgParams.ValueF2 = this.ValueF2;

            if (dlgParams.ShowDialog() == DialogResult.OK)
            {
                this.ValueR1 = dlgParams.ValueR1;
                this.ValueR2 = dlgParams.ValueR2;
                this.ValueL = dlgParams.ValueL;
                this.ValueC = dlgParams.ValueC;
                this.ValueU1 = dlgParams.ValueU1;
                this.ValueF1 = dlgParams.ValueF1;
                this.ValueF2 = dlgParams.ValueF2;
                labelTextWithUnit(labelR1Value);
                labelTextWithUnit(labelR2Value);
                labelTextWithUnit(labelLValue);
                labelTextWithUnit(labelCValue);
                labelTextWithUnit(labelU1Value);
                labelTextWithUnit(labelFMin);
                labelTextWithUnit(labelFMax);
            }
        }

        private void edycjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cofnijToolStripMenuItem.Enabled = schemeEnabled;
        }

        private void contextMenuStripChart1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStripChart1.Enabled = chartsPrinted;
        }

        private void czcionkaTytułuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.Titles[0].Font = fontDialog1.Font;
            }
        }


        private void czcionkaPodpisówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.ChartAreas[0].AxisY.TitleFont = fontDialog1.Font;
                chart1.ChartAreas[0].AxisX.TitleFont = fontDialog1.Font;
            }
        }

        private void kolorSeriiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.Series["Prad"].Color = colorDialog1.Color;
            }
        }

        private void kolorTłaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.ChartAreas[0].BackColor = colorDialog1.Color;
            }
        }

        private void kolorTytułuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.Titles[0].ForeColor = colorDialog1.Color;
            }
        }

    }
}
