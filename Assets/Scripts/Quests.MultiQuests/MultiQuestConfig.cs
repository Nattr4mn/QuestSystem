// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System.Collections.Generic;
using Nattr4mn.Quests.Configs;
using Nattr4mn.Quests.Configs.Extensions;
using UnityEngine;

namespace Nattr4mn.Quests.MultiQuests
{
	[CreateAssetMenu(
		fileName = nameof(MultiQuestConfig), 
		menuName = nameof(Nattr4mn) + "/" + nameof(Quests) + "/" + nameof(MultiQuestConfig))]
	public class MultiQuestConfig : BaseQuestConfig
	{
		[field: SerializeField] public ExecutionMethod QuestExecutionMethod { get; private set; }
		[field: SerializeField] public List<BaseQuestConfig> Quests { get; private set; }
		[field: SerializeField, Space] public List<QuestExtensionConfig> Extensions { get; private set; }

		public override IQuest Create(QuestRepository repository)
		{
			IQuest quest = CreateQuest(repository);

			foreach (var extension in Extensions)
			{
				quest = extension.Create(quest);
				repository.Add(quest.GetType(), quest);
			}
			
			return quest;
		}

		public IQuest CreateQuest(QuestRepository repository)
		{
			var baseQuest = new Quest(ID, Name, Description);
			var quests = CreateNestedQuests(repository);
			var multiQuest = new MultiQuest(baseQuest, quests, QuestExecutionMethod);
			
			repository.Add(multiQuest.GetType(), multiQuest);
			return multiQuest;
		}

		private List<IQuest> CreateNestedQuests(QuestRepository repository)
		{
			var quests = new List<IQuest>();
			
			foreach (var questConfig in Quests)
			{
				var quest = questConfig.Create(repository);
				quests.Add(quest);
				repository.Add(quest.GetType(), quest);
			}
			
			return quests;
		}
	}
}
