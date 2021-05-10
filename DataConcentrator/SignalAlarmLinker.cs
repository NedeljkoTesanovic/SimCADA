using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class SignalAlarmLinker
    {
        [Key, Column(Order = 1)]
        public string SignalTag { get; set; }
        [Key, Column(Order = 2)]
        public string AlarmName { get; set; }
        public DateTime? LastActive { get; set; }

        public SignalAlarmLinker(string signal, string alarm)
        {
            SignalTag = signal;
            AlarmName = alarm;
        }
        public SignalAlarmLinker() { }
    }
}
