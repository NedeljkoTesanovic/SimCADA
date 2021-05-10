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
    /// Interaction logic for WindowLinkAlarm.xaml
    /// </summary>
    public partial class WindowLinkAlarm : Window
    {
        private Alarm alarm;
        public WindowLinkAlarm(Alarm a)
        {
            InitializeComponent();
            dtgd_Signals.ItemsSource = MainWindow.DCManager.Context.SignalsAI.Local;
            alarm = a;
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (dtgd_Signals.SelectedItem == null)
                MessageBox.Show("No signal selected!");
            else
            {
                AI signal = ((AI)dtgd_Signals.SelectedItem);
                MessageBox.Show(MainWindow.DCManager.LinkAlarmToSignal(alarm, MainWindow.DCManager.Context.SignalsAI.Find(signal.Tag)));
                this.Close();
            }
        }
    }
}
