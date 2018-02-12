using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class DebugLogModuleManager : BlockModuleManager {

		public static DebugLogModuleManager getInstance{private set;get;}

		public int lineMax = 50;

		public List<string> logData = new List<string> ();

		public UITextList logContent;

		private readonly string colorLight = "[ffffff]";

		private readonly string colorDark = "[a1a198]";

		private readonly string endToken = "[-]";

		private readonly string normalToken = "● ";

		private readonly string warningToken = "▲ ";

		private bool light = false;

		private UIPanel m_Panel;

		public UIPanel getPanel{
			get{
				if(m_Panel==null)
					m_Panel = GetComponent<UIPanel>();
				return m_Panel;
			}
		}

		void Awake () {
			getInstance = this;
		}	

		void Update () {
			//getPanel.GetViewSize() = new Vector2(getPanel.root.manualWidth ,getPanel.root.manualHeight);
			getPanel.baseClipRegion = new Vector4 (
				m_Panel.baseClipRegion.x,
				m_Panel.baseClipRegion.y,
				getPanel.root.manualWidth,
				getPanel.root.manualHeight
			);
		}

		public void Log (object message){
			logData.Add(message.ToString());
			logContent.Add(combineColor(normalToken) + message.ToString());
			Debug.Log (message);
			if ( logData.Count >= lineMax)
				logData.RemoveAt(0);
		}

		public void LogWarning (object message){
			logData.Add(message.ToString());
			logContent.Add(combineColor(warningToken) + message.ToString());
			Debug.Log ("Warning:" + message);
			if ( logData.Count >= lineMax)
				logData.RemoveAt(0);
		}

		public void Test_Add_A_Log () {
			Log("This is a log" + logData.Count);
		}

		private string combineColor (string text) {
			
			light = !light;

			if(light){
				return colorLight + text + endToken;	
			}

			return colorDark + text + endToken;	

		}

	}

}