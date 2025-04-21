using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
	internal class Job
	{
		public string name;
		public int hp;
		public int atk;
		public int def;

		public Job(string name, int hp, int atk, int def)
		{
			this.name = name;
			this.hp = hp;
			this.atk = atk;
			this.def = def;
		}

	}
}
