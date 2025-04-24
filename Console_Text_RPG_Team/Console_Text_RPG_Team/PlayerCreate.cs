using System;
using System.Text;

namespace Console_Text_RPG_Team
{
    internal class PlayerCreate
    {
        private StringBuilder sb = new StringBuilder();

        public void Start(Player player)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow; //npc가 말할 때는 DarkYellow로
            Console.WriteLine();
            Console.WriteLine("메이플 던전에 오신 여러분 환영합니다.".PadLeft(22));
            Console.WriteLine("용사님의 이름을 알려주세요.".PadLeft(20));
            Console.ResetColor();
            Console.WriteLine(sb.ToString());
            sb.Clear();

            Input(player);
        }

        private void Input(Player player)
        {
            //Console.WriteLine("이름을 입력해주세요.");
            sb.AppendLine(" 이름을 입력해주세요.");
            sb.Append(" >> ");
            Console.Write(sb.ToString());
            sb.Clear();
            while (true)
            {
                string playername = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playername))
                {
                    sb.AppendLine(" 이름을 다시 입력해주세요.");
                    sb.Append(" >> "); Console.Write(sb.ToString());
                    sb.Clear();
                    continue;
                }

                player.name = playername;

                while (true)
                {
                    Console.WriteLine($" {player.name}용사님이 맞나요?");
                    Console.ForegroundColor = ConsoleColor.DarkCyan; //선택지는 DarkCyan
                    Console.WriteLine($" [1. 네  2. 아니요]");
                    Console.ResetColor();
                    sb.Append(" >> ");
                    Console.Write(sb.ToString());
                    sb.Clear();
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out int choice))//tryparse는 bool형으로 성공여부를 반환합니다.    
                    {
                        sb.AppendLine("올바른 숫자를 입력해주세요.");
                        sb.Append(">> ");
                        Console.Write(sb.ToString());
                        sb.Clear();
                        continue;
                    }
                    if (choice == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow; //npc가 말할 때는 DarkYellow로
                        Console.WriteLine($" {player.name}용사님, 전설이 시작됩니다.");
                        Console.WriteLine(" 마지막에 엄청난 보상이 있을 수도, 없을 수도…? 그건 직접 겪어보셔야죠!");
                        Thread.Sleep(1500);
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