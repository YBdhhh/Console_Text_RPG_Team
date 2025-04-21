using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class SceneBattle
    {

        public void BattleStart()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!");
            Console.ResetColor();
            MonsterSpawn();


        }

        public void MonsterSpawn()
        {
            Monster minion = new Monster("미니언", 15, 5, 2);
            Monster cannonMinion = new Monster("대포미니언", 25, 8, 5);
            Monster voidBug = new Monster("공허충", 10, 7, 3);

            Random random = new Random();
            int[] monsters = new int[4];

            monsters[0] = random.Next(1, 4);        // 최소 한마리는 생성할수 있게
            for (int i = 0; i < monsters.Length; i++)
            {
                monsters[i] = random.Next(0, 4);
                switch (monsters[i])
                {
                    case 1:
                        Console.WriteLine($"Lv.{minion.level} {minion.name} HP {minion.hp}");
                        break;
                    case 2:
                        Console.WriteLine($"Lv.{cannonMinion.level} {cannonMinion.name} HP {cannonMinion.hp}");
                        break;
                    case 3:
                        Console.WriteLine($"Lv.{voidBug.level} {voidBug.name} HP {voidBug.hp}");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
