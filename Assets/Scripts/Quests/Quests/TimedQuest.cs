// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using Nattr4mn.Quests.Extensions;
using Nattr4mn.Quests.Status;
using Nattr4mn.Quests.Timers;

namespace Nattr4mn.Quests
{
	public class TimedQuest : IQuest
	{
		private IQuest _baseQuest;

		public event Action<IQuest> Updated
		{
			add => _baseQuest.Updated += value;
			remove => _baseQuest.Updated -= value;
		}

		public TimedQuest(IQuest quest, ITimer timer)
		{
			_baseQuest = quest;
			Timer = timer;
			Updated += OnComplete;
			Updated += OnActivate;
			Timer.Completed += OnTimerComplete;
		}
		
		public int ID => _baseQuest.ID;
		public string Name => _baseQuest.Name;
		public string Description => _baseQuest.Description;
		public QuestStatus Status => _baseQuest.Status;
		public ITimer Timer { get; private set; }

		public void SetStatus(QuestStatus status) => _baseQuest.SetStatus(status);
		
		private void OnTimerComplete()
		{
			SetStatus(QuestStatus.FAILED);
		}

		private void OnComplete(IQuest quest)
		{
			if (!quest.IsComplete())
			{
				return;
			}
			
			Timer.Stop();
			quest.Updated -= OnComplete;
			Timer.Completed -= OnTimerComplete;
		}

		private void OnActivate(IQuest quest)
		{
			if (quest.Status != QuestStatus.ACTIVE)
			{
				return;
			}
			
			Timer.Start();
			Updated -= OnActivate;
		}
	}
}
