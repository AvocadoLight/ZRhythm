using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class TrackMapEditorModuleManager : BlockModuleManager {

		public static TrackMapEditorModuleManager getInstance{private set;get;}

		public BlockElement_NoteLength noteLength;
		public BlockElement_NoteOffset noteOffset;
		public BlockElement_NoteAngle noteAngle;
		public BlockElement_NoteEditor noteEditor;
		public BlockElement_NoteSelector noteSelector;

		void Awake () {
			getInstance = this;
		}

	}
}