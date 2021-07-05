﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;

namespace FivePointNine.Windows.Controls
{
    public class SerialPortsComboBox : ComboBox
    {
        public event EventHandler USBDisconnected;
        public event EventHandler USBConnected;
        Timer ts, tp;
        public SerialPortsComboBox()
        {
            ContextMenuStrip = new ContextMenuStrip();
            ContextMenuStrip.Items.Add("Refresh");
            ContextMenuStrip.Items[0].Click += SerialPortsComboBox_Click;
            ts = new System.Windows.Forms.Timer();
            ts.Tick += Ts_Tick;
            ts.Interval = 30;
            ts.Enabled = true;
        }

        private void SerialPortsComboBox_Click(object sender, EventArgs e)
        {
            resumeSession();
        }

        public Form FindParentForm(Control c)
        {
            if (c == null)
                return null;
            if (c.Parent is Form) return (Form)c.Parent;
            return FindParentForm(c.Parent);
        }
        public void FormClosing()
        {
            tp.Enabled = false;
            usbmonitor.Stop();
        }
        private void Ts_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Enabled = false;
            usbmonitor.OnExternalUsbDetected += Usbmonitor_OnExternalUsbDetected;
            usbmonitor.OnExternalUsbRemoved += Usbmonitor_OnExternalUsbRemoved;
            usbmonitor.Start();
            UsbDisconnectedFlag = true;
            
            tp = new System.Windows.Forms.Timer();
            tp.Tick += Tp_Tick;
            tp.Interval = 30;
            tp.Enabled = true;

            if (FindParentForm(this) != null)
                FindParentForm(this).FormClosing += SerialPortsComboBox_FormClosing;
        }

        private void SerialPortsComboBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormClosing();
        }

        private void Tp_Tick(object sender, EventArgs e)
        {
            if (UsbConnectedFlag)
            {
                UsbConnectedFlag = false;
                resumeSession();
                USBConnected?.Invoke(this, null);
            }
            if (UsbDisconnectedFlag)
            {
                UsbDisconnectedFlag = false;
                resumeSession();
                USBDisconnected?.Invoke(this, null);
            }
        }

        private void Usbmonitor_OnExternalUsbRemoved(object sender, EventUsbMonitorEvent e)
        {
            UsbDisconnectedFlag = true;
        }

        private void Usbmonitor_OnExternalUsbDetected(object sender, EventUsbMonitorEvent e)
        {
            UsbConnectedFlag = true;
        }


        bool UsbConnectedFlag = false, UsbDisconnectedFlag = false;
        UsbMonitor usbmonitor = new UsbMonitor();
        int portNameScore(string port)
        {
            if (port.ToLower().Contains("arduino"))
                return 5;
            else if (port.ToLower().Contains("CH34"))
                return 4;
            else if (port.ToLower().Contains("prol"))
                return 3;
            else if (port.ToLower().Contains("communication"))
                return 2;
            else if (port.ToLower().Contains("serial"))
                return 1;
            else if (port.ToLower().Contains("intel"))
                return -1;
            else
                return 0;
        }
        Dictionary<string, int> portScores = new Dictionary<string, int>();
        private void resumeSession()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_PnPEntity");
                portScores = new Dictionary<string, int>();
                Items.Clear();
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["Caption"] == null) continue;
                    if (queryObj["Caption"].ToString().Contains("(COM"))
                    {
                        string str = queryObj["Caption"].ToString();

                        if (str.Contains("COM"))
                        {
                            string port = queryObj["Caption"].ToString();
                            Items.Add(port);
                            portScores.Add(port, portNameScore(port));
                        }
                    }
                }
                Text = BestPort;
            }
            catch
            {
            }
        }
        public string BestPort
        {
            get
            {
                int ind = portScores.Values.ToList().Max();
                ind = portScores.Values.ToList().IndexOf(ind);
                if (ind >= 0)
                    return portScores.Keys.ToList()[ind];
                return "";
            }
        }
        public string SelectedPort
        {
            get
            {
                try
                {
                    var address = Text.ToUpper();
                    if (address.Length < 4)
                        return "";
                    bool needsParsing = false;
                    if (!address.StartsWith("COM"))
                        needsParsing = true;
                    if (address.Length > 6)
                        needsParsing = true;

                    if (needsParsing)
                    {
                        address = address.Substring(address.IndexOf("(COM") + 1);
                        address = address.Substring(0, address.IndexOf(")"));
                    }
                    return address;
                }
                catch
                {
                    return "";
                }
            }
        }
    }
    public class EventUsbMonitorEvent : EventArgs
    {
        public string Model { get; set; }
        public string Drive { get; set; }
    }

    public class UsbMonitor
    {
        private const string _queryForEvents = @"SELECT * FROM __InstanceOperationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_PnPEntity'";


        private readonly ManagementEventWatcher _watcher;
        private System.Threading.Thread _thread;

        public UsbMonitor()
        {
            _watcher = new ManagementEventWatcher();
            var query = new WqlEventQuery(_queryForEvents);
            _watcher.EventArrived += Watcher_EventArrived;
            _watcher.Options.Timeout = new TimeSpan(30);
            _watcher.Query = query;
        }

        public void Start()
        {
            try
            {
                _watcher.Start();
                _thread = new System.Threading.Thread(Listen);
                _thread.Start();
            }
            catch { }
        }

        System.Threading.ManualResetEvent abortThread = new System.Threading.ManualResetEvent(false);
        bool abortThreadSignal = false;
        public void Stop()
        {
            abortThreadSignal = true;
            if (!abortThread.WaitOne(500))
            {
                try
                {
                    _thread.Abort();
                }
                catch { }
            }
            
            try
            {
                _watcher.Stop();
            }
            catch { }
        }

        private void Listen()
        {
            try
            {
                while (!abortThreadSignal)
                {
                    _watcher.WaitForNextEvent();
                }
                abortThread.Set();
            }
            catch { }
        }

        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            try
            {
                switch (e.NewEvent.ClassPath.ClassName)
                {
                    case "__InstanceCreationEvent":
                        if (OnExternalUsbDetected != null)
                            OnExternalUsbDetected(this, new EventUsbMonitorEvent { });
                        break;

                    case "__InstanceDeletionEvent":
                        if (OnExternalUsbRemoved != null)
                            OnExternalUsbRemoved(this, new EventUsbMonitorEvent { });
                        break;
                }
            }
            catch { };
        }

        public event EventHandler<EventUsbMonitorEvent> OnExternalUsbDetected;
        public event EventHandler<EventUsbMonitorEvent> OnExternalUsbRemoved;
    }
}
