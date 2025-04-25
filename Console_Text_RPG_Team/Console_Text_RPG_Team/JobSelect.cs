using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{

	internal class JobSelect
	{
		//Player player = new Player(); 
		StringBuilder sb = new StringBuilder();
		List<Job> job = new List<Job>(4)
		{
			new Job("전사  "        , 1000+500, 150+ 30, 0+20, 25),
			new Job("궁수  "        , 1000+200, 150+120, 0+15, 30),
			new Job("마법사"		, 1000+100, 150+ 60, 0+10, 50),
			new Job("도적  "        , 1000+300, 150+120, 0+15, 25)
		};

		public void Start()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("[ 직업 선택 ]".PadLeft(10));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($" 장로 스탄: 직업을 선택해주게나");
			Console.ResetColor();

            sb.AppendLine(" =========================================================================");
            for (int i = 0; i < job.Count; i++)
			{
				sb.AppendFormat($" | {(i + 1)}. 직업: {job[i].name}" + ("체력 :").PadLeft(8) + $"{job[i].hp, -4} 공격력 : {job[i].atk, -4} 방어력 : {job[i].def, -4} 마나 : {job[i].mp,-4}||").Append("\n");
			}
            sb.AppendLine(" =========================================================================");
            sb.Append("\n");
            sb.Append(" 원하시는 직업의 번호를 선택해주세요.").Append("\n");
			Console.Write(sb.ToString());
			sb.Clear();
		}

		public void Input(Player player)
		{
			Console.Write(" >> ");
			try
			{
                Thread.Sleep(500);
                int input = int.Parse(Console.ReadLine());
				if (input > 0 && input <= job.Count)
				{
					int index = input - 1;
					player.HpUpSet(job[index].hp - player.hp);
					player.atk = job[index].atk;
					player.def = job[index].def;
					player.job = job[index].name;
					player.MpUpSet(job[index].mp - player.mp);
					int hp = player.maxHp;
					int atk = player.atk;
					int def = player.def;
					int mp = player.maxMp;
					string name = player.job;


					StringBuilder sb = new StringBuilder();

                    sb.AppendLine($" {index + 1}. 직업 : {name, -4} 체력 : {hp,-4} 공격력 : {atk, -4} 방어력 : {def, -4} 마력 : {mp, -4} 을 선택하셨습니다.");
                    sb.Append("\n");
                    sb.AppendLine($" {player.name} 용사님 {player.job} 직업을 선택하셨습니다.");
                    Console.Write(sb.ToString());
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
					Console.Write(" 장로 스탄: 단풍 마을로 가기 위해서  나를 따라오게나 ");
				    Console.ResetColor();	
                    Thread.Sleep(500);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("...");
					Thread.Sleep(500);
					Console.Write(" 이동 중");
                    Console.ResetColor();
                    Thread.Sleep(1000);
					return;
				}
				else
				{
					Console.WriteLine(" 다시 입력해주십시오");
                    Console.Write(" >> ");
                }
			}
			catch (Exception)
			{
				Console.WriteLine(" 다시 입력해주십시오");
                Console.Write(" >> ");
            }

		}
	}
}

