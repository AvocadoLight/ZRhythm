using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Tools{
	
	[ExecuteInEditMode]
	public class ZRUIBorder : MonoBehaviour {

		private UIWidget m_widger;

		public int width = 100;

		public int height = 100;

		public bool lockSize = false;

		// Use this for initialization
		void OnEnable () {
			m_widger = GetComponent<UIWidget> ();

			if(m_widger == null)return;

			if(m_widger.isAnchored){				
				m_widger.bottomAnchor.absolute = (int)ConfigUtility.border.y;
				m_widger.topAnchor.absolute = (int)-ConfigUtility.border.y;
				m_widger.leftAnchor.absolute = (int)ConfigUtility.border.x;
				m_widger.rightAnchor.absolute = (int)-ConfigUtility.border.x;
			}

			if(!lockSize){
				width = m_widger.width;
				height = m_widger.height;
			}
		}
		
		// Update is called once per frame
		void Update () {
			if(m_widger.isAnchored){				
				m_widger.bottomAnchor.absolute = (int)ConfigUtility.border.y;
				m_widger.topAnchor.absolute = (int)-ConfigUtility.border.y;
				m_widger.leftAnchor.absolute = (int)ConfigUtility.border.x;
				m_widger.rightAnchor.absolute = (int)-ConfigUtility.border.x;
			}

			if(lockSize){
				m_widger.width = width;
				m_widger.height = height;
			}

		}
	}

}