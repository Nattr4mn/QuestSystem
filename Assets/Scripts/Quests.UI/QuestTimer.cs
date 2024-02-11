// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Aleksandr Kalashnikov <aleksandrr.kalashnikov@gmail.com>

using System;
using Nattr4mn.Quests.Timers;
using TMPro;
using UnityEngine;

namespace Nattr4mn.Quests.UI
{
	public class QuestTimer : MonoBehaviour
	{
		[SerializeField] private TMP_Text _timerText;
		[SerializeField] private string _timerFormat = "{1:d2}:{2:d2}";
		private ITimer _timer;
		
		public void Draw(ITimer timer)
		{
			_timerText.gameObject.SetActive(true);
			_timer = timer;
			_timer.Updated += UpdateTimer;
		}

		public void Clear()
		{
			if (_timer != null)
			{
				_timer.Updated -= UpdateTimer;
			}
		}

		public void Disable()
		{
			_timerText.gameObject.SetActive(false);
		}

		private void UpdateTimer(float seconds)
		{
			TimeSpan time = TimeSpan.FromSeconds(seconds);
			_timerText.text = string.Format(_timerFormat, time.Hours, time.Minutes, time.Seconds);
		}
	}
}
