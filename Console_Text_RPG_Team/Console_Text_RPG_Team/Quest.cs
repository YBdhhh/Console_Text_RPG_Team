using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

enum EventType
{
	HitDamage,
	KillOther,
	KillBoss,
	UseGold,
	EquipItem,
	Stack,
	Mission
}


namespace Console_Text_RPG_Team
{
	internal class Quest
	{
		public string name; //퀘스트명
		public string explain; //퀘스트 설명
		public int curProgress; //퀘스트 현재 수치
		public int endProgress; //퀘스트 완료 수치

		public Item item = new Item(); 
		public string pickName; //대상
		//public EventType eventType;

		public EventType eventType; //이벤트? if( ) 값 -> 람다식 
		//public Item rewardItem;
		public int rewardExp;

		public Quest()
		{
		}

		public Quest(string Name, string Explain, int CurProgress, int EndProgress, Item item, EventType EventType, int RewardExp, string pickName)
		{
			// Item RewardItem
			this.name = Name;
			this.explain = Explain;
			curProgress = 0;
			endProgress = EndProgress;
			eventType = EventType;
			//rewardItem = RewardItem;
			rewardExp = RewardExp;
			this.pickName = pickName;
			this.item = item;
		}

		public void PlayEvent(Quest quest, string name)
		{
			switch (quest.eventType)
			{
				case EventType.EquipItem:
					EventEquipItem(quest, name);
					break;
			}
		}

		public void PlayEvent(Quest quest, int value)
		{
			switch (quest.eventType)
			{
				case EventType.HitDamage:
					if (value > 0)
					{
						EventHit(quest, value);
					}
					break;

				case EventType.UseGold:
					EventUseGold(quest, value);
					break;
			}
		}

		public void PlayEvent(Quest quest, int value, string name)
		{
			switch (quest.eventType)
			{
				case EventType.KillOther:
					EventKill(quest, value, name);
					break;
			}
		}

		public void EventHit(Quest quest, int damage)
		{
			curProgress += damage;
		}

		public void EventKill(Quest quest, int kill, string name)
		{
			if (quest.pickName == null)
			{
				quest.curProgress += kill;
			}
			if (name.Contains(quest.pickName))
			{
				quest.curProgress += kill;
			}
		}

		public void EventUseGold(Quest quest, int gold)
		{
			curProgress = gold;
		}

		public void EventEquipItem(Quest quest, string name)
		{
			if (quest.pickName == null)
			{
				quest.curProgress++;
			}
			if (name.Contains(quest.pickName))
			{
				quest.curProgress++;
			}
		}

	}
}
