using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Console_Text_RPG_Team
{
	internal class Player
	{
		public string name;
		public string job;

		public float hp = 100;
		public float atk = 10;
		public float def = 5;
		public int gold = 1500;
		public int level = 1;


        public Player(string name, string job, float hp, float atk, float def, int gold, int level)
        {
            this.name = name;
            this.job = job;
			this.hp = hp;
            this.atk = atk;
			this.def = def;
            this.gold = gold;
            this.level = level;
        }
        public Player(string name)
        {
            this.name = name;
        }



    }
}
