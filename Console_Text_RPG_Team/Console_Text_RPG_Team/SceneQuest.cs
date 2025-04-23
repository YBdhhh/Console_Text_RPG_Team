using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
	internal class SceneQuest
	{
		public List<Quest> quest = new List<Quest>
		{
			new Quest("몬스터 잡기", "던전에 서식하는 몬스터를 10마리 잡아주세요", 0, 10, EventType.KillOther, 10, ""),
			new Quest("500골드 사용", "상점에서 500골드를 사용하세요", 0, 500, EventType.UseGold, 100, ""),
			new Quest("노가다 목장갑 착용", "노가다하는 당신께 선물을 준비하세요", 0, 1, EventType.EquipItem, 500, "노가다목장갑"),
			new Quest("1층 보스 잡기!", "1층 보스를 만나고 그를 이기세요", 0, 1, EventType.UseGold, 100, "머쉬맘"),
		};
		public Quest? selectedQuest = null;
			

		StringBuilder sb = new StringBuilder();

		public void Start()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("퀘스트");
			Console.ResetColor();
			sb.Append("퀘스트의 정보가 표시됩니다").Append("\n");
			sb.AppendLine("");

			for (int i = 0; i < quest.Count; i++)
			{
				string name = quest[i].name.Length > 6
					? quest[i].name.Substring(0, 6) + "..."
					: quest[i].name;
				/*
				string rewardItem = quest[i].rewardItem.name.Length > 10
					? quest[i].rewardItem.name.Substring(0, 20) + "..."
					: quest[i].rewardItem.name;
				*/
				string toString = quest[i].rewardExp.ToString();
				string rewardExp = toString.Length > 5 ? toString.Substring(0, 5) : toString;
				/*
				sb.Append($"{i + 1}. 제목 : {name,-6 - 2} 보상 : {rewardItem,-10 - 2} {rewardExp,-5 - 2} ").Append("\n");
				*/
			}
			sb.Append("\n");
			sb.Append("흥미 있는 퀘스트를 선택해주세요").Append("\n\n");
			sb.Append("원하시는 행동을 입력해주세요.").Append("\n");
			sb.Append(">> ");
			Console.Write(sb.ToString());
			sb.Clear();
		}

		public void Quest()
		{
			while (true)
			{
				try
				{
					int input = int.Parse(Console.ReadLine());
					if (input > 0 && input <= quest.Count)
					{
						int index = input - 1;
						ReturnQuest(index);
						
						return;
					}
					else
					{
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
		public void ReturnQuest(int index)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine($"{index + 1}");
			sb.AppendLine($"퀘스트 : {quest[index].name}");
			sb.AppendLine($"{quest[index].explain}");
			/*
			sb.AppendLine($"보상 : {quest[index].rewardItem} {quest[index].rewardExp}");
			*/
			sb.AppendLine();
			if (selectedQuest != null)
			{
				StringBuilder sbb = new StringBuilder();
				sbb.AppendLine($"{selectedQuest.name}을 수행중입니다.");
				sbb.AppendLine("");
				sbb.AppendLine("아무키나 눌러서 나가기");
				Console.WriteLine(sbb.ToString());
				sbb.Clear();
				Console.ReadLine();
			}

			sb.AppendLine("1. 퀘스트 수락");
			sb.AppendLine("2. 퀘스트 거절");
			sb.AppendLine();
			sb.Append(">> ");
			Console.Write(sb.ToString());
			sb.Clear();
			while (true)
			{
				try
				{
					int input = int.Parse(Console.ReadLine());
					switch (input)
					{
						case 1:
							Console.WriteLine($"[{quest[index].name}]퀘스트를 수락하셨습니다.");
							selectedQuest = quest[index];
							break;

						case 2:
							Console.WriteLine($"[{quest[index].name}]퀘스트를 거절하셨습니다.");
							break;

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

		/*public void IsQuestClear()
		{
			if (selectedQuest.curProgress == selectedQuest.endProgress)
			{

				//player.Item = selectedQuest.rewardItem;
				//player.exp = selectedQuest.rewardExp;
				sb.Clear();
				sb.AppendLine($"{selectedQuest.Rewarditem}");
				selectedQuest = null;

			}
		}*/
	}
}
