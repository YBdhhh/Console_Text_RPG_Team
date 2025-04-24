using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class SceneRest
    {
		StringBuilder sb = new StringBuilder();

		public void Start(Player player)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("=====회복의 요람=====");
			Console.ResetColor();
			sb.AppendLine("\"이용료는 한 번에 30G입니다.\"");
			sb.AppendLine("\"체력의 30%를 회복시켜드립니다.\"");
			sb.AppendLine("");
			sb.AppendLine("캐릭터의 정보가 표시됩니다");
			sb.AppendLine("");
			sb.AppendLine($"이  름	:{player.name}");
			sb.AppendLine($"Lv.	: {player.level}");
			sb.AppendLine($"체  력	: ({player.hp} / {player.maxHp})");
			sb.AppendLine($"돈	: {player.Gold}");
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine("1. 휴식하기");
			sb.AppendLine("0. 나가기");
			sb.AppendLine();
			sb.AppendLine("원하시는 행동을 입력해주세요.");
			sb.Append(">> ");
			Console.Write(sb.ToString());
			sb.Clear();

			Input(player);
		}

		public void Input(Player player)
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
						case 1:
							Console.Clear();
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine("당신은 치유의 요람에서 즐겁게 놀았습니다.");
							Thread.Sleep(500);
							Console.WriteLine("...");
							Thread.Sleep(500);
							Console.ResetColor();
							Console.WriteLine();
							Console.WriteLine("요람의 신비한 힘이 당신을 회복시켰습니다.");
							if (player.maxHp < player.hp * 1.3f)
							{
								player.hp = player.maxHp;
							}
							else
							{
								player.hp += (int)(player.maxHp * 0.3f);
							}
							
							Console.WriteLine($"체력이 회복되었습니다.");
							player.Gold -= 30;
							Thread.Sleep(500);
							Console.WriteLine();
							Console.WriteLine($"현재 체력 : ({player.hp} / {player.maxHp})  | 현재 골드 : {player.Gold}");
							Console.ReadLine();
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
