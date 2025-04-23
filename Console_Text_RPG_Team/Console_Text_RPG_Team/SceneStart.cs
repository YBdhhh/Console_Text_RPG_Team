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
		List<Skill> skills = new List<Skill>
		{//string name, string explain, int damageType, int[] damage, int valueMp, int price
			new Skill("새비지 블로우", "단검으로 적을 타격한다. Mp 1.5 소모 | 공격력 3배의 피해", (int)DamageType.Atk , new int[] {300}, (int)ValueType.Mp ,1.5f ,1000),
			new Skill("파워 스트라이크", "장착한 무기로 적에게 일격을 가한다. Mp 1.0 소모 | 공격력 1.2배의 피해", (int)DamageType.Atk , new int[] {120}, (int)ValueType.Mp, 1.0f ,1000),
			new Skill("매직 클로", "마력을 활용하여 할퀸다. Mp 0.5 소모 | 공격력 1.2배의 피해", (int)DamageType.Atk , new int[] {120}, (int)ValueType.Mp ,0.5f ,1000),
			new Skill("메소 익스플로전", "동전에 폭탄을 부착하여 던진다. Gold 500 소모 | 공격력의 1.5배의 피해", (int)DamageType.Atk , new int[] {150}, (int)ValueType.Gold, 0.5f ,1000),
			new Skill("방패 밀치기", "방패로 상대를 밀친다. Mp 1.0 소모 | 방어력*5의 피해", (int)DamageType.Def , new int[] {500}, (int)ValueType.Mp, 1.0f ,1000),
			new Skill("몸통박치기", "상대방에게 돌진한다. Hp를 1.0 소모 | 체력*2의 피해", (int)DamageType.Hp , new int[] {200}, (int)ValueType.Hp, 1.0f ,1000),
		};


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
					sceneStatus.Start(player);
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
