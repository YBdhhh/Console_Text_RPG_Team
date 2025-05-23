﻿using System;
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
		public Player playerCopy;
		public int index;

		public List<Quest> quest = new List<Quest>
		{
			
		};
		public Quest? selectedQuest = null;
			
		StringBuilder sb = new StringBuilder();

		public void QuestLoad()
		{
			quest.Add(new Quest("몬스터 잡기", "던전에 서식하는 몬스터를 10마리 잡아주세요", 0, 10, Item.ShopItems(5), EventType.KillOther, 10, ""));
			quest.Add(new Quest("500골드 사용", "상점에서 500골드를 사용하세요", 0, 500, Item.ShopItems(2), EventType.UseGold, 100, ""));
			quest.Add(new Quest("노가다 목장갑 착용", "노가다하는 당신께 선물을 준비하세요", 0, 1, Item.ShopItems(1), EventType.EquipItem, 500, "노가다 목장갑"));
			quest.Add(new Quest("1층 보스 잡기!", "1층 보스를 만나고 그를 이기세요", 0, 1, Item.ShopItems(7), EventType.KillBoss, 100, "머쉬맘"));
		}

		public void RewardQuest()
		{

		}

		public void Start(Player player)
		{
			playerCopy = player;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(" [ 퀘스트 술집 ]");
			Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine($" 뒷골목의 제이엠: 어서 오시오, 용사여");
            Console.WriteLine($" 이곳 퀘스트 술집에선 가장 위태로운 퀘스트 의뢰를 받으실 수 있소 ");
            Console.WriteLine(" 흥미 있는 퀘스트를 골라보시게"); 
            Console.ResetColor();
            Console.WriteLine(" =======================================================");
            for (int i = 0; i < quest.Count; i++)
			{
				string name = quest[i].name.Length > 6
					? quest[i].name.Substring(0, 6) + "..."
					: quest[i].name;
				
				string rewardItem = quest[i].item.name.Length > 10 ? quest[i].item.name.Substring(0, 10) + "...": quest[i].item.name;

				//string rewardItem = quest[i].item.name.Length > 6 ? quest[i].item.name.Substring(0, 6) + "..." : quest[i].item.name;

				string toString = quest[i].rewardExp.ToString();
				string rewardExp = toString.Length > 5 ? toString.Substring(0, 5) : toString;

				sb.Append($" {i + 1}. 제목 : {name,-6 - 2} 보상 : {rewardItem,-10 - 2} {rewardExp,-5 - 2} ").Append("\n");
				Console.Write(sb.ToString());
				sb.Clear();
            }
            Console.WriteLine(" =======================================================");
            Console.WriteLine();
            sb.Append(" 원하시는 퀘스트의 번호를 입력해주세요.").Append("\n");
            Console.Write(sb.ToString());
            sb.Clear();
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine(" 0. 마을로 돌아가기");
            Console.ResetColor();
            sb.Append( " >> ");
			Console.Write(sb.ToString());
			Quest();
			sb.Clear();
		}

		public void Quest()
		{
			while (true)
			{
				try
				{
					int input = int.Parse(Console.ReadLine());
					if (input == 0)
						return;

					if (input > 0 && input <= quest.Count)
					{
						index = input - 1;
						ReturnQuest(index, playerCopy);
						
						return;
					}
					else
					{
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
		public void ReturnQuest(int index, Player player)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine($" {index + 1}");
			sb.AppendLine($" 퀘스트 : {quest[index].name}");
			sb.AppendLine($" {quest[index].explain}");
			
			sb.AppendLine($"보상 : {quest[index].item.name} 경험치 : {quest[index].rewardExp}");
			
			sb.AppendLine();
			if ((player.quest.name != null) && (player.quest.name != quest[index].name))
			{
				StringBuilder sbb = new StringBuilder();
				sbb.AppendLine($" {player.quest.name}을 수행중입니다.");
				sbb.AppendLine("");
				sbb.AppendLine(" 아무키나 눌러서 마을로 돌아가기");
				Console.WriteLine(sbb.ToString());
				sbb.Clear();
				Console.ReadLine();
				return;
			}
            Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine(sb.ToString());
			Console.WriteLine("1. 퀘스트 수락");
            Console.WriteLine("2. 퀘스트 거절");
            Console.WriteLine("3. 퀘스트 완료");
			Console.WriteLine();
            Console.ResetColor();
            Console.Write(" >> ");
	
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
							Console.WriteLine($" [{quest[index].name}]퀘스트를 수락하셨습니다.");
							player.quest = quest[index];
							Thread.Sleep(500);
							return;

						case 2:
							Console.WriteLine($" [{quest[index].name}]퀘스트를 거절하셨습니다.");
							return;

						case 3:
							if (player.quest.name == null)
							{
								Console.WriteLine(" 받은 퀘스트가 존재하지 않습니다.");
								Thread.Sleep(500);
								continue;
							}
							else
							{
								if(player.quest.curProgress >= player.quest.endProgress)
								{
									Console.WriteLine($" [{quest[index].name}]퀘스트를 완료하셨습니다.");
									if (player.quest.item != null)
									{
										player.inventory.AddItem(player.quest.item);
										Console.Write($" [{quest[index].name}]퀘스트 보상 : {player.quest.item.name}");
									}
									else
									{
										Console.Write($" [{quest[index].name}]퀘스트 보상 : {player.quest.rewardExp}");
									}
									int level = player.level; 
									int exp = player.Exp;
									player.Exp += player.quest.rewardExp;
									Console.WriteLine($", {player.quest.rewardExp} 경험치를 얻었습니다.");
									Console.WriteLine($" 현재 레벨 : {level} -> {player.level}");
									Console.WriteLine($" 현재 경험치 : {exp} -> ({player.quest.rewardExp}){player.Exp}");

									quest.RemoveAt(index);
									player.quest = new Quest();
									Thread.Sleep(500);
									return;
								}
								else
								{
									Console.WriteLine(" 퀘스트를 완료하지 못했습니다.");
									Thread.Sleep(500);
									return;
								}
							}

							default:

								Console.WriteLine(" 다시 입력해주십시오");
							continue;
					}
				}
				catch (Exception)
				{
					Console.WriteLine(" 다시 입력해주십시오");
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
