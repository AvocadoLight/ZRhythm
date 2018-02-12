using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class BlockElement_ToolBox_ProgressBar : BlockElement_ToolBox {

		public float defaultValue;

		public UILabel display;
		public UIProgressBar progress;
		public UIButton button_SubValue;
		public UIButton button_AddValue;

		public virtual float Value{
			get{
				return progress.value;
			}
			set{
				progress.value = value;
			}
		}

		public override void Start ()
		{
			base.Start ();
			Value = defaultValue;
		}

		public virtual void onValueChange () {
			display.text = Value.ToString();
		}

		public virtual void onSubValue () {
			Value -= 0.1f;
		}

		public virtual void onAddValue () {
			Value += 0.1f;
		}
	}
}