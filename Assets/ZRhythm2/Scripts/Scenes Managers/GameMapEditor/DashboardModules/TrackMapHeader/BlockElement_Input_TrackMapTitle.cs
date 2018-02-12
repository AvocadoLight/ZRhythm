using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	public class BlockElement_Input_TrackMapTitle : BlockElement_Input{
	
		public override void onContentChange () {
			GameMapEditorManager.getTrackMap.header.Title = input.value;			
		}

	}

}