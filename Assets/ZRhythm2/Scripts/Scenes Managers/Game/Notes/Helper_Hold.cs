using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm.Game{
	public class Helper_Hold : GameNoteHelper<Note_Hold>{

		public bool isPressed = false;

		enum PressState{
			none,
			excellent,
			good,
			bad
		}

		int passLength = 0;

		PressState pressState = PressState.none;

		public override void onUpdate () {
			
			note.update(timeProgress,manager.currentTrackMap.header.SecondPer32Note);

			if(isPressed && passLength < cachedNote.lenght){
				var per32 = manager.currentTrackMap.header.SecondPer32Note;

				if(timeProgress > (cachedNote.position + passLength) * per32){
					passLength++;

					if(	pressState == PressState.excellent){
						onExcellent();					
					}else if(pressState == PressState.good){
						onGood();
					}else{
						onBad();
					}
				}
			}else if(passLength >= cachedNote.lenght){
				Destroy(this.gameObject);
				onDestroy ();
			}else if(!isPressed && timeProgress >= time + manager.config.range_Bad){						
				onMiss ();
			}

			getNote.length = cachedNote.lenght;
			getNote.passLength = passLength;

		}


		void OnPress(bool pressed){

			isPressed = pressed;

			if(pressed){
				if(timeProgress <= time + manager.config.range_Excellent){						
					onExcellent ();
					pressState = PressState.excellent;
				}else if(timeProgress <= time + manager.config.range_Good){						
					onGood ();
					pressState = PressState.good;
				}else{						
					onBad ();
					pressState = PressState.bad;
				}
			}
		}

		public override void onExcellent () {
			manager.scorer.AddExcellent(1);
			manager.effect.NoteExcellent(getTransform.localPosition);
		}

		public override void onGood () {
			manager.scorer.AddGood(1);
			manager.effect.NoteGood(getTransform.localPosition);
		}

		public override void onBad () {
			manager.scorer.AddBad(1);
			manager.effect.NoteBad(getTransform.localPosition);
		}

		/*public override void onMiss () {			
			manager.scorer.AddMiss(1);
			manager.effect.NoteMiss(getTransform.localPosition);
			Destroy(this.gameObject);
			onDestroy ();
		}*/

	}

}