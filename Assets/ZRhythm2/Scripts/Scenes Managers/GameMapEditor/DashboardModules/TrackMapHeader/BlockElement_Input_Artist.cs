using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class BlockElement_Input_Artist : BlockElement_Input{

		public override void onContentChange () {
			GameMapEditorManager.getTrackMap.header.Artist = input.value;			
		}

	}
}