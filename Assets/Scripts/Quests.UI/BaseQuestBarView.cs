// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using Nattr4mn.Quests.Repositories;
using UnityEngine;

namespace Nattr4mn.Quests.UI
{
	public abstract class BaseQuestBarView : MonoBehaviour
	{
		public abstract void Init(IQuest quest, QuestRepository questRepository);

		public void Enable()
		{
			gameObject.SetActive(true);
		}

		public void Disable()
		{
			gameObject.SetActive(false);
		}
	}
}
