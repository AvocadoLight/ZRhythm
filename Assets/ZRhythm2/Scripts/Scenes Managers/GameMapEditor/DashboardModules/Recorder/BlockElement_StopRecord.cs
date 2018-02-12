using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_StopRecord : BlockElement {

		private RecorderModuleManager manager{get{return RecorderModuleManager.getInstance;}}

		void OnClick () {
			manager.onStopRecord ();
			editor.gameMapPlayer.Stop ();

		}
	}

}