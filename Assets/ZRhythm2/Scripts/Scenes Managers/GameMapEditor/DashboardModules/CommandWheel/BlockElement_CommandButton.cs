using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class BlockElement_CommandButton : BlockElement {

		public CommandWheelModuleManager manager{get{return CommandWheelModuleManager.getInstance;}}

		public void onClick () {
			manager.background.SetActive(true);	
		}

	}
}