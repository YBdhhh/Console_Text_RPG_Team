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

        public void Start(Inventory inventory)
        {
            this.inventory = inventory;
            this.items = inventory.GetItems();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=====아이템 목록=====");
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("===== 인벤토리 =====");
                Console.ResetColor();

                if (items.Count == 0)
                {
                    sb.AppendLine("인벤토리가 비어 있습니다.");
                    Console.WriteLine(sb.ToString());
                    sb.Clear();
                    sb.AppendLine("0: 뒤로가기");
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
                        sb.AppendLine($"{i + 1}.{(item.equippedItem ? "[E] | " : " ")}{item.name} | {item.toolTip}{atkTxt}{defTxt}");
                        Console.WriteLine(sb.ToString());
                        sb.Clear();

                    }
                    sb.AppendLine("0: 뒤로가기");
                    sb.AppendLine("1: 아이템 장착");
                    sb.AppendLine("2: 아이템 해제");
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
                }
                // 잘못된 입력 처리
                sb.AppendLine("잘못된 입력입니다. 다시 입력해주세요.");
                Console.WriteLine(sb.ToString());
                sb.Clear();
                Thread.Sleep(1000);
            }
        }
        private void DisplayEquipItem()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== 장착할 아이템 선택 =====");
                Console.ResetColor();

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    string equip = item.equippedItem ? "[E] | " : string.Empty;
                    string defTxt = item.def > 0 ? $" | DEF: {item.def}" : string.Empty;
                    string atkTxt = item.atk > 0 ? $" | ATK: {item.atk}" : string.Empty;
                    sb.AppendLine($"{i + 1}. {equip}{item.name} | {item.toolTip}{atkTxt}{defTxt}");
                    Console.WriteLine(sb.ToString());
                    sb.Clear();
                }
                sb.AppendLine();
                sb.AppendLine("0: 취소");
                sb.AppendLine("[장착하실 아이템을 선택해주세요]");
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
                        if (selected.equippedItem)
                        {
                            sb.AppendLine("이미 장착된 아이템입니다.");
                            Console.WriteLine(sb.ToString());
                            sb.Clear();
                        }
                        else
                        {
                            selected.equippedItem = true;
                            sb.AppendLine($"{selected.name}을(를) 장착했습니다.");
                            Console.WriteLine(sb.ToString());
                            sb.Clear();
                        }
                        Thread.Sleep(1000);
                        continue;
                    }
                }

                // 잘못된 입력 처리
                sb.AppendLine("잘못된 입력입니다. 다시 입력해주세요.");
                Console.WriteLine(sb.ToString());
                sb.Clear();
                Thread.Sleep(1000);
            }
        }
        private void DisplayUnEquipItem()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== 장착 해제할 아이템 선택 =====");
                Console.ResetColor();

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    string equip = item.unEquippedItem ?  string.Empty : "[E] | " ;
                    string defTxt = item.def > 0 ? $" | DEF: {item.def}" : string.Empty;
                    string atkTxt = item.atk > 0 ? $" | ATK: {item.atk}" : string.Empty;
                    sb.AppendLine($"{i + 1}. {equip}{item.name} | {item.toolTip}{atkTxt}{defTxt}");
                    Console.WriteLine(sb.ToString());
                    sb.Clear();
                }
                sb.AppendLine();
                sb.AppendLine("0: 취소");
                sb.AppendLine("[장착 해제하실 아이템을 선택해주세요]");
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
                        if (selected.unEquippedItem)
                        {
                            sb.AppendLine("이미 장착 해제된 아이템입니다.");
                            Console.WriteLine(sb.ToString());
                            sb.Clear();
                        }
                        else
                        {
                            selected.unEquippedItem = true;
                            sb.AppendLine($"{selected.name}을(를) 해제했습니다.");
                            Console.WriteLine(sb.ToString());
                            sb.Clear();
                        }
                        Thread.Sleep(1000);
                        continue;
                    }
                }
                // 잘못된 입력 처리
                sb.AppendLine("잘못된 입력입니다. 다시 입력해주세요.");
                Console.WriteLine(sb.ToString());
                sb.Clear();
                Thread.Sleep(1000);
            }
        }
    }
}

