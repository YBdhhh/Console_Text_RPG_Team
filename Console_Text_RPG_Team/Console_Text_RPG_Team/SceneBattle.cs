using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class SceneBattle
<<<<<<< Updated upstream
    {
=======
	{
        SceneBattleAttack sceneBattleAttack = new SceneBattleAttack();  //임시
		public List<Monster> spawnList = new List<Monster>(4);
		public List<Monster> monsters = new List<Monster>
			{
				new Monster("미니언", 15f, 5f, 2),
				new Monster("공허충", 10f, 9f, 3),
				new Monster("대포미니언", 25f, 8f, 5)
			};

>>>>>>> Stashed changes

		public void StartBattle(Player player)
        {
            StringBuilder sb = new StringBuilder();

            sceneBattleAttack = new SceneBattleAttack();

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
<<<<<<< Updated upstream
            InputAttack();


=======

            InputAttack(player);
>>>>>>> Stashed changes
        }

        public void SpawnMonster()
        {
            Monster minion = new Monster("미니언", 15, 5, 2);
            Monster cannonMinion = new Monster("대포미니언", 25, 8, 5);
            Monster voidBug = new Monster("공허충", 10, 9, 3);

<<<<<<< Updated upstream
=======
            int typeCount = 3;           //나오게 할 몬스터 종류 개수
            int monsterCount = 4;        //나오게 할 몬스터 개수 (나오지 않는 경우 포함)
            int monsterTypeStart = 0;   //나오게 할 몬스터타입 시작점       던전층수따라 변환 예정
            int monsterTypeEnd = 4;     //나오게 할 몬스터타입 끝지점       던전층수따라 변환 예정
            int probability = 4;        //나오게 할 확률 (1/n)
            int spawn;                  //소환 확률용 변수
            //List < >
               //랜덤값 지정을 위한 int 리스트
>>>>>>> Stashed changes
            Random random = new Random();
            int[] monsters = new int[4];

<<<<<<< Updated upstream
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
=======
            for (int i = monsterTypeStart; i < monsterCount; i++)
            {
                  // 스폰리스트에 랜덤한 몬스터 타입 추가
                if (i == 0)
                {
                    spawn = random.Next(1, probability);      //첫 몬스터는 확정으로 소환
                }
                else
                {
                    spawn = random.Next(0, probability);      //이후로는 확률 1/4
                }

                if (spawn != 0)
                {
					int rand = random.Next(0, 3);
					spawnList.Add(monsters[rand]);
					
>>>>>>> Stashed changes
                }
            }
            for (int j = 0; j < spawnList.Count; j++)
            {
				Console.WriteLine($"Lv.{spawnList[j].level} {spawnList[j].name} HP {spawnList[j].hp}");
			}
            Console.WriteLine();
        }

        public void InputAttack()
        {
            while (true)
            {
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 1:
<<<<<<< Updated upstream
                            Console.WriteLine("공격창"); // SceneBattleAttack
=======
                            Console.Clear();
                            Console.WriteLine("전투시작");
                            sceneBattleAttack.BattleLoop(player, spawnList);
>>>>>>> Stashed changes
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
