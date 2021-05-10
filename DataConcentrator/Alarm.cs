using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class Alarm: INotifyPropertyChanged
    {

        private string name;
        private double threshold;
        private bool activehigh; // 0 - The Alarm will activate if the signal value drops below the threshold; 1 - if it rises above the threshold
        private string msg;
        public virtual List<SignalAlarmLinker> Links { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string arg)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(arg));
        }
        [Key]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public double Threshold
        {
            get
            {
                return threshold;
            }
            set
            {
                threshold = value;
                OnPropertyChanged("Threshold");
            }
        }
        public bool ActiveHigh
        {
            get
            {
                return activehigh;
            }
            set
            {
                activehigh = value;
                OnPropertyChanged("ActiveHigh");
            }
        }
        public string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
                OnPropertyChanged("Msg");
            }
        }

        public Alarm(string name, double threshold, bool activeHigh, string msg)
        {
            Name = name;
            Threshold = threshold;
            ActiveHigh = activeHigh;
            Msg = msg;
            Links = new List<SignalAlarmLinker>();
        }

        public Alarm() 
        {
            Links = new List<SignalAlarmLinker>();
        }

        public int CheckAlarm(double value)
        {
            if ((ActiveHigh && value > Threshold)||(!ActiveHigh && value < Threshold))
                return 2;
            else if (value == Threshold)
                return 1;
            return 0;
        }
    }
}
