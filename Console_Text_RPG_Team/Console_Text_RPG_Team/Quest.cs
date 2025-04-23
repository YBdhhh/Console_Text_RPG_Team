using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
enum EventType
{
	hit,
	money,
	attack,
	hp,
	kill,
	
}
*/

namespace Console_Text_RPG_Team
{
	internal class Quest
	{
		public string name; //퀘스트명
		public string explain; //퀘스트 설명
		public int curProgress; //퀘스트 현재 수치
		public int endProgress; //퀘스트 완료 수치
		//public EventType eventType;

		public Event eventType; //이벤트? if( ) 값 -> 람다식 
		//public Item rewardItem;
		public int rewardExp;

		public Quest(string Name, string Explain, int CurProgress, int EndProgress, Event EventType, int RewardExp)
		{
			// Item RewardItem
			this.name = Name;
			this.explain = Explain;
			curProgress = 0;
			endProgress = EndProgress;
			eventType = EventType;
			//rewardItem = RewardItem;
			rewardExp = RewardExp;
		}
	}
}
