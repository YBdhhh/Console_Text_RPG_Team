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
			new Job("전사"        , 100+50, 15+ 3, 0+4),
			new Job("궁수"        , 100+20, 15+12, 0+3),
			new Job("마법사"		, 100+10, 15+ 6, 0+2),
			new Job("도적"        , 100+30, 15+12, 0+3)
		};

		public void Start()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("직업 선택");
			Console.ResetColor();
			sb.Append("원하시는 직업을 선택하여 주십시오").Append("\n");
			sb.AppendLine("");
			for (int i = 0; i < job.Count; i++)
			{
				sb.Append($"{i + 1}. 직업 : {job[i].name,-8} 체력 : {job[i].hp,-5} 공격력 : {job[i].atk,-5} 방어력 : {job[i].def,-5} ").Append("\n");
			}
			sb.Append("\n");
			sb.Append("원하시는 행동을 입력해주세요.").Append("\n");
			Console.Write(sb.ToString());
		}

		public void Input(Player player)
		{
			Console.Write(">> ");
			try
			{
				int input = int.Parse(Console.ReadLine());
				if (input > 0 && input <= 3)
				{
					int index = input - 1;
					player.hp = job[index].hp;
					player.maxHp = job[index].hp;
					player.atk = job[index].atk;
					player.def = job[index].def;
					player.job = job[index].name;
					float hp = player.hp;
					float atk = player.atk;
					float def = player.def;
					string name = player.job;


					StringBuilder sb = new StringBuilder();

					sb.AppendLine($"{index + 1}. 직업 : {name,-8} 체력 : {hp,-5} 공격력 : {atk,-5} 방어력 : {def,-5}을 선택하셨습니다.");
					sb.AppendLine("계속 하려면 아무 키나 눌러주십시오");
					sb.Append(">> ");
					Console.Write(sb.ToString());
					Console.ReadLine();
					return;
				}
				else
				{
					Console.WriteLine("다시 입력해주십시오");
				}
			}
			catch (Exception)
			{
				Console.WriteLine("다시 입력해주십시오");
			}

		}
	}
}
