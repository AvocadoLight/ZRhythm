using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_NoteTrackScroll : BlockElement {

		public NoteTrackModuleManager manager{get{return NoteTrackModuleManager.getInstance;}}

		private UIScrollBar _scroll;

		public UIScrollBar m_ScrollBar{
			get{
				if(_scroll == null)
					_scroll = GetComponent<UIScrollBar>();
				return _scroll;
			}
		}

		void Awake () {
			UIEventListener.Get(m_ScrollBar.gameObject).onPress += onPress;
			UIEventListener.Get(m_ScrollBar.foregroundWidget.gameObject).onPress += onPress;
			EventDelegate.Add(m_ScrollBar.onChange,this.OnChange);
			m_ScrollBar.onDragFinished += OnFinish;
		}

		void LateUpdate () {
			updateView();
		}

		void updateView () {

			//print(Mathf.Clamp01( -(float)manager.gridStart/(float)editor.getTrackMapGridPositionMax));

			//print(manager.gridStart + "/" + editor.getTrackMapGridPositionMax);

			m_ScrollBar.value = 
				Mathf.Clamp01( -(float)manager.gridStart/(float)editor.getTrackMapGridPositionMax);
			
			m_ScrollBar.numberOfSteps = editor.getTrackMapGridPositionMax;
			m_ScrollBar.barSize = 
				Mathf.Clamp01 (editor.getTrackMap.header.SecondPer32Note / editor.getTrackMapGridPositionMax);
			if(m_ScrollBar.barSize<0.025f) m_ScrollBar.barSize = 0.025f;
		}

		void onPress (GameObject go, bool pressed) {			
			enabled = !pressed;

			if(!pressed)return;
			if(m_ScrollBar.onChange.Count<=0){
				EventDelegate.Add(m_ScrollBar.onChange,this.OnChange);
			}
		}

		void OnChange() {
			//manager.m_Panel.clipOffset = new Vector2(-manager.noteRoot.localPosition.x,0);

			manager.gridOffset = 0;
			//manager.noteRoot.transform.localPosition = 
			//	new Vector3(-(manager.gridMax*(m_ScrollBar.value)+manager.gridStart)*manager.gridSize,0);
			manager.noteRoot.transform.localPosition = 
				new Vector3(-(editor.getTrackMapGridPositionMax*(m_ScrollBar.value)+manager.gridStart)*manager.gridSize,0);
			
			//updateView ();
		}

		void OnFinish () {

			m_ScrollBar.onChange.Clear ();

			if(editor.getCurrentNote != null)
				manager.gridOffset = Mathf.FloorToInt(m_ScrollBar.value * editor.getTrackMapGridPositionMax) - editor.getCurrentNote.position;       
			else
				manager.gridOffset = 0;

			//manager.m_Panel.clipOffset = Vector2.zero;
			manager.noteRoot.localPosition = Vector3.zero;
		}
	}
}