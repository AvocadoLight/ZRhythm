using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;
	using helper = NoteTrackNoteHelper;

	public class NoteTrackModuleManager : BlockModuleManager {

		public static NoteTrackModuleManager getInstance{private set;get;}

		public panelState state = panelState.folded;

		private UIPanel _panel;

		public UIPanel m_Panel{
			get{
				if(_panel == null)
					_panel = GetComponent<UIPanel> ();
				return _panel;
			}
		}

		public GameObject icon_locked;
		public GameObject icon_unlock;

		public UIScrollBar scroll;

		public GameObject noteHelperPrefab;
		public GameObject measureBarPrefab;
		public GameObject gridBarPrefab;

		public Transform noteRoot;
		public Transform gridRoot;

		private List<helper> helpers = new List<helper>();
		private List<helper> helpersPool = new List<helper>();
		private List<Transform> grids = new List<Transform>();
		private List<Transform> gridsPool = new List<Transform>();
		private List<Transform> measures = new List<Transform>();
		private List<Transform> measuresPool = new List<Transform>();

		//trackOffsetGrid 滾動note track之後的補正
		public int gridOffset = 0;

		//Note起始繪製的位置 以current Note為基準零計算
		public int gridStart{get{				
				if(editor.audioPlayer.isPlaying)
					return	-(editor.getTrackMap.header.LeadInGridCount + Mathf.FloorToInt(editor.audioPlayer.getProgress.totalSeconds/editor.getTrackMap.header.SecondPer32Note));
					//return	-(editor.getTrackMap.header.LeadInGridCount + editor.getCurrentNoteGridPosition) ;

				return	-(editor.getTrackMap.header.LeadInGridCount + editor.getCurrentNoteGridPosition + gridOffset) ;
			}
		}

		[SerializeField,Range(100,850)]
		private float _gridSize = 500;

		public float gridSize {
			get{
				return editor.getTrackMap.header.SecondPer32Note * _gridSize;
			}set{
				_gridSize = value / editor.getTrackMap.header.SecondPer32Note;
			}
		}

		public int gridMax = 160;

		private UIRoot root {get{ return m_Panel.root;} }

		void Awake () {
			getInstance = this;
		}
		
		// Update is called once per frame
		void Update () {
			updateGrids ();
			updateNotes ();
			if( state == panelState.unfolded ){
				icon_locked.SetActive(false);
				icon_unlock.SetActive(true);
			}else{
				icon_locked.SetActive(true);
				icon_unlock.SetActive(false);
			}
//grid測試
//			if(Input.GetKeyDown(KeyCode.Space)){
//				foreach(var g in grids){
//					Destroy(g.gameObject);
//				}
//
//				foreach(var m in measures){
//					Destroy(m.gameObject);
//				}
//
//				grids.Clear();
//				measures.Clear();
//
//				enabled = false;
//			}

		}

		void updateGrids () {
			float space = this.gridSize * 2;

			int need = Mathf.FloorToInt (root.manualWidth / space);

			if (need > gridMax) need = gridMax;

			if(need<4)need = 4;

			int half = need / 2;

			int objIndex = -1;

			for (int i = 0; (i / half <= 1 || i < grids.Count); i++) {

				if((i % half + 1) % 8 == 0){
					//measure bar
					continue;
				}else{
					//grid bar
					objIndex++;
				}

				if (i > need) {
					var grid = grids [objIndex];
					grids.RemoveAt (objIndex);
					gridsPool.Add (grid);
					grid.gameObject.SetActive(false);
					i--;
					objIndex--;
					continue;
				}

				if (objIndex >= this.grids.Count){					
					Transform grid = null;

					if(gridsPool.Count > 0){
						//get useable
						grid = gridsPool[0];
						gridsPool.RemoveAt(0);
					}else{
						//generate new
						grid = (Instantiate(gridBarPrefab,gridRoot) as GameObject).transform;
						var widget = grid.GetComponent<UIWidget> ();
						widget.bottomAnchor.target = m_Panel.cachedTransform;
						widget.topAnchor.target = m_Panel.cachedTransform;
					}

					grids.Add (grid);
					grid.gameObject.SetActive(true);
				}

				grids [objIndex].transform.localScale = Vector3.one;

				if(i > (half - 1)){
					grids [objIndex].transform.localPosition =
						new Vector3 (space * (i % half + 1) * -1 , 0, 0);
				}else{
					grids [objIndex].transform.localPosition =
						new Vector3 (space * (i % half + 1) , 0, 0);
				}
			}

			objIndex = -1;
		
			for (int i = 0; (i / half <= 1 || i < measures.Count); i++) {

				if((i % half + 1) % 8 == 0){
					//measure bar
					objIndex++;
				}else{
					//grid bar
					continue;
				}

				if (i > need) {
					var measure = measures [objIndex];
					measures.RemoveAt (objIndex);
					measuresPool.Add (measure);
					measure.gameObject.SetActive(false);
					i--;
					objIndex--;
					continue;
				}

				if (objIndex >= measures.Count){					
					Transform measure = null;

					if(measuresPool.Count > 0){
						measure = measuresPool[0];
						measuresPool.RemoveAt(0);
					}else{
						measure = (Instantiate(measureBarPrefab,gridRoot) as GameObject).transform;
						var widget = measure.GetComponent<UIWidget> ();
						widget.bottomAnchor.target = m_Panel.cachedTransform;
						widget.topAnchor.target = m_Panel.cachedTransform;
					}

					measures.Add (measure);
					measure.gameObject.SetActive(true);
				}

				measures [objIndex].transform.localScale = Vector3.one;

				measures [objIndex].transform.localPosition =
					new Vector3 (space * (i % half + 1) * ((i > (half - 1)) ? -1 : 1), 0, 0);
			}
		
		}

		void updateNotes () {

			var notes = editor.getTrackMap.Notes;

			int helperIndex = -1;

			var current_page = -1f;

			if(!editor.audioPlayer.isPlaying){
				current_page = editor.getTrackMap.GetPage(editor.getCurrentNoteGridPosition);
			}else{
				current_page = editor.getTrackMap.GetPage(Mathf.FloorToInt(editor.audioPlayer.getProgress.totalSeconds/editor.getTrackMap.header.SecondPer32Note));
			}

			for (int dataIndex = 0; dataIndex < notes.Count; dataIndex++) {

				helper useable = null;

				float PosX = (gridStart + notes [dataIndex].position + editor.getTrackMap.header.LeadInGridCount) * this.gridSize;
				float PosY = (state == panelState.unfolded)?(m_Panel.height - 400) * notes[dataIndex].Xoffset : 0;

				if(Mathf.Abs(PosX + noteRoot.transform.localPosition.x)>root.manualWidth/2f)continue;

				helperIndex++;

				for (int p = helperIndex; p < helpers.Count; p++) {
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

				var note_page = editor.getTrackMap.GetPage(notes[dataIndex].position);

				if((int)note_page == (int)current_page){
					useable.getTransform.localScale = Vector3.one * 0.5f;
				}else{
					useable.getTransform.localScale = Vector3.one * 0.35f;
				}

				useable.note.noteId = notes[dataIndex].InstanceID;

				//useable.getTransform.localScale = Vector3.one * 0.5f;

				useable.getTransform.localPosition = new Vector3(PosX,PosY);

				useable.getTransform.localEulerAngles = 
					Vector3.forward * notes[dataIndex].angle;
			}

			for (int i = helpers.Count-1;i>=helperIndex+1;i--){
				if(i<0)continue;
				var n = helpers[i];
				helpers.RemoveAt(i);
				helpersPool.Add(n);
				n.gameObject.SetActive(false);
			}
		}

		helper GetUseableNote (NoteType type) {
			foreach (var n in helpersPool){
				if(n.note.type == type){
					helpersPool.Remove(n);
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

		public enum panelState{
			folded,
			regular,
			unfolded
		}

	}
}