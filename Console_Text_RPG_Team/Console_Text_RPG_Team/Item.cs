using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class Item
    {
        public string name;//아이템 이름
        public string toolTip; // 아이템 설명
        public float atk; // 무기 공격력
        public float def;// 갑옷 방어력
        public int price; //아이템 가격 
        public bool purchaseItem; // 상점 - 아이템 구매 가능 여부
        public bool equippedItem; // 인벤토리 - 아이템 창착 가능 여부
        public bool unEquippedItem; // 인벤토리 - 아이템 해제 가능 여부

        public Item(string name, string toolTip, float atk, float def, int price)
        {
            this.name = name;
            this.toolTip = toolTip;
            this.atk = atk;
            this.def = def;
            this.price = price;
            this.purchaseItem = false;
            this.equippedItem = false;
            this.unEquippedItem = false;
        }
        public List<Item> Items { get; set; }// // Items 프로퍼티: 현재 객체가 보유한 아이템 리스트를 저장하고 관리
        public static List<Item> ShopItems()//상점에 진열할 아이템 목록
        {
            return new List<Item> // return으로 호출한 곳으로 이 리스트 객체를 돌려보냅니다.
            {
                new Item("물컹물컹한 신발    ",  "일부 애호가에게 잘 팔리는 신발이다.        "  ,0   ,5,   500),
                new Item("노가다 목장갑      ",  "노가다를 하는 사람에겐 필수이다.           "  ,0   ,10, 1000),
                new Item("홍색 꽃무늬 튜브   ",  "이걸 사용한다면 당신도 풀장 인싸남!!       "  ,5   ,0,  1000),
                new Item("머리 위에 떡 두개  ",  "나랑 떡먹을래? 이 게임 필수 헌팅아이템     "  ,0   ,15, 1500),
                new Item("다크로드의 두건    ",  "도적이 되고 싶은 자… 나에게로…           "  ,0   ,20, 2000),
                new Item("돼지치기 막대      ",  "주의사항 : 뚱뚱한 사람을 때리면 안된다.    "  ,15  ,0,  1500),
                new Item("메이플스노우보드   ",  "스~노우보드                                "  ,20  ,0,  2000),
                new Item("카이세리움         ",  "선대 카이저가 사용했던 명검이자 수호검     "  ,50  ,0,  5000),
                new Item("화염의 카타나      ",  "귀살대의 검으로 오니의 영혼이 깃들어 있다. "  ,100 ,0, 10000),
            };
        }
    }
}