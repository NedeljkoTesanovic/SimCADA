using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataConcentrator;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for WindowAddInput.xaml
    /// </summary>
    public partial class WindowAddInput : Window
    {
        private AI NewInput = new AI();
        public WindowAddInput()
        {
            InitializeComponent();
            this.DataContext = NewInput;
            Reset();
        }
        private bool Validate()
        {
            int retval;
            int.TryParse(txbx_ScanTime.Text, out retval);
            if (retval <= 0)
                {
                    MessageBox.Show("Scan Time must be a positive integer!");
                    txbx_ScanTime.BorderBrush = Brushes.Red;
                    return false;
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
        private void Reset()
        {
            rbtn_Digital.IsChecked = true;
            if (MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DI).Count() > 0)
            {
                cmbx_Pins.ItemsSource = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DI);
                cmbx_Pins.SelectedItem = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DI).First();
            }
            txbx_Desc.Text = "";
            txbx_ScanTime.Text = "";
            txbx_Tag.Text = "";
            txbx_Units.Text = "";
            txbx_Tag.BorderBrush = Brushes.Black;
            txbx_ScanTime.BorderBrush = Brushes.Black;
            txbx_Units.IsEnabled = false;
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;
            if (rbtn_Analogue.IsChecked == true)
                 MessageBox.Show(MainWindow.DCManager.AddSignal(NewInput));
            else
                MessageBox.Show(MainWindow.DCManager.AddSignal(new DI(NewInput.Tag, NewInput.Description, NewInput.Address, NewInput.ScanTime, NewInput.Val)));
            this.Close();
        }

        private void Rbtn_Digital_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DI).Count() > 0)
            {
                cmbx_Pins.ItemsSource = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DI);
                cmbx_Pins.SelectedItem = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.DI).First();
            }
            else
                cmbx_Pins.ItemsSource = null;
            txbx_Units.IsEnabled = false;
        }

        private void Rbtn_Analogue_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.AI).Count() > 0)
            {
                cmbx_Pins.ItemsSource = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.AI);
                cmbx_Pins.SelectedItem = MainWindow.DCManager.PLC.GetPins(PLCSimulator.SignalType.AI).First();
            }
            else
                cmbx_Pins.ItemsSource = null;
            txbx_Units.IsEnabled = true;
        }
    }
}
