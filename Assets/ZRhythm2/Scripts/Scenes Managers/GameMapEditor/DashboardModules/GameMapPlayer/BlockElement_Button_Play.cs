using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	/// <summary>
	/// 從頭撥放譜面
	/// </summary>
	public class BlockElement_Button_Play : BlockElement_Button {

		private static GameMapPlayerModuleManager manager{get{return GameMapPlayerModuleManager.getInstace;}}

		public override void onClick () {
			manager.Seek(0);
			manager.Play ();
		}

	}
}