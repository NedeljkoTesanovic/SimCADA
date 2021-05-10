using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class DO : INotifyPropertyChanged
    {
        private string tag;
        private string description;
        private string adress;
        private double val;
        private double initVal;

        public event PropertyChangedEventHandler PropertyChanged;

        public DO() { }

        public DO(string t, string d, string a, double iv , double v)
        {
            Tag = t;
            Description = d;
            Address = a;
            Val = v;
            initVal = iv;
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

        public double Val
        {
            get { return val; }
            set
            {
                val = value;
                OnPropertyChanged("Val");
            }
        }

        public double InitVal
        {
            get { return initVal; }
            set
            {
                initVal = value;
                OnPropertyChanged("InitVal");
            }
        }
        private void OnPropertyChanged(string arg)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(arg));
        }
    }
}
