using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BurningxEmpires.ZRhythm.Game{
	public class GameNoteHelper<TNote_Base> : GameNoteHelper where TNote_Base:Note_Base {

		public TNote_Base getNote{
			get{
				return note as TNote_Base;
			}
		}

	}
}