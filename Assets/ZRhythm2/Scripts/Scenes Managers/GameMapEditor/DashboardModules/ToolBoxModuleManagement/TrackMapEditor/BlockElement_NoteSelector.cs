using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_NoteSelector : BlockElement_ToolBox {

		public UILabel display;

		public override void Start ()
		{
			base.Start ();
			display.gameObject.AddComponent<Drag>().manager = this;
		}

		public override void Update () {
			base.Update ();
			display.text = string.Format("{0}/{1}",editor.getCurrentNoteIndex+1,editor.getTrackMap.Notes.Count);
		}

		public void onPreviousNote () {
			int id = editor.getCurrentNoteIndex;

			if(id <= -1){
				if(editor.getTrackMap.Notes.Count > 0)
					editor.getCurrentNoteIndex = 0;
			}else if (id - 1 >= 0) {
				editor.getCurrentNoteIndex = id - 1;
			} 

			if(editor.getCurrentNote != null){
				editor.trackMapEditor.noteLength.Value = editor.getCurrentNote.lenght;
				editor.trackMapEditor.noteOffset.Value = editor.getCurrentNote.Xoffset;
				editor.trackMapEditor.noteAngle.Value = editor.getCurrentNote.angle;
			}

		}

		public void onNextNote () {
			int id = editor.getCurrentNoteIndex;

			if (id + 1 < editor.getTrackMap.Notes.Count) {
				editor.getCurrentNoteIndex = id + 1;
			}

			if(editor.getCurrentNote != null){
				editor.trackMapEditor.noteLength.Value = editor.getCurrentNote.lenght;
				editor.trackMapEditor.noteOffset.Value = editor.getCurrentNote.Xoffset;
				editor.trackMapEditor.noteAngle.Value = editor.getCurrentNote.angle;
			}
		}
	
		public class Drag : MonoBehaviour {

			public BlockElement_NoteSelector manager;

			private float rec;

			private float sensitive = 15;

			void OnDrag (Vector2 delta) {
				rec += delta.x;
				if(Mathf.Abs(rec) > sensitive){
					if(rec>0){
						manager.onNextNote();
					}else{
						manager.onPreviousNote();
					}
					rec = 0;
				}
			}


		}

	}
}