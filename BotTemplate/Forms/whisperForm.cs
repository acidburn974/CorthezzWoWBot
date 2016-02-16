using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotTemplate.Interact;

namespace BotTemplate.Forms
{
    public partial class whisperForm : Form
    {
        public whisperForm()
        {
            InitializeComponent();
            List<Objects.ChatMessage> tmpChat = ChatReader.ChatMessageList;

            foreach (Objects.ChatMessage x in tmpChat)
            {
                if (x.Type == "6" || x.Type == "0"
                                            || x.Text.ToLower().Contains(ObjectManager.playerName.ToLower()))
                {
                    dataGridView1.Rows.Add(x.Time, x.Type, x.playerName, x.Text);
                }
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            ChatReader.ChatMessageList.Clear();
            this.Close();
        }
    }
}
