using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
	internal class SceneStatus
	{
		public Player player = new Player();
		StringBuilder sb = new StringBuilder();

		public void Start()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("상태 보기");
			Console.ResetColor();
			sb.Append("캐릭터의 정보가 표시됩니다").Append("\n");
			sb.AppendLine("");
			sb.Append($"이  름	:{player.name}").Append("\n");
			sb.Append($"Lv.	: {player.level}").Append("\n");
			sb.Append($"직업	: {player.job}").Append("\n");
			sb.Append($"체  력	: {player.hp}").Append("\n"); 
			sb.Append($"공격력	: {player.atk}").Append("\n"); 
			sb.Append($"방어력	: {player.def}").Append("\n"); 
			sb.Append($"돈	: {player.gold}").Append("\n");
			sb.Append("\n");
			sb.Append("0. 나가기").Append("\n\n");
			sb.Append("원하시는 행동을 입력해주세요.").Append("\n");
			sb.Append(">> ");
			Console.Write(sb.ToString());
		}

		public void Input()
		{
			while (true)
			{
				try
				{
					int input = int.Parse(Console.ReadLine());
					switch (input)
					{
						case 0:
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
