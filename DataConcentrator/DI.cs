using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class DI : INotifyPropertyChanged
    {
        private string tag;
        private string description;
        private string adress;
        private double val;
        private int scantime;

        public event PropertyChangedEventHandler PropertyChanged;

        public DI() { }

        public DI(string t, string d, string a, int s, double v)
        {
            Tag = t;
            Description = d;
            Address = a;
            Val = v;
            ScanTime = s;
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
        private void OnPropertyChanged(string arg)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(arg));
        }
    }
}
