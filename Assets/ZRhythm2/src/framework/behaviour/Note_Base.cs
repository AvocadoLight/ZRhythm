using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm{

	public class Note_Base : MonoBehaviour {

		public int noteId;

		private Transform m_Transform;

		public Transform getTransform{
			get{
				if(m_Transform == null)
					m_Transform = transform;
				return m_Transform;
			}
		}

		public NoteType type = NoteType.Null;

		public virtual int childCount{get{return 1;}}

		public virtual void update (float timeProgress,float SecondPer32Note) {
			
		}

		public virtual void SetDepth (int depth) {
			
		}
	
	}

}