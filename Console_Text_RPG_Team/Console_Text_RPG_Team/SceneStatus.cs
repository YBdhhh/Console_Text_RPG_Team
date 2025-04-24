using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
	internal class SceneStatus
	{
		StringBuilder sb = new StringBuilder();

		public void Start(Player player)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("상태 보기");
			Console.ResetColor();
			player.ViewStatus();
			sb.AppendLine($"스탯포인트 : {player.stat}");
			sb.AppendLine("\n");
			sb.AppendLine("1. 스탯분배");
			sb.AppendLine("0. 나가기").Append("\n");
			sb.Append("원하시는 행동을 입력해주세요.").Append("\n");
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
						case 1:
							StatSelect(player);
							return;
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

		public void StatSelect(Player player)
		{
			while (true)
			{
				Console.Clear();
				sb.AppendLine("스탯을 분배합니다");
				sb.AppendLine($"현재 스탯포인트 : {player.stat}");
				player.ViewStatus();
				sb.AppendLine("1. 체력");
				sb.AppendLine("2. 공격력");
				sb.AppendLine("3. 방어력");
				sb.AppendLine("4. 마력");
				sb.AppendLine("0. 나가기");
				sb.Append("원하시는 행동을 입력해주세요.").Append("\n");
				Console.Write(sb.ToString());
				sb.Clear();
				try
				{
					int input = int.Parse(Console.ReadLine());
					if (input > 4 || 0 > input)
					{
						Console.WriteLine("다시 입력해주십시오");
						continue;
					}

					if (input == 0)
						return;
					player.StatAdd(input);
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
