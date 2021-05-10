using DataConcentrator;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AddAlarmWindow.xaml
    /// </summary>
    public partial class AddAlarmWindow : Window
    {
        public Alarm newAlarm = new Alarm();
        public AddAlarmWindow()
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
            this.DataContext = newAlarm;
            //cmbx_Signals.ItemsSource = 
            
        }
        public void Reset()
        {
            this.txb_Name.Text = "New Alarm";
            this.txb_trigger.Text = "Threshold";
            this.txb_alarmMessage.Text = "Alarm message";
            this.chbx_activeHigh.IsChecked = false;
            this.cmbx_Signals.SelectedItem = null;
        }
        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
