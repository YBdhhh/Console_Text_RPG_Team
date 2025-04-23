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

		public float critical = 1000; //10%
		public float miss = 1000; //10%
		public float hp = 100;
		public float atk = 10;
		public float def = 5;
		public float mp = 10;

		public int stat = 5;
		public int gold = 1500;
		public int level = 1;
		public int[] expCount = new int[5] { 10, 35, 65, 100, 150 };
		public int exp = 0;

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
				stat += 1;
				hp += 10;
				atk += 3;
				def += 1;
				Console.WriteLine($"레벨이 {level - 1} -> {level} 로 상승하셨습니다.!");
				Console.WriteLine($"남은 경험치 : {exp}");
			}
		}

		public bool IsAlive()
		{
			return hp > 0;
		}

		public void TakeDamage(float damage)
		{
			PreviousHP = hp;
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
				Console.WriteLine("크리티컬 작렬!");
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
				Console.WriteLine("깔끔한 회피");
				Thread.Sleep(500);
			}
			return isMiss;
		}
	}
}