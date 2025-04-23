using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    /// <summary>
    /// 게임 내 상점 화면을 담당하는 클래스
    /// </summary>
    internal class SceneShop
    {
        // 문자열 출력을 위한 StringBuilder
        private StringBuilder sb = new StringBuilder();
        // 구매 정보와 골드를 가진 플레이어 참조
        private Player player;
        // 상점에 진열된 아이템 목록
        private List<Item> shopItems;

        /// <summary>
        /// 생성자: 상점 객체 생성 시 한 번만 아이템 목록 로드
        /// </summary>
        public SceneShop()
        {
            shopItems = Item.ShopItems(); // 스태틱 메서드로부터 기본 아이템 리스트 가져오기
        }

        public void Start(Player player)
        {
            this.player = player; // 필드에 플레이어 할당

            // 화면 구성 텍스트 작성
            Thread.Sleep(500); // 0.5초 대기
            Console.Clear(); // 화면 초기화
            sb.AppendLine("여기는 상점입니다.");
            sb.AppendLine("=====아이템 목록=====");
            sb.AppendLine($"[보유골드]: {this.player.gold}G");
            Console.WriteLine(sb.ToString());
            sb.Clear(); // StringBuilder 초기화

            // 아이템 목록 순회 출력
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];

                // 구매 여부에 따라 "- 구매 완료" 또는 가격 정보 표시
                string purchaseItem = item.purchaseItem ? " | - 구매 완료" : $" | 가격: {item.price}G";
                // 방어력(def)이나 공격력(atk)이 0보다 클 때만 텍스트 추가
                string defTxt = item.def > 0 ? $" | DEF: {item.def}" : string.Empty;
                string atkTxt = item.atk > 0 ? $" | ATK: {item.atk}" : string.Empty;

                // 완성된 한 줄 출력 문자열 생성
                sb.AppendLine($"{i + 1}. {item.name} | {item.toolTip}{atkTxt}{defTxt}{purchaseItem}");
                Console.WriteLine(sb.ToString());
                sb.Clear(); // 다음 출력 준비
            }

            Input(); // 구매 입력 처리 메서드 호출
        }

        /// <summary>
        /// 사용자 입력을 받아 구매 처리하거나 상점을 종료하는 메서드
        /// </summary>
        private void Input()
        {
            while (true)
            {
                // 입력 안내 메시지
                sb.AppendLine($"구매하시겠습니까? [0: 뒤로가기, 1~{shopItems.Count}: 아이템 번호 입력]");
                sb.Append(" >> ");
                Console.Write(sb.ToString());
                sb.Clear(); // StringBuilder 초기화
                string userInput = Console.ReadLine();
                // 숫자 입력 여부 확인
                if (int.TryParse(userInput, out int choice))
                {
                    if (choice == 0)
                    {
                        break;
                    }
                    else if (choice >= 1 && choice <= shopItems.Count)
                    {
                        var selected = shopItems[choice - 1];

                        if (selected.purchaseItem)
                        {
                            sb.Append("이미 구매 완료된 아이템입니다.");
                            Console.Write(sb.ToString());
                            sb.Clear();
                        }
                        else if (this.player.gold < selected.price)
                        {
                            sb.Append("골드가 부족합니다.");
                            Console.Write(sb.ToString());
                            sb.Clear();
                        }
                        else
                        {
                            // 골드 차감
                            this.player.gold -= selected.price;
                            // 구매 상태 true로 변경
                            selected.purchaseItem = true;
                            sb.Append($"{selected.name}을(를) 구매했습니다.");
                            Console.Write(sb.ToString());
                            sb.Clear();
                        }
                        //상점 재시작
                        Start(player);
                        break;
                    }
                }
                sb.Append("잘못된 입력입니다. 다시 입력해주세요.");
                Console.WriteLine(sb.ToString());
                sb.Clear();
            }
        }
    }
}
