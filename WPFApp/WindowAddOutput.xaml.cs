using DataConcentrator;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for WindowAddOutput.xaml
    /// </summary>
    public partial class WindowAddOutput : Window
    {
        public AO NewOutput { get; set; }
        public WindowAddOutput()
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
            NewOutput = new AO();
            this.DataContext = NewOutput;
            Reset();
        }

        public void Reset()
        {
            rbtn_Digital.IsChecked = true;
            
            if(MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DO).Count() > 0)
            {
                cmbx_Pins.ItemsSource = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DO);
                cmbx_Pins.SelectedItem = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DO).First();
            }
                
            txbx_Desc.Text = "";
            txbx_InitVal.Text = "";
            txbx_Tag.Text = "";
            txbx_Units.Text = "";
            txbx_Units.IsEnabled = false;
            txbx_InitVal.BorderBrush = Brushes.Black;
            txbx_Tag.BorderBrush = Brushes.Black;
        }
        private bool Validate()
        {
            if (rbtn_Analogue.IsChecked == false)
            {
                if (txbx_InitVal.Text != "0" && txbx_InitVal.Text != "1")
                {
                    MessageBox.Show("InitVal must be either 0 or 1!");
                    txbx_InitVal.BorderBrush = Brushes.Red;
                    return false;
                }
            }
            else
            {
                if(!double.TryParse(txbx_InitVal.Text, out _))
                {
                    MessageBox.Show("InitVal must be a number!");
                    txbx_InitVal.BorderBrush = Brushes.Red;
                    return false;
                }
            }
            if (txbx_Tag.Text == "")
            {
                MessageBox.Show("Tag cannot be empty!");
                txbx_Tag.BorderBrush = Brushes.Red;
                return false;
            }
            if (cmbx_Pins.SelectedItem == null)
            {
                MessageBox.Show("Address not selected!");
                return false;
            }
            return true;
        }
        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;
            NewOutput.Val = NewOutput.InitVal;
            if (rbtn_Analogue.IsChecked == false)
                MessageBox.Show(MainWindow.DCManager.AddSignal(new DO(NewOutput.Tag, NewOutput.Description, NewOutput.Address, NewOutput.InitVal, NewOutput.InitVal)));
            else
                MessageBox.Show(MainWindow.DCManager.AddSignal(NewOutput));
            this.Close();
        }

        private void Rbtn_Digital_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DO).Count() > 0)
            {
                cmbx_Pins.ItemsSource = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DO);
                cmbx_Pins.SelectedItem = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DO).First();
            }
            else
                cmbx_Pins.ItemsSource = null;
            txbx_Units.IsEnabled = false;
        }

        private void Rbtn_Analogue_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.AO).Count() > 0)
            {
                cmbx_Pins.ItemsSource = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.AO);
                cmbx_Pins.SelectedItem = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.AO).First();
            }
            else
                cmbx_Pins.ItemsSource = null;
            txbx_Units.IsEnabled = true;
        }
    }
}
