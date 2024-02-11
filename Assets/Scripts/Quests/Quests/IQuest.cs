// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using Nattr4mn.Quests.Status;

namespace Nattr4mn.Quests
{
	public interface IQuest
	{
		event Action<IQuest> Updated;

		int ID { get; }
		string Name { get; }
		string Description { get; }
		QuestStatus Status { get; }

		void SetStatus(QuestStatus status);
	}
}
