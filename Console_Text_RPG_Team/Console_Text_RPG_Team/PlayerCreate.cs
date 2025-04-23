using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Console_Text_RPG_Team
{
    internal class PlayerCreate
    {
        StringBuilder sb = new StringBuilder();

        public void Start(Player player)
        {
            sb.AppendLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 설정해주세요.");
            Console.Write(sb.ToString());
            Input(player);
            //JobInput();
        }
        public void Input(Player player)
        {
            string? playername;

			{
                Console.WriteLine("이름을 입력해주세요 ");
                Console.Write(">> ");
                while (true)
                {

                    playername = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(playername))
                    {
                        Console.Write("잘못된 이름입니다. \n이름을 다시 입력해주세요.\n>> ");
                    }
                    else
                    {
                        Console.Write($"{playername}으로 시작하시겠습니까?\n 1.네 2. 아니요\n>> ");
                        break;
                    }
                }
                while (true)
                {
                    int input = int.Parse(Console.ReadLine());
                    if (input == 1)
                    {
                        player.name = playername;
                        Console.WriteLine($"{player.name}님 환영합니다.");
                        break;
                    }
                    else if (input == 2)
                    {
                        sb.AppendLine("이름을 다시 입력해주세요.");
                        sb.Append(">> ");
                        Console.Write(sb.ToString());
                        sb.Clear();
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("올바른 숫자를 입력해주세요.");
                    }

                }

            }

        }

        public PlayerCreate()
        {
        }

    }
}
   

