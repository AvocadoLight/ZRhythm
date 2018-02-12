using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class GameMapScreenNoteHelper : MonoBehaviour {

		public GameMapScreenModuleManager manager{get{return GameMapScreenModuleManager.getInstance;}}

		private Note_Base m_note;

		public Note_Base note{get{
				if(m_note == null)
					m_note = GetComponentInChildren<Note_Base> ();
				return m_note;
			}
		}

		private Transform m_Transform;

		public Transform getTransform {
			get{
				if(m_Transform == null)
					m_Transform = transform;
				return m_Transform;
			}
		}

		private UIWidget m_widget;

		public UIWidget getWidget{
			get{
				if(m_widget==null)
					m_widget = GetComponentInChildren<UIWidget>();
				return m_widget;
			}
		}

		public int page = -1;

		public void SetAlpha (float alpha) {
			getWidget.alpha = alpha;
		}

		void OnDoubleClick () {

			if(editor.audioPlayer.isPlaying)return;

			int uid = note.noteId;

			int index = editor.getTrackMap.GetIndex(uid);

			editor.getCurrentNoteIndex = index;

			editor.noteTrack.gridOffset = 0;

			editor.noteTrack.noteRoot.transform.localPosition = Vector3.zero;

		}

		void OnDragEnd () {

			//if(editor.audioPlayer.isPlaying)return;

			int uid = note.noteId;

			var _note = editor.getTrackMap.GetNote(uid);

			var screenwidth = editor.getTrackMap.getScreenSize().x;

			_note.Xoffset = getTransform.localPosition.x / screenwidth;

			if(_note.Xoffset > 5f){
				_note.Xoffset = 5f;
			}else if (_note.Xoffset <-5f){
				_note.Xoffset = -5f;
			}

			//end
			manager.enabled = true;

		}

		void OnDrag (Vector2 delta) {

			if(editor.audioPlayer.isPlaying)return;

			int uid = note.noteId;

			int index = editor.getTrackMap.GetIndex(uid);

			getTransform.localPosition += new Vector3(delta.x, 0);

			var screenwidth = editor.getTrackMap.getScreenSize().x;

			var position = getTransform.localPosition;

			if(getTransform.localPosition.x > screenwidth/2f){
				position.x = screenwidth/2f;
			}else if (getTransform.localPosition.x <- screenwidth/2f){
				position.x =- screenwidth/2f;
			}

			getTransform.localPosition = position;

			if(index == editor.getCurrentNoteIndex)
				editor.trackMapEditor.noteOffset.Value = getTransform.localPosition.x / screenwidth;

			//drag
			manager.enabled = false;

		}

	}

}