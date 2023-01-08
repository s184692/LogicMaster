using LogicMaster.gameplay.logic.gates;
using LogicMaster.gui.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogicMaster.gameplay
{
    public class GameInventory
    {
        private GameManager gameManager { get; set; }

        public Dictionary<string, int> InventoryAmounts { get; private set; }

        public Dictionary<string, GateInventoryBox> InventoryBoxes { get; private set; }

        public GameInventory(GameManager manager)
        {
            gameManager = manager;
            InventoryAmounts = new Dictionary<string, int>();
            InventoryBoxes = new Dictionary<string, GateInventoryBox>();
        }

        public void InitializeInventory(Dictionary<string, int> amounts)
        {
            InventoryAmounts.Add("BUF", amounts["BUF"]);
            InventoryAmounts.Add("NOT", amounts["NOT"]);
            InventoryAmounts.Add("AND", amounts["AND"]);
            InventoryAmounts.Add("NAND", amounts["NAND"]);
            InventoryAmounts.Add("OR", amounts["OR"]);
            InventoryAmounts.Add("NOR", amounts["NOR"]);
            InventoryAmounts.Add("XOR", amounts["XOR"]);
            InventoryAmounts.Add("XNOR", amounts["XNOR"]);

            InventoryBoxes.Add("BUF", new GateInventoryBox(new BUF(), 1, 1));
            InventoryBoxes.Add("NOT", new GateInventoryBox(new NOT(), 1, 3));
            InventoryBoxes.Add("AND", new GateInventoryBox(new AND(), 3, 1));
            InventoryBoxes.Add("NAND", new GateInventoryBox(new NAND(), 3, 3));
            InventoryBoxes.Add("OR", new GateInventoryBox(new OR(), 5, 1));
            InventoryBoxes.Add("NOR", new GateInventoryBox(new NOR(), 5, 3));
            InventoryBoxes.Add("XOR", new GateInventoryBox(new XOR(), 7, 1));
            InventoryBoxes.Add("XNOR", new GateInventoryBox(new XNOR(), 7, 3));

            InventoryBoxes["BUF"].PropertyChanged += InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["NOT"].PropertyChanged += InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["AND"].PropertyChanged += InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["NAND"].PropertyChanged += InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["OR"].PropertyChanged += InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["NOR"].PropertyChanged += InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["XOR"].PropertyChanged += InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["XNOR"].PropertyChanged += InventoryAmountChanged_PropertyChanged;

            InventoryBoxes["BUF"].SetAmountLabel(InventoryAmounts["BUF"]);
            InventoryBoxes["NOT"].SetAmountLabel(InventoryAmounts["NOT"]);
            InventoryBoxes["AND"].SetAmountLabel(InventoryAmounts["AND"]);
            InventoryBoxes["NAND"].SetAmountLabel(InventoryAmounts["NAND"]);
            InventoryBoxes["OR"].SetAmountLabel(InventoryAmounts["OR"]);
            InventoryBoxes["NOR"].SetAmountLabel(InventoryAmounts["NOR"]);
            InventoryBoxes["XOR"].SetAmountLabel(InventoryAmounts["XOR"]);
            InventoryBoxes["XNOR"].SetAmountLabel(InventoryAmounts["XNOR"]);
        }

        private void InventoryAmountChanged_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            string? key = e.PropertyName;

            if (key != null)
            {
                if (InventoryAmounts.ContainsKey(key))
                {
                    InventoryAmounts[key]--;
                    InventoryBoxes[key].SetAmountLabel(InventoryAmounts[key]);
                }
            }
        }

        public void AddGateBackToInventory(string key)
        {
            if (InventoryAmounts.ContainsKey(key))
            {
                InventoryAmounts[key]++;
                InventoryBoxes[key].SetAmountLabel(InventoryAmounts[key]);
            }
        }

        public void ClearInventory()
        {
            InventoryBoxes["BUF"].PropertyChanged -= InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["NOT"].PropertyChanged -= InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["AND"].PropertyChanged -= InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["NAND"].PropertyChanged -= InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["OR"].PropertyChanged -= InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["NOR"].PropertyChanged -= InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["XOR"].PropertyChanged -= InventoryAmountChanged_PropertyChanged;
            InventoryBoxes["XNOR"].PropertyChanged -= InventoryAmountChanged_PropertyChanged;

            InventoryAmounts.Clear();
            InventoryBoxes.Clear();
        }
    }
}
