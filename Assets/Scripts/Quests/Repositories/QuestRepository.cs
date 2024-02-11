// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using System.Collections.Generic;

namespace Nattr4mn.Quests
{
	public class QuestRepository
	{
		private Dictionary<Type, Dictionary<int, IQuest>> _quests;

		public QuestRepository()
		{
			_quests = new Dictionary<Type, Dictionary<int, IQuest>>();
		}

		public void Add(Type type, IQuest quest)
		{
			if (!_quests.ContainsKey(type))
			{
				_quests.Add(type, new Dictionary<int, IQuest>());
			}

			_quests[type][quest.ID] = quest;
		}

		public TQuest GetQuestByType<TQuest>(IQuest quest) where TQuest : class, IQuest
		{
			return _quests[typeof(TQuest)][quest.ID] as TQuest;
		}

		public bool TryGetQuestByType<TQuest>(IQuest quest, out TQuest typedQuest) where TQuest : class, IQuest
		{
			if (_quests.ContainsKey(typeof(TQuest)) && _quests[typeof(TQuest)].ContainsKey(quest.ID))
			{
				typedQuest =_quests[typeof(TQuest)][quest.ID] as TQuest;
				return true;
			}

			typedQuest = null;
			return false;
		}
		
		public bool Exists<TQuest>(IQuest quest) where TQuest : class, IQuest
		{
			return _quests.ContainsKey(typeof(TQuest)) && _quests[typeof(TQuest)].ContainsKey(quest.ID);
		}
	}
}