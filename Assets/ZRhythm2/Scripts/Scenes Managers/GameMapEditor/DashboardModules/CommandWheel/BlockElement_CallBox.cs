using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	[ExecuteInEditMode]
	public class BlockElement_CallBox : BlockElement {

		public CommandWheelModuleManager manager{get{return CommandWheelModuleManager.getInstance;}}

		public int index;

		public UILabel label;

		private UIWidget _widget;

		public UIWidget widget{get{
				if(_widget == null)	{
					_widget = GetComponent<UIWidget>();			
				}
				return _widget;
			}
		}

		public string display = "Display";

		public BlockElement_ToolBox call;

		// Use this for initialization
		void Start () {			
			if(Application.isPlaying)
				EventDelegate.Add(label.GetComponent<UIButton>().onClick,onCall);
		}

		void Update () {

			if(!manager.apply){
				return;
			}
				
			if(label == null){
				label = GetComponentInChildren<UILabel>();
			}

			label.fontSize = manager.fontSize;

			var angle = 360 / manager.number * index + manager.offset;
			var axis_angle = 360 / manager.number * (index + 0.5f) + manager.offset;

			cachedTransform.localPosition = Vector3.zero;
			cachedTransform.localEulerAngles = Vector3.zero;

			label.cachedTransform.localPosition = Vector3.zero;
			label.cachedTransform.localEulerAngles = Vector3.zero;

			label.cachedTransform.localEulerAngles = Vector3.forward * axis_angle;
			label.cachedTransform.localPosition += label.cachedTransform.up * (manager.arm + (float)widget.height/2f);

			var axis_position = label.cachedTransform.position;

			cachedTransform.localEulerAngles = Vector3.forward * angle;
			cachedTransform.localPosition += cachedTransform.up * manager.arm;

			label.cachedTransform.position = axis_position;

			label.text = display;
			label.cachedTransform.rotation = Quaternion.identity;
		}

		void onCall () {
			call.OpenDialog ();
			ToolBoxModuleManager.BringToFront(call);
			manager.SetDisable ();

		}	
	}
}