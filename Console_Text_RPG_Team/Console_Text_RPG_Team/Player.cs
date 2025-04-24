using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
	internal class Player
	{
		public string name;
		public string job;
		public List<Skill> skill = new List<Skill>(4);


		public Quest quest = new Quest();

		public float critical = 100; //10%
		public float miss = 100; //10%
		public float maxHp = 100;
		public float hp;

		public float atk = 10;
		public float def = 5;
		public float mp = 10;
		public int stat = 5;
		private int gold = 1500; 

		public int Gold
		{
			get
			{
				return gold;
			}
			set
			{
				int i = gold;
				gold = value;
				if (quest.name != null)
				{
					if (i -gold > 0)
					{
						quest.PlayEvent(quest, i-gold);
					}
				}
			}
		}

		public void ViewStatus()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("캐릭터의 정보가 표시됩니다").Append("\n");
			sb.AppendLine($"이름 : {name}");
			sb.AppendLine($"레벨 : {level}");
			sb.AppendLine($"직업 : {job}");
			sb.AppendLine($"체력 : {hp} / {maxHp}");
			sb.AppendLine($"공격력 : {atk}");
			sb.AppendLine($"방어력 : {def}");
			sb.AppendLine($"마나 : {mp}");
			sb.AppendLine();
			sb.AppendLine($"골드 : {gold}");
			Console.WriteLine(sb.ToString());
		}

		public int level = 1;
		public int[] expCount = new int[5] { 10, 35, 65, 100, 150 };
		public int exp = 0;

		public Inventory inventory { get; set; }

		public Player()
		{
			inventory = new Inventory();
			Gold = 1500;
			hp = maxHp;
		}

		public void HpUpSet(float value)
		{
			maxHp += value;
			hp += value;
		}

		public int Exp
		{
			get { return exp; }
			set { exp = value; IsLevelUp(); }
		}
		public float PreviousHP;

		public void IsLevelUp()
		{
			if (exp > expCount[level - 1])
			{
				exp -= expCount[level - 1];
				level += 1;
				stat += 3;
				maxHp += 10;
				atk += 3;
				def += 1;
				miss += 10;
				critical += 10;
				//Console.WriteLine($"레벨이 {level - 1} -> {level} 로 상승하셨습니다.!");
				//Console.WriteLine($"남은 경험치 : {exp}");
			}
		}

		public void StatAdd(int value)
		{
			switch(value)
			{
				case 1:
					HpUpSet(10);
					break;
				case 2:
					this.atk += 3;
					break;
				case 3:
					this.def += 1;
					break;
				case 4:
					this.mp += 1;
					break;	
			}
			this.stat -= 1;
		}

		public bool IsAlive()
		{
			return hp > 0;
		}

		public void TakeDamage(float damage)
		{
			PreviousHP = maxHp;
			float reduced = damage - def;
			if (reduced <= 0)
			{
				reduced = 1;
			}
			if (!(MissDamage(this)))
			{
				hp -= reduced;
			}
			if (hp < 0)
			{
				hp = 0;
			}
		}

		public float CriticalDamage(Player player, float damage)
		{
			Random rand = new Random();
			int percent = rand.Next(0, 1000);
			float critical = player.critical;

			if (percent < critical)
			{
				damage = (int)(damage + damage * critical * 0.1f * 0.01f);
				Console.ForegroundColor = ConsoleColor.Red; //크리티컬 데미지
                Console.WriteLine(" 크리티컬 작렬!");
				Console.ResetColor();
                Thread.Sleep(1000);
			}

			return damage;
		}

		public bool MissDamage(Player player)
		{
			bool isMiss = false;
			Random rand = new Random();
			int percent = rand.Next(0, 1000);
			float miss = player.miss;

			if (percent < miss)
			{
				isMiss = true;
                Console.ForegroundColor = ConsoleColor.Cyan; //크리티컬 데미지
                Console.WriteLine(" 깔끔한 회피");
                Console.ResetColor();
                Thread.Sleep(500);
			}
			return isMiss;
		}

		public void Heal(float amount)
		{
			hp += amount;
			if (hp > maxHp) hp = maxHp;
		}
	}
}