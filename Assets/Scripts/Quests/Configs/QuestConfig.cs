// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System.Collections.Generic;
using Nattr4mn.Quests.Configs.Conditions;
using Nattr4mn.Quests.Configs.Extensions;
using Nattr4mn.Quests.Repositories;
using UnityEngine;

namespace Nattr4mn.Quests.Configs
{
	[CreateAssetMenu(
		fileName = nameof(QuestConfig),
		menuName = nameof(Nattr4mn) + "/" + nameof(Quests) + "/" + nameof(QuestConfig))]
	public class QuestConfig : BaseQuestConfig
	{
		[SerializeField, Space] private QuestConditionConfig _condition;
		[SerializeField, Space] private List<QuestExtensionConfig> _extensions;

		public override IQuest Create(QuestRepository repository)
		{
			IQuest baseQuest = new Quest(ID, Name, Description);
			baseQuest = _condition.Create(baseQuest);
			repository.Add(baseQuest.GetType(), baseQuest);
			return CreateExtensions(baseQuest, repository);
		}

		private IQuest CreateExtensions(IQuest quest, QuestRepository repository)
		{
			foreach (var extension in _extensions)
			{
				quest = extension.Create(quest);
				repository.Add(quest.GetType(), quest);
			}
			
			return quest;
		}
	}
}
