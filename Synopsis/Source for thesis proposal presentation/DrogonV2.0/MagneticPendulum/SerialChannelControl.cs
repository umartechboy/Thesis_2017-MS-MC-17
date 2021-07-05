﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using FivePointNine.Windows.IO;
using System.Drawing;

namespace FivePointNine.Windows.Controls
{

    delegate void PacketCommandRecieveHandler(PacketCommand command);
    class SerialChannelControl:UserControl
    {
        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event PacketCommandRecieveHandler PacketReceived;
        private SerialPortsComboBox debugChannelAddressesCB;
        public bool ReceiveEnabled { get; set; } = true;
        public SerialDataChannel Channel { get; private set; }
        private Button connectB;
        private CheckBox dtrCB;
        private ComboBox baudRatesCB;
        private System.Timers.Timer spPollTimer;
        private TextBox flushTB;
        private System.ComponentModel.IContainer components;
        private Timer pingTimer;
        private Timer uiUpdater;

        public bool ShowDTR
        {
            get { return dtrCB.Visible; }
            set
            {
                dtrCB.Visible = value;
                if (value)
                {
                    baudRatesCB.Width = Width - 20 - dtrCB.Width;
                    baudRatesCB.Left = 10 + dtrCB.Width;
                }
                else
                {
                    baudRatesCB.Width = Width - 20;
                    baudRatesCB.Left = 10;
                }

            }
        }
        public bool DefaultDTR { get; set; }
        public int PingDuration { get { return pingTimer.Interval; } set { pingTimer.Interval = value; } }
        public bool PingEnabled { get { return pingTimer.Enabled; } set { pingTimer.Enabled = value; } }
        byte thisNodeIndex = 0;
        public byte ID { get { return thisNodeIndex; } set { if (value < 0) value = 0; thisNodeIndex = value; } }
        public SerialChannelControl()
        {
            InitializeComponent();
            spPollTimer.Elapsed += SpPollTimer_Tick;
            baudRatesCB.SelectedIndex = 5;
            dtrCB.Checked = DefaultDTR;
        }
        public void Send(PacketCommand command)
        {
            command.SourceID = ID;
            command.SendCommand(Channel);
        }
        private void SpPollTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Channel == null)
                return;
            while (Channel.IsOpen && ReceiveEnabled)
            {
                int toRead = 0;
                try
                {
                    toRead = Channel.BytesToRead;
                }
                catch
                {
                    disconnectSP();
                    return;
                }
                if (toRead > 7)
                {
                    var com = new PacketCommand();
                    var resp = ProtocolError.Unknown;
                    try
                    {
                        resp = PacketCommand.FromStream(ref com, Channel, 500, flushTB);
                    }
                    catch { disconnectSP(); return; }
                    if (resp == ProtocolError.None)
                    {
                        bool bkp = ReceiveEnabled;
                        ReceiveEnabled = false;
                        PacketReceived?.Invoke(com);
                        ReceiveEnabled = bkp;
                    }
                }
                else break;
            }
        }
        
        void connectChannel(int retries = 20)
        {
            hasUiUpdate = true;
            Text_ = "Disconnected";
            try
            {
                string address = debugChannelAddressesCB.Text.ToUpper();
                if (address.Contains(".") || address == "LOCALHOST")
                {
                    Channel = new SocketChannel(address);
                }
                else
                {
                    if (!address.StartsWith("COM"))
                    {
                        address = address.Substring(address.IndexOf("(") + 1);
                        address = address.Substring(0, address.IndexOf(")"));
                    }
                    if (baudRatesCB.Text == "")
                        baudRatesCB.Text = "115200";
                    Channel = new SerialPortChannel(address, Convert.ToInt32(baudRatesCB.Text));
                    Channel.DtrEnable = dtrCB.Checked;
                    Channel.ReadBufferSize = 1024 * 1024 * 10;
                }
                Channel.Open();
                Connected?.Invoke(this, new EventArgs());
                connectB_Text = "Disconnect";

                Text_ = debugChannelAddressesCB.Text;
                connectB_ForeColor = Color.Green;
                spPollTimer.Enabled = true;
                
            }
            catch (Exception ex)
            {
                if (retries > 0)
                {
                    connectB.ForeColor = Color.Orange;
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(500);
                    connectChannel(retries - 1);
                    return;
                }
                MessageBox.Show("Could not open the channel.");
                Channel = null;
                connectB.Text = "Connect";
                connectB.ForeColor = Color.Red;
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.connectB = new System.Windows.Forms.Button();
            this.dtrCB = new System.Windows.Forms.CheckBox();
            this.baudRatesCB = new System.Windows.Forms.ComboBox();
            this.spPollTimer = new System.Timers.Timer();
            this.flushTB = new System.Windows.Forms.TextBox();
            this.debugChannelAddressesCB = new FivePointNine.Windows.Controls.SerialPortsComboBox();
            this.pingTimer = new System.Windows.Forms.Timer(this.components);
            this.uiUpdater = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.spPollTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // connectB
            // 
            this.connectB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectB.FlatAppearance.BorderSize = 0;
            this.connectB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectB.Location = new System.Drawing.Point(0, 27);
            this.connectB.Name = "connectB";
            this.connectB.Size = new System.Drawing.Size(409, 25);
            this.connectB.TabIndex = 1;
            this.connectB.Text = "Connect";
            this.connectB.UseVisualStyleBackColor = true;
            this.connectB.Click += new System.EventHandler(this.connectB_Click);
            // 
            // dtrCB
            // 
            this.dtrCB.AutoSize = true;
            this.dtrCB.Location = new System.Drawing.Point(3, 58);
            this.dtrCB.Name = "dtrCB";
            this.dtrCB.Size = new System.Drawing.Size(49, 17);
            this.dtrCB.TabIndex = 3;
            this.dtrCB.Text = "DTR";
            this.dtrCB.UseVisualStyleBackColor = true;
            this.dtrCB.CheckedChanged += new System.EventHandler(this.dtrCB_CheckedChanged);
            // 
            // baudRatesCB
            // 
            this.baudRatesCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.baudRatesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudRatesCB.FormattingEnabled = true;
            this.baudRatesCB.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200",
            "230400",
            "921000",
            "20000000"});
            this.baudRatesCB.Location = new System.Drawing.Point(58, 54);
            this.baudRatesCB.Name = "baudRatesCB";
            this.baudRatesCB.Size = new System.Drawing.Size(348, 21);
            this.baudRatesCB.TabIndex = 4;
            // 
            // spPollTimer
            // 
            this.spPollTimer.Enabled = true;
            this.spPollTimer.Interval = 30D;
            this.spPollTimer.SynchronizingObject = this;
            this.spPollTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.spPollTimer_Elapsed);
            // 
            // flushTB
            // 
            this.flushTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flushTB.Location = new System.Drawing.Point(3, 81);
            this.flushTB.Multiline = true;
            this.flushTB.Name = "flushTB";
            this.flushTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.flushTB.Size = new System.Drawing.Size(403, 116);
            this.flushTB.TabIndex = 5;
            // 
            // debugChannelAddressesCB
            // 
            this.debugChannelAddressesCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugChannelAddressesCB.FormattingEnabled = true;
            this.debugChannelAddressesCB.Items.AddRange(new object[] {
            "com0com - bus for serial port pair emulator 1 (COM33 <-> COM34)",
            "Arduino Mega 2560 (COM18)",
            "com0com - serial port emulator (COM32)",
            "com0com - serial port emulator CNCB1 (COM34)",
            "Communications Port (COM1)",
            "Intel(R) Active Management Technology - SOL (COM4)",
            "com0com - serial port emulator (COM31)",
            "com0com - serial port emulator CNCA1 (COM33)"});
            this.debugChannelAddressesCB.Location = new System.Drawing.Point(0, 0);
            this.debugChannelAddressesCB.Name = "debugChannelAddressesCB";
            this.debugChannelAddressesCB.Size = new System.Drawing.Size(409, 21);
            this.debugChannelAddressesCB.TabIndex = 0;
            this.debugChannelAddressesCB.Text = "Arduino Mega 2560 (COM18)";
            // 
            // pingTimer
            // 
            this.pingTimer.Interval = 10000;
            this.pingTimer.Tick += new System.EventHandler(this.pingTimer_Tick);
            // 
            // uiUpdater
            // 
            this.uiUpdater.Enabled = true;
            this.uiUpdater.Interval = 30;
            this.uiUpdater.Tick += new System.EventHandler(this.uiUpdater_Tick);
            // 
            // SerialChannelControl
            // 
            this.Controls.Add(this.flushTB);
            this.Controls.Add(this.baudRatesCB);
            this.Controls.Add(this.dtrCB);
            this.Controls.Add(this.connectB);
            this.Controls.Add(this.debugChannelAddressesCB);
            this.MinimumSize = new System.Drawing.Size(170, 51);
            this.Name = "SerialChannelControl";
            this.Size = new System.Drawing.Size(409, 200);
            ((System.ComponentModel.ISupportInitialize)(this.spPollTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void connectB_Click(object sender, EventArgs e)
        {
            if ((connectB.Text as string) == "Connect")
                connectChannel();
            else
                disconnectSP();
            hasUiUpdate = true;
        }
        void disconnectSP()
        {
            try
            {
                Channel.Close();
                Channel = null;
            }
            catch { }
            hasUiUpdate = true;
            connectB_Text = "Connect";
            connectB_ForeColor = Color.Black;
            Disconnected?.Invoke(this, new EventArgs());
        }

        private void pingTimer_Tick(object sender, EventArgs e)
        {
            if (Channel == null)
                return;
            if (!Channel.IsOpen)
                return;
            Send(new IO.PacketCommand(PacketCommandID.Ping));
        }

        private void dtrCB_CheckedChanged(object sender, EventArgs e)
        {
            if (Channel == null)
                return;
            if (!Channel.IsOpen)
                return;
            Channel.DtrEnable = dtrCB.Checked;
        }

        bool hasUiUpdate = false;
        string connectB_Text, Text_;
        Color connectB_ForeColor;

        private void spPollTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        private void uiUpdater_Tick(object sender, EventArgs e)
        {
            if (!hasUiUpdate)
                return;
            hasUiUpdate = false;
            connectB.Text = connectB_Text;
            Text = Text;
            connectB.ForeColor = connectB_ForeColor;

        }
    }
}
