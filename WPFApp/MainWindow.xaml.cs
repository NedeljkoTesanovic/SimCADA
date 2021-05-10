using DataConcentrator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static DataConcentratorManager DCManager { get; set; }
        public DI SignalDI { get; set; }
        public AI SignalAI { get; set; }
        public DO SignalDO { get; set; }
        public AO SignalAO { get; set; }
        public Alarm Alarm { get; set; }
        public SignalAlarmLinker Link { get; set; }
        public ObservableCollection<string> Logs {get; set;}
            public MainWindow()
        {
            InitializeComponent();
            Startup();
        }
        public void Startup()
        {
            DCManager = new DataConcentratorManager();
            dtgd_DI.ItemsSource = DCManager.Context.SignalsDI.Local;
            dtgd_AI.ItemsSource = DCManager.Context.SignalsAI.Local;
            dtgd_DO.ItemsSource = DCManager.Context.SignalsDO.Local;
            dtgd_AO.ItemsSource = DCManager.Context.SignalsAO.Local;
            dtgd_Alarms.ItemsSource = DCManager.Context.Alarms.Local;
            dtgd_Links.ItemsSource = DCManager.Context.Links.Local;
            Logs = new ObservableCollection<string>() {"-=|   Alarm Messages will appear here    |=-"};
            dtgd_Logs.ItemsSource = Logs;
            DCManager.AlarmRaised += OnAlarmRaised;
            this.DataContext = this;
        }

        public void OnAlarmRaised(string signalTag, Alarm alarm, DateTime timestamp)
        {
            Dispatcher.Invoke(() => { Logs.Insert(0, $"{timestamp} : Alarm {alarm.Name} raised from signal {signalTag}\n\n{alarm.Msg}"); });
        }
        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            //            Application.Current.Shutdown();
            DCManager.Context.SaveChanges();
            DCManager.StopThreads();
            //DCManager.Context.Dispose();
            Environment.Exit(0);
        }

        private void Btn_New_Input_Click(object sender, RoutedEventArgs e)
        {
            WindowAddInput win_NewInput = new WindowAddInput();
            win_NewInput.ShowDialog();
        }

        /*private void WindowClosing(Object sender, CancelEventArgs e)
        {
            DCManager.Context.SaveChanges();
            DCManager.StopThreads();
            DCManager.Context.Dispose();
            Environment.Exit(0);
        }*/

        private void Btn_Delete_Input_Click(object sender, RoutedEventArgs e)
        {
            if(TabControl.SelectedIndex == 0) //Analogue
            {

                if (dtgd_AI.SelectedItem != null)
                {
                    var result = MessageBox.Show("Are you sure? This action will also delete any links for this signal and cannot be undone!", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if(result == MessageBoxResult.Yes)
                        MessageBox.Show(DCManager.RemoveSignal((AI)dtgd_AI.SelectedItem));
                }
                else
                {
                    MessageBox.Show("Please select an input!");
                    return;
                }
                DCManager.Context.SaveChanges();
            }
            else //Digital
            {
                if (dtgd_DI.SelectedItem != null)
                {
                   var result = MessageBox.Show("Are you sure? This action cannot be undone!", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                        MessageBox.Show(DCManager.RemoveSignal((DI)dtgd_DI.SelectedItem));
                }
                else
                {
                    MessageBox.Show("Please select an input!");
                    return;
                }
                DCManager.Context.SaveChanges();
            }
        }

        private void Btn_New_Output_Click(object sender, RoutedEventArgs e)
        {
            WindowAddOutput win_NewOutput = new WindowAddOutput();
            win_NewOutput.ShowDialog();
        }
        private void Btn_Delete_Output_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.SelectedIndex == 0)
            {
                if (dtgd_AO.SelectedItem != null)
                {
                    var result = MessageBox.Show("Are you sure? This action cannot be undone!", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                        MessageBox.Show(DCManager.RemoveSignal((AO)dtgd_AO.SelectedItem));
                }
                else
                {
                    MessageBox.Show("Please select an input!");
                    return;
                }
            } else
            {
                if (dtgd_DO.SelectedItem != null)
                {
                   var result = MessageBox.Show("Are you sure? This action cannot be undone!", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes) 
                        MessageBox.Show(DCManager.RemoveSignal((DO)dtgd_DO.SelectedItem));
                }
                else
                {
                    MessageBox.Show("Please select an input!");
                    return;
                }
            }
                
        }

        private void Btn_Link_Click(object sender, RoutedEventArgs e)
        {
            if (dtgd_Alarms.SelectedItem != null)
            {
                WindowLinkAlarm wla = new WindowLinkAlarm((Alarm)dtgd_Alarms.SelectedItem);
                wla.ShowDialog();
                return;
            }
            MessageBox.Show("Please select an alarm!");
        }

        private void Btn_Unlink_Click(object sender, RoutedEventArgs e)
        {
            if (dtgd_Links.SelectedItem != null)
            {
                SignalAlarmLinker link = (SignalAlarmLinker)dtgd_Links.SelectedItem;
                var result = MessageBox.Show("Are you sure? This action cannot be undone!", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) 
                    MessageBox.Show(DCManager.UnlinkAlarmFromSignal(DCManager.Context.Alarms.Find(link.AlarmName), DCManager.Context.SignalsAI.Find(link.SignalTag)));
            }
            else
            {
                MessageBox.Show("Please select a link!");
                return;
            }
        }

        private void Btn_Clear_Console_click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => { Logs.Clear(); Logs.Add("-=|   Alarm Messages will appear here    |=-"); });
        }

        private void Btn_Edit_Output_click(object sender, RoutedEventArgs e)
        {
            WindowEditOutput weo;
            if (TabControl.SelectedIndex == 1)
            {
                if(dtgd_DO.SelectedItem != null)
                weo = new WindowEditOutput(TabControl.SelectedIndex, (DO)dtgd_DO.SelectedItem);
                else
                {
                    MessageBox.Show("Please select an output!");
                    return;
                }    
            }
            else 
            {
                if (dtgd_AO.SelectedItem != null)
                    weo = new WindowEditOutput(TabControl.SelectedIndex, (AO)dtgd_AO.SelectedItem);
                else
                {
                    MessageBox.Show("Please select an output!");
                    return;
                }
            }
            weo.ShowDialog();
        }

        private void Btn_New_Alarm_Click(object sender, RoutedEventArgs e)
        {
            WindowAddAlarm waa = new WindowAddAlarm();
            waa.ShowDialog();
        }

        private void Btn_Delete_Alarm_Click(object sender, RoutedEventArgs e)
        {

                if (dtgd_Alarms.SelectedItem != null)
                {
                    var result = MessageBox.Show("Are you sure? This action will also delete any links with this alarm and cannot be undone!", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                        MessageBox.Show(DCManager.RemoveAlarm((Alarm)dtgd_Alarms.SelectedItem));
                }
                else
                {
                    MessageBox.Show("Please select an alarm!");
                    return;
                }
        }
    }
}
