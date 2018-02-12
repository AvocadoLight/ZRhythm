using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_NoteTrackGridScale : MonoBehaviour {

		private UISlider slider;

		void Start () {
			slider = GetComponentInChildren<UISlider>();
			EventDelegate.Add(slider.onChange,onChange);
			slider.onDragFinished += onDragFinished;
			slider.value = 
				(editor.noteTrack.gridSize - 10)/50f;
		}

		void onChange () {
			editor.noteTrack.gridSize = (slider.value * 50f) + 10;
		}

		// Update is called once per frame
		void onDragFinished () {
			editor.noteTrack.gridSize = (slider.value * 50f) + 10;
		}
	}
}