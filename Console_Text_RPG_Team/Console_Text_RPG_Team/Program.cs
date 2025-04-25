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
			AudioManager audioManager = new AudioManager("./BGM01.mp3");
			audioManager.PlayLooping();
			audioManager.SetVolume(0.05f);
			LoadData(ref gameLogic);
			gameLogic.Start();

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
