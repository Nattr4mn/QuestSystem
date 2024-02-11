// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using UnityEngine;

namespace Nattr4mn.Quests.Rewards
{
	public abstract class RewardConfig : ScriptableObject
	{
		public abstract IRewardSource Create();
	}
}
