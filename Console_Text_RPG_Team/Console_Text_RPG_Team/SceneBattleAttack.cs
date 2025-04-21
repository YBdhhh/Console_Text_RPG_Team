using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class SceneBattleAttack
    {
        public static void AttackLog(Player attacker, Monster target, int damage)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Battle!!\n");
            sb.AppendLine($"{attacker.name} 의 공격!");
            sb.AppendLine($"Lv.{target.level} {target.name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");

            if ( target.hp <= 0)
            {
                sb.AppendLine($"Lv.{target.level} {target.name}");
                sb.AppendLine($"HP {target.PreviousHP} -> Dead");
            }
            else
            {
                sb.AppendLine($"Lv.{target.level} {target.name}");
                sb.AppendLine($"HP {target.PreviousHP} -> {target.hp}");
            }

            sb.AppendLine("\n0. 다음\n>>");
            Console.WriteLine(sb.ToString());
            Console.ReadLine();

        }




    }
}
