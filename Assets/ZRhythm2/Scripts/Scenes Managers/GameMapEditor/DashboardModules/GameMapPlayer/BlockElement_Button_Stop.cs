using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	/// <summary>
	/// 停止撥放譜面並回到播放前的位置
	/// </summary>
	public class BlockElement_Button_Stop : BlockElement_Button {
		private static GameMapPlayerModuleManager manager{get{return GameMapPlayerModuleManager.getInstace;}}

		public override void onClick () {
			manager.Stop ();
		}
	}
}