// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using Nattr4mn.Quests.Timers;
using UnityEngine;

namespace Nattr4mn.Quests.Configs.Extensions
{
	[CreateAssetMenu(
		fileName = nameof(TimedQuest), 
		menuName = nameof(Nattr4mn) + "/" + nameof(Quests) + "/" + nameof(Extensions) + "/" + nameof(TimedQuest))]
	public class TimedQuestConfig : QuestExtensionConfig
	{
		[field: SerializeField, Min(0f)] public float TimePerSeconds { get; private set; }
		[field: SerializeField] public TimerFactory Timer { get; private set; }
		
		public override IQuest Create(IQuest quest)
		{
			var timer = Timer.Create(TimePerSeconds);
			return new TimedQuest(quest, timer);
		}
	}
}
