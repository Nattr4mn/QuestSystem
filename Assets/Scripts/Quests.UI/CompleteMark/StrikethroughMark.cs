// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using TMPro;
using UnityEngine;

namespace Nattr4mn.Quests.UI.CompleteMark
{
	public class StrikethroughMark : CompleteMarkView
	{
		[SerializeField] private TMP_Text _text;
		
		public override void DrawMark()
		{
			_text.fontStyle = FontStyles.Strikethrough;
		}
		
		public override void ResetMark()
		{
			_text.fontStyle = FontStyles.Normal;
		}
	}
}
