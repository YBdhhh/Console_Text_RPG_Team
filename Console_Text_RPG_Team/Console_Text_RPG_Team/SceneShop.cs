using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class SceneShop
    {
        private StringBuilder sb = new StringBuilder();
        private Player player;
        public void Start()
        {
            sb.AppendLine("여기는 상점입니다.");
            sb.AppendLine("=====아이템 목록=====");
            Console.WriteLine(sb.ToString());
            sb.Clear();
            // Item 클래스의 정적 메서드로 호출해서 상점 아이템 목록 가져오기
            List<Item> items = Item.ShopItems();

            // 아이템 목록 출력 (반복문 안에서는 출력만 수행)
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                // 삼향 연산다 item.purchaseItem true이면 - 구매완료 false면 가격 표시
                string purchaseItem = item.purchaseItem ? " - 구매 완료" : $" | 가격: {item.price}G";
                // DEF, ATK 값이 0보다 클 때만 표시 작을 때 string.Empty(비우기)
                string defTxt = item.def > 0 ? $" | DEF: -{item.def}" : string.Empty;
                string atkTxt = item.atk > 0 ? $" | ATK: {item.atk}" : string.Empty;
                // 최종 출력 형식
                Console.WriteLine($"{i + 1}. {item.name} | {item.toolTip}{atkTxt}{defTxt}{purchaseItem}");
            }
            Input(); // 아이템 구매 
            }

        public void Input()
        {
            sb.AppendLine("\n구매하실 아이템의 번호를 입력해주세요.");
            sb.AppendLine("0. 뒤로 가기");
            sb.Append(">> ");
            Console.Write(sb.ToString());
            sb.Clear();

            string? input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {

            }
        }
    }
}
