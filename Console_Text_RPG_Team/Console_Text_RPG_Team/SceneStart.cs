using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum State
{
	Exit = 0,
	Status = 1,
	Battle = 2
}

namespace Console_Text_RPG_Team
{

	internal class SceneStart
	{
		SceneStatus sceneStatus = new SceneStatus();
		SceneBattleAttack sceneBattle = new SceneBattleAttack();

		StringBuilder sb = new StringBuilder();
		public void Start(Player player)
		{
			Console.Clear();
			sb.AppendLine($"스파르타 던전에 오신 {player.name} 님 환영합니다.");
			sb.AppendLine("이제 전투를 시작할수 있습니다.");
			sb.AppendLine();
			sb.AppendLine("1.상태 보기");
			sb.AppendLine("2.전투 시작");
			sb.AppendLine("0.게임 종료");
			sb.AppendLine();
			sb.AppendLine("원하시는 행동을 입력해주세요.");
			Console.WriteLine(sb.ToString());
			sb.Clear();
			int result = Checkinpvt(0, 2);

			switch (result)
			{
				case 1:
					sceneStatus.Start();
					break;

				case 2:
					sceneBattle.BattleLoop(player);
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
