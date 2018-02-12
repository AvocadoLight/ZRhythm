using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class NoteTrackNoteHelper : MonoBehaviour
	{

		public NoteTrackModuleManager manager{get{return NoteTrackModuleManager.getInstance;}}

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

		private BoxCollider[] m_Colliders;

		void Start () {
			m_Colliders = GetComponentsInChildren<BoxCollider>();
		}

		void Update () {
			if(manager.state == NoteTrackModuleManager.panelState.unfolded){
				EnableCoillder();
			}else{
				DisableCoillder();
			}
		}

		void EnableCoillder () {
			foreach(var coll in m_Colliders){
				coll.enabled = true;
			}
		}

		void DisableCoillder () {
			foreach(var coll in m_Colliders){
				coll.enabled = false;
			}
		}

		void OnDoubleClick () {

			if(editor.audioPlayer.isPlaying)return;

			int uid = note.noteId;

			int index = editor.getTrackMap.GetIndex(uid);

			editor.getCurrentNoteIndex = index;

			manager.gridOffset = 0;

			manager.noteRoot.transform.localPosition = Vector3.zero;

		}

		void OnDragEnd () {

			int uid = note.noteId;

			var _note = editor.getTrackMap.GetNote(uid);

			int position =
				Mathf.FloorToInt(
					(getTransform.localPosition.x - (manager.gridStart + _note.position ) * manager.gridSize + manager.gridSize/2f)/manager.gridSize 
				);

			_note.position += position;
			if(_note.position < 0) _note.position = 0;

			manager.gridOffset = -(editor.getCurrentNote.position + manager.gridStart);

			//end
			manager.enabled = true;

		}

		void OnDrag (Vector2 delta) {
			
			if(editor.audioPlayer.isPlaying)return;

			int uid = note.noteId;

			var _note = editor.getTrackMap.GetNote(uid);

			getTransform.localPosition += new Vector3(delta.x, 0);

			int Xoffset = 
				Mathf.FloorToInt(
					(getTransform.localPosition.x - (manager.gridStart + _note.position )*manager.gridSize + manager.gridSize/2f)/manager.gridSize 
				) + _note.position;

			if(Xoffset < 0){
				getTransform.localPosition = new Vector3 (manager.gridStart * manager.gridSize,0);
			}
			//drag
			manager.enabled = false;

		}

	}

}