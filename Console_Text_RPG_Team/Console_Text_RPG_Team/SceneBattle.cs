using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class SceneBattle
    {
        private Player player;

        SceneBattleAttack sceneBattleAttack;  //임시

        public List<Monster> monsters = new List<Monster>
        {
            new Monster("슬라임", 200, 30, 5, 4, 4, new List<Item> {new Item("빨간 포션", "체력을 회복해주는 빨간 포션", 400, 400) }),
            new Monster("초록버섯", 300, 30, 10, 6, 6, new List<Item> {new Item("빨간 포션", "체력을 회복해주는 빨간 포션", 400, 400) }),
            new Monster("리본돼지", 300, 60, 15, 8, 8, new List<Item> {new Item("빨간 포션", "체력을 회복해주는 빨간 포션", 400, 400) }),
            new Monster("스톤골렘", 400, 60, 20, 10, 10, new List<Item> {new Item("빨간 포션", "체력을 회복해주는 빨간 포션", 400, 400) }),
            new Monster("웨어울프", 600, 90, 25, 14, 14, new List<Item> {new Item("빨간 포션", "체력을 회복해주는 빨간 포션", 400, 400) }),
            new Monster("주황버섯", 600, 120, 40, 18, 18, new List<Item> {new Item("빨간 포션", "체력을 회복해주는 빨간 포션", 400, 400) }),
        //1층몹
            new Monster("좀비버섯", 700, 120, 55, 22, 22, new List<Item> {new Item("주황 포션", "체력을 회복해주는 주황 포션", 500, 500) }),
            new Monster("고스트픽시", 600, 180, 40, 20, 20, new List<Item> {new Item("주황 포션", "체력을 회복해주는 주황 포션", 500, 500) }),
            new Monster("브라운테니", 800, 90, 20, 15, 15, new List<Item> {new Item("주황 포션", "체력을 회복해주는 주황 포션", 500, 500) }),
            new Monster("로보토이", 800, 240, 40, 24, 24, new List < Item > { new Item("주황 포션", "체력을 회복해주는 주황 포션", 500, 500) }),
            new Monster("예티", 1000, 240, 15, 21, 21, new List < Item > { new Item("주황 포션", "체력을 회복해주는 주황 포션", 500, 500) }),
            new Monster("화이트팽", 500, 300, 15, 18, 18, new List < Item > { new Item("주황 포션", "체력을 회복해주는 주황 포션", 500, 500) }),
        //2층몹
            new Monster("블러드하프", 1300, 390, 20, 30, 30, new List < Item > { new Item("하얀 포션", "체력을 회복해주는 하얀 포션", 600, 600) }),
            new Monster("붉은 켄타우로스", 800, 450, 45, 32, 32, new List < Item > { new Item("하얀 포션", "체력을 회복해주는 하얀 포션", 600, 600) }),
            new Monster("추억의 사제", 500, 450, 25, 30, 30, new List < Item > { new Item("하얀 포션", "체력을 회복해주는 하얀 포션", 600, 600) }),
            new Monster("훈련용 나무인형", 1600, 450, 50, 41, 41, new List < Item > { new Item("하얀 포션", "체력을 회복해주는 하얀 포션", 600, 600) }),
            new Monster("부기", 1000, 300, 25, 25, 25, new List < Item > { new Item("하얀 포션", "체력을 회복해주는 하얀 포션", 600, 600) }),
            new Monster("오파츠", 700, 600, 50, 35, 35, new List < Item > { new Item("하얀 포션", "체력을 회복해주는 하얀 포션", 600, 600) })
        //3층몹
        };
        public List<Monster> bossMonsters = new List<Monster>(3)
        {
            new Monster("머쉬맘", 2300, 210, 50, 40, 40, new List < Item > { new Item("빨간 포션", "체력을 회복해주는 빨간 포션", 400, 400) }),
            new Monster("바이킹", 3300, 510, 75, 65, 65, new List < Item > { new Item("주황 포션", "체력을 회복해주는 주황 포션", 500, 500) }),
            new Monster("자쿰", 5500, 750, 100, 110, 110, new List < Item > { new Item("하얀 포션", "체력을 회복해주는 하얀 포션", 600, 600) }),
        };

        public List<Monster> spawnList = new List<Monster>(6);      
        public List<Monster> spawnBoss = new List<Monster>();      //혹시나 보스 여러개 나올까 싶어서

        public List<int> dungeonFloor = new List<int>(3) { 1 };           //최대층 3층, 초기 1층

        public int currentFloor = 1;       //현재 층수
        public int clearCount = 1;         
        public int maxClearCount = 5;       //보스포함 5번 전투

        public void SelectDungeon(Player player)
        {
            this.player = player;
            int choose = 1;
            while (choose != 0)
            {
                Console.Clear();
                StringBuilder sb = new StringBuilder();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine(" [ 슬리피우드 던전 ]");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" 경비병 루크: ...Zzz...: \n");
                Console.ResetColor();
                for (int i = 1; i <= dungeonFloor.Count; i++)
                {
                    sb.AppendLine($" {i} {dungeonFloor[i - 1]}층 ");
                    Console.Write(sb.ToString());
                    sb.Clear();
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\n 0. 돌아가기");
                Console.ResetColor();
                sb.AppendLine(" 원하시는 던전의 번호를 입력해주세요.");
                sb.Append(" >> ");
                Console.Write(sb.ToString());
                sb.Clear();
                choose = Checkinput(1, dungeonFloor.Count);
                switch (choose)
                {
                    case 0:
                        break;
                    case 1:
                        currentFloor = 1;
                        StartBattle(player);
                        continue;
                    case 2:
                        currentFloor = 2;
                        StartBattle(player);
                        continue;
                    case 3:
                        currentFloor = 3;
                        StartBattle(player);
                        continue;
                }
            }
        }

        public int Checkinput(int min, int max)
        {
            int choose;
            while (true)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out choose);
                if (isNumber)
                {
                    if ((choose >= min && choose <= max) || choose ==0)
                    {
                        return choose;
                    }
                }
                Console.WriteLine(" 잘못된 입력입니다. 다시 입력해주세요: ");
                Console.Write(" >> ");
            }
        }
        public void StartBattle(Player player)
        {
            clearCount = 1;
            while (clearCount < maxClearCount && clearCount != 0 )      //보스포함 5번 전투 
            {
                StringBuilder sb = new StringBuilder();

                sceneBattleAttack = new SceneBattleAttack(this, player);


                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine();
                Console.WriteLine($" {player.name}: 앗! 몬스터가 나타났다!\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" [ {currentFloor}층 - {clearCount}스테이지 ]");
                Console.WriteLine();
                Console.WriteLine(" [ 몬스터 ]");
                Console.ResetColor();
                Console.WriteLine(" ====================");
                SpawnMonster();
                Console.WriteLine(" ====================");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [내정보]");
                Console.ResetColor();
                Console.WriteLine(" ====================");
                player.ViewStatus();
                //sb.AppendLine($" Lv.	: {player.level}");
                //sb.AppendLine($" 직업	: {player.job}");
                //sb.AppendLine($" 체  력	: {player.PreviousHP}/{player.hp}");
                Console.WriteLine(" ====================");
                sb.AppendLine();
                Console.Write(sb.ToString());
                sb.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(" 1. 전투시작");
                Console.WriteLine(" 0. 도망치기 (스테이지 초기화)");
                Console.ResetColor();
                sb.AppendLine(" 원하시는 행동을 입력해주세요.");
                sb.Append(" >> ");

                Console.Write(sb.ToString());
                sb.Clear();
                int choose = Checkinput(1, 1);
                switch (choose)
                {
                    case 0:
                        clearCount = 0;
                        spawnList.Clear();
                        break;
                    case 1:
                        sceneBattleAttack.BattleLoop(player, spawnList);
                        spawnList.Clear();
                        break;
                }
            }
            if(clearCount == maxClearCount)
            {
                StartBattle(player, spawnBoss);
            }

        }

        public void StartBattle(Player player, List<Monster> bossMonster)
        {
            StringBuilder sb = new StringBuilder();

            sceneBattleAttack = new SceneBattleAttack(this, player);


            player.audio[0].Stop(); //일반전투 음악 정지
			      player.audio[6].Play(); //보스전 음악
            Thread.Sleep(3000);
			      Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine($" {player.name}: 앗! 어디서 거대한 그림자가...");
            Console.ResetColor();

            Thread.Sleep(1200);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" [ 전투 시작! {currentFloor}층 - 보스 ]");
            Console.ResetColor();
            Console.WriteLine($" 야생의 머쉬맘이 나타났다!!\n");
            bossMonster.Add(new Monster (bossMonsters[currentFloor-1]));
            player.audio[0].Play();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [ 몬스터 ]");
            Console.ResetColor();
            Console.WriteLine(" ====================");
            Console.ForegroundColor = ConsoleColor.Magenta;//보스몹 소환     
            Console.WriteLine($" Lv.{bossMonsters[currentFloor-1].level} {bossMonsters[currentFloor-1].name}  HP  {bossMonsters[currentFloor - 1].hp}\n");
            Console.ResetColor();
            Console.WriteLine(" ====================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(" [ 내정보 ]");
            Console.ResetColor();
            Console.WriteLine(" ====================");

            player.ViewStatus();
            sb.AppendLine(" ====================");
            Console.Write(sb.ToString());
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.WriteLine(" 1. 전투시작");
            Console.WriteLine(" 0. 돌아가기\n");
            Console.ResetColor();
            Console.WriteLine(" 원하시는 행동의 번호를 입력해주세요.");
            

            sb.Append(" >> ");
            Console.Write(sb.ToString());
            sb.Clear();
            int choose = Checkinput(1, 1);
            switch (choose)
            {
                case 0:
                    Console.WriteLine(" 이런! 주황버섯 무리가 통로를 막고있다!");
                    Thread.Sleep(1200);
                    sceneBattleAttack.BattleLoop(player, bossMonster);
                    break;
                case 1:
                    sceneBattleAttack.BattleLoop(player, bossMonster);
                    spawnBoss.Clear();
                    break;
            }

        }

        public void SpawnMonster()
        {


            int monsterCount = currentFloor + 3;                //나오게 할 몬스터 개수 (나오지 않는 경우 포함)
            int monsterTypeStart = (currentFloor - 1) * 6;      //나오게 할 몬스터타입 시작점       던전층수따라 변환
            int monsterTypeEnd = (currentFloor * 6);            //나오게 할 몬스터타입 끝지점       던전층수따라 변환
            int probability = 4;                                //소환 안 나오게 할 확률 (1/n)
            int spawn;                                          //소환 확률용 변수

            Random random = new Random();

            for (int i = 0; i < monsterCount; i++)
            {
                // 스폰리스트에 랜덤한 몬스터 타입 추가
                if (i == 0)
                {
                    spawn = random.Next(1, probability);      //첫 몬스터는 확정으로 소환
                }
                else
                {
                    spawn = random.Next(0, probability);      //이후로는 확률 1/4
                }

                if (spawn != 0)
                {

                    int rand = random.Next(monsterTypeStart, monsterTypeEnd);
                    spawnList.Add(new Monster(monsters[rand]));

                }
            }
            for (int index = 0; index < spawnList.Count; index++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($" Lv.{spawnList[index].level} {spawnList[index].name} HP {spawnList[index].hp}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        public void InputAttack(Player player)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choose) || (choose != 1))
                {
                    Console.WriteLine(" 다시 입력해주십시오");
                    Console.Write(" >> ");
                    continue;
                }
                else
                {
                    sceneBattleAttack.BattleLoop(player, spawnList);
                    spawnList.Clear();
                    break;
                }
            }
        }
        public void AddDroppedItemsToInventory(Player player, List<Item> droppedItems)
        {
            foreach (var item in droppedItems)
            {
                player.inventory.AddItem(item);
                Console.WriteLine($"{item.name}을 획득했습니다.");
            }
        }

        public SceneBattle()
        {
            
        }
    }
}