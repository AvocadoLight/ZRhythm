using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm.Game{
	public class GameHUDManager : MonoBehaviour	{

		public GameManager manager{get{return GameManager.getInstance;}}

		public UITexture backgroundImage;

		public UILabel text_Score;
		public UILabel text_Title;
		public UILabel text_Artist;
		public UILabel text_AudioProgress;
		public UIProgressBar progress_AudioProgress;

		// Use this for initialization
		void Start () {
			text_Title.text = string.Empty;
			text_Artist.text = string.Empty;
			text_Score.text = "Score:0";
			text_AudioProgress.text = "0:00/0:00";
			progress_AudioProgress.value = 0;
		}
		
		// Update is called once per frame
		void Update () {	

			if(!manager.isPrepare)
				return;
			
			text_Title.text = manager.currentTrackMap.header.Title;
			text_Artist.text = manager.currentTrackMap.header.Artist;
			text_Score.text = string.Format("Score:{0}",manager.scorer.total_Score.ToString ());
			text_AudioProgress.text = 
				string.Format(
					"{0}/{1}",
					AudioPlayerManager.getInstance.getProgress.ToString(),
					AudioPlayerManager.getInstance.getClipLength.ToString()
				);
			progress_AudioProgress.value = 
				AudioPlayerManager.getInstance.getProgress.totalSeconds/
				AudioPlayerManager.getInstance.getClipLength.totalSeconds;
		}
	}

}