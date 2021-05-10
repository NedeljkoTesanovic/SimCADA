using DataConcentrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for WindowAddAlarm.xaml
    /// </summary>
    public partial class WindowAddAlarm : Window
    {
        private Alarm alarm = new Alarm();
        public WindowAddAlarm()
        {
            InitializeComponent();
            this.DataContext = alarm;
            Reset();
        }
        private void Reset()
        {
            txbx_Name.Text = "";
            txbx_Name.BorderBrush = Brushes.Black;
            txbx_Msg.Text = "";
            txbx_Threshold.Text = "";
            txbx_Threshold.BorderBrush = Brushes.Black;
        }
        private bool Validate()
        {
            if (txbx_Name.Text == "")
            {
                MessageBox.Show("Name field must not be empty!");
                txbx_Name.BorderBrush = Brushes.Red;
                return false;
            }
            if (txbx_Threshold.Text == "")
            {
                MessageBox.Show("Threshold field must not be empty!");
                txbx_Threshold.BorderBrush = Brushes.Red;
                return false;
            } else if (!int.TryParse(txbx_Threshold.Text, out _))
            {
                MessageBox.Show("Threshold field must a number!");
                txbx_Threshold.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }
        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
          if (!Validate())
                return;

            MainWindow.DCManager.AddAlarm(alarm);
            this.Close();
            return;
        }
    }
}

