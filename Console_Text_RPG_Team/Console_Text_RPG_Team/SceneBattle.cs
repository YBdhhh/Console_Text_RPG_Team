using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class SceneBattle
    {
        SceneBattleAttack sceneBattleAttack = new SceneBattleAttack();

        public void StartBattle(Player player)
        {
            StringBuilder sb = new StringBuilder();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();
            SpawnMonster();
            sb.AppendLine("[내정보]");
            sb.AppendLine($"Lv.	: {player.level}");
            sb.AppendLine($"직업	: {player.job}");
            sb.AppendLine($"체  력	: {player.PreviousHP}/{player.hp}");
            sb.AppendLine("1. 공격").Append("\n");
            sb.AppendLine("원하시는 행동을 입력해주세요.");
            sb.Append(">> ");
            Console.Write(sb.ToString());
            sb.Clear();
            InputAttack(player);


        }

        public void SpawnMonster()
        {
            Monster minion = new Monster("미니언", 15, 5, 2);
            Monster cannonMinion = new Monster("대포미니언", 25, 8, 5);
            Monster voidBug = new Monster("공허충", 10, 9, 3);

            Random random = new Random();
            int[] monsters = new int[4];

            monsters[0] = random.Next(1, 4);        // 최소 한마리는 생성할수 있게

            for (int i = 1; i < monsters.Length; i++)
            {
                monsters[i] = random.Next(0, 4);
            }
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
            Console.WriteLine();
        }

        public void InputAttack(Player player)
        {
            while (true)
            {
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 1:
                            Console.WriteLine("1. 전투시작"); // SceneBattleAttack
                            sceneBattleAttack.BattleLoop(player);
							return;
                        default:
                            Console.WriteLine("다시 입력해주십시오");
                            continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("다시 입력해주십시오");
                    continue;
                }

            }
        }
    }
}
