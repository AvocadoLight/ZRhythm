using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_NoteEditor : BlockElement_ToolBox {

		public UIButton button_Add;
		public UIButton button_Insert;
		public UIButton button_Delete;

		public override void Update () {
			base.Update ();

			if(editor.audioPlayer.isPlaying){
				button_Add.isEnabled = false;
				button_Insert.isEnabled = false;
				button_Delete.isEnabled = false;
			}else{				
				button_Add.isEnabled = true;
				button_Insert.isEnabled = true;
				button_Delete.isEnabled = !(editor.getCurrentNoteIndex < 0 || editor.getTrackMap.Notes.Count <= 0);
			}

		}

		public Note AddNote () {
			if(editor.brush.currentBrush == NoteType.Null){
				editor.brush.currentBrush = NoteType.Tap;
			}

			Note note = NoteUtility.GenerateNote(editor.brush.currentBrush);

			var length = (int)editor.trackMapEditor.noteLength.Value;
			var offset = editor.trackMapEditor.noteOffset.Value;
			var angle = editor.trackMapEditor.noteAngle.Value;

			if(editor.getCurrentNote != null)
				note.position = length + editor.getCurrentNoteGridPosition;
			else
				note.position = editor.getCurrentNoteGridPosition;

			note.lenght = length;
			note.Xoffset = offset;
			note.angle = angle;

			editor.getTrackMap.Notes.Add(note);

			editor.getTrackMap.Sort();

			editor.getCurrentNoteIndex = editor.getTrackMap.Notes.IndexOf(note);

			return note;
		}

		//在目前位置增加一個音符
		public void onAddNote () {			

			if(editor.brush.currentBrush == NoteType.Null){
				print(editor.brush.currentBrush.ToString());
				return;
			}

			Note note = NoteUtility.GenerateNote(editor.brush.currentBrush);

			var length = (int)editor.trackMapEditor.noteLength.Value;
			var offset = editor.trackMapEditor.noteOffset.Value;
			var angle = editor.trackMapEditor.noteAngle.Value;

			if(editor.getCurrentNote != null)
				note.position = length + editor.getCurrentNoteGridPosition;
			else
				note.position = editor.getCurrentNoteGridPosition;
			
			note.lenght = length;
			note.Xoffset = offset;
			note.angle = angle;

			editor.getTrackMap.Notes.Add(note);

			editor.getTrackMap.Sort();

			editor.getCurrentNoteIndex = editor.getTrackMap.Notes.IndexOf(note);

		}

		//在目前位置插入一個音符,之後的音符會位移這個音符的長度
		public void onInsertNote () {	

			if(editor.brush.currentBrush == NoteType.Null){
				print(editor.brush.currentBrush.ToString());
				return;
			}


			Note note = NoteUtility.GenerateNote(editor.brush.currentBrush);

			var length = (int)editor.trackMapEditor.noteLength.Value;
			var offset = editor.trackMapEditor.noteOffset.Value;
			var angle = editor.trackMapEditor.noteAngle.Value;

			note.position = editor.getCurrentNoteGridPosition;

			note.lenght = length;
			note.Xoffset = offset;	
			note.angle = angle;

			editor.getTrackMap.Notes.Add(note);

			editor.getCurrentNoteIndex = editor.getTrackMap.Notes.IndexOf(note);

			for(int i = 0; i < editor.getTrackMap.Notes.Count;i++){

				if(editor.getTrackMap.Notes[i] == note ||
					editor.getTrackMap.Notes[i].position < note.position)
					continue;			

				editor.getTrackMap.Notes[i].position += note.lenght;
			}

			editor.getTrackMap.Sort();

			editor.getCurrentNoteIndex = editor.getTrackMap.Notes.IndexOf(note);
		}

		//刪除目前選擇的音符
		public void onDeleteNote () {
			if( editor.getTrackMap.Notes.Count <= 0){
				button_Delete.isEnabled = false;
				return;
			}

//			if(editor.getCurrentNoteIndex - 1 >= editor.getTrackMap.Notes.Count){
//				editor.getCurrentNoteIndex = editor.getTrackMap.Notes.Count - 1;			
//			}		

			editor.getTrackMap.Notes.RemoveAt(editor.getCurrentNoteIndex);

			if (editor.getCurrentNoteIndex >= editor.getTrackMap.Notes.Count){
				editor.getCurrentNoteIndex = editor.getTrackMap.Notes.Count - 1;
			}

			editor.getTrackMap.Sort();
		}

	}
}