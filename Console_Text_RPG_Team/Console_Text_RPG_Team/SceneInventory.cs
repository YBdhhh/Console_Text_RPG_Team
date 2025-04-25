using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
	internal class SceneInventory
	{
		private Inventory inventory;
		StringBuilder sb = new StringBuilder();
		private List<Item> items;
		private Player player;

		public void Start(Inventory inventory, Player player)
		{
			this.inventory = inventory;
			this.player = player;
			this.items = inventory.GetItems();

			Console.ForegroundColor = ConsoleColor.Green;
			//Console.WriteLine(" =====아이템 목록=====");
			Console.WriteLine();
			Console.WriteLine(" [보유 아이템 목록]\n");
			Console.ResetColor();
			ShowItems();
			Console.WriteLine(sb.ToString());
			sb.Clear();
		}
		public void ShowItems()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(" ====================== 인벤토리 ======================");
				Console.ResetColor();

				if (items.Count == 0)
				{
					sb.AppendLine(" 인벤토리가 비어 있습니다.");
					Console.WriteLine(sb.ToString());
					sb.Clear();
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.DarkCyan;
					Console.WriteLine(" 0: 나가기");
					Console.ResetColor();
					sb.Append(" >> ");
					Console.Write(sb.ToString());
					sb.Clear();
				}
				else
				{
					// 아이템 목록 출력
					for (int i = 0; i < items.Count; i++)
					{
						var item = items[i];
						string defTxt = item.def > 0 ? $" | DEF: {item.def}" : string.Empty;
						string atkTxt = item.atk > 0 ? $" | ATK: {item.atk}" : string.Empty;
						sb.AppendLine($" {i + 1}.{(item.equippedItem ? "[E] | " : " ")}{item.name} | {item.toolTip}{atkTxt}{defTxt}");
						Console.WriteLine(sb.ToString());
						sb.Clear();

					}

					Console.ForegroundColor = ConsoleColor.DarkCyan;
					Console.WriteLine(" 0: 뒤로가기");
					Console.WriteLine(" 1: 아이템 장착");
					Console.WriteLine(" 2: 아이템 해제");
					Console.WriteLine(" 3: 포션 사용");
					Console.ResetColor();
					sb.Append(" >> ");
					Console.Write(sb.ToString());
					sb.Clear();
				}

				string input = Console.ReadLine();

				if (int.TryParse(input, out int choice))
				{
					if (choice == 0)
					{
						// 인벤토리 종료
						break;
					}
					if (choice == 1)
					{
						// 장착 메뉴 호출
						DisplayEquipItem();
						continue;
					}
					if (choice == 2)
					{
						// 장착 메뉴 호출
						DisplayUnEquipItem();
						continue;
					}
					if (choice == 3)
					{
						inventory.UsePotion(player);
						Thread.Sleep(500);
						continue;
					}
				}
				// 잘못된 입력 처리
				sb.AppendLine(" 잘못된 입력입니다. 다시 입력해주세요.");
				Console.WriteLine(sb.ToString());
				sb.Clear();
				Thread.Sleep(500);
			}
		}
		private void DisplayEquipItem()
		{
			while (true)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine();
				Console.WriteLine(" ====================== 장착할 아이템 선택 ======================");
				Console.ResetColor();

				for (int i = 0; i < items.Count; i++)
				{
					var item = items[i];
					string equip = item.equippedItem ? "[E] | " : string.Empty;
					string statTxt = item.atk > 0 ? $"| 공격력:{item.atk}" : item.def > 0 ? $"| 방어력:{item.def}" : "";
					sb.AppendLine($" {i + 1,-1}.{equip} {item.name,-6}| {item.toolTip,-15}{statTxt,-15}");
					Console.WriteLine(sb.ToString());
					sb.Clear();
				}
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.DarkCyan;
				Console.WriteLine(" 0: 취소");
				Console.WriteLine(" 장착하실 아이템의 번호를 선택해주세요");
				Console.ResetColor();
				sb.Append(" >> ");
				Console.Write(sb.ToString());
				sb.Clear();
				string input = Console.ReadLine();


				if (int.TryParse(input, out int choice))
				{
					if (choice == 0)
					{
						// 장착 메뉴 종료
						break;
					}
					if (choice >= 1 && choice <= items.Count)
					{
						var selected = items[choice - 1];

						if (selected.Type == ItemType.Potion)
						{
							sb.AppendLine($"[!]{selected.name}은(는) 포션이므로 장착할 수 없습니다.");
							Console.WriteLine(sb.ToString());
							sb.Clear();
							Thread.Sleep(1000);
							continue;
						}
						if (selected
							.equippedItem)
						{
							sb.AppendLine(" 이미 장착된 아이템입니다.");
							Console.WriteLine(sb.ToString());
							sb.Clear();
						}
						else
						{
							selected.equippedItem = true;
							player.quest.PlayEvent(player.quest, selected.name);
							player.itematk += selected.atk;
							player.itemdef += selected.def;
							player.atk += selected.atk;
							player.def += selected.def;
							sb.AppendLine($" {selected.name}을(를) 장착했습니다.");
							Console.WriteLine(sb.ToString());
							sb.Clear();
						}
						Thread.Sleep(500);
						continue;
					}
				}

				// 잘못된 입력 처리
				sb.AppendLine(" 잘못된 입력입니다. 다시 입력해주세요.");
				Console.WriteLine(sb.ToString());
				sb.Clear();
				Thread.Sleep(500);
			}
		}
		private void DisplayUnEquipItem()
		{
			while (true)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine();
				Console.WriteLine(" ====================== 해제할 아이템 선택 ======================");
				Console.ResetColor();

				for (int i = 0; i < items.Count; i++)
				{
					var item = items[i];
					// 해제 대상만 보여주기 위해, 장착된 아이템만 리스트업
					if (!item.equippedItem) continue;

					string defTxt = item.def > 0 ? $" | DEF: {item.def}" : string.Empty;
					string atkTxt = item.atk > 0 ? $" | ATK: {item.atk}" : string.Empty;
					Console.WriteLine($" {i + 1}. [E] {item.name} | {item.toolTip}{atkTxt}{defTxt}");
				}
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.DarkCyan;
				Console.WriteLine(" 0: 취소");
				Console.WriteLine(" 장착하실 아이템의 번호를 선택해주세요");
				Console.ResetColor();
				Console.Write(" >> ");

				string input = Console.ReadLine();
				if (int.TryParse(input, out int choice))
				{
					if (choice == 0) break;
					if (choice >= 1 && choice <= items.Count)
					{
						var selected = items[choice - 1];
						if (selected.equippedItem)
						{
							selected.equippedItem = false;  // 장착 해제
							Console.WriteLine($"{selected.name}을(를) 해제했습니다.");
							player.itematk -= selected.atk;
							player.itemdef -= selected.def;
							player.atk -= selected.atk;
							player.def -= selected.def;
						}
						else
						{
							Console.WriteLine(" 장착된 아이템이 아닙니다.");
						}
						Thread.Sleep(500);
						continue;
					}
				}
				Console.WriteLine(" 잘못된 입력입니다.");
				Thread.Sleep(500);
			}
		}
	}
}

