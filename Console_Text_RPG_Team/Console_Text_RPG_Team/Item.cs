﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal enum ItemType
    {
        Weapon,
        Armor,
        Potion
    }
    internal class Item
    {
        public string name;//아이템 이름
        public string toolTip; // 아이템 설명
        public int atk; // 무기 공격력
        public int def;// 갑옷 방어력
        public int price; //아이템 가격
        public bool purchaseItem; // 상점 - 아이템 구매 가능 여부
        public bool equippedItem; // 인벤토리 - 아이템 창착 가능 여부
        public bool unEquippedItem; // 인벤토리 - 아이템 해제 가능 여부
        public ItemType Type { get; set; } // 아이템 종류
        public int HealAmount { get; set; } // 포션 회복량 (포션일 경우)

        // 무기 또는 방어구 생성자
        public Item(string name, string toolTip, int atk, int def, int price, ItemType type = ItemType.Weapon)
        {
            this.name = name;
            this.toolTip = toolTip;
            this.atk = atk;
            this.def = def;
            this.price = price;
            this.purchaseItem = false;
            this.equippedItem = false;
            this.unEquippedItem = false;
            this.Type = type;
            this.HealAmount = 0; // 기본값
        }
        public Item()
        {
        }

        // 포션 생성자
        public Item(string name, string toolTip, int price, int healAmount)

        {
            this.name = name;
            this.toolTip = toolTip;
            this.atk = 0;
            this.def = 0;
            this.price = price;
            this.purchaseItem = false;
            this.equippedItem = false;
            this.unEquippedItem = false;
            this.Type = ItemType.Potion;
            this.HealAmount = healAmount;
        }

        public List<Item> Items { get; set; }
        public static List<Item> ShopItems()
        {
            List<Item> shopItemList = new List<Item>
        {
            new Item("물컹물컹한 신발     ", "일부 애호가에게 잘 팔리는 신발이다.         " ,0  ,6,   1000, ItemType.Armor),
            new Item("노가다 목장갑       ", "노가다를 하는 사람에겐 필수이다.            " ,0  ,12,  1500, ItemType.Armor),
            new Item("홍색 꽃무늬 튜브    ", "이걸 사용한다면 당신도 풀장 인싸남!!        " ,50  ,0,  1500, ItemType.Weapon),
            new Item("머리 위에 떡 두개   ", "나랑 떡먹을래? 이 게임 필수 헌팅아이템      " ,0  ,18,  2000, ItemType.Armor),
            new Item("다크로드의 두건     ", "도적이 되고 싶은 자… 나에게로…            " ,0  ,25,  2500, ItemType.Armor),
            new Item("돼지치기 막대       ", "주의사항 : 뚱뚱한 사람을 때리면 안된다.     " ,150 ,0,  2000, ItemType.Weapon),
            new Item("메이플스노우보드    ", "스~노우보드                                 " ,200 ,0,  2500, ItemType.Weapon),
            new Item("카이세리움          ", "선대 카이저가 사용했던 명검이자 수호검      " ,500 ,0,  5500, ItemType.Weapon),
            new Item("화염의 카타나       ", "귀살대의 검으로 오니의 영혼이 깃들어 있다.  " ,1000,0, 10500, ItemType.Weapon),
            new Item("빨간 포션          ", "체력을 회복해주는 빨간 포션                    " , 400, 400),
            new Item("주황 포션          ", "체력을 회복해주는 주황 포션                    " , 500, 500),
            new Item("하얀 포션          ", "체력을 회복해주는 하얀 포션                    " , 600, 600)
        };
            return shopItemList;
        }

		public static Item ShopItems(int value)
		{
			List<Item> shopItemList = new List<Item>
		{
			new Item("물컹물컹한 신발     ", "일부 애호가에게 잘 팔리는 신발이다.         " ,0  ,6,   1000, ItemType.Armor),
			new Item("노가다 목장갑       ", "노가다를 하는 사람에겐 필수이다.            " ,0  ,12,  1500, ItemType.Armor),
			new Item("홍색 꽃무늬 튜브    ", "이걸 사용한다면 당신도 풀장 인싸남!!        " ,50  ,0,  1500, ItemType.Weapon),
			new Item("머리 위에 떡 두개   ", "나랑 떡먹을래? 이 게임 필수 헌팅아이템      " ,0  ,18,  2000, ItemType.Armor),
			new Item("다크로드의 두건     ", "도적이 되고 싶은 자… 나에게로…            " ,0  ,25,  2500, ItemType.Armor),
			new Item("돼지치기 막대       ", "주의사항 : 뚱뚱한 사람을 때리면 안된다.     " ,150 ,0,  2000, ItemType.Weapon),
			new Item("메이플스노우보드    ", "스~노우보드                                 " ,200 ,0,  2500, ItemType.Weapon),
			new Item("카이세리움          ", "선대 카이저가 사용했던 명검이자 수호검      " ,500 ,0,  5500, ItemType.Weapon),
			new Item("화염의 카타나       ", "귀살대의 검으로 오니의 영혼이 깃들어 있다.  " ,1000,0, 10500, ItemType.Weapon),
			new Item("빨간 포션          ", "체력을 회복해주는 빨간 포션                    " , 400, 400),
			new Item("주황 포션          ", "체력을 회복해주는 주황 포션                    " , 500, 500),
			new Item("하얀 포션          ", "체력을 회복해주는 하얀 포션                    " , 600, 600)
		};
            return shopItemList[value];
		}
	}
}