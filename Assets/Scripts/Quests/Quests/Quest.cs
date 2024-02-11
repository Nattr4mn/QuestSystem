// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using Nattr4mn.Quests.Status;

namespace Nattr4mn.Quests
{
	public class Quest : IQuest
	{
		public event Action<IQuest> Updated;

		public Quest(int id, string name, string description)
		{
			ID = id;
			Name = name;
			Description = description;
			Status = QuestStatus.PAUSED;
		}

		public int ID { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public QuestStatus Status { get; private set; }

		public void SetStatus(QuestStatus status)
		{
			if (Status == QuestStatus.COMPLETED || Status == QuestStatus.FAILED || Status == QuestStatus.CANCELED)
			{
				throw new Exception($"Quest {ID} completed!");
			}

			Status = status;
			Updated?.Invoke(this);
		}
	}
}
