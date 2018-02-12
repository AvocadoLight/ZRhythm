using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Game{
	public class BeatBar : MonoBehaviour {

		private GameManager manager{get{return GameManager.getInstance;}}

		private Transform m_Transform;

		public Transform cachedTransform{
			get{
				if(m_Transform == null)
					m_Transform = transform;
				return m_Transform;
			}
		}

		public int page = -1;
		
		// Update is called once per frame
		void Update () {

			float posY = 0;
			//if(manager.isPrepare && manager.audioPlayer.isPlaying){
			if(manager.isPrepare){
				posY = AudioPlayerManager.getInstance.getProgress.totalSeconds / manager.currentTrackMap.header.SecondPer32Note;
			}else{
				posY = (-manager.countDown.progressValue) / manager.currentTrackMap.header.SecondPer32Note;
			}

			//print(AudioPlayerManager.getInstance.getProgress.totalSeconds + "/"+  manager.currentTrackMap.header.SecondPer32Note + "=>" + posY);

			cachedTransform.localPosition = 
				new Vector3(0,
					manager.currentTrackMap.GetPositionY(posY)
				);


			page = (int)manager.currentTrackMap.GetPage(
				AudioPlayerManager.getInstance.getProgress.totalSeconds / manager.currentTrackMap.header.SecondPer32Note
			);


		}
	}
}