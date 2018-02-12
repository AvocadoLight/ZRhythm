using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_PageScale : BlockElement_ToolBox_ProgressBar {

		public float _value = 0;

		//0.1~2.0
		public override float Value {
			get {				
				return _value;
			}
			set {
				_value = value;
				progress.value = (_value - 0.1f)/1.9f;
			}
		}

		public override void onValueChange () {
			Value = progress.value*1.9f + 0.1f;
			display.text = Value.ToString("#0.##") + "x";
			editor.getTrackMap.setting.PageScale = Value;
		}

		public override void onAddValue () {
			if(Value + 0.1f <= 2)
				Value += 0.1f;
		}

		public override void onSubValue () {
			if(Value - 0.1f >= 0.1f)
				Value -= 0.1f;
		}
	}

}