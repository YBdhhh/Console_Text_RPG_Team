using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Console_Text_RPG_Team
{

    internal class Inventory
    {
        // 보유한 아이템 리스트
        private List<Item> items = new List<Item>();
        private StringBuilder sb = new StringBuilder();

        // 외부에서 아이템 리스트를 추가
        public void AddItem(Item item)
        {
            items.Add(item);
            sb.AppendLine($" {item.name}이(가) 인벤토리에 추가되었습니다.");
            Console.WriteLine(sb.ToString());
            sb.Clear();
        }
        // 외부에서 아이템 리스트를 가져올 수 있도록
        public List<Item> GetItems()
        {
            return items;
        }

        public void UsePotion(Player player)
        {
            List<Item> availablePotions = items.Where(item => item.Type == ItemType.Potion).ToList();

            if (availablePotions.Count == 0)
            {
                Console.WriteLine(" 사용할 수 있는 포션이 없습니다.");
                return;
            }

            Console.WriteLine(" 사용할 포션을 선택하세요:");
            for (int i = 0; i < availablePotions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availablePotions[i].name} - {availablePotions[i].toolTip} (회복량: {availablePotions[i].HealAmount})");
            }
			Console.WriteLine("0. 포션 사용 안함");
			Console.Write(" >> ");
            string input = Console.ReadLine();
            if (input == "0")
            {
                Console.WriteLine("포션을 사용하지 않습니다.");
                Thread.Sleep(500);
				return;
			}
            


            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= availablePotions.Count)
            {
                Item selectedPotion = availablePotions[choice - 1];
                Console.WriteLine($" {player.name}은(는) {selectedPotion.name}을(를) 사용하여 {selectedPotion.HealAmount}만큼 체력을 회복했습니다!");
                player.Heal(selectedPotion.HealAmount);
                items.Remove(selectedPotion);
            }
            else
            {
                Console.WriteLine(" 잘못된 입력입니다.");
            }
        }
    }
}
