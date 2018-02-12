using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm{

	public class Note_Swipe : Note_Base {

		public UITexture note;

		public UITexture arrow;

		public float angle = 0;

		public override int childCount {
			get {
				return 2;
			}
		}

		public override void SetDepth (int depth) {
			arrow.depth = depth;
			note.depth = depth + 1;

		}

		public override void update (float timeProgress,float SecondPer32Note) {

		}

		#if UNITY_EDITOR

		protected void OnValidate (){
			if(!Application.isPlaying){
				if(type != NoteType.Swipe){
					type = NoteType.Swipe;
				}
			}
		}

		#endif

		protected void OnEnable () {
			if(type != NoteType.Swipe){
				type = NoteType.Swipe;
			}
		}

	}

}