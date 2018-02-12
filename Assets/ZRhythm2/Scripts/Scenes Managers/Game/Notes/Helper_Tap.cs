using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm.Game{

	public class Helper_Tap : GameNoteHelper<Note_Tap> {
		void OnPress(bool pressed){
			if(pressed){
				if(timeProgress <= time + manager.config.range_Excellent){						
					onExcellent ();
				}else if(timeProgress <= time + manager.config.range_Good){						
					onGood ();
				}else{						
					onBad ();
				}
			}
		}
	}

}