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
        private const double default1ValueR1 = 10;
        private const double default1ValueR2 = 40;
        private const double default1ValueL = 70;
        private const double default1ValueC = 100;
        private const double default1ValueU1 = 130;
        private const double default1ValueU2 = 160;

        private const double default2ValueR1 = 20;
        private const double default2ValueR2 = 50;
        private const double default2ValueL = 80;
        private const double default2ValueC = 110;
        private const double default2ValueU1 = 140;
        private const double default2ValueU2 = 170;

        private const double default3ValueR1 = 30;
        private const double default3ValueR2 = 60;
        private const double default3ValueL = 90;
        private const double default3ValueC = 120;
        private const double default3ValueU1 = 150;
        private const double default3ValueU2 = 180;

        private double valueR1;
        private double valueR2;
        private double valueL;
        private double valueC;
        private double valueU1;
        private double valueU2;
        private double valueF1;
        private double valueF2;
        
        private double F;
        
        // pulsacja
        private double Omega;

        // wartości zespolone
        // źródło zasilania 
        private Complex E;
        // impedancje zespolone
        private Complex Z1; 
        private Complex Z2; 
        private Complex Z3; 
        // napięcia zespolone
        private Complex U1; 
        private Complex U2;
        // prady zespolone
        private Complex I1;
        private Complex I2;
        private Complex I3;
        // wspolczynnik podobieństwa
        private Complex k;

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

        enum unit
        {
            Ω,
            μH,
            μF,
            V
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
                    unitType = unit.μH;
                    value = valueL;
                    break;
                case "labelCValue":
                    unitType = unit.μF;
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
                default:
                    unitType = unit.Ω;
                    value = valueR1;
                    break;
            }
            label.Text = value.ToString() + unitType;
        }

        public void Calculations()
        {
            E = new Complex((valueU1 / Math.Sqrt(2)), 0);
            Omega = 2 * Math.PI * F;
            Z1 = new Complex(0, Omega * valueL);
            Z2 = new Complex(valueR1, 0);
            Z3 = new Complex(valueR2, -(1 / Omega * valueC));

            I3 = new Complex(1, 0);
            U2 = Complex.Multiply(I3, Z3);
            I2 = Complex.Divide(U2, Z2);

            I1 = Complex.Add(I2, I3);

            U1 = Complex.Add(Complex.Multiply(Z1, I1), U2);

            k = Complex.Divide(E, U1);

            I1 = Complex.Multiply(k, I1);
            I2 = Complex.Multiply(k, I2);
            I3 = Complex.Multiply(k, I3);

            U2 = Complex.Multiply(I3, Z3);
            U1 = Complex.Add(Complex.Multiply(Z1, I1), U2);

            label12.Text = E.ToString();
            label13.Text = Omega.ToString();
            label14.Text = Z1.ToString();
            label15.Text = Z2.ToString();
            label16.Text = Z3.ToString();
            label17.Text = U1.ToString();
            label18.Text = U2.ToString();
            label19.Text = k.ToString();
            label20.Text = I1.ToString();
            label21.Text = I2.ToString();
            label22.Text = I3.ToString();
        }

        public Form1()
        {
            InitializeComponent();

            this.ValueR1 = default1ValueR1;
            this.ValueR2 = default1ValueR2;
            this.ValueL = default1ValueL;
            this.ValueC = default1ValueC;
            this.ValueU1 = default1ValueU1;
            this.ValueU2 = default1ValueU2;

            labelTextWithUnit(labelR1Value);
            labelTextWithUnit(labelR2Value);
            labelTextWithUnit(labelLValue);
            labelTextWithUnit(labelCValue);
            labelTextWithUnit(labelU1Value);
            labelTextWithUnit(labelU2Value);

            this.toolStripMenuItem2.Text = default1ValueR1 + "" + unit.Ω;
            this.toolStripMenuItem3.Text = default2ValueR1 + "" + unit.Ω;
            this.toolStripMenuItem4.Text = default3ValueR1 + "" + unit.Ω;

            this.toolStripMenuItem5.Text = default1ValueR2 + "" + unit.Ω;
            this.toolStripMenuItem6.Text = default2ValueR2 + "" + unit.Ω;
            this.toolStripMenuItem7.Text = default3ValueR2 + "" + unit.Ω;

            this.toolStripMenuItem11.Text = default1ValueL + "" + unit.μH;
            this.toolStripMenuItem12.Text = default2ValueL + "" + unit.μH;
            this.toolStripMenuItem13.Text = default3ValueL + "" + unit.μH;

            this.toolStripMenuItem8.Text = default1ValueC + "" + unit.μF;
            this.toolStripMenuItem9.Text = default2ValueC + "" + unit.μF;
            this.toolStripMenuItem10.Text = default3ValueC + "" + unit.μF;

            this.toolStripMenuItem14.Text = default1ValueU1 + "" + unit.V;
            this.toolStripMenuItem15.Text = default2ValueU1 + "" + unit.V;
            this.toolStripMenuItem16.Text = default3ValueU1 + "" + unit.V;

            this.toolStripMenuItem17.Text = default1ValueU2 + "" + unit.V;
            this.toolStripMenuItem18.Text = default2ValueU2 + "" + unit.V;
            this.toolStripMenuItem19.Text = default3ValueU2 + "" + unit.V;

            toolStripStatusLabel1.Text = "gotowy";
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "schemat ideowy";
        }

        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "gotowy";
        }

        private void panelR1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "rezystor R1 - " + this.ValueR1 + unit.Ω;
        }

        private void panelR2_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "rezystor R2 - " + this.ValueR2 + unit.Ω;
        }

        private void panelL_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "cewka L - " + this.ValueL + unit.μH;
        }

        private void panelC_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "kondensator C - " + this.ValueC + unit.μF;
        }

        private void panelU1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "napięcie U1 - " + this.ValueU1 + unit.V;
        }

        private void panelU2_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "napięcie U2 - " + this.ValueU2 + unit.V;
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
            }
            else
            {
                labelU1Value.Visible = false;
            }
        }

        private void wyświetlajNapięcieToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (wyświetlajNapięcieToolStripMenuItem1.Checked == true)
            {
                labelU2Value.Visible = true;
            }
            else
            {
                labelU2Value.Visible = false;
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
                wyświetlajNapięcieToolStripMenuItem1.Checked = true;
            }
            else
            {
                wyświetlajRezystancjeToolStripMenuItem.Checked = false;
                wyświetlajRezystancjeToolStripMenuItem1.Checked = false;
                wyświetlajIndukcyjnośćToolStripMenuItem.Checked = false;
                wyświetlajPojemnośćToolStripMenuItem.Checked = false;
                wyświetlajNapięcieToolStripMenuItem.Checked = false;
                wyświetlajNapięcieToolStripMenuItem1.Checked = false;
            }
            wyświetlajRezystancjeToolStripMenuItem_Click(sender, e);
            wyświetlajRezystancjeToolStripMenuItem1_Click(sender, e);
            wyświetlajIndukcyjnośćToolStripMenuItem_Click(sender, e);
            wyświetlajPojemnośćToolStripMenuItem_Click(sender, e);
            wyświetlajNapięcieToolStripMenuItem_Click(sender, e);
            wyświetlajNapięcieToolStripMenuItem1_Click(sender, e);
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if(!double.TryParse(toolStripTextBox1.Text, out this.valueR1))
            {
                MessageBox.Show("Błędna wartość rezystancji", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else 
            {
                labelTextWithUnit(labelR1Value);
            }
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(toolStripTextBox2.Text, out this.valueR2))
            {
                MessageBox.Show("Błędna wartość rezystancji", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                labelTextWithUnit(labelR2Value);
            }
        }

        private void toolStripTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(toolStripTextBox3.Text, out this.valueL))
            {
                MessageBox.Show("Błędna wartość rezystancji", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                labelTextWithUnit(labelLValue);
            }
        }

        private void toolStripTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(toolStripTextBox4.Text, out this.valueC))
            {
                MessageBox.Show("Błędna wartość rezystancji", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                labelTextWithUnit(labelCValue);
            }
        }

        private void toolStripTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(toolStripTextBox5.Text, out this.valueU1))
            {
                MessageBox.Show("Błędna wartość rezystancji", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                labelTextWithUnit(labelU1Value);
            }
        }

        private void toolStripTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(toolStripTextBox6.Text, out this.valueU2))
            {
                MessageBox.Show("Błędna wartość rezystancji", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                labelTextWithUnit(labelU2Value);
            }
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                contextMenuStripR1.Close();
            }
        }

        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                contextMenuStripR2.Close();
            }
        }

        private void toolStripTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                contextMenuStripL.Close();
            }
        }

        private void toolStripTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                contextMenuStripC.Close();
            }
        }

        private void toolStripTextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                contextMenuStripU1.Close();
            }
        }

        private void toolStripTextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                contextMenuStripU2.Close();
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

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            this.valueU2 = default1ValueU2;
            labelTextWithUnit(labelU2Value);
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            this.valueU2 = default2ValueU2;
            labelTextWithUnit(labelU2Value);
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            this.valueU2 = default3ValueU2;
            labelTextWithUnit(labelU2Value);

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

        private void toolStripTextBox5_MouseEnter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = this.ValueU1.ToString();

        }

        private void toolStripTextBox6_MouseEnter(object sender, EventArgs e)
        {
            toolStripTextBox6.Text = this.ValueU2.ToString();
        }

        private void buttonPrintChart_Click(object sender, EventArgs e)
        {
            Calculations();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
 
    }
}
