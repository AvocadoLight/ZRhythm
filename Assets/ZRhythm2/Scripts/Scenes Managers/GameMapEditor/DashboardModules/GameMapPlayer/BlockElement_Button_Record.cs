using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	/// <summary>
	/// 在播放同時記錄點擊的位置到譜面中
	/// </summary>
	public class BlockElement_Button_Record : BlockElement_Button {
		private static GameMapPlayerModuleManager manager{get{return GameMapPlayerModuleManager.getInstace;}}

		//TODO:完成功能
		public override void onClick () {
			manager.Play ();
			editor.record.onStartRecord ();
		}
	}
}