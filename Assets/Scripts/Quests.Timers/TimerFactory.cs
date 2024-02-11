// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using UnityEngine;

namespace Nattr4mn.Quests.Timers
{
	public abstract class TimerFactory : ScriptableObject
	{
		public abstract ITimer Create(float timePerSeconds);
	}
}
