// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using Nattr4mn.Quests.Status;

namespace Nattr4mn.Quests.Extensions
{
	public static class QuestExtension
	{
		public static bool IsComplete(this IQuest quest)
		{
			return quest.Status is QuestStatus.COMPLETED or QuestStatus.FAILED or QuestStatus.CANCELED;
		}
		
		public static bool IsActive(this IQuest quest)
		{
			return quest.Status is QuestStatus.ACTIVE;
		}
		
		public static bool IsPaused(this IQuest quest)
		{
			return quest.Status is QuestStatus.PAUSED;
		}
	}
}
