using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm{

	public class Note_Hold : Note_Base {

		[HideInInspector]
		public float passLength = 0;

		[HideInInspector]
		public float length = 0;

		public UITexture progress;

		public UITexture note;

		public override int childCount {
			get {
				return 2;
			}
		}

		public override void SetDepth (int depth) {
			note.depth = depth;
			progress.depth = depth + 1;
		}

		public override void update (float timeProgress,float SecondPer32Note) {
			if(passLength == 0){
				progress.transform.localScale = Vector3.one;
			}else{
				progress.transform.localScale = Vector3.one +
					Vector3.one * (passLength/length);				
			}
		}

		#if UNITY_EDITOR

		protected void OnValidate (){
			if(!Application.isPlaying){
				if(type != NoteType.Hold){
					type = NoteType.Hold;
				}
			}
		}

		#endif

		protected void OnEnable () {
			if(type != NoteType.Hold){
				type = NoteType.Hold;
			}
		}
	}

}