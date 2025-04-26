using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

enum State
{
	Exit = 0,
	Status = 1,
	Battle = 2
}

namespace Console_Text_RPG_Team
{

	internal class SceneStart
	{
		SceneStatus sceneStatus = new SceneStatus();		
		SceneRest sceneRest = new SceneRest();
		public SceneQuest SceneQuest = new SceneQuest();
		private Player player = new Player();
		private SceneShop sceneShop = new SceneShop();
		private SceneBattle sceneBattle = new SceneBattle();
		private SceneInventory sceneInventory = new SceneInventory();
        private Inventory inventory = new Inventory();

		public SceneStart()
		{
		}

        

		StringBuilder sb = new StringBuilder();
		public void Start(Player player, GameLogic gameLogic)
		{
			//gameLogic.SaveData(gameLogic);
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine($" 장로 스탄: 단풍 마을에 오신 {player.name} 용사여 환영하네.");
            Console.WriteLine(" 여기서는 자유롭게 행동 할 수있네.");
			Console.ResetColor();
            sb.AppendLine();

			sb.AppendLine(" 1. 상태 보기");
			sb.AppendLine(" 2. 전투 시작");
            sb.AppendLine(" 3. 인벤토리 보기");
            sb.AppendLine(" 4. 상점 이동");
			sb.AppendLine(" 5. 휴식 하기");
			sb.AppendLine(" 6. 퀘스트술집");
            sb.AppendLine(" 0. 게임 종료");

			sb.AppendLine();


			sb.AppendLine(" 원하시는 행동의 번호를 입력해주세요.");
            sb.Append(" >> ");
            Console.Write(sb.ToString());

			sb.Clear();

			int result = Checkinpvt(0, 7);


			switch (result)
			{
				case 0:
					Environment.Exit(0);
					break;
				case 1:
					sceneStatus.Start(player);
                    break;
				case 2:
					sceneBattle.SelectDungeon(player);
					break;
				case 3:
                    sceneInventory.Start(player.inventory, player);
                    break;
                case 4:
                    sceneShop.Start(player, player.inventory);
                    break;
				case 5:
					sceneRest.Start(player);
					break;
				case 6:
					SceneQuest.Start(player);
					break;
				case 7:
					gameLogic.Reset();
					break;


            }

		}

		public int Checkinpvt(int min, int max)
		{
			int result;
			while (true)
			{
				string input = Console.ReadLine();
				bool isNumber = int.TryParse(input, out result);
				if (isNumber)
				{
					if (result >= min && result <= max)
						return result;
				}
				Console.WriteLine(" 잘못된 입력입니다.");
                Console.Write(" >> ");
            }
		}
	}
}
