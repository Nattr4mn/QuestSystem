// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using Nattr4mn.Quests.Rewards;
using UnityEngine;

namespace Nattr4mn.Quests.Configs.Extensions
{
	[CreateAssetMenu(
		fileName = nameof(RewardedQuest), 
		menuName = nameof(Nattr4mn) + "/" + nameof(Quests) + "/" + nameof(Extensions) + "/" + nameof(RewardedQuest))]
	public class RewardedQuestConfig : QuestExtensionConfig
	{
		[field: SerializeField] public RewardConfig Reward { get; private set; }
		
		public override IQuest Create(IQuest quest)
		{
			var reward = Reward.Create();
			return new RewardedQuest(quest, reward);
		}
	}
}
