using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class SceneBattleAttack
    {
        SceneBattle sceneBattle;

        public Player _player;
        public SceneBattleAttack(SceneBattle _sceneBattle, Player player)
        {
            sceneBattle = _sceneBattle;
            _player = player;
        }
        public List<Monster> monsters = new List<Monster>();

        /* public void CreateMonster(List<Monster> monster)
         {
             for (int i = 0; i < monster.Count; i++)
             {
                 monsters.Add((monster[i]));
             }
         }
        */
        public void BattleLoop(Player player, List<Monster> monster)
        {
            player.inventory.UsePotion(player); // Player의 인벤토리 사용

            if (sceneBattle.clearCount >= sceneBattle.maxClearCount)        //보스방이면
            {
                monsters.Add(new Monster(sceneBattle.bossMonsters[sceneBattle.currentFloor - 1]));
            }
            else
            {
                for (int i = 0; i < monster.Count; i++)
                {
                    monsters.Add(monster[i]);
                }
            }

            while (true)
            {
                PlayerAttack();
                if (CheckBattleEnd(_player)) break;

                EnemyPhase(_player);
                if (CheckBattleEnd(_player)) break;
            }
        }

        public (int, Player) WhatSelectDamage(Player player)
        {
            int result;
            while (true)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" 어떤 방식으로 공격하시겠습니까?");
                sb.AppendLine("");
                sb.AppendLine(" 1. 공격");
                for (int i = 0; i < player.skill.Count; i++)
                {
                    sb.AppendLine($" {i + 2}. {player.skill[i].name}");
                }
                sb.AppendLine();
                sb.Append(" >> ");

                Console.Write(sb.ToString());
                string input = Console.ReadLine();                       //input 추가해서
                bool isNumber = int.TryParse(input, out result);        //bool 값을 받아서
                if (isNumber)                                           //숫자일때만
                {
                    if (1 <= result && result <= player.skill.Count + 1)    //1~스킬개수만큼
                        return (result , player);
                }
                Console.WriteLine(" 잘못된 값을 입력하셨습니다.");          //나머지 (범위밖 숫자, 문자 등)
            }
        }

        public float SelectDamage(int result, Player player)
        {
            while (true)
            {
                float damage = 0;
                if (result == 1)                                        //result = -2 에서 1로 수정
                    damage = player.atk;
                else
                {
                    damage = player.skill[result - 2].UseSkill(player);   //result에서 result-2로 수정
                    
				}
                return damage;
            }
        }

        public void PlayerAttack()
        {
            while (true)
            {
                BattleMenu();

                string input = Console.ReadLine();

                if (input == "0") return; // 0번을 누르면 전투 턴 종료 (임시)

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                    continue;
                }

                if (choice == 1) // 공격 선택
                {
                    // 공격할 몬스터 선택 로직
                    Console.WriteLine("\n 공격할 몬스터 번호를 선택하세요:");
                    string monsterInput = Console.ReadLine();
                    if (!int.TryParse(monsterInput, out int monsterChoice) || monsterChoice < 3 || monsterChoice > monsters.Count + 2)
                    {
                        Console.WriteLine(" 잘못된 입력입니다.");
                        continue;
                    }

                    Monster target = monsters[monsterChoice - 3];
                    if (!target.IsAlive())
                    {
                        Console.WriteLine(" 이미 죽은 몬스터입니다.");
                        continue;
                    }
                    float damaged;

					while (true)
                    {
                        (int result, _player) = WhatSelectDamage(_player);
                        damaged = SelectDamage(result, _player);
                        if (damaged != 0)
                            break;
                    }
                    float criticalDamage = _player.CriticalDamage(_player, damaged);
                    float finalDamage = GetRandomDamage(criticalDamage);
                    target.TakeDamage(finalDamage);
                    PlayerAttackLog(_player, target, finalDamage);
                    break; // 공격 후 플레이어 턴 종료
                }
                else if (choice == 2) // 포션 사용 선택
                {
                    _player.inventory.UsePotion(_player);
                    Console.WriteLine("\n 계속하려면 아무 키나 누르세요...");
                    Console.ReadKey();
                    continue; // 포션 사용 후 다시 행동 선택
                }
                else if (choice >= 3 && choice <= monsters.Count + 2) // 몬스터 공격 선택 (직접 번호 입력)
                {
                    Monster target = monsters[choice - 3];
                    if (!target.IsAlive())
                    {
                        Console.WriteLine(" 이미 죽은 몬스터입니다.");
                        continue;
                    }

                    (int result, _player) = WhatSelectDamage(_player);
                    float damaged = SelectDamage(result, _player);
                    float criticalDamage = _player.CriticalDamage(_player, damaged);
                    float finalDamage = GetRandomDamage(criticalDamage);
                    target.TakeDamage(finalDamage);
                    PlayerAttackLog(_player, target, finalDamage);
                    break; // 공격 후 플레이어 턴 종료
                }
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                }
            }
        }

        public void EnemyPhase(Player player)
        {
            foreach (var monster in monsters)
            {
                if (!monster.IsAlive()) continue;

                float damage = GetRandomDamage(monster.atk);
                player.TakeDamage(damage);
                MonsterAttackLog(monster, player, damage);
            }
            Console.WriteLine("계속 하려면 아무키나 입력");
            Console.ReadLine();
			if (!player.IsAlive()) return;
        }

        public bool CheckBattleEnd(Player player)
        {
            if (!player.IsAlive())
            {
                Result(false, player, monsters);
                return true;
            }

            int aliveCount = 0;
            foreach (var monster in monsters)
            {
                if (monster.IsAlive())
                    aliveCount++;
            }

            if (aliveCount == 0)
            {
                Result(true, player, monsters);
                return true;
            }

            return false;
        }

        private Random rand = new Random();

        public float GetRandomDamage(float defaultDamage)
        {
            float error = defaultDamage * 0.1f;
            int roundedError = (int)(error + 0.999f); // 올림 작업

            int minDamage = (int)(defaultDamage - roundedError);
            int maxDamage = (int)(defaultDamage + roundedError);

            return rand.Next(minDamage, maxDamage + 1);
        }
        public void BattleMenu()
        {
            Console.Clear();
            Console.WriteLine(" 원하는 행동을 선택하세요:");
            Console.WriteLine(" 1. 공격");
            Console.WriteLine(" 2. 포션 사용"); // 포션 사용 선택지 추가
            Console.WriteLine("\n 공격할 몬스터를 선택하세요:");
            for (int i = 0; i < monsters.Count; i++)
            {
                var m = monsters[i];
                string status = m.IsAlive() ? $" HP: {m.hp}" : "Dead";
                Console.WriteLine($" {i + 3}. {m.name} (Lv.{m.level}) - {status}"); // 공격 선택지 번호 조정
            }

            //Console.WriteLine("\n0. 돌아가기");
            Console.WriteLine("\n");
            _player.ViewStatus();
            
            Console.Write(">> ");
        }

        public void PlayerAttackLog(Player attacker, Monster target, float damage)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("Battle!!\n");
            sb.AppendLine($"{attacker.name} 의 공격!");
            sb.Append($"Lv.{target.level} {target.name} 을(를) 맞췄습니다. [데미지 : {damage}] | ");

            if (target.hp <= 0)
            {
                //sb.Append($"Lv.{target.level} {target.name} |  ");
                sb.AppendLine($"(HP {target.PreviousHP} -> Dead)");
            }
            else
            {
               // sb.Append($"Lv.{target.level} {target.name} |  ");
                sb.AppendLine($"(HP {target.PreviousHP} -> {target.hp})");
            }
            Console.WriteLine(sb.ToString());

            Thread.Sleep(1000);

        }

		public void MonsterAttackLog(Monster attacker, Player target, float damage)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine($"{attacker.name} 의 공격!");
			sb.Append($"Lv.{target.level} {target.name} 을(를) 맞췄습니다. [데미지 : {damage-target.def}] | ");

			if (target.maxHp <= 0)
			{
				sb.AppendLine($"(HP {target.hp} -> Dead)");
			}
			else
			{
				sb.AppendLine($"(HP {target.PreviousHP} -> {target.hp})");
			}

			Console.WriteLine(sb.ToString());
			Thread.Sleep(1000);

		}

		/*
        public void MonsterAttackLog(Monster attacker, Player target, float damage)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Battle!!\n");
            sb.AppendLine($"{attacker.name} 의 공격!");
            sb.AppendLine($"Lv.{target.level} {target.name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");

            if (target.maxHp <= 0)
            {
                sb.AppendLine($"Lv.{target.level} {target.name}");
                sb.AppendLine($"HP {target.PreviousHP} -> Dead");
            }
            else
            {
                sb.AppendLine($"Lv.{target.level} {target.name}");
                sb.AppendLine($"HP {target.PreviousHP} -> {target.maxHp}");
            }

            sb.AppendLine("\n0. 다음\n>>");
            Console.WriteLine(sb.ToString());
            Console.ReadLine();

        }
        */
		public void Result(bool isVictory, Player player, List<Monster> monsters)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();

            if (!isVictory)
            {
                sceneBattle.clearCount = 0;
                sb.AppendLine(" Battle!! - Result\n");
                Console.Write(sb.ToString());
                sb.Clear();
                Console.WriteLine(sb.ToString());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" You Lose\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(" 0. 다음");
                Console.ResetColor();
                sb.AppendLine(" >> ");
                Console.Write(sb.ToString());
                Console.ReadLine();
                sb.Clear();
				return;
            }

            List<Item> droppedItems = new List<Item>();
            int totalGold = 0;
            int totalExp = 0;
            int killCount = 0;
            Random rand = new Random();

            foreach (var monster in monsters)
            {
                if (monster.IsAlive()) continue;

                Reward reward = monster.GetReward();
                totalGold += reward.gold;
                totalExp += reward.exp;
                killCount++;

                List<Item> dropItems = monster.GetDropItems(sceneBattle.currentFloor);
                foreach (var item in dropItems)
                {
                    int dropCount = rand.Next(0, 3);
                    for (int i = 0; i < dropCount; i++)
                    {
                        droppedItems.Add(item);
                    }
                }
            }
            sceneBattle.clearCount++;
            if (sceneBattle.clearCount > sceneBattle.maxClearCount) //
            {
                if (sceneBattle.dungeonFloor.Exists(x => sceneBattle.currentFloor == sceneBattle.dungeonFloor.Count && sceneBattle.dungeonFloor.Count < 3))   //3층 이하일때 현재최고층 난이도를 깨야만 층이 추가되도록
                {
                    player.quest.PlayEvent(player.quest, monsters.Count ,monsters[0].name);
                    sceneBattle.dungeonFloor.Add(sceneBattle.currentFloor + 1);
                }
                sceneBattle.clearCount = 1;
            }
            int prevLevel = player.level;
            int prevExp = player.exp;
            float prevHP = player.maxHp;
            int prevGold = player.Gold;

            player.Gold += totalGold;
            player.Exp += totalExp;
            sceneBattle.AddDroppedItemsToInventory(player, droppedItems);

            sb.AppendLine(" Battle!! - Result\n");
            sb.AppendLine(" Victory\n");
            sb.AppendLine($" 던전에서 몬스터 {killCount}마리를 잡았습니다.\n");
            Console.WriteLine(sb.ToString());
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [캐릭터 정보]");
            Console.ResetColor();
            sb.AppendLine(" =========================");
            sb.AppendLine($" Lv.{prevLevel} {player.name} -> Lv{player.level}. {player.name}");
            sb.AppendLine($" HP {prevHP} -> {player.hp}");
            sb.AppendLine($" exp {prevExp} -> {player.exp}\n");

            sb.AppendLine("[캐릭터 정보]");
            sb.AppendLine($"Lv.{prevLevel} {player.name}  -> Lv{player.level}. {player.name}");
            sb.AppendLine($"HP {prevHP}  -> {player.maxHp}");
            sb.AppendLine($"exp {prevExp}  -> ({totalExp}){player.exp}\n");

            sb.AppendLine("[획득 아이템]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            sb.AppendLine($"({prevGold} -> {player.Gold}) Gold");
            Console.ResetColor();


            var groupedItems = droppedItems.GroupBy(i => i.name).Select(g => new { Name = g.Key, Count = g.Count() });
            foreach (var item in groupedItems)
            {
                sb.AppendLine($" {item.Name} - {item.Count}");
                Console.WriteLine(sb.ToString());
                sb.Clear();
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.WriteLine(" 0. 다음");
            Console.ResetColor();
            sb.Append(" >> ");
            Console.Write(sb.ToString());
            sb.Clear();
        }

        public void AddItem(Dictionary<string, int> items, List<string> dropItems)
        {
            Random rand = new Random();
            foreach (var item in dropItems)
            {
                int dropCount = rand.Next(0, 3); // 0~2개 드랍.
                for (int i = 0; i < dropCount; i++)
                {
                    if (items.ContainsKey(item))
                        items[item]++; // 중복 아이템일 시 수량 증가
                    else
                        items[item] = 1; // 새 아이템일 시 새로운 항목 추가
                }

            }
        }
    }
}
