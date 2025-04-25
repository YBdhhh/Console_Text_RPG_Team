using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
	internal class GameLogic
	{
		PlayerCreate playerCreate = new PlayerCreate();
		JobSelect jobSelect = new JobSelect();
		public Player player = new Player();
		SceneStart sceneStart = new SceneStart();

		public GameLogic()
		{

		}

		public GameLogic(GameLogic gameLogic)
		{
			this.player = gameLogic.player;
			this.sceneStart = gameLogic.sceneStart;
		}

		public void Reset()
		{
			player = new Player();
			sceneStart.InitSceneStart(player);
			playerCreate.Start(player);
			jobSelect.Start();
		}

        public void Start()
		{
			if (player.name == null)
			{
				Reset();
				while (player.job == null)
				{
					jobSelect.Input(player);
				}
			}

			while (true)
			{
				sceneStart.Start(player, this);
			}

		}
		public void SaveData(GameLogic gameLogic)
		{
			string filePath = "./DataSave.json";
			var testList = new GameLogic(gameLogic);
			var jsonSerializeTestList = JsonConvert.SerializeObject(testList);
			File.WriteAllText(filePath, jsonSerializeTestList);
		}

	}
}
