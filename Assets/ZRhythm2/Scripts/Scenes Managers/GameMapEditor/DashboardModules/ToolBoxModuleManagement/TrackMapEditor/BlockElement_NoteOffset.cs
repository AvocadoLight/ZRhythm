using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_NoteOffset : BlockElement_ToolBox_ProgressBar {

		public float _value = 0;

		//-10~10,-1~1
		public override float Value {
			get {				
				return _value;
			}
			set {
				_value = value;
				progress.value = _value+0.5f;
			}
		}

		public override void onValueChange () {
			Value = (progress.value - 0.5f);
			display.text = (Value*2).ToString("#0.##");
			if(editor.getCurrentNote != null){

				//print("+onValueChange" + editor.getCurrentNote.Xoffset);
				//print(Value);
				editor.getCurrentNote.Xoffset = Value;
				//print(Value);
				//print("-onValueChange"+editor.getCurrentNote.Xoffset);
			}
		}

		public override void onAddValue () {
			if(Value + 0.05f <= 0.5f){
				Value += 0.05f;
			}else{
				Value = 0.5f;
			}
		}

		public override void onSubValue () {
			if(Value - 0.05f >= -0.5f){
				Value -= 0.05f;
			}else{
				Value = -0.5f;
			}
		}

	}
}