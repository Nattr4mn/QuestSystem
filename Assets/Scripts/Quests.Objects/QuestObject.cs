// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using Nattr4mn.Quests.Configs;
using UnityEngine;

namespace Nattr4mn.Quests.Objects
{
	public class QuestObject : MonoBehaviour
	{
		[field: SerializeField] public BaseQuestConfig ReferenceQuest { get; private set; }
	}
}
