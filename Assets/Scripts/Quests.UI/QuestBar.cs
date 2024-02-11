// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using Nattr4mn.Quests.Extensions;
using Nattr4mn.Quests.Journals;
using Nattr4mn.Quests.MultiQuests;
using UnityEngine;

namespace Nattr4mn.Quests.UI
{
	public class QuestBar : MonoBehaviour
	{
		[SerializeField] private QuestJournal _journal;
		[SerializeField] private QuestBarView _questBar;
		[SerializeField] private MultiQuestBarView _multiQuestBar;
		
		private IQuest _currentQuest;
		private BaseQuestBarView _activeQuestBar;
		
		
		private void Awake()
		{
			_questBar.Disable();
			_multiQuestBar.Disable();
		}

		private void OnEnable()
		{
			_journal.QuestActivated += OnQuestStart;
		}

		private void OnDisable()
		{
			_journal.QuestActivated -= OnQuestStart;
		}

		private void OnDestroy()
		{
			Clear();
		}

		private void OnQuestStart(IQuest quest)
		{
			if (quest.IsComplete())
			{
				return;
			}
			
			Clear();
			Init(quest);
		}

		private void Clear()
		{
			if (_currentQuest != null)
			{
				_currentQuest.Updated -= OnQuestComplete;
			}
			_multiQuestBar.Disable();
			_questBar.Disable();
		}

		private void Init(IQuest quest)
		{
			var repository = _journal.Repository;
			if (repository.Exists<MultiQuest>(quest))
			{
				_activeQuestBar = _multiQuestBar;
				_currentQuest = repository.GetQuestByType<MultiQuest>(quest);
			}
			else
			{
				_activeQuestBar = _questBar;
				_currentQuest = quest;
			}
			
			_activeQuestBar.Init(_currentQuest, repository);
			_activeQuestBar.Enable();
			_currentQuest.Updated += OnQuestComplete;
		}

		private void OnQuestComplete(IQuest quest)
		{
			if (quest.IsComplete())
			{
				_activeQuestBar.Disable();
				quest.Updated -= OnQuestComplete;
			}
		}
	}
}
