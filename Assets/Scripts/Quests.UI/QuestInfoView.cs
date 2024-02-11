// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using Nattr4mn.Quests.Extensions;
using Nattr4mn.Quests.UI.CompleteMark;
using TMPro;
using UnityEngine;

namespace Nattr4mn.Quests.UI
{
	public class QuestInfoView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _questName;
		[SerializeField] private CompleteMarkView _completeMark;
		[SerializeField] private QuestTimer _timer;

		private IQuest _quest;
		private bool _dependsOnTime;

		public void Enable()
		{
			gameObject.SetActive(true);
		}

		public void Disable()
		{
			gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			Clear();
		}
		
		public void Init(IQuest quest, QuestRepository questRepository)
		{
			Clear();
			_quest = quest;
			_questName.text = _quest.Name;
			CheckTimeDependence(questRepository);
			CheckQuestStatus();
		}

		private void Clear()
		{
			if (_quest != null)
			{
				_quest.Updated -= DrawCompleteMark;
			}
			_timer.Disable();
			_timer.Clear();
		}

		private void CheckTimeDependence(QuestRepository questRepository)
		{
			_dependsOnTime = questRepository.Exists<TimedQuest>(_quest);

			if (_dependsOnTime)
			{
				var timedQuest = questRepository.GetQuestByType<TimedQuest>(_quest);
				_timer.Draw(timedQuest.Timer);
			}
		}

		private void CheckQuestStatus()
		{
			if (_quest.IsComplete())
			{
				DrawCompleteMark(_quest);
			}
			else
			{
				_completeMark.ResetMark();
				_quest.Updated += DrawCompleteMark;
			}
		}

		private void DrawCompleteMark(IQuest quest)
		{
			if (quest.IsComplete())
			{
				_completeMark.DrawMark();
				_timer.Disable();
				quest.Updated -= DrawCompleteMark;
			}
		}
	}
}
