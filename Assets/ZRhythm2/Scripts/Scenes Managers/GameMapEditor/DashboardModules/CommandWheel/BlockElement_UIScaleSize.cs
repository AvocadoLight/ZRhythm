using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class BlockElement_UIScaleSize : BlockElement {

		private UISlider slider;

		// Use this for initialization
		void Start () {
			slider = GetComponentInChildren<UISlider>();
			EventDelegate.Add(slider.onChange,onChange);
			slider.onDragFinished += onDragFinished;
			slider.value = ConfigUtility.toolBoxSize -1;
		}

		void onChange () {
			ConfigUtility.toolBoxSize = 1 + slider.value;
		}

		// Update is called once per frame
		void onDragFinished () {
			ConfigUtility.toolBoxSize = 1 + slider.value;
		}
	}
}