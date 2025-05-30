﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
	internal class Monster
	{
		public string name;
		public int hp;
		public int atk;
		public int def;
		public int level;
		public int expReward;
		public int PreviousHP;
		public List<Item> dropItems;

		public Monster(string name, int hp, int atk, int def, int level, int expReward, List<Item> dropItems)
		{
			this.name = name;
			this.hp = hp;
			this.atk = atk;
			this.def = def;
			this.level = level;
			this.expReward = expReward;
			this.dropItems = dropItems;
		}

		public Monster(Monster monster)
		{
			name = monster.name; 
			hp = monster.hp;
			atk = monster.atk;
			level = monster.level;
			this.expReward = monster.expReward;
			PreviousHP = monster.PreviousHP;
			this.dropItems = monster.dropItems;
		}

		public Monster()
		{

		}

		public bool IsAlive()
		{
			return hp > 0;
		}

        public void TakeDamage(int damage)
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
			return new Reward(expReward * 10, expReward);
		}

        // 드랍 아이템 리스트 반환
        public List<Item> GetDropItems(int currentFloor)
        {
            List<Item> dropItems = new List<Item>();
            Random rand = new Random();

            if (rand.Next(0, 100) < 20) // 20% 확률로 아이템 드랍
            {
                Item potionToDrop = null;
                switch (currentFloor)
                {
                    case 1:
                        potionToDrop = new Item("빨간 포션", "체력을 회복해주는 빨간 포션", 0, 0, 0, ItemType.Potion) { HealAmount = 400 };
                        break;
                    case 2:
                        potionToDrop = new Item("주황 포션", "체력을 회복해주는 주황 포션", 0, 0, 0, ItemType.Potion) { HealAmount = 500 };
                        break;
                    case 3:
                        potionToDrop = new Item("하얀 포션", "체력을 회복해주는 하얀 포션", 0, 0, 0, ItemType.Potion) { HealAmount = 600 };
                        break;
                    default:
                        break;
                }

                if (potionToDrop != null)
                {
                    dropItems.Add(potionToDrop);
                }
            }
            return dropItems;
        }
    }
}
