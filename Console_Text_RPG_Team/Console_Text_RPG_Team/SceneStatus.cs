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
            Console.WriteLine();
            Console.WriteLine(" [ 상태 보기 ]".PadLeft(10));
            Console.ResetColor();
            Console.WriteLine(" 용사님의 정보가 표시됩니다.");
            Console.WriteLine(" ====================");
			player.ViewStatus();
            //	sb.Append($" 이  름	: {player.name}").Append("\n");
            //sb.Append($" Lv.	: {player.level}").Append("\n");
            //sb.Append($" 직업	: {player.job}").Append("\n");
            //sb.Append($" 체  력	: {player.hp}").Append("\n"); 
            //sb.Append($" 공격력	: {player.atk}").Append("\n"); 
            //sb.Append($" 방어력	: {player.def}").Append("\n"); 
            //sb.Append($" 돈	: {player.Gold}").Append("\n");
            Console.WriteLine(" ====================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($" 보유 스탯 포인트 : {player.stat}");
            Console.WriteLine();
            Console.ResetColor();
            Console.Write(sb.ToString());
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("1. 스탯분배");
			Console.WriteLine("0. 나가기\n");
			Console.ResetColor();
			Console.Write(" >> ");

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
							Console.WriteLine(" 다시 입력해주십시오");

                            Thread.Sleep(500);

                            continue;
					}
				}
				catch (Exception)
				{
					Console.WriteLine(" 다시 입력해주십시오");

                    Thread.Sleep(500);

                    continue;
				}
				
			}
		}

		public void StatSelect(Player player)
		{
			while (true)
			{
				Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [ 스탯 분배 ]".PadLeft(10));
                Console.ResetColor();
                Console.WriteLine(" 용사님의 정보가 표시됩니다.\n");
                Console.WriteLine(" ====================");
                player.ViewStatus();
                Console.WriteLine(" ====================");
                Console.WriteLine();
                Console.WriteLine(" 스탯을 분배할 수 있습니다.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($" 보유 스탯포인트 : {player.stat}");	
				Console.WriteLine();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($" 1. {"체력",-5} {"(+10)",-5}");
                Console.WriteLine($" 2. {"공격력",-5}{"(+3)",-5} ");
                Console.WriteLine($" 3. {"방어력",-5}{"(+1)",-5}");
                Console.WriteLine($" 4. {"마나",-5} {"(+1)",-5}");
                Console.WriteLine(" 0. 나가기");
                Console.ResetColor();
				sb.AppendLine(" 원하시는 행동의 번호를 입력해주세요.");
				sb.Append(" >> ");
                Console.Write(sb.ToString());

				sb.Clear();
				try
				{
					int input = int.Parse(Console.ReadLine());
					if (input > 4 || 0 > input)
					{

						Console.WriteLine("다시 입력해주십시오");
						Thread.Sleep(500);
						continue;

					}

					if (input == 0)
						return;
					player.StatAdd(input);
				}
				catch (Exception)
				{

					Console.WriteLine("다시 입력해주십시오");
                    Thread.Sleep(500);

                    continue;
				}

			}
		}
	}
}
