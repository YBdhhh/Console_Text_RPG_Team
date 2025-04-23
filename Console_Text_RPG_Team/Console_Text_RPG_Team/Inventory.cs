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
            sb.AppendLine($"{item.name}이(가) 인벤토리에 추가되었습니다.");
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
            foreach (var item in items)
            {
                if (item is Potion potion)
                {
                    potion.UsePotion(player);
                    items.Remove(potion);
                    break;
                }
            }
        }
    }
    
}
