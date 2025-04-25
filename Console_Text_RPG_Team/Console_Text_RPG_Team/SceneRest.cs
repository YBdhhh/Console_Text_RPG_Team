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
            Console.WriteLine();
            Console.WriteLine(" [ 회복의 요람 ]");
			Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" 시워언해: 30G만 내주신다면 용사님의 체력을 30%씩이나 회복시켜드립니다");
            Console.WriteLine($" 용사님의 현재 상태입니다. 30G를 지불하시겠습니까?");
            Console.ResetColor();
			sb.AppendLine(" =====================");
			sb.AppendLine($" 이  름	: {player.name}");
			sb.AppendLine($" Lv.	: {player.level}");
			sb.AppendLine($" 체  력	: {player.hp} / {player.maxHp}");
			sb.AppendLine($" 마  력	: {player.mp} / {player.maxMp}");
			sb.AppendLine($" 돈	: {player.Gold}");
            sb.AppendLine(" =====================");
            sb.AppendLine();
            Console.Write(sb.ToString());
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" 1. 30G를 지불하고 휴식하기");
            Console.WriteLine(" 0. 마을로 돌아가기");
            Console.ResetColor();
            sb.AppendLine();
			sb.AppendLine(" 원하시는 행동의 번호를 입력해주세요.");
			sb.Append(" >> ");
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
							Console.WriteLine(" 당신은 30G를 내고 치유의 요람에서 즐겁게 놀았습니다.");
							Thread.Sleep(500);
							Console.WriteLine("...");
							player.audio[0].Play();
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

							if (player.maxMp < player.mp * 1.3f)
							{
								player.mp = player.maxMp;
							}
							else
							{
								player.mp += (int)(player.maxMp * 0.3f);
							}

							player.Gold -= 30;
							Thread.Sleep(500);
							Console.WriteLine();

							Console.WriteLine($" 현재 체력 : ({player.hp} / {player.maxHp})  | 현재 골드 : {player.Gold}");
							Console.WriteLine($" 현재 마나 : ({player.mp} / {player.maxMp})  |");



							sb.AppendLine("");
                            sb.AppendLine(" 단풍 마을로 가실려면 아무 키나 눌러주십시오 ");
                            Console.Write(sb.ToString());
                            sb.Clear();

							Console.ReadLine();
                            return;

						default:
							Console.WriteLine(" 다시 입력해주십시오");
                            Console.Write(" >> ");
                            continue;
					}
				}
				catch (Exception)
				{
					Console.WriteLine(" 다시 입력해주십시오");
                    Console.Write(" >> ");
                    continue;
				}

			}
		}
	}
}
