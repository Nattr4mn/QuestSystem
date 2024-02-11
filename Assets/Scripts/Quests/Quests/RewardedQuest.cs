// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using Nattr4mn.Quests.Rewards;
using Nattr4mn.Quests.Status;

namespace Nattr4mn.Quests
{
	public class RewardedQuest : IQuest
	{
		private IQuest _baseQuest;
		private bool _rewarded;

		public event Action<IQuest> Updated
		{
			add => _baseQuest.Updated += value;
			remove => _baseQuest.Updated -= value;
		}

		public RewardedQuest(IQuest quest, IRewardSource reward)
		{
			_baseQuest = quest;
			Reward = reward;
			_rewarded = false;
			Updated += OnComplete;
		}

		public int ID => _baseQuest.ID;
		public string Name => _baseQuest.Name;
		public string Description => _baseQuest.Description;
		public QuestStatus Status => _baseQuest.Status;
		public IRewardSource Reward { get; private set; }

		public void SetStatus(QuestStatus status) => _baseQuest.SetStatus(status);

		private void OnComplete(IQuest quest)
		{
			if (!_rewarded && quest.Status == QuestStatus.COMPLETED)
			{
				_rewarded = true;
				Reward?.GiveReward();
				quest.Updated -= OnComplete;
			}
		}
	}
}
