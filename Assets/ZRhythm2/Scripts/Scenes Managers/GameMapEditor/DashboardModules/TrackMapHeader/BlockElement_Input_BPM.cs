using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_Input_BPM : BlockElement_Input{

		void Start () {
			input.value = GameMapEditorManager.getTrackMap.header.Bpm.ToString();
		}

		public override void onContentChange () {

			if(editor.audioPlayer.isPlaying){
				input.value = GameMapEditorManager.getTrackMap.header.Bpm.ToString();
				return;
			}

			var value = input.value;
			var bpm = GameMapEditorManager.getTrackMap.header.Bpm;
			if(float.TryParse(value,out bpm)){
				GameMapEditorManager.getTrackMap.header.Bpm = bpm;
			}else{
				input.value = GameMapEditorManager.getTrackMap.header.Bpm.ToString();
				editor.debugLog.LogWarning(ExceptionList.shouldBeNumber);
			}
		}

	}
}