using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class NoteTrackTween : MonoBehaviour {

		public NoteTrackModuleManager manager{get{return NoteTrackModuleManager.getInstance;}}

		public UIPanel m_Panel{
			get{				
				return manager.m_Panel;
			}
		}

		public float width {get{return m_Panel.baseClipRegion.z;}}

		public float heigth {get{return m_Panel.baseClipRegion.w;}}

		private BoxCollider _collider;

		public BoxCollider m_BoxCollider{
			get{
				if(_collider == null)
					_collider = GetComponent<BoxCollider> ();
				return _collider;
			}
		}

		public UIWidget foldedWidget;
		public UIWidget regularWidget;
		public UIWidget unfoldedWidget;

		private Vector2 dragTotal;

		private Vector2 dragBox = new Vector2(25,25);

		private UITweener currentSizeTweener;
		private UITweener currentPositionTweener;

		void Start () {
			m_Panel.baseClipRegion = new Vector4(m_Panel.baseClipRegion.x,m_Panel.baseClipRegion.y,foldedWidget.width,foldedWidget.height);
			m_Panel.cachedTransform.position = foldedWidget.cachedTransform.position;
			manager.state = NoteTrackModuleManager.panelState.folded;
		}
		
		// Update is called once per frame
		void Update () {
			m_BoxCollider.size = new Vector3(m_Panel.baseClipRegion.z,m_Panel.baseClipRegion.w);
			m_BoxCollider.center = new Vector3(m_Panel.baseClipRegion.x,m_Panel.baseClipRegion.y);
		}	

		void OnDragStart () {
			dragTotal = Vector2.zero;
			if(isTween())
				return;
			CancelTween();
		}

		void OnDragEnd () {
			if(isTween())
				return;
			if(Mathf.Abs( dragTotal.x) > Mathf.Abs( dragTotal.y)){
			//橫向	
				if(manager.state == NoteTrackModuleManager.panelState.folded && dragTotal.x > 0){
					//向右展開
					currentSizeTweener = TweenPanelWidth.Begin(m_Panel,ConfigUtility.reactionSensitive,regularWidget.width);
					currentPositionTweener = TweenPosition.Begin(m_Panel.gameObject,ConfigUtility.reactionSensitive,regularWidget.cachedTransform.position,true);
					currentSizeTweener.AddOnFinished(GoState_Regular);
				}else if (manager.state == NoteTrackModuleManager.panelState.regular && dragTotal.x < 0){
					//向左縮起
					currentSizeTweener = TweenPanelWidth.Begin(m_Panel,ConfigUtility.reactionSensitive,foldedWidget.width);
					currentPositionTweener = TweenPosition.Begin(m_Panel.gameObject,ConfigUtility.reactionSensitive,foldedWidget.cachedTransform.position,true);
					currentSizeTweener.AddOnFinished(GoState_Folded);
				}
			}else{
			//直向
				if(manager.state == NoteTrackModuleManager.panelState.regular && dragTotal.y > 0){
					//向上展開
					currentSizeTweener = TweenPanelHeight.Begin(m_Panel,ConfigUtility.reactionSensitive,unfoldedWidget.height);
					currentPositionTweener = TweenPosition.Begin(m_Panel.gameObject,ConfigUtility.reactionSensitive,unfoldedWidget.cachedTransform.position,true);
					currentSizeTweener.AddOnFinished(GoState_Unfolded);
				}else if (manager.state == NoteTrackModuleManager.panelState.unfolded && dragTotal.y < 0){
					//向下縮起
					currentSizeTweener = TweenPanelHeight.Begin(m_Panel,ConfigUtility.reactionSensitive,regularWidget.height);
					currentPositionTweener = TweenPosition.Begin(m_Panel.gameObject,ConfigUtility.reactionSensitive,regularWidget.cachedTransform.position,true);
					currentSizeTweener.AddOnFinished(GoState_Regular);
				}
			}
					
		}

		void OnDrag (Vector2 delta) {
			if(isTween())
				return;
			dragTotal += delta;
		}

		bool isTween () {
			bool sizeTween = currentSizeTweener != null ? currentSizeTweener.enabled : false;
			bool positionTween = currentPositionTweener != null ? currentPositionTweener.enabled : false;
			return sizeTween || positionTween;
		}

		void CancelTween () {
			if(currentSizeTweener != null)
				Destroy(currentSizeTweener);
			if(currentPositionTweener != null)
				Destroy(currentPositionTweener);
		}

		void GoState_Folded () {
			manager.state = NoteTrackModuleManager.panelState.folded;
		}

		void GoState_Regular () {
			manager.state = NoteTrackModuleManager.panelState.regular;
		}

		void GoState_Unfolded () {
			manager.state = NoteTrackModuleManager.panelState.unfolded;
		}



	}
}