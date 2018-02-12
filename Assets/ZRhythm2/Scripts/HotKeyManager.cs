using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	
	using editor = GameMapEditorManager;

	public class HotKeyManager : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			if(Input.GetKeyDown(KeyCode.KeypadEnter)){
				editor.trackMapEditor.noteEditor.AddNote();			
			}
			if(Input.GetKeyDown(KeyCode.Insert)){
				editor.trackMapEditor.noteEditor.onInsertNote();
			}
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				editor.trackMapEditor.noteOffset.onSubValue();
			}
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				editor.trackMapEditor.noteOffset.onAddValue();
			}
			if(Input.GetKeyDown(KeyCode.UpArrow)){
				editor.trackMapEditor.noteSelector.onPreviousNote();
			}
			if(Input.GetKeyDown(KeyCode.DownArrow)){
				editor.trackMapEditor.noteSelector.onNextNote();
			}
			if(Input.GetKeyDown(KeyCode.Keypad6)){
				editor.brush.onNextBrush();
			}
			if(Input.GetKeyDown(KeyCode.Keypad4)){
				editor.brush.onPreviousBrush();
			}


		}
	}
}