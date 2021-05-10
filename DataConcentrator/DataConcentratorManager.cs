using PLCSimulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataConcentrator
{
    
    public sealed class DataConcentratorManager
    {
        public event Action<string, Alarm, DateTime> AlarmRaised;
        private static DataConcentratorManager instance = null;
        private static readonly object padlock = new object();

        public static DataConcentratorManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataConcentratorManager();
                    }
                    else
                        instance.Load();
                    return instance;
                }
            }
        }
        public PLCManager PLC { get; set; }
        public SignalContext Context { get; set; }
        public Dictionary<string, Thread> SignalThreads { get; set; }

        public DataConcentratorManager()
        {
            PLC = new PLCManager();
            PLC.StartPLCSimulator();
            Load();
        }

        public void Load()
        {
            if (Context == null)
                Context = new SignalContext();
            Context.SignalsDI.Load();
            Context.SignalsAI.Load();
            Context.SignalsDO.Load();
            Context.SignalsAO.Load();
            Context.Alarms.Load();
            Context.Links.Load();
            SignalThreads = new Dictionary<string, Thread>();
            TakePins();
            StartThreads();
        }

        public void TakePins()
        {
            foreach (DI signal in Context.SignalsDI)
            {
                PLC.TakePin(signal.Address);
            }
            foreach (AI signal in Context.SignalsAI)
            {
                PLC.TakePin(signal.Address);
            }
            foreach (DO signal in Context.SignalsDO)
            {
                PLC.TakePin(signal.Address);
                PLC.WritePin(signal.Address, signal.Val);
            }
            foreach (AO signal in Context.SignalsAO)
            {
                PLC.TakePin(signal.Address);
                PLC.WritePin(signal.Address, signal.Val);
            }
        }

        #region Signal methods
        public string AddSignal(DI signal)
        {
            try
            {
                Context.SignalsDI.Add(signal);
                Context.SaveChanges();
                PLC.TakePin(signal.Address);
            } catch (Exception ex)
            {
                try
                {
                    Context.SignalsDI.Remove(signal);
                    Context.SaveChanges();
                    PLC.ReleasePin(signal.Address);
                } catch (Exception ex2)
                {
                    return "Signal not added! Error1 info: \n" + ex.StackTrace + "\n\nError2 info:\n" + ex2.StackTrace;
                }
                return "Signal not added! Error1 info: \n" + ex.StackTrace;
            }
            try
            {
                SignalThreads.Add(signal.Tag, new Thread(ScanDI));
                SignalThreads[signal.Tag].Start(signal);
            } catch (Exception ex)
            {
                try
                {
                    if (SignalThreads.ContainsKey(signal.Tag))
                        SignalThreads.Remove(signal.Tag);
                } catch (Exception ex2)
                {
                    return "Signal not added! (Thread breakage) Error1 info: \n" + ex.StackTrace + "\n\nError2 info:\n" + ex2.StackTrace;
                }
                return "Signal not added! Error1 info: \n" + ex.StackTrace;
            }
            return $"DI Signal {signal.Tag} successfully added on pin {signal.Address}!\n";
        }
        public string AddSignal(AI signal)
        {
            try
            {
                Context.SignalsAI.Add(signal);
                Context.SaveChanges();
                PLC.TakePin(signal.Address);
            }
            catch (Exception ex)
            {
                try
                {
                    Context.SignalsAI.Remove(signal);
                    Context.SaveChanges();
                    PLC.ReleasePin(signal.Address);
                }
                catch (Exception ex2)
                {
                    return "Signal not added! Error1 info: \n" + ex.StackTrace + "\n\nError2 info:\n" + ex2.StackTrace;
                }
                return "Signal not added! Error1 info: \n" + ex.StackTrace;
            }
            try
            {
                SignalThreads.Add(signal.Tag, new Thread(ScanAI));
                SignalThreads[signal.Tag].Start(signal);
            }
            catch (Exception ex)
            {
                try
                {
                    if (SignalThreads.ContainsKey(signal.Tag))
                        SignalThreads.Remove(signal.Tag);
                }
                catch (Exception ex2)
                {
                    return "Signal not added! (Thread breakage) Error1 info: \n" + ex.StackTrace + "\n\nError2 info: \n" + ex2.StackTrace;
                }
                return "Signal not added! Error1 info: \n" + ex.StackTrace;
            }
            return $"AI Signal {signal.Tag} successfully added on pin {signal.Address}!\n";
        }
        public string AddSignal(DO signal)
        {
            try
            {
                Context.SignalsDO.Add(signal);

                Context.SaveChanges();
                PLC.TakePin(signal.Address);
            }
            catch (Exception ex)
            {
                try 
                {
                    Context.SignalsDO.Remove(signal);
                    Context.SaveChanges();
                    PLC.ReleasePin(signal.Address);
                }
                catch(Exception ex2)
                {
                    return "Signal not added! (Thread breakage) Error1 info: \n" + ex.StackTrace + "\n\nError2 info: \n" + ex2.StackTrace;
                }
                return "Signal not added! Error1 info: \n" + ex.StackTrace;
            }
            
            return $"DO Signal {signal.Tag} successfully added on pin {signal.Address}!\n";
        }
        public string AddSignal(AO signal)
        {
            try
            {
                Context.SignalsAO.Add(signal);
                Context.SaveChanges();
                PLC.TakePin(signal.Address);
            }
            catch (Exception ex)
            {
                try
                {
                    Context.SignalsAO.Remove(signal);
                    Context.SaveChanges();
                    PLC.ReleasePin(signal.Address);
                }
                catch (Exception ex2)
                {
                    return "Signal not added! (Thread breakage) Error1 info: \n" + ex.StackTrace + "\n\nError2 info: \n" + ex2.StackTrace;
                }
                return "Signal not added! Error1 info: \n" + ex.StackTrace;
            }

            return $"AO Signal {signal.Tag} successfully added on pin {signal.Address}!\n";
        }
        public string RemoveSignal(DI signal)
        {
            try
            {
                Context.SignalsDI.Remove(signal);
                PLC.ReleasePin(signal.Address);
                SignalThreads[signal.Tag].Abort();
                SignalThreads.Remove(signal.Tag);
                Context.SaveChanges();
            } catch (Exception ex)
            {
                return "Signal not removed! Error info: \n" + ex.StackTrace;
            }
            return $"Successfully removed signal {signal.Tag} from {signal.Address}";
        }
        public string RemoveSignal(AI signal)
        {
            try
            {
                Context.SignalsAI.Remove(signal);
                SignalThreads[signal.Tag].Abort();
                SignalThreads.Remove(signal.Tag);
                PLC.ReleasePin(signal.Address);
                foreach (SignalAlarmLinker link in Context.Links)
                {
                    if (link.SignalTag == signal.Tag)
                        Context.Links.Remove(link);
                }
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Signal not removed! Error info: \n" + ex.StackTrace;
            }
            return $"Successfully removed signal {signal.Tag} from {signal.Address}";
        }
        public string RemoveSignal(DO signal)
        {
            try
            {
                Context.SignalsDO.Remove(signal);
                PLC.ReleasePin(signal.Address);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Signal not removed! Error info: \n" + ex.StackTrace;
            }
            return $"Successfully removed signal {signal.Tag} from {signal.Address}";
        }
        public string RemoveSignal(AO signal)
        {
            try
            {
                Context.SignalsAO.Remove(signal);
                PLC.ReleasePin(signal.Address);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Signal not removed! Error info: \n" + ex.StackTrace;
            }
            return $"Successfully removed signal {signal.Tag} from {signal.Address}";
        }
        public void ScanDI(object obj)
        {
            while (true)
            {
                DI signal = (DI)obj;
                signal.Val = PLC.ReadPin(signal.Address);
                Thread.Sleep(signal.ScanTime);
            }
        }

        public void ScanAI(object obj)
        {
            List<string> ActiveAlarms = new List<string>();
            Context.Links.Load();
            while (true)
            {
                AI signal = (AI)obj;
                signal.Val = PLC.ReadPin(signal.Address);
                object padlock = new object();
                lock (padlock)
                {
                    if (signal.Links.Count() != 0)
                    {
                        int dangerLevel;
                        int highestDanger = 0;
                        foreach (SignalAlarmLinker link in signal.Links)
                        {
                            Alarm alarm = null;
                            try
                            {
                                alarm = Context.Alarms.Find(link.AlarmName);
                            } catch (Exception ex)
                            {
                                Trace.WriteLine("Uh oh... Alarm context broke!\n" + ex.Message);
                            }
                            
                            if (alarm == null)
                                continue;
                            if ((dangerLevel = alarm.CheckAlarm(signal.Val)) == 2)
                            {
                                if (!ActiveAlarms.Contains(alarm.Name))
                                {
                                    DateTime t = DateTime.Now;
                                    link.LastActive = t;
                                    try
                                    {
                                        Context.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        Trace.WriteLine("Uh oh, something broke whilst scanning for alarms!!\n"+ ex.StackTrace);
                                    }
                                    AlarmRaised?.Invoke(signal.Tag, alarm, t);
                                    ActiveAlarms.Add(alarm.Name);
                                }
                            }
                            else
                            {
                                if (ActiveAlarms.Contains(alarm.Name))
                                    ActiveAlarms.Remove(alarm.Name);
                            }
                            if (dangerLevel > highestDanger)
                                highestDanger = dangerLevel;
                        }
                        signal.Critical = highestDanger;
                    }
                    else
                        signal.Critical = 0; //no alarms, ergo safe
                }
                Thread.Sleep(signal.ScanTime);
            }
        }
        #endregion

        #region Alarm methods
        public string AddAlarm(Alarm alarm)
        {
            try
            {
                Context.Alarms.Add(alarm);
                Context.SaveChanges();
            } catch (Exception ex)
            {
                try
                {
                    Context.Alarms.Remove(alarm);
                    Context.SaveChanges();
                } catch (Exception ex2)
                {
                    return "Failed to add Alarm! (Removal breakage) Error info:\n" + ex.StackTrace + "\n\nError2 info:\n"+ ex2.StackTrace;
                }
                return "Failed to add Alarm! Error info:\n" + ex.StackTrace;
            }
            return "Successfully added alarm!";
        }
        public string RemoveAlarm(Alarm alarm)
        {
            try
            {
                foreach(SignalAlarmLinker link in Context.Links)
                {
                    if (link.AlarmName == alarm.Name)
                        Context.Links.Remove(link);
                }
                Context.Alarms.Remove(alarm);
                Context.SaveChanges();
            } catch (Exception ex)
            {
                return "Failed to add Alarm! Error info:\n" + ex.StackTrace;
            }
            return "Successfully removed alarm!";
        }

        public string LinkAlarmToSignal(Alarm alarm, AI signal)
        {
            try
            {
                SignalAlarmLinker link = new SignalAlarmLinker(signal.Tag, alarm.Name);
                signal.Links.Add(link);
                alarm.Links.Add(link);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return $"Failed to link {alarm.Name} to {signal.Tag}! Error info:\n{ex.StackTrace}";
            }
            return $"Successfully linked {alarm.Name} to {signal.Tag}!";
        }
        public string UnlinkAlarmFromSignal(Alarm alarm, AI signal)
        {
            try
            {
                SignalAlarmLinker link = Context.Links.Find(signal.Tag, alarm.Name);
                signal.Links.Remove(link);
                alarm.Links.Remove(link);
                Context.Links.Remove(link);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return $"Failed to unlink {alarm.Name} from {signal.Tag}! Error info:\n{ex.StackTrace}";
            }
            return $"Successfully unlinked {alarm.Name} from {signal.Tag}!";
        }
        #endregion
        #region Thread methods
        public void StartThreads()
        {   
            foreach (DI signal in Context.SignalsDI)
            {
                Thread t = new Thread(ScanDI);
                SignalThreads.Add(signal.Tag, t);
                t.Start(Context.SignalsDI.Find(signal.Tag));
            }
            foreach (AI signal in Context.SignalsAI)
            {
                Thread t = new Thread(ScanAI);
                SignalThreads.Add(signal.Tag, t);
                t.Start(Context.SignalsAI.Find(signal.Tag));
            }
        }
        public void StopThreads()
        {
            foreach(KeyValuePair<string, Thread> pair in SignalThreads)
            {
                pair.Value.Abort();
                Trace.WriteLine($"Killed thread {pair.Key}");
            }
        }
        #endregion
    }   
}
