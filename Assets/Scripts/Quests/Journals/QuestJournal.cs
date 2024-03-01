// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;
using Nattr4mn.Quests.Extensions;
using Nattr4mn.Quests.Configs;
using Nattr4mn.Quests.Databases;
using Nattr4mn.Quests.Repositories;
using Nattr4mn.Quests.Status;
using UnityEngine;

namespace Nattr4mn.Quests.Journals
{
	public sealed class QuestJournal : MonoBehaviour
	{
		[field: SerializeField] public QuestDatabase QuestDatabase { get; private set; }

		private List<IQuest> _quests;

		public event Action<IQuest> QuestStarted;
		public event Action<IQuest> QuestActivated;

		private void Awake()
		{
			_quests = new List<IQuest>();
			Repository = new QuestRepository();
		}

		private void OnDestroy()
		{
			if (ActiveQuest != null)
			{
				ActiveQuest.Updated -= OnActiveQuestComplete;
			}
		}

		public IQuest ActiveQuest { get; private set; }
		public QuestRepository Repository { get; private set; }

		public void StartQuest(int id)
		{
			var quest = QuestDatabase.GetQuestByID(id);
			StartQuest(quest);
		}

		public void StartQuest(BaseQuestConfig questConfig)
		{
			if (QuestExists(questConfig.ID))
			{
				throw new ArgumentException($"Quest {questConfig.ID} already exists!");
			}
			var quest = CreateNewQuest(questConfig);
			ActivateQuest(quest);
			QuestStarted?.Invoke(quest);
		}

		public void CancelQuest(int id)
		{
			var quest = _quests.First(activeQuest => activeQuest.ID == id);
			quest.SetStatus(QuestStatus.CANCELED);
		}

		public bool QuestExists(int id)
		{
			return _quests.Exists(quest => quest.ID == id);
		}

		public IQuest GetQuest(int id)
		{
			return _quests.First(activeQuest => activeQuest.ID == id);
		}

		public void ActivateQuest(IQuest quest)
		{
			if (ActiveQuest != null)
			{
				ActiveQuest.Updated -= OnActiveQuestComplete;
			}
			
			ActiveQuest = quest;
			ActiveQuest.SetStatus(QuestStatus.ACTIVE);
			QuestActivated?.Invoke(ActiveQuest);

			ActiveQuest.Updated += OnActiveQuestComplete;
		}

		private void OnActiveQuestComplete(IQuest quest)
		{
			if (!quest.IsComplete())
			{
				return;
			}
			
			ActiveQuest.Updated -= OnActiveQuestComplete;
			var nextQuest = _quests.FirstOrDefault(incompleteQuest => !incompleteQuest.IsComplete());
			if (nextQuest != null)
			{
				ActivateQuest(nextQuest);
			}
		}

		private IQuest CreateNewQuest(BaseQuestConfig questConfig)
		{
			var quest = questConfig.Create(Repository);
			_quests.Add(quest);
			return quest;
		}
	}
}
