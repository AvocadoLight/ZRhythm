using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class BlockElement_CloseToolBox : BlockElement {

		public ToolBoxModuleManager manager{get{return ToolBoxModuleManager.getInstance;}}

		private UIPanel _panel;

		private UIPanel m_Panel{
			get{
				if(_panel == null)
					_panel = GetComponent<UIPanel> ();
				return _panel;
			}
		}

		private UIRoot root{get{return m_Panel.root;}}

		public UIWidget icon;

		public float sensitive = 100;

		private bool isDisplay = false;

		public TweenAlpha tweenAlpha;

		public TweenScale tweenScale;

		private BlockElement_ToolBox last;

		// Use this for initialization
		void Start () {
			m_Panel.sortingOrder = manager.sortingOrder + 1;
			m_Panel.depth = manager.toolBoxes.Count * manager.step;
		}

		// Update is called once per frame
		void Update () {		

			if(!isDisplay && Input.GetMouseButtonUp(0) ){				
				if(last != null)
					last.CloseDialog();
			}

			if(manager.getActiveToolBox == null){
				if(!isDisplay){
					isDisplay = true;
					tweenAlpha.PlayReverse();
					tweenScale.PlayReverse();
				}		
				return;
			}				

			var mouse = UICamera.lastEventPosition;
			var point = m_Panel.cachedTransform.InverseTransformPoint(m_Panel.anchorCamera.ScreenToWorldPoint(mouse));
			var position = m_Panel.cachedTransform.InverseTransformPoint(icon.cachedTransform.position);
			//var distance = Vector2.Distance(icon.cachedTransform.localPosition,point);
			var distance = Vector2.Distance(position,point);

			if(distance < sensitive){
				if(isDisplay){
					isDisplay = false;
					tweenAlpha.PlayForward();
					tweenScale.PlayForward();
				}
			}else{
				if(!isDisplay){
					isDisplay = true;
					tweenAlpha.PlayReverse();
					tweenScale.PlayReverse();
				}		
			}

			last = manager.getActiveToolBox;
		}

	}
}