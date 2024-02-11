// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using UnityEngine;

namespace Nattr4mn.Quests.UI.CompleteMark
{
	public abstract class CompleteMarkView : MonoBehaviour
	{
		public abstract void DrawMark();
		public abstract void ResetMark();
	}
}
