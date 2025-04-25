using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Text_RPG_Team
{
    internal class Program
    {
        static void Main(string[] args)
        {
			Console.SetBufferSize(120, 100);
			Console.SetWindowSize(120, 30);
            GameLogic gameLogic = new GameLogic();
			LoadData(ref gameLogic);
			gameLogic.Start(gameLogic);

        }
		static void LoadData(ref GameLogic gameLogic)
		{
			string filePath = "./DataSave.json";
			if (File.Exists(filePath))
			{
				string json = File.ReadAllText(filePath);
				var jsonDeserializeList = JsonConvert.DeserializeObject<GameLogic>(json);
				gameLogic = jsonDeserializeList;
			}
		}
	}
    
}
