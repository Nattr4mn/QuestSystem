// SPDX-License-Identifier: Apache-2.0
// © 2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using Nattr4mn.Quests.Extensions;
using Nattr4mn.Quests.Configs;
using Nattr4mn.Quests.Journals;
using Nattr4mn.Quests.MultiQuests;
using UnityEngine;

namespace Nattr4mn.Quests.Listeners
{
	public class QuestListener : MonoBehaviour
	{
		[SerializeField] private QuestJournal _journal;
		[SerializeField] private BaseQuestConfig _targetQuest;
		
		private IQuest _quest;

		public event Action<IQuest> QuestStarted;
		public event Action QuestActivated;
		public event Action QuestCompleted;

		private void Awake()
		{
			_journal.QuestStarted += OnQuestStart;
		}
		
		private void OnDestroy()
		{
			if (_quest != null)
			{
				_quest.Updated += CheckQuestCompletion;
				_quest.Updated += CheckQuestActivation;
			}
			
			_journal.QuestStarted -= OnQuestStart;
		}

		private void OnQuestStart(IQuest quest)
		{
			var repository = _journal.Repository;
			var isMultiQuest = repository.TryGetQuestByType(quest, out MultiQuest multiQuest);
			
			if (isMultiQuest && multiQuest.TryGetQuest(_targetQuest.ID, out IQuest nestedQuest))
			{
				Init(nestedQuest);
			}
			else if (quest.ID == _targetQuest.ID)
			{
				Init(quest);
			}
		}

		private void Init(IQuest quest)
		{
			QuestStarted?.Invoke(quest);
			_quest = quest;
			
			_quest.Updated += CheckQuestCompletion;
			CheckQuestCompletion(quest);
			
			_quest.Updated += CheckQuestActivation;
			CheckQuestActivation(quest);
			
			_journal.QuestStarted -= OnQuestStart;
		}

		private void CheckQuestCompletion(IQuest quest)
		{
			if (quest.IsComplete())
			{
				QuestCompleted?.Invoke();
				quest.Updated -= CheckQuestCompletion;
			}
		}

		private void CheckQuestActivation(IQuest quest)
		{
			if (quest.IsActive())
			{
				QuestActivated?.Invoke();
				quest.Updated -= CheckQuestActivation;
			}
		}
	}
}
