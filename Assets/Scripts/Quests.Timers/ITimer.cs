// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;

namespace Nattr4mn.Quests.Timers
{
	public interface ITimer
	{
		public event Action Completed;
		public event Action<float> Updated;

		void Start();
		void Stop();
	}
}
