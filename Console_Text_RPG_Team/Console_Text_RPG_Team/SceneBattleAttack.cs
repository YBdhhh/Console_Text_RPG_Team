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
        public List<Monster> monsters;

        public SceneBattleAttack()
        {
            monsters = new List<Monster>
            {
                new Monster("미니언", 15f, 5f, 2),
                new Monster("대포미니언", 25f, 8f, 5),
                new Monster("공허충", 10f, 4f, 3)
            };
        }

        /*public void Start() //1
        {
            PlayerAttack(player);
        }*/

        public void BattleLoop(Player player)
        {
            while (true)
            {
                PlayerAttack(player);
                if (CheckBattleEnd(player)) break;

                EnemyPhase(player);
                if (CheckBattleEnd(player)) break;
            }
        }

        public void PlayerAttack(Player player) //2
        {
            while (true)
            {
                BattleMenu(); //3

                string input = Console.ReadLine();
                if (input == "0") return;
                
                if (!int.TryParse(input, out int choice) || choice < 1 || choice > monsters.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

                Monster target = monsters[choice - 1];
                if(!target.IsAlive())
                {
                    Console.WriteLine("이미 죽은 몬스터입니다.");
                    continue;
                }

                float damage = GetRandomDamage(player.atk);
                target.TakeDamage(damage);
                PlayerAttackLog(player, target, damage);
                break;
            }
        }

        public void EnemyPhase(Player player)
        {
            foreach(var monster in monsters)
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
            if(!player.IsAlive())
            {
                Result(false, player);
                return true;
            }

            int aliveCount = 0;
            foreach (var monster in  monsters)
            {
                if(monster.IsAlive())
                    aliveCount++;
            }

            if (aliveCount == 0)
            {
                Result(true, player);
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

        public void Result(bool isVictory, Player player)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Battle!! - Result\n");

            if(isVictory)
            {
                sb.AppendLine("Victory\n");

                int killCount = 0;

                foreach(var monster in monsters)
                {
                    if (monster.hp <= 0)
                        killCount++;
                }

                sb.AppendLine($"던전에서 몬스터 {killCount}마리를 잡았습니다.\n");
            }
            else
            {
                sb.AppendLine("You Lose\n");
            }

            // 플레이어 현재 상태 출력

            sb.AppendLine($"Lv.{player.level} {player.name}");
            sb.AppendLine($"HP {player.PreviousHP} -> {(player.hp <= 0 ? "0" : player.hp.ToString())}");

            sb.AppendLine("\n0. 다음");
            sb.Append(">> ");
            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }




    }
}
