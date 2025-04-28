using Newtonsoft.Json;
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
		[JsonIgnore]
		public List<AudioManager> audio = new List<AudioManager>();

		public int critical = 100; //10%
		public int miss = 100; //10%
		public int maxHp = 1000;
		public int hp;

		public int itematk = 0;
		public int itemdef = 0;

		public int atk = 100;
		public int def = 0;
		public int maxMp = 50;
		public int mp;
		public int stat = 3;
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
				if (i - gold > 0)
				{
					if (audio.Count != 0)
					{
						audio[1].Play();
					}
					if (quest.name != null)
					{
						quest.PlayEvent(quest, i-gold);
					}
				}
			}
		}

		public void ViewStatus()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine($" 이름   : {name}");
			sb.AppendLine($" 레벨   : {level}");
			sb.AppendLine($" 직업   : {job}");
			sb.AppendLine($" 체력   : {hp} / {maxHp}");
			sb.AppendLine($" 공격력 : {atk} (+{this.itematk})");
			sb.AppendLine($" 방어력 : {def} (+{this.itemdef})");
			sb.AppendLine($" 마나   : {mp} / {maxMp}");
			//sb.AppendLine();
			sb.AppendLine($" 골드   : {gold}");
			Console.Write(sb.ToString());

		}

		public void RegenerateMp()
		{
			if (maxMp > mp + 2)
			{
				mp += 2;
			}
			else
			{
				mp = maxMp;
			}
		}

		public int level = 1;
		public int[] expCount = new int[20] { 10, 35, 65, 100, 150, 195, 245, 300, 360, 425, 495, 570, 650, 735, 825, 920, 1020, 1125, 1235, 999999};
		public int exp = 0;

		public Inventory inventory = new Inventory();

		public Player()
		{
			Gold = 1900;
			hp = maxHp;
			mp = maxMp;
		}

		public void HpUpSet(int value)
		{
			maxHp += value;
			hp += value;
		}

		public void MpUpSet(int value)
		{
			maxMp += value;
			mp += value;
		}

		public int Exp
		{
			get { return exp; }
			set { exp = value; IsLevelUp(); }
		}
		public int PreviousHP;

		public void IsLevelUp()
		{
			if (exp > expCount[level - 1])
			{
				audio[2].Play();
				exp -= expCount[level - 1];
				level += 1;
				stat += 3;
				HpUpSet(100);
				atk += 30;
				def += 5;
				MpUpSet(5);
				miss += 10;
				critical += 10;
				//Console.WriteLine($"레벨이 {level - 1} -> {level} 로 상승하셨습니다.!");
				//Console.WriteLine($"남은 경험치 : {exp}");
			}
		}

		public void StatAdd(int value)
		{
			if(stat > 0)
			{
                switch (value)
                {
                    case 1:
                        HpUpSet(100);
                        break;
                    case 2:
                        this.atk += 30;
                        break;
                    case 3:
                        this.def += 5;
                        break;
                    case 4:
						MpUpSet(5);
                        break;
                }
                this.stat -= 1;
            }
			else
			{
				Console.WriteLine("보유스탯이 부족합니다.");
                Thread.Sleep(500);
            }
		}

		public bool IsAlive()
		{
			return hp > 0;
		}

		public void TakeDamage(int damage)
		{
			PreviousHP = hp;
			int reduced = damage - def;
			this.quest.PlayEvent(quest, (int)reduced);

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

		public int CriticalDamage(Player player, int damage)
		{
			Random rand = new Random();
			int percent = rand.Next(0, 1000);
			int critical = player.critical;

			if (percent < critical)
			{
				damage = (int)(damage + damage * critical * 0.1f * 0.01f);
				Console.ForegroundColor = ConsoleColor.Red; //크리티컬 데미지
                Console.Write(" 크리티컬 작렬");
				Thread.Sleep(200);
                Console.Write("!!");
                Thread.Sleep(200);
                Console.WriteLine("!!");
                Console.WriteLine("");
                Console.ResetColor();
                Thread.Sleep(200);
			}

			return damage;
		}

		public bool MissDamage(Player player)
		{
			bool isMiss = false;
			Random rand = new Random();
			int percent = rand.Next(0, 1000);
			int miss = player.miss;

			if (percent < miss)
			{
				isMiss = true;
                Console.ForegroundColor = ConsoleColor.Cyan; //크리티컬 데미지
                Console.Write(" 엄청나게!! ");
                Thread.Sleep(200);
                Console.WriteLine("깔끔한 회피!!");
                Console.WriteLine("");
                Console.ResetColor();
                Thread.Sleep(200);
			}
			return isMiss;
		}

		public void Heal(int amount)
		{
			audio[0].Play();
			hp += amount;
			if (hp > maxHp) hp = maxHp;
		}
	}
}