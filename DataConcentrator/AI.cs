using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class AI : INotifyPropertyChanged
    {
        private string tag;
        private string description;
        private string adress;
        private double val;
        private int scantime;
        private string unit;
        private int critical; //0 - Safe; 1 - Warning; 2 - Danger
        public virtual List<SignalAlarmLinker> Links { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AI()
        {
            Links = new List<SignalAlarmLinker>();
        }
        public AI(string t, string d, string a, int s, double v)
        {
            Tag = t;
            Description = d;
            Address = a;
            Val = v;
            ScanTime = s;
            Critical = 0;
            Links = new List<SignalAlarmLinker>();
        }

        [Key]
        public string Tag
        {
            get { return tag; }
            set
            {
                tag = value;
                OnPropertyChanged("Tag");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public int Critical
        {
            get
            {
                return critical;
            }
            set
            {
                critical = value;
                OnPropertyChanged("Critical");
            }
        }

        public string Address
        {
            get { return adress; }
            set
            {
                adress = value;
                OnPropertyChanged("Address");
            }
        }

        public int ScanTime
        {
            get { return scantime; }
            set
            {
                scantime = value;
                OnPropertyChanged("ScanTime");
            }
        }

        public double Val
        {
            get { return val; }
            set
            {
                val = value;
                OnPropertyChanged("Val");
            }
        }

        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged("Unit");
            }
        }
        private void OnPropertyChanged(string arg)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(arg));
        }
    }
}
