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
        AsciiArt asciiArt = new AsciiArt();
        PlayerCreate playerCreate = new PlayerCreate();
		JobSelect jobSelect = new JobSelect();
		public Player player = new Player();
		SceneStart sceneStart = new SceneStart();
		List<Skill> skills = new List<Skill>
		{//string name, string explain, int damageType, int[] damage, int valueMp, int price
			new Skill("새비지 블로우", "단검으로 적을 타격한다. Mp 1.5 소모 | 공격력 3배의 피해", (int)DamageType.Atk+(int)DamageType.Mp , new int[] {1500, 200}, (int)ValueType.Mp ,20 ,1000),
			new Skill("파워 스트라이크", "장착한 무기로 적에게 일격을 가한다. Mp 1.0 소모 | 공격력 1.2배의 피해", (int)DamageType.Atk , new int[] {120}, (int)ValueType.Mp, 15 ,1000),
			new Skill("매직 클로", "마력을 활용하여 할퀸다. Mp 0.5 소모 | 공격력 1.2배의 피해", (int)DamageType.Atk , new int[] {120}, (int)ValueType.Mp ,8 ,1000),
			new Skill("메소 익스플로전", "동전에 폭탄을 부착하여 던진다. Gold 500 소모 | 공격력의 1.5배의 피해", (int)DamageType.Atk , new int[] {150}, (int)ValueType.Gold, 8 ,1000),
			new Skill("방패 밀치기", "방패로 상대를 밀친다. Mp 1.0 소모 | 방어력*5의 피해", (int)DamageType.Def , new int[] {500}, (int)ValueType.Mp, 15 ,1000),
			new Skill("몸통박치기", "상대방에게 돌진한다. Hp를 1.0 소모 | 체력*2의 피해", (int)DamageType.Hp , new int[] {200}, (int)ValueType.Hp, 15 ,1000),
		};
		public void InitSceneStart(Player player)
		{
			player.skill.Add(skills[0]);
		}

		public GameLogic()
		{
			
			
		}

		[JsonIgnore]
		public List<AudioManager> audioManager = new List<AudioManager>
		{
			new AudioManager("./BGM01.mp3"),
			new AudioManager("./Heal01.mp3"),
			new AudioManager("./Buy01.mp3"),
			new AudioManager("./levelUp01.mp3"),
			new AudioManager("./Attack01.mp3"),
			new AudioManager("./Kill01.mp3"),
			new AudioManager("./InBoss01.mp3"),
			new AudioManager("./GameClear01.mp3")
		};

		public void VolumeSetting()
		{
			for(int i=0; i<audioManager.Count; i++)
			{
				audioManager[i].SetVolume(0.05f);
			}
		}

		public void BGMStart()
		{
			audioManager[0].Stop();
			audioManager[0].SetVolume(0.05f);
			audioManager[0].PlayLooping();
		}


		public GameLogic(GameLogic gameLogic)
		{
			this.player = gameLogic.player;
			this.sceneStart = gameLogic.sceneStart;
			this.audioManager = gameLogic.audioManager;
		}

		public void Reset()
		{

			Console.Clear();
			player = new Player();
			asciiArt.Start();
            InitSceneStart(player);
			playerCreate.Start(player);

			jobSelect.Start();
			while (player.job == null)
			{
				jobSelect.Input(player);
			}
		}

        public void Start(GameLogic gameLogic)
		{
			if (player.name == null)
			{
				Reset();
			}
			VolumeSetting();
			if (player.audio.Count == 0)
			{
				player.audio.Add(audioManager[1]);
				player.audio.Add(audioManager[2]);
				player.audio.Add(audioManager[3]);
				player.audio.Add(audioManager[4]);
				player.audio.Add(audioManager[5]);
				player.audio.Add(audioManager[6]);
				player.audio.Add(audioManager[7]);
			}
			BGMStart();
			

			while (true)
			{
				sceneStart.Start(player, gameLogic);
			}

		}
		//public void SaveData(GameLogic gameLogic)
		//{
		//	string filePath = "./DataSave.json";
		//	var testList = new GameLogic(gameLogic);
		//	var jsonSerializeTestList = JsonConvert.SerializeObject(testList);
		//	File.WriteAllText(filePath, jsonSerializeTestList);
		//}

	}
}
