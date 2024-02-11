using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nattr4mn.Quests.Timers
{
	public class StandardTimer : ITimer
	{
		private const int TIMER_INTERVAL = 1000;

		private float _timePerSeconds;
		private float _currentTime;
		private Timer _timer;
		private bool _isActive;

		public event Action Completed;
		public event Action<float> Updated;
		
		public StandardTimer(float timePerSeconds)
		{
			_timePerSeconds = timePerSeconds;
		}

		public async void Start()
		{
			_currentTime = 0f;
			var timerCallback = new TimerCallback(Update);
			_timer = new Timer(timerCallback, null, 0, 1000);
			_isActive = true;  
			await Task.Run(() => Update()); 
		}

		public void Stop()
		{
			_isActive = false;
			_timer.Dispose();
		}

		private void Update()
		{
			while (_isActive && _currentTime < _timePerSeconds)
			{
				Task.Delay(TIMER_INTERVAL).Wait();
			}
		}
		
		private void Update(object obj)
		{
			_currentTime += 1;
			Updated?.Invoke(_timePerSeconds - _currentTime);
			
			if (_currentTime >= _timePerSeconds)
			{
				Stop();
				Completed?.Invoke();
			}
		}
	}
}
