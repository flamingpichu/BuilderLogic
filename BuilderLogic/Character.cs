using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuilderLogic
{
    public class Character
    {
        private string name;
        // total character level must be 1-20
        private int exp; // TODO: must be positive

        // stats
        private int str;
        private int dex;
        private int con;
        private int intg;
        private int wis;
        private int cha;

        private int maxHp;
        private int currentHp;

        private Dictionary<string, int> inventory; // id, quantity
        private Dictionary<string, int> classList; // class, level
        private List<string> feats;

        // equipment
        private string armor = string.Empty;
        private string mainHand = string.Empty;
        private string offHand = string.Empty;

        // derived stats
        public int ArmorClass // depends on equipped armor
        {
            get { return 10 + DexMod; } // formula without armor or a shield
        }

        public int StrMod
        {
            get { return (int)Math.Floor((str - 10.0) / 2.0); }
        }

        public int DexMod
        {
            get { return (int)Math.Floor((dex - 10.0) / 2.0); }
        }

        public int ConMod
        {
            get { return (int)Math.Floor((con - 10.0) / 2.0); }
        }

        public int IntgMod
        {
            get { return (int)Math.Floor((intg - 10.0) / 2.0); }
        }

        public int WisMod
        {
            get { return (int)Math.Floor((wis - 10.0) / 2.0); }
        }

        public int ChaMod
        {
            get { return (int)Math.Floor((cha - 10.0) / 2.0); }
        }

        // other property methods
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public  int Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        // methods
        public void AddItem(string id)
        {
            int quantity;
            if (inventory.TryGetValue(id, out quantity))
            {
                // increment quantity
                inventory[id] = quantity + 1;
            }
            else
            {
                inventory.Add(id, 1);
            }
        }

        public void AddItem(string id, int quantity)
        {
            int currentQuantity;
            if (inventory.TryGetValue(id, out currentQuantity))
            {
                inventory[id] = currentQuantity + quantity;
            }
            else
            {
                inventory.Add(id, quantity);
            }
        }

        public bool Equip(string id)
        {
            XElement root = XElement.Load("equipment.xml");
            IEnumerable<XElement> item =
                from el in root.Elements("item")
                where (string)el.Attribute("id") == id
                select el;
            return true;
        }
    }
}
