using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	/// <summary>
	/// 在目前的位置停止撥放音樂和譜面
	/// </summary>
	public class BlockElement_Button_Pause : BlockElement_Button {
		private static GameMapPlayerModuleManager manager{get{return GameMapPlayerModuleManager.getInstace;}}

		public override void onClick () {
			manager.Pause ();
		}
	}
}