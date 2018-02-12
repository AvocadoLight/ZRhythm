using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_NoteLength : BlockElement_ToolBox_ProgressBar {

		//0~32
		public override float Value {
			get {
				return Mathf.FloorToInt(progress.value*32);
			}
			set {
				progress.value = value/32f;
			}
		}

		public override void onValueChange () {
			base.onValueChange ();
			if(editor.getCurrentNote != null)
				editor.getCurrentNote.lenght = (int)Value;
		}

		public override void onAddValue () {
			if(Value + 1 <= 32){
				Value += 1;
			}else{
				Value = 32;
			}
		}

		public override void onSubValue () {
			if(Value - 1 >= 0){
				Value -= 1;
			}else{
				Value = 0;
			}
		}
	}
}