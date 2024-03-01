// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using Nattr4mn.Quests.Repositories;
using UnityEngine;

namespace Nattr4mn.Quests.Configs
{
	public abstract class BaseQuestConfig : ScriptableObject
	{
		[field: SerializeField] public int ID { get; private set; }
		
		[SerializeField] private string _name;
		[SerializeField] private string _description;

		public string Name => _name;
		public string Description => _description;

		public abstract IQuest Create(QuestRepository repository);
	}
}
