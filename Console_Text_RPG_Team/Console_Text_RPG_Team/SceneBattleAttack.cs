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
        public Player player;
        public List<Monster> monsters;

        public SceneBattleAttack()
        {
            player = new Player();
            monsters = new List<Monster>
            {
                new Monster("미니언", 15f, 5f, 2),
                new Monster("대포미니언", 25f, 8f, 5),
                new Monster("공허충", 10f, 4f, 3)
            };
        }

        public void Start()
        {
            PlayerAttack();
        }

        public void PlayerAttack()
        {
            while (true)
            {
                BattleMenu();

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

        public void EnemyPhase()
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

        public bool CheckBattleEnd()
        {
            if(!player.IsAlive())
            {
                Result(false);
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
                Result(true);
                return true;
            }

            return false;
        }

       
        public float GetRandomDamage(float defaultDamage)
        {
            return defaultDamage;
        }
        public void BattleMenu()
        {

        }

        public static void PlayerAttackLog(Player attacker, Monster target, float damage)
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

        public static void MonsterAttackLog(Monster attacker, Player target, float damage)
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

        public void Result(bool isVictory)
        {

        }




    }
}
