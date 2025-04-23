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
		Player player = new Player();
		SceneStart sceneStart = new SceneStart();



        public void Start()
		{
			//player.Example();
			playerCreate.Start(player);
			jobSelect.Start();
			while (player.job == null)
			{
				jobSelect.Input(player);
			}

			while (true)
			{
				sceneStart.Start(player);
			}

		}
	}
}
