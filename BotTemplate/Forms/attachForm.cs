using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using BotTemplate.Helper;
using BotTemplate.Constants;
using System.Linq;
using BotTemplate.Interact;

namespace BotTemplate.Forms
{
    public partial class attachForm : Form
    {

        private List<string> playerNames = new List<string>();
        private List<int> pids = new List<int>();

        private void getProcesses()
        {
            playerNames.Clear();
            pids.Clear();

            // Loop through all WoW processes
            Process[] first = Process.GetProcessesByName("WoW");
            Process[] third = Process.GetProcessesByName("WoW1");
            Process[] second = Process.GetProcessesByName("Proxy");
            Process[] pro = first.Concat(second).Concat(third).ToArray();
            foreach (Process p in pro)
            {
                BmWrapper.memory.OpenProcessAndThread(p.Id);
                string version = BmWrapper.memory.ReadASCIIString((uint)Offsets.misc.GameVersion, 6);

                if (version == "1.12.1")
                {
                    bool x = BmWrapper.memory.ReadByte(Inject.isAttached) == 0;
                    if (x)
                    {
                        string playerName = BmWrapper.memory.ReadASCIIString((uint)Offsets.player.Name + Offsets.baseAddress, 10);
                        if (playerName.Trim() != "")
                        {
                            playerNames.Add(playerName.Trim());
                        }
                        else
                        {
                            playerNames.Add(p.Id.ToString());
                        }
                        pids.Add(p.Id);
                    }
                }
                BmWrapper.memory.Close();
            }
        }

        internal static bool isAttached;
        public attachForm()
        {
            InitializeComponent();
            isAttached = false;

            // Fill dictionary with active WoW processes
            getProcesses();

            // Fill dictionary pairs into listview
            foreach (string str in playerNames)
            {
                processList.Items.Add(str);
            }
        }

        private void processList_Click(object sender, EventArgs e)
        {
            if (processList.SelectedIndex != -1)
            {
                BmWrapper.memory.OpenProcessAndThread(pids[processList.SelectedIndex]);
                isAttached = true;
                this.Close();
            }
        }
    }
}
