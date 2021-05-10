using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLCSimulator
{
    /// <summary>
    /// PLC Simulator
    /// 
    /// 4 x ANALOG INPUT : ADDR001 - ADDR004
    /// 4 x ANALOG OUTPUT: ADDR005 - ADDR008
    /// 2 x DIGITAL INPUT: ADDR009 - ADDR010
    /// 2 x DIGITAL OUTPUT: ADDR011 - ADDR012
    /// </summary>
    public sealed class PLCManager
    {
        private static PLCManager instance = null;
        private static readonly object padlock = new object();

        public static PLCManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PLCManager();
                    }
                    return instance;
                }
            }
        }

        private Dictionary<string, Pin> addressValues;
        private readonly object locker = new object();
        public PLCManager()
        {
            addressValues = new Dictionary<string, Pin>
            {
                { "ADDR001", new Pin("ADDR001", 0, SignalType.AI, false) },
                { "ADDR002", new Pin("ADDR002", 0, SignalType.AI, false) },
                { "ADDR003", new Pin("ADDR003", 0, SignalType.AI, false) },
                { "ADDR004", new Pin("ADDR004", 0, SignalType.AI, false) },
                { "ADDR005", new Pin("ADDR005", 0, SignalType.AO, false) },
                { "ADDR006", new Pin("ADDR006", 0, SignalType.AO, false) },
                { "ADDR007", new Pin("ADDR007", 0, SignalType.AO, false) },
                { "ADDR008", new Pin("ADDR008", 0, SignalType.AO, false) },
                { "ADDR009", new Pin("ADDR009", 0, SignalType.DI, false) },
                { "ADDR010", new Pin("ADDR010", 1, SignalType.DI, false) },
                { "ADDR011", new Pin("ADDR011", 0, SignalType.DO, false) },
                { "ADDR012", new Pin("ADDR012", 0, SignalType.DO, false) }
            };

        }
        public void StartPLCSimulator()
        {
            Thread t1 = new Thread(GeneratingAnalogInputs);
            t1.Start();
            Thread t2 = new Thread(GeneratingDigitalInputs);
            t2.Start();
        }

        #region Pin methods
        public List<string> GetPins(SignalType st)
        {
            List<string> retVal = new List<string>();
            foreach(string address in addressValues.Keys)
            {
                if (addressValues[address].Type == st && addressValues[address].Taken == false)
                    retVal.Add(address);
            }
            return retVal;
        }

        public bool WritePin(string address, double value)
        {
            if (!addressValues.ContainsKey(address))
            {
                Trace.WriteLine($"Pin {address} does not exist!");
                return false;
            }

            addressValues[address].Value = value;
            Trace.WriteLine($"Successfully wrote {value} to pin {address}");
            return true;
        }
        public double ReadPin(string address)
        {
            if (!addressValues.ContainsKey(address))
            {
                Trace.WriteLine($"Pin {address} does not exist, returning -1!");
                return -1;
            }

            return addressValues[address].Value;
        }

        public void TakePin(string address)
        {
            addressValues[address].Taken = true;
            Trace.WriteLine($"Pin {address} now occupied!");
        }
        public void ReleasePin(string address)
        {
            addressValues[address].Taken = false;
            Trace.WriteLine($"Pin {address} now free!");
        }
        #endregion

        private void GeneratingAnalogInputs()
        {
            while (true)
            {
                Thread.Sleep(100);
                lock (locker)
                {
                    addressValues["ADDR001"].Value = 100 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI); //SINE
                    addressValues["ADDR002"].Value = 100 * DateTime.Now.Second / 60; //RAMP
                    addressValues["ADDR003"].Value = 50 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI) * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI); //Cos*Sin
                    addressValues["ADDR004"].Value = 10 + 75 * DateTime.Now.Second / 60 * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
                }
            }
        }
        private void GeneratingDigitalInputs()
        {
            while (true)
            {

                Thread.Sleep(100);
                lock (locker)
                {
                    if (addressValues["ADDR009"].Value == 0)
                    {
                        addressValues["ADDR009"].Value = 1;
                    }
                    else
                    {
                        addressValues["ADDR009"].Value = 0;
                    }
                    if (addressValues["ADDR010"].Value == 0)
                    {
                        addressValues["ADDR010"].Value = 1;
                    }
                    else
                    {
                        addressValues["ADDR010"].Value = 0;
                    }
                }
            }
        }
    }
}
