using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm{
	public struct Clock{

		public int minute{
			get{
				return Mathf.FloorToInt(totalSeconds / 60);
			}
		}

		public int second{
			get{
				return Mathf.FloorToInt(totalSeconds % 60);
			}
		}

		public float totalSeconds;

		public Clock (float _totalSeconds) {
			totalSeconds = _totalSeconds;
		}

		public Clock (int min, int sec) {
			totalSeconds = min * 60 + sec;
		}

		public void Set (float _totalSeconds) {
			totalSeconds = _totalSeconds;
		}

		public void Set (int min, int sec) {
			totalSeconds = min * 60 + sec;
		}

		public override string ToString ()
		{
			return string.Format ("{0}:{1}", minute, second.ToString("00"));
		}
	}
}