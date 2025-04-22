using System;
using System.Text;

namespace Console_Text_RPG_Team
{
    internal class PlayerCreate
    {
        private StringBuilder sb = new StringBuilder();
        private Player player;
        public void Start()
        {
            player = new Player();
            sb.AppendLine("스파르타 던전에 오신 여러분 환영합니다.");
            sb.AppendLine("원하시는 이름을 설정해주세요.");
            Console.WriteLine(sb.ToString());
            sb.Clear();

            Input();
        }

        private void Input()
        {
            //Console.WriteLine("이름을 입력해주세요.");
            sb.AppendLine("이름을 입력해주세요.");
            sb.Append(">> ");
            Console.Write(sb.ToString());
            sb.Clear();
            while (true)
            {            
                string playername = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playername))
                {
                    sb.AppendLine("잘못된 이름입니다.");
                    sb.AppendLine("이름을 다시 입력해주세요.");
                    sb.Append(">> "); Console.Write(sb.ToString());
                    sb.Clear();
                    continue;
                }

                player.name = playername;

                while (true)
                {
                    sb.AppendLine($"{player.name}으로 시작하시겠습니까? 1. 네  2. 아니요");
                    sb.Append(">> ");
                    Console.Write(sb.ToString());
                    sb.Clear();
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out int choice))
                    {
                        sb.AppendLine("올바른 숫자를 입력해주세요.");
                        sb.Append(">> ");
                        Console.Write(sb.ToString());
                        sb.Clear();
                        continue;
                    }

                    if (choice == 1)
                    {
                        sb.AppendLine($"{player.name}님 환영합니다. 게임을 시작합니다.");
                        Console.Write(sb.ToString());
                        sb.Clear();
                        return;
                    }
                    else if (choice == 2)
                    {
                        sb.AppendLine("이름을 다시 입력해주세요.");
                        sb.Append(">> ");
                        Console.Write(sb.ToString());
                        sb.Clear();
                        break;  // 외부 while 루프로 이동
                    }
                    else
                    {
                        sb.AppendLine("올바른 숫자를 입력해주세요.");
                        sb.Append(">> ");
                        Console.Write(sb.ToString());
                        sb.Clear();
                    }
                }
            }
        }
    }
}
