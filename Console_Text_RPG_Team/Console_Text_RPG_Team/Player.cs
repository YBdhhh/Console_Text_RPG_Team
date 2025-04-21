using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public float PreviousHP;
		
		public bool IsAlive()
		{
			return hp > 0;
		}

		public void TakeDamage(float damage)
		{
			PreviousHP = hp;
			float reduced = damage - def;
			if (reduced < 0)
			{
				reduced = 0;
			}
			hp -= reduced;
			if(hp < 0)
			{
				hp = 0;
			}	
		}
	}
}
