using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm{

	public class Note_Tap : Note_Base {

		public UITexture note;

		public override void SetDepth (int depth)
		{
			note.depth = depth;
		}

		public override void update (float timeProgress,float SecondPer32Note) {
			
		}

		#if UNITY_EDITOR

		protected void OnValidate (){
			if(!Application.isPlaying){
				if(type != NoteType.Tap){
					type = NoteType.Tap;
				}
			}
		}

		#endif

		protected void OnEnable () {
			if(type != NoteType.Tap){
				type = NoteType.Tap;
			}
		}
	}

}