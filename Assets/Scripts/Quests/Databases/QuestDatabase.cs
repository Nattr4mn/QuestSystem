// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;
using Nattr4mn.Quests.Configs;
using UnityEngine;

namespace Nattr4mn.Quests.Databases
{
	[CreateAssetMenu(fileName = nameof(QuestDatabase),
		menuName = nameof(Nattr4mn) + "/" + nameof(Quests) + "/" + nameof(QuestDatabase))]
	public class QuestDatabase : ScriptableObject
	{
		[SerializeField] private List<BaseQuestConfig> _quests;

		public BaseQuestConfig GetQuestByID(int id)
		{
			var quest = _quests.FirstOrDefault(nextQuest => nextQuest.ID == id);

			if (quest == null)
			{
				throw new ArgumentException($"The quest with ID {id} does not exist in the database!");
			}

			return quest;
		}
	}
}
