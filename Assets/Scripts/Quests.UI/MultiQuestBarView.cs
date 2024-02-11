// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System.Collections.Generic;
using System.Linq;
using Nattr4mn.Quests.MultiQuests;
using UnityEngine;

namespace Nattr4mn.Quests.UI
{
	public class MultiQuestBarView : BaseQuestBarView
	{
		[SerializeField] private QuestInfoView _questInfo;
		[SerializeField] private Transform _nestedQuestContainer;

		private List<QuestInfoView> _questInfoPool = new List<QuestInfoView>();
		private MultiQuest _multiQuest;

		public override void Init(IQuest quest, QuestRepository questRepository)
		{
			_multiQuest = (MultiQuest) quest;
			_questInfoPool.ForEach(questInfo => questInfo.Disable());
			_questInfo.Init(quest, questRepository);
			DrawQuestsInfo(questRepository);
		}

		private void DrawQuestsInfo(QuestRepository questRepository)
		{
			var quests = _multiQuest.Quests;
			foreach (var quest in quests)
			{
				var questInfo = _questInfoPool.FirstOrDefault(questInfo => !questInfo.gameObject.activeSelf);
				if (questInfo == null)
				{
					questInfo = CreateQuestInfo();
				}
				questInfo.Init(quest, questRepository);
				questInfo.Enable();
			}
		}

		private QuestInfoView CreateQuestInfo()
		{
			var questInfo = Instantiate(_questInfo, _nestedQuestContainer);
			_questInfoPool.Add(questInfo);
			return questInfo;
		}
	}
}
