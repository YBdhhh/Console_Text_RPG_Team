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
        public SceneBattleAttack(SceneBattle _sceneBattle)
        {
            sceneBattle = _sceneBattle;
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
                PlayerAttack(player);
                if (CheckBattleEnd(player)) break;

                EnemyPhase(player);
                if (CheckBattleEnd(player)) break;
            }
        }

        public (int, Player) WhatSelectDamage(Player player)
        {
            int result;
            while (true)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("어떤 방식으로 공격하시겠습니까?");
                sb.AppendLine("");
                sb.AppendLine("1. 공격");
                for (int i = 0; i < player.skill.Count; i++)
                {
                    sb.AppendLine($"{i + 2}. {player.skill[i].name}");
                }
                sb.AppendLine();
                sb.Append(" >>");
                Console.Write(sb.ToString());
                int.TryParse(Console.ReadLine(), out result);
                if (1 <= result || result <= player.skill.Count + 1)
                    return (result - 2, player);
                else
                {
                    Console.WriteLine("잘못된 값을 입력하셨습니다.");
                }
            }
        }

        public float SelectDamage(int result, Player player)
        {
            float damage = 0;
            if (result == -1)
                damage = player.atk;
            else
            {
                damage = player.skill[result].UseSkill(player);
            }
            return damage;
        }

        public void PlayerAttack(Player player)
        {
            while (true)
            {
                BattleMenu(); //3

                string input = Console.ReadLine();
                //if (input == "0") return;

                if (!int.TryParse(input, out int choice) || choice < 1 || choice > monsters.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

                Monster target = monsters[choice - 1];
                if (!target.IsAlive())
                {
                    Console.WriteLine("이미 죽은 몬스터입니다.");
                    continue;
                }
                int result;
                (result, player) = WhatSelectDamage(player);

                float damaged = SelectDamage(result, player);
                float criticalDamage = player.CriticalDamage(player, damaged);
                float finalDamage = GetRandomDamage(criticalDamage);
                target.TakeDamage(finalDamage);
                PlayerAttackLog(player, target, finalDamage);
                break;
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
                if (!player.IsAlive()) return;
            }
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
            Console.WriteLine("공격할 몬스터를 선택하세요:");
            for (int i = 0; i < monsters.Count; i++)
            {
                var m = monsters[i];
                string status = m.IsAlive() ? $"HP: {m.hp}" : "Dead";
                Console.WriteLine($"{i + 1}. {m.name} (Lv.{m.level}) - {status}");
            }
            Console.WriteLine("\n0. 돌아가기");
            Console.Write(">> ");
        }

        public void PlayerAttackLog(Player attacker, Monster target, float damage)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Battle!!\n");
            sb.AppendLine($"{attacker.name} 의 공격!");
            sb.AppendLine($"Lv.{target.level} {target.name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");

            if (target.hp <= 0)
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

        public void MonsterAttackLog(Monster attacker, Player target, float damage)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Battle!!\n");
            sb.AppendLine($"{attacker.name} 의 공격!");
            sb.AppendLine($"Lv.{target.level} {target.name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");

            if (target.hp <= 0)
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

        public void Result(bool isVictory, Player player, List<Monster> monsters)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();

            if (!isVictory)
            {
                sb.AppendLine("Battle!! - Result\n");
                sb.AppendLine("You Lose\n");
                sb.AppendLine("0. 다음\n>>");
                Console.WriteLine(sb.ToString());
                Console.ReadLine();
                return;
            }

            List<Item> droppedItems = new List<Item>();
            int totalGold = 0;
            int totalExp = 0;
            int killCount = 0;
            Random rand = new Random();

            foreach (var monster in monsters)
            {
                if (!monster.IsAlive()) continue;

                Reward reward = monster.GetReward();
                totalGold += reward.gold;
                totalExp += reward.exp;
                killCount++;

                List<Item> dropItems = monster.GetDropItems();
                foreach (var item in dropItems)
                {
                    int dropCount = rand.Next(0, 3);
                    for (int i = 0; i < dropCount; i++)
                    {
                        droppedItems.Add(item);
                    }
                }
            }

            int prevLevel = player.level;
            int prevExp = player.exp;
            float prevHP = player.hp;

            player.gold += totalGold;
            player.exp += totalExp;
            sceneBattle.AddDroppedItemsToInventory(player, droppedItems);

            sb.AppendLine("Battle!! - Result\n");
            sb.AppendLine("Victory\n");
            sb.AppendLine($"던전에서 몬스터 {killCount}마리를 잡았습니다.\n");

            sb.AppendLine("[캐릭터 정보]");
            sb.AppendLine($"Lv.{prevLevel} {player.name} -> Lv{player.level}. {player.name}");
            sb.AppendLine($"HP {prevHP} -> {player.hp}");
            sb.AppendLine($"exp {prevExp} -> {player.exp}\n");

            sb.AppendLine("[획득 아이템]");
            sb.AppendLine($"{totalGold} Gold");

            var groupedItems = droppedItems.GroupBy(i => i.name).Select(g => new { Name = g.Key, Count = g.Count() });
            foreach (var item in groupedItems)
            {
                sb.AppendLine($"{item.Name} - {item.Count}");
            }

            sb.AppendLine("\n0. 다음\n>>");
            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }

        public void AddItem(Dictionary<string, int> items, List<string> dropItems)
        {
            Random rand = new Random();
            foreach (var item in dropItems)
            {
                int dropCount = rand.Next(0, 3); // 0~2개 드랍
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
