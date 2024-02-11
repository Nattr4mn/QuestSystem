// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using Nattr4mn.Quests.Timers;
using UnityEngine;

namespace Nattr4mn.Quests.Timers
{
	[CreateAssetMenu(
		fileName = nameof(StandardTimer), 
		menuName = nameof(Nattr4mn) + "/" + nameof(Timers) + "/" + nameof(StandardTimerFactory))]
	public class StandardTimerFactory : TimerFactory
	{
		public override ITimer Create(float timePerSeconds)
		{  
			return new StandardTimer(timePerSeconds);
		}
	}
}
