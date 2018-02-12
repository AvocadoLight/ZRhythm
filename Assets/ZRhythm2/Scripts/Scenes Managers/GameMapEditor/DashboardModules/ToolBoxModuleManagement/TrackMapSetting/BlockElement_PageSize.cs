using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_PageSize : BlockElement_ToolBox_ProgressBar {

		public float _value = 0;

		//0.5~1.0
		public override float Value {
			get {				
				return _value;
			}
			set {
				_value = value;
				progress.value = (_value - 0.5f) * 2f;
			}
		}

		public override void onValueChange () {
			Value = progress.value / 2f + 0.5f;
			display.text = Value.ToString("#0.##") + "x";
			editor.getTrackMap.setting.PageSize_x = Value;
			editor.getTrackMap.setting.PageSize_y = Value;
		}

		public override void onAddValue () {
			if(Value + 0.1f <= 1)
				Value += 0.1f;
		}

		public override void onSubValue () {
			if(Value - 0.1f >= 0.5f)
				Value -= 0.1f;
		}
	}
}