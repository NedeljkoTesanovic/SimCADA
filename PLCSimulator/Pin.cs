using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator
{
    public enum SignalType
    {
        DI, AI, DO, AO
    }
    class Pin
    {
        public string Adress { get; set; }
        public double Value { get; set; }
        public SignalType Type { get; set; }
        public bool Taken { get; set; }
        public Pin(string s, double v, SignalType st, bool t)
        {
            Adress = s;
            Value = v;
            Type = st;
            Taken = t;
        }
    }
}
