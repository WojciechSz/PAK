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
    public partial class FormParameters : Form
    {
        private double valueR1;
        private double valueR2;
        private double valueL;
        private double valueC;
        private double valueU1;
        private double valueF1;
        private double valueF2;

        public double ValueR1
        {
            get
            {
                double temp;
                if(!double.TryParse(textBoxR1.Text, out temp))
                {
                    MessageBox.Show("Błędna wartość rezystancji R1", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return this.valueR1;
                }
                else
                {
                    this.valueR1 = temp;
                    return this.valueR1;
                }
            }
            set
            {
                this.valueR1 = value;
                textBoxR1.Text = value.ToString();
            }
        }

        public double ValueR2
        {
            get
            {
                double temp;
                if (!double.TryParse(textBoxR2.Text, out temp))
                {
                    MessageBox.Show("Błędna wartość rezystancji R2", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return this.valueR2;
                }
                else
                {
                    this.valueR2 = temp;
                    return this.valueR2;
                }
            }
            set
            {
                this.valueR2 = value;
                textBoxR2.Text = value.ToString();
            }
        }

        public double ValueL
        {
            get
            {
                double temp;
                if (!double.TryParse(textBoxL.Text, out temp))
                {
                    MessageBox.Show("Błędna wartość indukcyjności L", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return this.valueL;
                }
                else
                {
                    this.valueL = temp;
                    return this.valueL;
                }
            }
            set
            {
                this.valueL = value;
                textBoxL.Text = value.ToString();
            }
        }

        public double ValueC
        {
            get
            {
                double temp;
                if (!double.TryParse(textBoxC.Text, out temp))
                {
                    MessageBox.Show("Błędna wartość pojemności C", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return this.valueC;
                }
                else
                {
                    this.valueC = temp;
                    return this.valueC;
                }
            }
            set
            {
                this.valueC = value;
                textBoxC.Text = value.ToString();
            }
        }

        public double ValueU1
        {
            get
            {
                double temp;
                if (!double.TryParse(textBoxU1.Text, out temp))
                {
                    MessageBox.Show("Błędna wartość napięcia U1", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return this.valueU1;
                }
                else
                {
                    this.valueU1 = temp;
                    return this.valueU1;
                }
            }
            set
            {
                this.valueU1 = value;
                textBoxU1.Text = value.ToString();
            }
        }

        public double ValueF1
        {
            get
            {
                double temp;
                if (!double.TryParse(textBoxF1.Text, out temp))
                {
                    MessageBox.Show("Błędna wartość min częstotliwości F1", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return this.valueF1;
                }
                else if(temp >= this.valueF2)
                {
                    MessageBox.Show("Min >= Max", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return this.valueF1;
                }
                else
                {
                    this.valueF1 = temp;
                    return this.valueF1;
                }
            }
            set
            {
                this.valueF1 = value;
                textBoxF1.Text = value.ToString();
            }
        }

        public double ValueF2
        {
            get
            {
                double temp;
                if (!double.TryParse(textBoxF2.Text, out temp))
                {
                    MessageBox.Show("Błędna wartość max częstotliwości  F2", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return this.valueF2;
                }
                else if (temp <= this.valueF1)
                {
                    MessageBox.Show("Min >= Max", "Parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return this.valueF2;
                }
                else
                {
                    this.valueF2 = temp;
                    return this.valueF2;
                }
            }
            set
            {
                this.valueF2 = value;
                textBoxF2.Text = value.ToString();
            }
        }

        public FormParameters()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
