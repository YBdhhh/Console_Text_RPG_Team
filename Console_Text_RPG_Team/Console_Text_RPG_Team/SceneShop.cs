using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Console_Text_RPG_Team
{

    internal class SceneShop
    {
        private StringBuilder sb = new StringBuilder();
        private Player player;
        private Inventory inventory;
        private List<Item> shopItems;

        /// 생성자: 상점 객체 생성 시 한 번만 아이템 목록 로드
        public SceneShop()
        {
            shopItems = Item.ShopItems();
        }

        /// 상점 메인 메뉴를 표시하고 입력을 처리
        public void Start(Player player, Inventory inventory)
        {
            this.player = player;
            this.inventory = inventory;
            Console.Clear();

            while (true)
            {
                Console.Clear();
                DisPlayeShop();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    if (choice == 0)
                    {
                        // 상점 종료
                        return;
                    }
                    if (choice == 1)
                    {
                        // 구매 메뉴로 이동
                        DisplayPurchaseItem();
                        continue;
                    }
                }
                    // 잘못된 입력 처리
                    sb.AppendLine(" 잘못된 입력입니다. 다시 입력해주세요.");
                    sb.Append(" >> ");
                    Console.Write(sb.ToString());
                    sb.Clear();
                   Thread.Sleep(1000); // 1초 대기

            }
        }


        private void DisPlayeShop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine(" 돼지와 함께 춤을: 반갑네 여기서는 좋은 아이템만 취급하니까 한 번 골라보라고");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ============================================  아이템 목록 ============================================");
            Console.ResetColor();
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                //string flag = item.purchaseItem ? " | - 구매 완료" : $" | 가격: {item.price}G";             
                string statTxt = item.atk > 0 ? $"| 공격력:{item.atk}" : item.def > 0 ? $"| 방어력:{item.def}" : "";
                if (item.purchaseItem)
                {
                    sb.Append($" das{i + 1,-1}. {item.name,-6}| {item.toolTip,-15}{statTxt,-16} | ");
                    Console.Write(sb.ToString());
                    sb.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("구매 완료");
                    Console.ResetColor();

                }
                else
                {
                    sb.Append($" {i + 1,-1}. {item.name,-6}| {item.toolTip,-15}{statTxt,-15}  |");
                    Console.Write(sb.ToString());
                    sb.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" 가격: {item.price}G");
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($" [보유골드]: {player.gold}G\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" 0: 마을로 돌아가기");
            Console.WriteLine(" 1: 아이템 구매");

            Console.ResetColor();
            sb.Append(">> ");
            Console.Write(sb.ToString());
            sb.Clear();
        }

        /// 아이템 구매 메뉴를 표시하고 선택한 아이템을 구매
        private void DisplayPurchaseItem()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine(" ======= 아이템 구매 가능 목록 =======");
                Console.ResetColor();
                for (int i = 0; i < shopItems.Count; i++)
                {
                    var item = shopItems[i];
                    //string flag = item.purchaseItem ? " | - 구매 완료" : $" | 가격: {item.price}G";             
                    string statTxt = item.atk > 0 ? $"| 공격력:{item.atk}" : item.def > 0 ? $"| 방어력:{item.def}" : "";
                    if (item.purchaseItem)
                    {
                        sb.Append($"{i + 1,-1}. {item.name,-6}| {item.toolTip,-15}{statTxt,-16} | ");
                        Console.Write(sb.ToString());
                        sb.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("구매 완료");
                        Console.ResetColor();

                    }
                    else
                    {
                        sb.Append($"{i + 1,-1}. {item.name,-6}| {item.toolTip,-15}{statTxt,-15}  |");
                        Console.Write(sb.ToString());
                        sb.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"가격: {item.price}G");
                        Console.ResetColor();
                    }
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[보유골드]: {player.Gold}G\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("0: 뒤로가기");
                Console.ResetColor();
                sb.AppendLine("[구매하실 아이템 번호를 입력해주세요]");
                sb.Append(">> ");
                Console.Write(sb.ToString());
                sb.Clear();
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice == 0)
                    {
                        // 메인 메뉴로 복귀
                        Console.Clear();
                        return;
                    }
                    if (choice >= 1 && choice <= shopItems.Count)
                    {
                        var selected = shopItems[choice - 1];
                        if (selected.purchaseItem)
                        {
                            Console.WriteLine("이미 구매 완료된 아이템입니다.");
                        }
                        else if (player.Gold < selected.price)
                        {
                            Console.WriteLine("골드가 부족합니다.");
                        }
                        else
                        {
                            // 구매 처리
                            player.Gold -= selected.price;
                            selected.purchaseItem = true;
                            Console.WriteLine($"{selected.name}을(를) 구매했습니다.");
                            inventory.AddItem(selected);
                            
                        }
                        Thread.Sleep(1000);
                        continue;
                    }

                }

                // 잘못된 입력 처리 및 대기
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                Thread.Sleep(1000);
            }
        }
    }
}
