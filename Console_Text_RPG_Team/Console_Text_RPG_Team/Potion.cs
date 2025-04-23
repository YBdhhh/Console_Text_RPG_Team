using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class Potion : Item
    {
        public string Type { get; set; }
        public float HealAmount { get; set; } // 회복량

        // 힐량 항목 추가
        public Potion(string name, string toolTip, int price, string type, float healAmount)
            : base(name, toolTip, 0, 0, price)
        {
            Type = type;
            HealAmount = healAmount;
        }
        
        // 플레이어에게 회복 효과 적용
        public void UsePotion(Player player)
        {
            Console.WriteLine($"{player.name}은(는) {Type}을(를) 사용하여 {HealAmount}만큼 체력을 회복했습니다!");
            player.Heal(HealAmount);
        }
    }
}
