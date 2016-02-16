using System;
using System.Windows.Forms;
using BotTemplate.Engines;

namespace BotTemplate.Forms
{
    public partial class settingsForm : Form
    {
        public settingsForm()
        {
            InitializeComponent();

            {
                nudHealth.Value = Data.regnerateHealthAt;
                nudMana.Value = Data.regnerateManaAt;
                nudRoam.Value = Data.roamAway;
                nudSearch.Value = Data.searchRange;
                mtbDrink.Text = Data.drinkName.Replace("\\\"", "\"").Replace("\\'", "\'");
                mtbFood.Text = Data.foodName.Replace("\\\"", "\"").Replace("\\'", "\'");
                mtbAcc.Text = Data.AccName;
                mtbPw.Text = Data.AccPw;
                nudRange.Value = Data.fightRange;
                nudLeaveSlots.Value = Data.LeaveFreeSlots;
                mtbLure.Text = Data.Lure.Replace("\\\"", "\"").Replace("\\'", "\'");
                cbResting.Checked = Data.UseCcRest;
                cbVendor.Checked = Data.VendorItems;
                cbGreen.Checked = Data.keepGreen;
                cbBlue.Checked = Data.keepBlue;
                cbPurple.Checked = Data.keepPurple;
                cbStopOnVendorFail.Checked = Data.StopOnVendorFail;
                mtbPort.Text = Data.Port.ToString();
            }

            {
                string tmp = "";
                foreach (string x in Data.ProtectedItems)
                {
                    tmp += x + Environment.NewLine;
                }
                tbProtected.Text = tmp;
            }

            {
                string tmp = "";
                foreach (string x in Data.MailerCharacters)
                {
                    tmp += x + Environment.NewLine;
                }
                tbMailer.Text = tmp;
            }


            bSave.DialogResult = DialogResult.OK;
            bCancel.DialogResult = DialogResult.Cancel;

        }

        private void bSave_Click(object sender, EventArgs e)
        {
            string[] protectedItems = tbProtected.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string[] mailReciever = tbMailer.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            
            Data.SaveSettings(Convert.ToInt32(nudHealth.Value),
                Convert.ToInt32(nudMana.Value),
                Convert.ToInt32(nudRoam.Value),
                Convert.ToInt32(nudSearch.Value),
                mtbDrink.Text,
                mtbFood.Text,
                Convert.ToInt32(nudRange.Value),
                mtbAcc.Text,
                mtbPw.Text,
                Convert.ToInt32(nudLeaveSlots.Value),
                mtbLure.Text,
                cbResting.Checked,
                cbVendor.Checked,
                cbGreen.Checked,
                cbBlue.Checked,
                cbPurple.Checked,
                cbStopOnVendorFail.Checked,
                mtbPort.Text);

            Data.SaveProtected(protectedItems);
            Data.SaveMailer(mailReciever);
        }

        private void EventOnlyLetterDigit(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                if (!(e.KeyChar >= 97 && e.KeyChar <= 122) /* kleines Alphabet */ &&
                    !(e.KeyChar >= 65 && e.KeyChar <= 90) /* großes Alphabet */ &&
                    !(e.KeyChar >= 48 && e.KeyChar <= 57) /* Numerisch */ &&
                    e.KeyChar != 3 /* Copy */ &&
                    e.KeyChar != 22 /* Paste */
                    )
                {
                    e.Handled = true;
                }
            }
        }

        private void EventOnlyDigits(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                if (!(e.KeyChar >= 48 && e.KeyChar <= 57) /* Numerisch */ &&
                    e.KeyChar != 3 /* Copy */ &&
                    e.KeyChar != 22 /* Paste */
                    )
                {
                    e.Handled = true;
                }
            }
        }

        private void EventOnlyLetter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                if (!(e.KeyChar >= 97 && e.KeyChar <= 122) /* kleines Alphabet */ &&
                    !(e.KeyChar >= 65 && e.KeyChar <= 90) /* großes Alphabet */ &&
                    e.KeyChar != 13 /* New Line Character */ &&
                    e.KeyChar != 3 /* Copy */ &&
                    e.KeyChar != 22 /* Paste */
                    )
                {
                    e.Handled = true;
                }
            }
        }

        private void EventNoBackslash(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = e.KeyChar == '\\';
            }
        }

        private void lConnectPort_Click(object sender, EventArgs e)
        {

        }

        private void mtbConnectPort_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
