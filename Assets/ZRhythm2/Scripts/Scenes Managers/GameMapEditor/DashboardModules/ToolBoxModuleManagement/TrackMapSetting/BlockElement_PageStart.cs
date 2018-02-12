using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_PageStart : BlockElement_ToolBox_ProgressBar {
		public float _value = 0;

		//-10~10 -> 0~20
		public override float Value {
			get {				
				return _value;
			}
			set {
				_value = Mathf.RoundToInt(value);
				progress.value = _value/20f+0.5f;
			}
		}

		public override void onValueChange () {
			Value = (progress.value - 0.5f)*20;
			display.text = Value.ToString("#0.##");

			editor.getTrackMap.setting.PageStart = (Value + 10) / 10;
		}

		public override void onAddValue () {
			if(Value + 1 <= 10)
				Value += 1;
		}

		public override void onSubValue () {
			if(Value - 1 >= -10)
				Value -= 1;
		}
	}
}