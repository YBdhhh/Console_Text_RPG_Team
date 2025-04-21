using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{

	internal class JobSelect
	{
		//Player player = new Player(); 
		public SceneStatus sceneStatus = new SceneStatus(); //테스트
		StringBuilder sb = new StringBuilder();
		List<Job> job = new List<Job>();

		public void JobList()
		{
			job.Add(new Job("Warrior", 200, 10, 20));
			job.Add(new Job("Wizzard", 70, 50, 10));
			job.Add(new Job("Archor", 100, 20, 15));
		}

		public void Start()
		{
			JobList(); // 직업 생성
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("직업 선택");
			Console.ResetColor();
			sb.Append("원하시는 직업을 선택하여 주십시오").Append("\n");
			sb.AppendLine("");
			for (int i = 0; i < job.Count; i++)
			{
				sb.Append($"{i+1}. 직업 : {job[i].name, -8} 체력 : {job[i].hp, -5} 공격력 : {job[i].atk, -5} 방어력 : {job[i].def, -5} ").Append("\n");
			}
			sb.Append("\n");
			sb.Append("원하시는 행동을 입력해주세요.").Append("\n");
			sb.Append(">> ");
			Console.Write(sb.ToString());
		}

		public void Input()
		{
			while (true)
			{
				try
				{
					int input = int.Parse(Console.ReadLine());
					if (input > 0 && input <= 3)
					{
						int index = input - 1;
						sceneStatus.player.hp = job[index].hp;
						sceneStatus.player.atk = job[index].atk;
						sceneStatus.player.def = job[index].def;
						sceneStatus.player.job = job[index].name;
						float hp = sceneStatus.player.hp;
						float atk = sceneStatus.player.atk;
						float def = sceneStatus.player.def;
						string name = sceneStatus.player.job;
						

						StringBuilder sb = new StringBuilder();

						sb.AppendLine($"{index+1}. 직업 : {name, -8} 체력 : {hp, -5} 공격력 : {atk, -5} 방어력 : {def,-5}을 선택하셨습니다.");
						sb.AppendLine("계속 하려면 아무 키나 눌러주십시오");
						sb.Append(">> ");
						Console.Write(sb.ToString());
						Console.ReadLine();
						return;
					}
					else
					{
						Console.WriteLine("다시 입력해주십시오");
						continue;
					}
				}
				catch (Exception)
				{
					Console.WriteLine("다시 입력해주십시오");
					continue;
				}

			}
		}
	}
}
