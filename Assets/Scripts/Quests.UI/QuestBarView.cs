// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using UnityEngine;

namespace Nattr4mn.Quests.UI
{
	public class QuestBarView : BaseQuestBarView
	{
		[SerializeField] private QuestInfoView _questInfo;

		public override void Init(IQuest quest, QuestRepository questRepository)
		{
			_questInfo.Init(quest, questRepository);
		}
	}
}
