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
    /// Interaction logic for WindowEditOutput.xaml
    /// </summary>
    public partial class WindowEditOutput : Window
    {
        bool Analogue { get; set; }
        DO DSignal { get; set; }
        AO ASignal { get; set; }
        public WindowEditOutput(int arg, DO signal)
        {
            InitializeComponent();
            Analogue = arg == 0 ? true : false;
            DSignal = MainWindow.DCManager.Context.SignalsDO.Find(signal.Tag);
            txbx_Val.Text = signal.Val.ToString();

        }
        public WindowEditOutput(int arg, AO signal)
        {
            InitializeComponent();
            ASignal = MainWindow.DCManager.Context.SignalsAO.Find(signal.Tag);
            txbx_Val.Text = signal.Val.ToString();
            Analogue = arg == 0 ? true : false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double retval = 0;
            if (Analogue)
            {
                if (!double.TryParse(txbx_Val.Text, out retval))
                {
                    MessageBox.Show("Val must be a number!");
                    return;
                }
                MainWindow.DCManager.Context.SignalsAO.Find(ASignal.Tag).Val = retval;
                MainWindow.DCManager.PLC.WritePin(ASignal.Address, retval);
            }
            else
            {
                if (txbx_Val.Text != "0" && txbx_Val.Text != "1")
                {
                    MessageBox.Show("Val must be either 0 or 1!");
                    return;
                }
                MainWindow.DCManager.Context.SignalsDO.Find(DSignal.Tag).Val = retval;
                MainWindow.DCManager.PLC.WritePin(DSignal.Address, retval);
            }
            
            MainWindow.DCManager.Context.SaveChanges();
            this.Close();
        }
    }
}
