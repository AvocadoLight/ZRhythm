using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;
	using helper = GameMapScreenNoteHelper;
	//遊戲模式的視窗模擬
	public class GameMapScreenModuleManager : BlockModuleManager {

		public static GameMapScreenModuleManager getInstance{private set;get;}

		public UITexture backgroundImage;

		public GameObject noteHelperPrefab;

		public Transform noteRoot;

		[Range(0.1f,1f)]
		public float showRange = 1;

		private List<helper> helpers = new List<helper>();

		private List<helper> pool = new List<helper>();

		void Awake () {			
			getInstance = this;			
		}

		void Update () {
			updateNotes ();
		}


		//TODO:Only Show Note On Same Page.

		void updateNotes () {

			var notes = editor.getTrackMap.Notes;

			int helperIndex = -1;

			var current_page = -1f;

			if(!editor.audioPlayer.isPlaying){
				current_page = editor.getTrackMap.GetPage(editor.getCurrentNoteGridPosition);
			}else{
				current_page = editor.getTrackMap.GetPage(Mathf.FloorToInt(editor.audioPlayer.getProgress.totalSeconds/editor.getTrackMap.header.SecondPer32Note));
			}

			for (int dataIndex = 0;	dataIndex < notes.Count; dataIndex++) {

				helperIndex ++;

				helper useable = null;

				for (int p = helperIndex;p <helpers.Count;p++){
					if(helpers[p].note.type == notes[dataIndex].type){
						useable = helpers[p];
						helpers.RemoveAt(p);
						break;
					}
				}

				if(useable == null)
					useable = GetUseableNote(notes[dataIndex].type);

				if(helperIndex < helpers.Count)
					helpers.Insert(helperIndex,useable);
				else
					helpers.Add(useable);

				useable.note.noteId = notes[dataIndex].InstanceID;

				var note_page = editor.getTrackMap.GetPage(notes[dataIndex].position);

				useable.page = (int)note_page;

				useable.getTransform.localPosition = 
					new Vector3(
						editor.getTrackMap.GetPositionX(notes[dataIndex].Xoffset),
						editor.getTrackMap.GetPositionY(notes[dataIndex].position));

				useable.getTransform.localEulerAngles = 
					Vector3.forward * notes[dataIndex].angle;
				
				/*var alpha = 0f;

				if(editor.audioPlayer.isPlaying){
					alpha = 
						Mathf.Abs(notes[dataIndex].position -
							Mathf.FloorToInt(editor.audioPlayer.getProgress.totalSeconds/editor.getTrackMap.header.SecondPer32Note));
				}else{
					alpha = Mathf.Abs(editor.getCurrentNoteGridPosition - notes[dataIndex].position);
				}

				alpha = (alpha > 0 )?(0.2f / alpha):1;

				if(alpha <= showRange){
					if(alpha<0.2f) alpha = 0.2f;
					if(alpha>1) alpha = 1;
					useable.SetAlpha(alpha);
				}else{
					useable.SetAlpha(0);
				}*/

				var alpha = 0f;

				if(Mathf.Abs(current_page - note_page) < showRange){
					alpha = 1 - (Mathf.Abs(note_page - current_page) / showRange);
				}

				if((int)note_page == (int)current_page){
					useable.getTransform.localScale = 
						Vector3.Lerp(useable.getTransform.localScale,Vector3.one,Time.deltaTime*2);
				}else{
					useable.getTransform.localScale = Vector3.one * 0.8f;
				}

				useable.SetAlpha(alpha);

			}

			for (int i = helpers.Count-1;i>=helperIndex+1;i--){
				if(i<0)continue;
				var n = helpers[i];
				helpers.RemoveAt(i);
				pool.Add(n);
				n.gameObject.SetActive(false);
			}

		}

		helper GetUseableNote (NoteType type) {
			foreach (var n in pool){
				if(n.note.type == type){
					pool.Remove(n);
					n.gameObject.SetActive(true);
					return n;
				}
			}
			return GenerateNote(type);
		}


		helper GenerateNote (NoteType type) {

			GameObject note = Instantiate(noteHelperPrefab,noteRoot) as GameObject;

			var note_trans = note.transform;
			note_trans.localScale = Vector3.one;

			GameObject content = null;

			switch (type) {
			case NoteType.Tap:
				content = Instantiate(editor.getInstance.prefabs.NoteTapPrefab,note_trans);
				break;
			case NoteType.Hold:
				content = Instantiate(editor.getInstance.prefabs.NoteHoldPrefab,note_trans);
				break;
			case NoteType.Swipe:
				content = Instantiate(editor.getInstance.prefabs.NoteSwipePrefab,note_trans);
				break;
			default:
				editor.debugLog.LogWarning(ExceptionList.error.Value);
				break;				
			}

			if(content == null){
				editor.debugLog.LogWarning(ExceptionList.error.Value);
				return null;
			}

			var content_trans = content.transform;
			content_trans.localPosition = Vector3.zero;
			content_trans.localScale = Vector3.one;

			return note.GetComponent<helper>();

		}

		public void setBackgroundImage (Texture2D image) {
			backgroundImage.mainTexture = image;
		}

	}
}