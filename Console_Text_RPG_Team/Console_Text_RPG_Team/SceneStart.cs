using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class SceneStart
    {
        StringBuilder sb = new StringBuilder();
        public void Start()
        {
            Console.Clear();
            sb.AppendLine("스파르타 던전에 오신 여러분 환영합니다.");
            sb.AppendLine("이제 전투를 시작할수 있습니다.");
            sb.AppendLine();
            sb.AppendLine("1.상태 보기");
            sb.AppendLine("2.전투 시작");
            sb.AppendLine();
            sb.AppendLine("원하시는 행동을 입력해주세요.");

            Console.WriteLine(sb.ToString());
            int result = Checkinpvt(1, 2);

            switch (result)
            {
                case 1:
                    //SceneStatus();
                    break;

                case 2:
                    //SceneBattle();
                    break;
            }
        }
        public int Checkinpvt(int min, int max)
        {
            int result;
            while (true)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out result);
                if (isNumber)
                {
                    if (result >= min && result <= max)
                        return result;
                }
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}