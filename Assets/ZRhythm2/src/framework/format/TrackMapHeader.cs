using System;
using System.IO;
using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm{

	[Serializable]
	public class TrackMapHeader{
		public string Title;

		public string Artist;

		public string Difficult;

		/// <summary>
		/// The name of the audio file.
		/// full path on editor mode.
		/// use "trackMapFile.audioFileName" get name.extension only
		/// </summary>
		public string AudioFileFullName;

		public string getAudioFileName{
			get{
				return Path.GetFileName(AudioFileFullName);
			}
		}

		public string getAudioFileNameWithoutExtension{
			get{
				return Path.GetFileNameWithoutExtension(AudioFileFullName);
			}
		}

		/// <summary>
		/// The name of the background file.
		/// full path on editor mode.
		/// use "trackMapFile.audioFileName" get name.extension only
		/// </summary>
		public string BackgroundFileFullName;

		public string getBackgroundFileName{
			get{
				return Path.GetFileName(BackgroundFileFullName);
			}
		}

		public string getBackgroundFileNameWithoutExtension{
			get{
				return Path.GetFileNameWithoutExtension(BackgroundFileFullName);
			}
		}

		/// <summary>
		/// 譜面資訊
		/// </summary>
		public string Memo;

		/// <summary>
		/// 曲速
		/// </summary>
		public float Bpm;

		/// <summary>
		/// 開場空白時間
		/// </summary>
		public float LeadIn;

		public int LeadInGridCount{
			get{
				return Mathf.FloorToInt( this.LeadIn/this.SecondPer32Note);
			}
		}

		public float SecondPer32Note{
			get{
				if(this.Bpm<=0)
					return 0;
				return 7.5f/this.Bpm;
			}
		}

		public static TrackMapHeader Default{
			get{
				return new TrackMapHeader{
					Title = "標題",
					Artist = "作者",
					Difficult = "難易度",
					AudioFileFullName = "歌曲",
					BackgroundFileFullName = "背景圖片",
					Memo = "備忘錄",
					Bpm = 120f,
					LeadIn = 0f,
				};
			}
		}
			
	}
}