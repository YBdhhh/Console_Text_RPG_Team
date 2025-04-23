using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum DamageType
{
	Hp = 3,
	Def = 6,
	Atk = 12,
	Mp = 24
}

namespace Console_Text_RPG_Team
{
	internal class Skill
	{
		public string name;
		public string explain;
		public int[] damage = new int[4];
		public int damageType;
		public int valueMp;

		public int price;


		public void SkillDamage(int damageType)
		{
			


		}
	}
}
