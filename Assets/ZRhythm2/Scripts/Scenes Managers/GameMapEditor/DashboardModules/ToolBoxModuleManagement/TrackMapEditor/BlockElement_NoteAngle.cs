using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_NoteAngle : BlockElement_ToolBox_ProgressBar {

		public float _value = 0;

		public override float Value {
			get {				
				return _value;
			}
			set {
				_value = value;
				progress.value = _value/360f;
			}
		}

		public override void onValueChange () {
			Value = progress.value * 360;
			display.text = Value.ToString("##0.##");
			if(editor.getCurrentNote != null){
				editor.getCurrentNote.angle = Value;
			}
		}

		public override void onAddValue () {
			if(Value + 1 <= 360){
				Value += 1;
			}else{
				Value = 360;
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