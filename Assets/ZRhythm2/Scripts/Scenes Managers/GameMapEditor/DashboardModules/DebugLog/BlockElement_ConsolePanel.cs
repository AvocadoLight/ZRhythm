using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	
	[ExecuteInEditMode]
	public class BlockElement_ConsolePanel : BlockElement {

		private UIWidget m_widget;

		public bool small = false;

		public int smallSize = 25;

		void Start () {
			m_widget = GetComponent<UIWidget> ();
			update();
		}

		void Update () {
			if(!Application.isPlaying){
				update();
			}
		}

		void update () {
			if(small){
				m_widget.topAnchor.target = null;
				m_widget.height = smallSize;
			}else{
				m_widget.topAnchor.target = m_widget.bottomAnchor.target;
				m_widget.topAnchor.absolute = 0;
			}
		}

		void OnClick () {
			small = !small;
			update();
		}

	}
}