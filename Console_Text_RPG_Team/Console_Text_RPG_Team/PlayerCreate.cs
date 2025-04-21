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
        Player player;
        public void Start()
        {
            sb.AppendLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 설정해주세요.");
            Console.Write(sb.ToString());
            Input();
            //JobInput();
        }
        public void Input()
        {

        {
            Console.WriteLine("이름을 입력해주세요 ");
            Console.Write(">> ");
            while (true)
            {

                string? playername = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playername))
                {
                    Console.Write("잘못된 이름입니다. \n이름을 다시 입력해주세요.\n>> ");
                }
                else
                {
                    player = new Player(playername);
                    Console.Write($"{player.name}으로 시작하시겠습니까?\n 1.네 2. 아니요\n>> ");
                    break;
                }
            }
            while (true)
            {
                int input = int.Parse(Console.ReadLine());
                if (input == 1)
                {
                    Console.WriteLine($"{player.name}님 환영합니다.");
                    break;
                }
                else if (input == 2)
                {
                    Input();
                }
                else
                {
                    Console.WriteLine("올바른 숫자를 입력해주세요.");
                }

            }
          
        }
        
    }
}
   

