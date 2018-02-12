using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm.Game{
	
	public class Helper_Swipe : GameNoteHelper<Note_Swipe>{

		public bool isPressed = false;

		enum PressState{
			none,
			excellent,
			good,
			bad
		}

		int passLength = 0;

		PressState pressState = PressState.none;

		public Vector2 dragDirection;

		public override void onUpdate () {

			note.update(timeProgress,manager.currentTrackMap.header.SecondPer32Note);

			if(isPressed && passLength < cachedNote.lenght){
				var per32 = manager.currentTrackMap.header.SecondPer32Note;
				if(timeProgress > (cachedNote.position + passLength) * per32)
					passLength++;				
			}else if(passLength >= cachedNote.lenght){
				onMiss ();
			}else if(!isPressed && timeProgress >= time + manager.config.range_Bad){						
				onMiss ();
			}

			/*getNote.length = cachedNote.lenght;
			getNote.passLength = passLength;*/

		}

		void OnDrag (Vector2 delta) {
			dragDirection += delta;
		}

		/*void OnDragEnd () {
			
		}*/

		void OnPress(bool pressed){

			isPressed = pressed;

			if(pressed){
				dragDirection = Vector2.zero;
				if(timeProgress <= time + manager.config.range_Excellent){											
					pressState = PressState.excellent;
				}else if(timeProgress <= time + manager.config.range_Good){
					pressState = PressState.good;
				}else{
					pressState = PressState.bad;
				}
			}else{
				//drag end
				//var dragAngle = Vector2.Angle(Vector2.right,dragDirection);
				var dot = Vector2.Dot(dragDirection,getTransform.right);
				if(dot>0.75f){
					if(	pressState == PressState.excellent){
						onExcellent();					
					}else if(pressState == PressState.good){
						onGood();
					}else{
						onBad();
					}
				}else{
					onBad ();
				}
			}
		}

	}

}