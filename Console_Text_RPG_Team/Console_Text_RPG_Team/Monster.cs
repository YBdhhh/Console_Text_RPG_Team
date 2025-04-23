using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
	internal class Monster
	{
		public string name;
		public float hp;
		public float atk;
		public int level;
		public float PreviousHP;
		public List<string> dropItems;

		public Monster(string name, float hp, float atk, int level, List<string> dropItems)
		{
			this.name = name;
			this.hp = hp;
			this.atk = atk;
			this.level = level;
			this.dropItems = dropItems;
		}

		public bool IsAlive()
		{
			return hp > 0;
		}

        public void TakeDamage(float damage)
        {
            PreviousHP = hp;            
            hp -= damage;
            if (hp < 0)
            {
                hp = 0;
            }
        }

		public Reward GetReward()
		{
			int goldReward = 0;
			int expReward = 0;
			return new Reward(goldReward, expReward);
		}

		public List<string> GetDropItems()
		{
			return dropItems;
		}
    }
}
