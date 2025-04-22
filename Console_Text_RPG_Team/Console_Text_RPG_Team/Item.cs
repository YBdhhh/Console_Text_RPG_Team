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

        public Item(string name, string toolTip, float atk, float def, int price)
        {
            this.name = name;
            this.toolTip = toolTip;
            this.atk = atk;
            this.def = def;
            this.price = price;
            this.purchaseItem = false;
            this.equippedItem = false;
        }
        public List<Item> Items { get; set; }// // Items 프로퍼티: 현재 객체가 보유한 아이템 리스트를 저장하고 관리
        public static List<Item> ShopItems()//상점에 진열할 아이템 목록
        {
            return new List<Item> // return으로 호출한 곳으로 이 리스트 객체를 돌려보냅니다.
            {
                new Item("아이템 이름", "아이템 설명 ", 0, 5, 1000),// atk, def, price          
            };
        }
    }
}