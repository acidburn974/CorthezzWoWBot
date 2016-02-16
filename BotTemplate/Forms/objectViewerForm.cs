using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotTemplate.Interact;
using BotTemplate.Helper;
using System.Globalization;

namespace BotTemplate.Forms
{
    public partial class objectViewerForm : Form
    {
        public objectViewerForm()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            objectGrid.Columns.Clear();
            objectGrid.Rows.Clear();
        }

        List<List<object>> objectList = new List<List<object>>();

        enum typeEnum : int
        {
            unit = 1,
            gameObject = 0
        }

        private int startCoord = 0;
        private int startName = 0;
        private int type = 0;

        private void Add()
        {
            UInt64 targetGuid = ObjectManager.targetGuid;
            UInt64 playerGuid = ObjectManager.playerGuid;

            if (type == (int)typeEnum.unit)
            {
                bool foundTarget = false;
                int added = -1;
                object[] targetObject = new object[0];
                for (int i = 0; i < objectList.Count; i++)
                {
                    if (objectList[i][startName].ToString().ToLower().Contains(mtbFilter.Text.ToLower()))
                    {
                        if ((UInt64)objectList[i][0] == targetGuid && (UInt64)objectList[i][0] != playerGuid)
                        {
                            targetObject = objectList[i].ToArray();
                            foundTarget = true;
                        }
                        else
                        {
                            objectGrid.Rows.Add(objectList[i].ToArray());
                            if ((UInt64)objectList[i][0] == playerGuid)
                            {
                                objectGrid.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                            }
                            added++;
                        }
                    }
                }

                if (foundTarget)
                {
                    if (added != -1)
                    {
                        objectGrid.Rows.Insert(1, targetObject);
                        objectGrid.Rows[1].DefaultCellStyle.BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        objectGrid.Rows.Insert(0, targetObject);
                        objectGrid.Rows[0].DefaultCellStyle.BackColor = Color.YellowGreen;
                    }
                }
            }

            if (type == (int)typeEnum.gameObject)
            {
                for (int i = 0; i < objectList.Count; i++)
                {
                    if (objectList[i][startName].ToString().Contains(mtbFilter.Text))
                    {
                        objectGrid.Rows.Add(objectList[i].ToArray());
                    }
                }
            }
        }

        private void bGetGameobjects_Click(object sender, EventArgs e)
        {
            startCoord = 3;
            startName = 2;
            type = (int)typeEnum.gameObject;

            Clear();
            objectGrid.Columns.Add("objGuid", "Guid");
            objectGrid.Columns.Add("objBase", "Base");
            objectGrid.Columns.Add("objName", "Name");
            objectGrid.Columns.Add("objX", "X");
            objectGrid.Columns.Add("objY", "Y");
            objectGrid.Columns.Add("objZ", "Z");
            objectGrid.Columns.Add("objId", "ID");
            objectGrid.Columns.Add("objState", "State");
            objectGrid.Columns.Add("objIsGettingUsed", "Is getting used");

            List<Objects.GameObject> tmpList = ObjectManager.GameObjectList;
            objectList.Clear();
            foreach (Objects.GameObject x in tmpList)
            {
                List<object> tmpObjectValues = new List<object> { x.guid, x.baseAdd.ToString("X8"), x.name, x.Pos.x, x.Pos.y, x.Pos.z, x.objectId, x.state, x.isBusy };
                objectList.Add(tmpObjectValues);
            }

            Add();
        }

        private void objectGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Ingame.Tele(
                    new Objects.Location(
                        Convert.ToSingle(objectGrid.Rows[e.RowIndex].Cells[startCoord].Value),
                        Convert.ToSingle(objectGrid.Rows[e.RowIndex].Cells[startCoord + 1].Value),
                        Convert.ToSingle(objectGrid.Rows[e.RowIndex].Cells[startCoord + 2].Value)
                        ), 60, false);
            }
        }

        private void objectGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 3)
            {
                Clipboard.SetText(objectGrid.CurrentCell.Value.ToString());
                e.Handled = true;
            }
        }

        private void bGetUnits_Click(object sender, EventArgs e)
        {
            startCoord = 4;
            startName = 3;
            type = (int)typeEnum.unit;

            Clear();
            objectGrid.Columns.Add("unitGuid", "Guid");
            objectGrid.Columns.Add("unitBase", "Base");
            objectGrid.Columns.Add("unitIsUnit", "Is Unit?");
            objectGrid.Columns.Add("unitName", "Name");
            objectGrid.Columns.Add("unitX", "X");
            objectGrid.Columns.Add("unitY", "Y");
            objectGrid.Columns.Add("unitZ", "Z");
            objectGrid.Columns.Add("unitIsChannel", "Is Channeling?");
            objectGrid.Columns.Add("unitSummonedBy", "Summoned By");
            objectGrid.Columns.Add("unitDynFlags", "Dynamic Flags");
            objectGrid.Columns.Add("unitMovFlags", "Movement Flags");
            objectGrid.Columns.Add("unitTargetGuid", "Guid of target");
            objectGrid.Columns.Add("unitIsCasting", "Cast ID");
            objectGrid.Columns.Add("unitFactionId", "Faction ID");
            objectGrid.Columns.Add("unitNpcId", "NPC ID");

            List<Objects.UnitObject> tmpList = ObjectManager.UnitObjectList;
            objectList.Clear();
            foreach (Objects.UnitObject x in tmpList)
            {
                List<object> tmpObjectValues = new List<object> { x.guid, x.baseAdd.ToString("X8"), x.isUnit, x.UnitName, x.Pos.x, x.Pos.y, x.Pos.z, x.isChanneling, x.summonedBy, x.dynFlags.ToString("X8"), x.movementState.ToString("X8"), x.targetGuid, x.IsCasting, x.factionId, x.NpcId };
                objectList.Add(tmpObjectValues);
            }

            Add();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void bFilter_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < objectGrid.Rows.Count; i++)
            {
                if (!objectGrid.Rows[i].Cells[startName].Value.ToString().Contains(mtbFilter.Text))
                {
                    objectGrid.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void mtbFilter_KeyUp(object sender, KeyEventArgs e)
        {
            objectGrid.Rows.Clear();
            Add();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (type == (int)typeEnum.gameObject)
            {
                Calls.OnRightClickObject(UInt32.Parse(objectGrid.CurrentRow.Cells["objBase"].Value.ToString(), System.Globalization.NumberStyles.HexNumber), 1);
            }
        }
    }
}
