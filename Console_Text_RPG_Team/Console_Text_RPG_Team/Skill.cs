using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum DamageType
{
	Hp = 1,
	Def = 2,
	Atk = 4,
	Mp = 8
}

enum ValueType
{
	Mp = 1,
	Gold = 2,
	Hp = 4
}

namespace Console_Text_RPG_Team
{
	internal class Skill
	{
		public string name;
		public string explain;
		public int[] damage = new int[4] { 0, 0, 0, 0 };
		public int damageType;
		public int valueType;
		public float value;

		public int price;

		public Skill()
		{
		}

		public Skill(Skill skill)
		{
			this.name = skill.name;
			this.explain = skill.explain;
				
			this.damage = skill.damage;
			this.damageType = skill.damageType;
			this.valueType = skill.valueType;
			this.value = skill.value;
			this.price = skill.price;
		}

		public Skill(string name, string explain, int damageType, int[] damage, int valueType, float value, int price)
		{
			this.name = name;
			this.explain = explain;
			this.damageType = damageType;
			this.valueType = valueType;
				this.damage = damage;
			this.value = value;
			this.price = price;
		}

		public int UseSkill(Player player)
		{
			string value = UseSkillValue(player);
			int damage = UseSkillDamage(player);
			SkillText(player, value, damage);
			return damage;
		}

		public void SkillText(Player player, string value, int damage)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"{name}(으)로 {value}을 소모하여 {name}을 사용했습니다.");
			Console.WriteLine(sb.ToString());
			Thread.Sleep(500);
		}

		public int UseSkillDamage(Player player)
		{
			int type = 0;
			type = damageType;
			int damage2 = 0;
			int i = 0;

			if (type >= (int)DamageType.Mp)
			{
				damage2 += (int)(damage[i++] * 0.01f * player.mp);
				type -= (int)DamageType.Mp;
			}

			if (type >= (int)DamageType.Atk)
			{
				damage2 += (int)(damage[i++] * 0.01f * player.atk);
				type -= (int)DamageType.Atk;
			}

			if (type >= (int)DamageType.Def)
			{
				damage2 += (int)(damage[i++] * 0.01f * player.def);
				type -= (int)DamageType.Def;
			}

			if (type >= (int)DamageType.Hp)
			{
				damage2 += (int)(damage[i++] * 0.01f * player.hp);
				type -= (int)DamageType.Hp;
			}

			return damage2;
		}

		public string UseSkillValue(Player player)
		{
			string typeName = "";
			int type = valueType;
			if (type >= (int)ValueType.Mp)
			{
				player.mp -= value;
				type -= (int)ValueType.Mp;
				typeName += "마나";
			}

			if (type >= (int)ValueType.Gold)
			{
				player.gold -= (int)value;
				type -= (int)ValueType.Gold;
				typeName += "골드";
			}

			if (type >= (int)ValueType.Hp)
			{
				player.hp -= (int)value;
				type -= (int)ValueType.Hp;
				typeName += "체력";
			}
			return typeName;
		}
	}
}
