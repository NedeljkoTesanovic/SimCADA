using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class SignalContext : DbContext
    {
        public DbSet<DI> SignalsDI { get; set; }
        public DbSet<AI> SignalsAI { get; set; }
        public DbSet<DO> SignalsDO { get; set; }
        public DbSet<AO> SignalsAO { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<SignalAlarmLinker> Links { get; set; } //logs
    }
}
