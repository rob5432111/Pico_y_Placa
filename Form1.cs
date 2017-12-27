using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Pico_Placa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            if (Is_Valide())
            {
                bool p_y_p_date = false, p_y_p_time = false;
                string plate = txt_plate.Text;
                string s_number = string.Empty;
                byte b_number;

                foreach (char c in plate)
                {
                    s_number = c.ToString();
                }


                if (Byte.TryParse(s_number, out b_number))
                {
                    DateTime d_date = DateTime.Parse(txt_date.Text);
                    DayOfWeek dow = d_date.DayOfWeek;
                   
                    switch (b_number)
                    {
                        case 1:
                        case 2:
                            if (dow == DayOfWeek.Monday)
                                p_y_p_date = true;
                            break;

                        case 3:
                        case 4:
                            if (dow == DayOfWeek.Tuesday)
                                p_y_p_date = true;
                            break;

                        case 5:
                        case 6:
                            if (dow == DayOfWeek.Wednesday)
                                p_y_p_date = true;
                            break;

                        case 7:
                        case 8:
                            if (dow == DayOfWeek.Thursday)
                                p_y_p_date = true;
                            break;

                        case 9:
                        case 0:
                            if (dow == DayOfWeek.Friday)
                                p_y_p_date = true;
                            break;

                    }

                   

                    TimeSpan t_time = TimeSpan.Parse(txt_time.Text);
                    TimeSpan t_start1 = new TimeSpan(7, 0, 0);
                    TimeSpan t_end1 = new TimeSpan(9, 30, 0);
                    TimeSpan t_start2 = new TimeSpan(16, 0, 0);
                    TimeSpan t_end2 = new TimeSpan(19, 30, 0);
                    if (t_time >= t_start1 && t_time <= t_end1)
                    {
                        p_y_p_time = true;
                    }
                    else if (t_time >= t_start2 && t_time <= t_end2)
                    {
                        p_y_p_time = true;
                    }

                    if (p_y_p_date && p_y_p_time)                    
                        lbl_answ.Text = "The car with this plate number can't go on road";                    
                    else
                        lbl_answ.Text = "The car with this plate number can go on road. Drive safe!";                    
                }
                else
                    MessageBox.Show("The plate number is incorrect, please check the format", "Error");
            }
        }
        private bool Is_Valide()
        {
            bool ok = true;

            err_1.Clear();
            var dateFormats = new[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" };

            DateTime date;
            bool validDate = DateTime.TryParseExact(
                txt_date.Text,
                dateFormats,
                DateTimeFormatInfo.InvariantInfo,
                DateTimeStyles.None,
                out date);

            TimeSpan time;

            if (string.IsNullOrEmpty(txt_plate.Text))
            {

                err_1.SetError(txt_plate, "You must enter a plate number");
                ok = false;
            }
            else if (txt_plate.Text.Length < 6)
            {
                err_1.SetError(txt_plate, "The plate number must contain at least 6 characters");
                ok = false;
            }


            if (string.IsNullOrEmpty(txt_date.Text))
            {
                err_1.SetError(txt_date, "Please enter a valid date");
                ok = false;
            }
            else if (!validDate)
            {
                err_1.SetError(txt_date, "The date must be in the order day month year");
                ok = false;
            }


            if (string.IsNullOrEmpty(txt_time.Text))
            {
                err_1.SetError(txt_time, "Please enter a valid time");
                ok = false;
            }
            else
            {
                try
                {
                    time = TimeSpan.Parse(txt_time.Text);
                }
                catch (FormatException)
                {
                    err_1.SetError(txt_time, "The time is incorrect");
                    ok = false;
                }


            }

            return ok;
        }
    }
}
