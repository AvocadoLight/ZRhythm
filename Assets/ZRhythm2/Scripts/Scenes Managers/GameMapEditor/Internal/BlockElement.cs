using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class BlockElement : MonoBehaviour {
		private Transform m_Transform;

		public Transform cachedTransform{
			get{
				if(m_Transform == null)
					m_Transform = transform;
				return m_Transform;
			}
		}
	}
}