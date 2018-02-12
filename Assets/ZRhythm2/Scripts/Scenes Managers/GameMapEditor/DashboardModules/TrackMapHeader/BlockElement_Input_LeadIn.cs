using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_Input_LeadIn : BlockElement_Input{

		void Start () {
			input.value = GameMapEditorManager.getTrackMap.header.LeadIn.ToString();
		}

		public override void onContentChange () {
			if(editor.audioPlayer.isPlaying){
				input.value = GameMapEditorManager.getTrackMap.header.LeadIn.ToString();
				return;
			}

			var origin_leadIn = GameMapEditorManager.getTrackMap.header.LeadIn;
			var origin_leadInGridCount = GameMapEditorManager.getTrackMap.header.LeadInGridCount;

			var value = input.value;
			var leadin = GameMapEditorManager.getTrackMap.header.LeadIn;
			if(float.TryParse(value,out leadin)){
				GameMapEditorManager.getTrackMap.header.LeadIn = leadin;
			}else{
				input.value = GameMapEditorManager.getTrackMap.header.LeadIn.ToString();
				editor.debugLog.LogWarning(ExceptionList.shouldBeNumber);
			}

			var leadIn = GameMapEditorManager.getTrackMap.header.LeadIn;
			var leadInGridCount = GameMapEditorManager.getTrackMap.header.LeadInGridCount;

			var sub = origin_leadInGridCount - leadInGridCount;

			foreach(var note in editor.getTrackMap.Notes){
				note.position -= sub;
			}

		}

	}
}