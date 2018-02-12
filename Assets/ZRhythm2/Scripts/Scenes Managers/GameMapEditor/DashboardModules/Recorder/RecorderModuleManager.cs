using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class RecorderModuleManager : BlockModuleManager {

		public static RecorderModuleManager getInstance{private set;get;}

		private UIPanel _panel;

		public UIPanel getPanel{
			get{
				if(_panel == null)
					_panel = GetComponent<UIPanel> ();
				return _panel;
			}
		}

		public BlockElement_Recorder recorder;

		public BlockElement_StopRecord stopRecord;

		void Awake () {
			getInstance = this;
		}

		public void onStopRecord () {
			recorder.gameObject.SetActive(false);
			stopRecord.gameObject.SetActive(false);
		}

		public void onStartRecord () {
			recorder.gameObject.SetActive(true);
			stopRecord.gameObject.SetActive(true);
		}
	}

}