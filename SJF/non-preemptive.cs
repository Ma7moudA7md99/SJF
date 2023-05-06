using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJF
{
    public partial class nonPreemptive : Form
    {
        public nonPreemptive()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (burstTB1.Text == "" || burstTB2.Text == ""
                || burstTB3.Text == "" || burstTB4.Text == ""
                || burstTB1.Text == "" || burstTB2.Text == ""
                || burstTB3.Text == "" || burstTB4.Text == ""
                || waitingTB1.Text == "" || waitingTB2.Text == ""
                || waitingTB3.Text == "" || waitingTB4.Text == "" 
                || processTB1.Text == "" || processTB2.Text == "" 
                || processTB3.Text == "" || processTB4.Text == ""
                
                )
            {
                MessageBox.Show("There is a missing value(s)", "SJF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                string[] process = new string[] {
            processTB1.Text,
            processTB2.Text,
            processTB3.Text,
            processTB4.Text
            };

                double[] burst = new double[]
                {
                Convert.ToDouble(burstTB1.Text),
                Convert.ToDouble(burstTB2.Text),
                Convert.ToDouble(burstTB3.Text),
                Convert.ToDouble(burstTB4.Text)
                };

                double[] arrival = new double[]
                {
                Convert.ToDouble(arrivalTB1.Text),
                Convert.ToDouble(arrivalTB2.Text),
                Convert.ToDouble(arrivalTB3.Text),
                Convert.ToDouble(arrivalTB4.Text)
                };

                // sorting the process Ascending to the burst time
                string swapProcess;
                double swapArrive, swapBurst;
                for (int i = 0; i < burst.Length - 1; i++)
                {
                    for (int j = i + 1; j < burst.Length; j++)
                    {
                        if (arrival[i] > arrival[j])
                        {
                            // arrival time sort
                            swapArrive = arrival[i];
                            arrival[i] = arrival[j];
                            arrival[j] = swapArrive;

                            // burst time sort
                            swapBurst = burst[i];
                            burst[i] = burst[j];
                            burst[j] = swapBurst;

                            // process names sort
                            swapProcess = process[i];
                            process[i] = process[j];
                            process[j] = swapProcess;
                        }
                    }
                }

                // process 1 in gantt chart
                timeLbl1.Text = "0";
                pLbl1.Text = process[0];
                timeLbl2.Text = Convert.ToString(burst[0]);

                for (int i = 1; i < burst.Length - 1; i++)
                {
                    for (int j = i + 1; j < burst.Length; j++)
                    {
                        if (burst[i] > burst[j])
                        {
                            // arrival time sort
                            swapArrive = arrival[i];
                            arrival[i] = arrival[j];
                            arrival[j] = swapArrive;

                            // burst time sort
                            swapBurst = burst[i];
                            burst[i] = burst[j];
                            burst[j] = swapBurst;

                            // process names sort
                            swapProcess = process[i];
                            process[i] = process[j];
                            process[j] = swapProcess;
                        }
                    }
                }
                double[] times = new double[]
                {
                0,
                burst[0],
                burst[0] + burst[1],
                burst[0] + burst[1] + burst[2],
                burst[0] + burst[1] + burst[2] + burst[3]
                };
                // process 2
                timeLbl3.Text = Convert.ToString(times[1]);
                pLbl2.Text = process[1];
                timeLbl4.Text = Convert.ToString(times[2]);

                // process 3 
                timeLbl5.Text = Convert.ToString(times[2]);
                pLbl3.Text = process[2];
                timeLbl6.Text = Convert.ToString(times[3]);

                // process 4
                timeLbl7.Text = Convert.ToString(times[3]);
                pLbl4.Text = process[3];
                timeLbl8.Text = Convert.ToString(times[4]);
                double[] waitingTime = new double[]
                {
                0 - arrival[0],
                times[1] - arrival[1],
                times[2] - arrival[2],
                times[3] - arrival[3]
                };

                // waiting times
                waitingTB1.Text = Convert.ToString(process[0] + " : " + 0 + " - " + arrival[0] + " = " + waitingTime[0]);
                waitingTB2.Text = Convert.ToString(process[1] + " : " + times[1] + " - " + arrival[1] + " = " + waitingTime[1]);
                waitingTB3.Text = Convert.ToString(process[2] + " : " + times[2] + " - " + arrival[2] + " = " + waitingTime[2]);
                waitingTB4.Text = Convert.ToString(process[3] + " : " + times[3] + " - " + arrival[3] + " = " + waitingTime[3]);
                double average = (waitingTime[0] + waitingTime[1] + waitingTime[2] + waitingTime[3]) / process.Length;
                valuelbl.Text =
                    Convert.ToString("( " + waitingTime[0] + " + " + waitingTime[1] +
                    " + " + waitingTime[2] + " + " + waitingTime[3] +
                    " ) / " + process.Length + " = " + average);

                valuelbl.Visible = true;
                awtlbl.Visible = true;
            }
            
        }

        private void nonPreemptive_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void burstTB1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
