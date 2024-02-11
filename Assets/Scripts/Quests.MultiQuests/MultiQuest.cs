// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;
using Nattr4mn.Quests.Extensions;
using Nattr4mn.Quests.Status;

namespace Nattr4mn.Quests.MultiQuests
{
	public class MultiQuest : IQuest
	{
		private IQuest _baseQuest;
		private IQuest _currentQuest;
		private List<IQuest> _quests;
		private ExecutionMethod _executionMethod;

		public event Action<IQuest> Updated
		{
			add => _baseQuest.Updated += value;
			remove => _baseQuest.Updated -= value;
		}

		public MultiQuest(IQuest baseQuest, List<IQuest> quests, ExecutionMethod executionMethod)
		{
			_baseQuest = baseQuest;
			_quests = quests;
			_executionMethod = executionMethod;
			_currentQuest = _quests[0];

			if (_executionMethod == ExecutionMethod.PARALLEL)
			{
				foreach (var quest in _quests)
				{
					quest.SetStatus(QuestStatus.ACTIVE);
					quest.Updated += OnQuestUpdate;
				}
			}
			else
			{
				_currentQuest.Updated += OnQuestUpdate;
				_currentQuest.SetStatus(QuestStatus.ACTIVE);
			}
		}

		public int ID => _baseQuest.ID;
		public string Name => _baseQuest.Name;
		public string Description => _baseQuest.Description;
		public QuestStatus Status => _baseQuest.Status;
		public IReadOnlyList<IQuest> Quests => _quests;

		public void SetStatus(QuestStatus status)
		{
			if (status is QuestStatus.FAILED or QuestStatus.CANCELED)
			{
				SetQuestsStatus(status);
			}
			
			_baseQuest.SetStatus(status);
		}

		public bool TryGetQuest(int id, out IQuest quest)
		{
			quest = _quests.FirstOrDefault(quest => quest.ID == id);
			return quest != null;
		}

		private void OnQuestUpdate(IQuest quest)
		{
			if (quest.Status == QuestStatus.COMPLETED)
			{
				SetNextQuest(quest);
				CheckCompletion();
			}
			else if (quest.Status == QuestStatus.FAILED)
			{
				SetStatus(QuestStatus.FAILED);
			}
			else if (quest.Status == QuestStatus.CANCELED)
			{
				throw new Exception($"MultiQuest {ID}: A nested quest {quest.ID} cannot be canceled!");
			}
		}

		private void SetNextQuest(IQuest quest)
		{
			if (_executionMethod == ExecutionMethod.PARALLEL)
			{
				_currentQuest = _quests.FirstOrDefault(nextTask => nextTask.Status == QuestStatus.ACTIVE);
			}
			else if (_executionMethod == ExecutionMethod.SEQUENTIAL && quest.ID == _currentQuest.ID)
			{
				var nextIndex = _quests.IndexOf(_currentQuest) + 1;
				SetNextQuest(nextIndex);
			}
			quest.Updated -= OnQuestUpdate;
		}

		private void SetNextQuest(int index)
		{
			_currentQuest = index < _quests.Count ? _quests[index] : null;
			
			if (_currentQuest != null)
			{
				_currentQuest.SetStatus(QuestStatus.ACTIVE);
				_currentQuest.Updated += OnQuestUpdate;
			}
		}

		private void SetQuestsStatus(QuestStatus status)
		{
			foreach (var quest in _quests)
			{
				quest.Updated -= OnQuestUpdate;
				if (!quest.IsComplete())
				{
					quest.SetStatus(status);
				}
			}
		}

		private void CheckCompletion()
		{
			if (_quests.All(quest => quest.IsComplete()))
			{
				SetStatus(QuestStatus.COMPLETED);
			}
		}
	}
}
