using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_BeatBar : BlockElement {

		private static GameMapScreenModuleManager manager{get{return GameMapScreenModuleManager.getInstance;}}

		public int page = -1;

		// Update is called once per frame
		void Update () {			

			if(editor.audioPlayer.isPlaying){
				cachedTransform.localPosition = 
					new Vector3(0,
						editor.getTrackMap.GetPositionY(
							editor.audioPlayer.getProgress.totalSeconds / editor.getTrackMap.header.SecondPer32Note
						));
				page = (int)editor.getTrackMap.GetPage(
					editor.audioPlayer.getProgress.totalSeconds / editor.getTrackMap.header.SecondPer32Note
				);
			}else{
				cachedTransform.localPosition = 
					new Vector3(0,editor.getTrackMap.GetPositionY(editor.getCurrentNoteGridPosition));
				page = 	(int)editor.getTrackMap.GetPage(editor.getCurrentNoteGridPosition);
			}

		}

	}
}