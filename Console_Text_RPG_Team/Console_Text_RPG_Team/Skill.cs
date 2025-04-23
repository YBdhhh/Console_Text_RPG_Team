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
		public int[] damage = new int[4];
		public int damageType;
		public int valueType;
		public float value;

		public int price;

		public Skill(string name, string explain, int damageType, int[] damage, int valueType, float value, int price)
		{
			this.name = name;
			this.explain = explain;
			this.damageType = damageType;
			this.valueType = valueType;
			for (int i = 0; i < damage.Length; i++)
			{
				this.damage[i] = damage[i];
			}
			this.value = value;
			this.price = price;
		}

		public void UseSkill(Player player, Skill skill)
		{
			string value = UseSkillValue(player, skill);
			int damage = UseSkillDamage(player, skill);
			SkillText(player, skill, value, damage);
		}

		public void SkillText(Player player, Skill skill, int value, int damage)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"{skill.name}(으)로 {value}을 소모하여 {damage} 데미지를 입혔습니다.");
			Console.WriteLine(sb.ToString());
		}

		public int UseSkillDamage(Player player, Skill skill)
		{
			int type = skill.damageType;
			int damage = 0;
			if (type >= (int)DamageType.Mp)
			{
				damage += (int)(skill.damage[0] * 0.01f * player.mp);
				type -= (int)DamageType.Mp;
			}

			if (type >= (int)DamageType.Atk)
			{
				damage += (int)(skill.damage[1] * 0.01f * player.atk);
				type -= (int)DamageType.Atk;
			}

			if (type >= (int)DamageType.Def)
			{
				damage += (int)(skill.damage[2] * 0.01f * player.def);
				type -= (int)DamageType.Def;
			}

			if (type >= (int)DamageType.Hp)
			{
				damage += (int)(skill.damage[3] * 0.01f * player.hp);
				type -= (int)DamageType.Hp;
			}

			return damage;
		}

		public string UseSkillValue(Player player, Skill skill)
		{
			string typeName = "";
			int type = skill.valueType;
			if (type >= (int)ValueType.Mp)
			{
				player.mp -= skill.value;
				type -= (int)ValueType.Mp;
				typeName += "마나";
			}

			if (type >= (int)ValueType.Gold)
			{
				player.gold -= (int)skill.value;
				type -= (int)ValueType.Gold;
				typeName += "골드";
			}

			if (type >= (int)ValueType.Hp)
			{
				player.hp -= (int)skill.value;
				type -= (int)ValueType.Hp;
				typeName += "체력";
			}
			return typeName;
		}
	}
}
