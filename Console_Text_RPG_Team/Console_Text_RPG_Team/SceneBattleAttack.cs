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
                player.RegenerateMp();
            }

            void BattleResult(Player player, List<Monster> deadMonster)  //이부분
            {
                // ... 기존 코드 ...
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("[획득 아이템]");
                Console.ForegroundColor = ConsoleColor.Yellow;

                List<Item> totalDroppedItems = new List<Item>();
                foreach (var monster in deadMonster)
                {
                    totalDroppedItems.AddRange(monster.GetDropItems(sceneBattle.currentFloor));
                }

                var groupedItems = totalDroppedItems.GroupBy(i => i.name).Select(g => new { Name = g.Key, Count = g.Count() });
                foreach (var item in groupedItems)
                {
                    sb.AppendLine($" {item.Name} - {item.Count}");
                }
                Console.ResetColor();
                Console.Write(sb.ToString());
                sb.Clear();

                sceneBattle.AddDroppedItemsToInventory(player, totalDroppedItems); // 획득한 아이템을 인벤토리에 추가

                // ... 기존 코드 ...
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

                Console.Write(sb.ToString());
                sb.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(" 1. 공격");
                Console.WriteLine(" 2. 물약(회복)");
                for (int i = 0; i < player.skill.Count; i++)
                {
                    Console.WriteLine($" {i + 3}. {player.skill[i].name}");
                    Console.ResetColor();

                }
                sb.AppendLine();
                sb.Append(" >> ");
                Console.Write(sb.ToString());
                sb.Clear();
                string input = Console.ReadLine();                       //input 추가해서
                bool isNumber = int.TryParse(input, out result);        //bool 값을 받아서
                if (isNumber)                                           //숫자일때만
                {
                    if (1 <= result && result <= player.skill.Count + 2)    //1~스킬개수만큼
                        return (result , player);
                }
                Console.WriteLine(" 잘못된 값을 입력하셨습니다.");
                Console.Write(" >> ");
            }
        }

        public int SelectDamage(int result, Player player)
        {
            while (true)
            {
                int damage = 0;
                if (result == 2)
                {

						_player.inventory.UsePotion(_player);
						Console.WriteLine("\n 계속하려면 아무 키나 누르세요...");
						Console.ReadKey();
                        // 포션 사용 후 다시 행동 선택

						return damage; //물약 사용
                }

                if (result == 1)                                        //result = -2 에서 1로 수정
                    damage = player.atk;
                else
                {
                    damage = player.skill[result - 3].UseSkill(player);   //result에서 result-2로 수정
                    
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
                    Console.Write(" >> ");
                    continue;
                }
               
             
                    Monster target = monsters[choice - 1];
                    if (!target.IsAlive())
                    {
                        Console.WriteLine(" 이미 죽은 몬스터입니다.");
                        Console.WriteLine(" 너무 잔인하시네요...");
                        Thread.Sleep(700);


                        continue;
                    }

                    (int result, _player) = WhatSelectDamage(_player);
                    int damaged = SelectDamage(result, _player);
                    if (damaged == 0) continue; // 물약 사용 시 턴 종료
                    int criticalDamage = _player.CriticalDamage(_player, damaged);
                    int finalDamage = GetRandomDamage(criticalDamage);
                    target.TakeDamage(finalDamage);
                    PlayerAttackLog(_player, target, finalDamage);
                    break; // 공격 후 플레이어 턴 종료

                           //}
                           //else
                           //{
                           //    Console.WriteLine(" 잘못된 입력입니다.");
                           //}

                }
            }
        

        public void EnemyPhase(Player player)
        {
            foreach (var monster in monsters)
            {
                if (!monster.IsAlive()) continue;

                int damage = GetRandomDamage(monster.atk);
                player.TakeDamage(damage);
                MonsterAttackLog(monster, player, damage);
            }
            Console.WriteLine(" 계속 하려면 아무키나 입력해주세요.");
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

        public int GetRandomDamage(int defaultDamage)
        {
            int error = (int)(defaultDamage * 0.1f);
            int roundedError = (int)(error + 0.999f); // 올림 작업

            int minDamage = (int)(defaultDamage - roundedError);
            int maxDamage = (int)(defaultDamage + roundedError);

            return rand.Next(minDamage, maxDamage + 1);
        }
        public void BattleMenu()
        {

            Console.Clear();         
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(" [ 몬스터 ]");
            Console.ResetColor();
            Console.WriteLine(" =========================");
            for (int i = 0; i < monsters.Count; i++)
            {
                var m = monsters[i];
                string status = m.IsAlive() ? $" HP: {m.hp}" : "Dead";

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($" {i + 1}. {m.name} (Lv.{m.level}) - {status}"); // 공격 선택지 번호 조정
                Console.ResetColor();
 

            }
            Console.WriteLine(" =========================");
            //Console.WriteLine("\n0. 돌아가기");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [ 내 정보 ]"); 
            Console.ResetColor();
            Console.WriteLine(" =========================");
            _player.ViewStatus();
            Console.WriteLine(" =========================");
            Console.WriteLine();
            Console.Write(" >>");
        }

        public void PlayerAttackLog(Player attacker, Monster target, int damage)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("");
            Console.WriteLine(" [ 전투 시작! ]\n");
            Console.ResetColor();
            sb.AppendLine($" {attacker.name} 의 공격!");
            sb.Append($" Lv.{target.level} {target.name} 을(를) 맞췄습니다. [데미지 : {(damage - target.def > 1 ? damage - target.def : 1)}] | ");


            if (target.hp <= 0)
            {
                _player.audio[4].Play();
                //sb.Append($"Lv.{target.level} {target.name} |  ");
                sb.AppendLine($" (HP {target.PreviousHP} -> Dead)");
            }
            else
            {

				// sb.Append($"Lv.{target.level} {target.name} |  ");
				_player.audio[3].Play();
				sb.AppendLine($"(HP {target.PreviousHP} -> {target.hp})");

            }
            Console.WriteLine(sb.ToString());

            Thread.Sleep(1000);

        }

		public void MonsterAttackLog(Monster attacker, Player target, int damage)
		{
			StringBuilder sb = new StringBuilder();


			sb.AppendLine($"{attacker.name} 의 공격!");
			sb.Append($"Lv.{target.level} {target.name} 을(를) 맞췄습니다. [데미지 : {(damage-target.def > 1 ? damage-target.def : 1)}] | ");


			if (target.maxHp <= 0)
			{
				sb.AppendLine($" (HP {target.hp} -> Dead)");
			}
			else
			{
				sb.AppendLine($" (HP {target.PreviousHP} -> {target.hp})");
			}

			Console.WriteLine(sb.ToString());
			Thread.Sleep(1000);

		}

		public void Result(bool isVictory, Player player, List<Monster> monsters)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();

            if (!isVictory)
            {
                sceneBattle.clearCount = 0;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("");
                Console.WriteLine(" [ 전투 결과.... ]\n");
                Console.ResetColor();
                Console.WriteLine(sb.ToString());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" [ 클리어에 실패하셨습니다. ]\n");
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
            int prevHP = player.maxHp;
            int prevGold = player.Gold;

            player.Gold += totalGold;
            player.Exp += totalExp;
            sceneBattle.AddDroppedItemsToInventory(player, droppedItems);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(" [ 전투 결과 ]\n");
            Console.ResetColor();
            sb.AppendLine(" Victory\n");
            sb.AppendLine($" 던전에서 몬스터 {killCount}마리를 잡았습니다.\n");
            Console.WriteLine(sb.ToString());
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [ 캐릭터 정보 ]");
            Console.ResetColor();
            sb.AppendLine(" =========================");
            sb.AppendLine($" Lv.{prevLevel} {player.name} -> Lv{player.level}. {player.name}");
            sb.AppendLine($" HP {prevHP} -> {player.hp}");
            sb.AppendLine($" exp {prevExp} -> {player.exp}\n");
            Console.WriteLine(sb.ToString());
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [ 캐릭터 변경 정보 ]");
            Console.ResetColor();
            sb.AppendLine(" =========================");
            sb.AppendLine($" Lv.{prevLevel} {player.name}  -> Lv{player.level}. {player.name}");
            sb.AppendLine($" HP {prevHP}  -> {player.maxHp}");
            sb.AppendLine($" exp {prevExp}  -> ({totalExp}){player.exp}\n");
            sb.AppendLine(" [ 획득 아이템 ]");
            sb.AppendLine($" ({prevGold} -> {player.Gold}) Gold");
            Console.WriteLine(sb.ToString());
            sb.Clear();

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
