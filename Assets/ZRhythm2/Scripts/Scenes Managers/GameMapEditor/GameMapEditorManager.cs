using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BurningxEmpires.ZRhythm.Tools;
using UnityEngine.SceneManagement;

namespace BurningxEmpires.ZRhythm.Editor{
	
	public class GameMapEditorManager : MonoBehaviour {

		public static AudioPlayerManager audioPlayer{get{return AudioPlayerManager.getInstance;}}

		public static DebugLogModuleManager debugLog{get{return DebugLogModuleManager.getInstance;}}

		public static BlockElement_BrushSelector brush{get{return BlockElement_BrushSelector.getInstance;}}

		public static TrackMapEditorModuleManager trackMapEditor{get{return TrackMapEditorModuleManager.getInstance;}}

		public static GameMapScreenModuleManager gameScreen{get{return GameMapScreenModuleManager.getInstance;}}

		public static NoteTrackModuleManager noteTrack{get{return NoteTrackModuleManager.getInstance;}}

		public static RecorderModuleManager record{get{return RecorderModuleManager.getInstance;}}

		public static GameMapPlayerModuleManager gameMapPlayer{get{return GameMapPlayerModuleManager.getInstace;}}

		public static GameMapEditorManager getInstance{private set;get;}

		[HideInInspector]
		private TrackMap m_TrackMap;

		public static TrackMap getTrackMap{
			get{
				return getInstance.m_TrackMap;
			}set{
				getInstance.m_TrackMap = value;
			}
		}

		private int m_currentNoteIndex = -1;

		public static int getCurrentNoteIndex{
			get{
				return getInstance.m_currentNoteIndex;
			}set{
				getInstance.m_currentNoteIndex = value;
			}
		}

		public static Note getCurrentNote{
			get{
				if(getTrackMap.Notes.Count == 0) {
					return null;
				}if(getCurrentNoteIndex < 0){
					return null;
				}if(getCurrentNoteIndex >= getTrackMap.Notes.Count){
					return null;
				}
				return getTrackMap.Notes[getCurrentNoteIndex];
			}set{
				if(getTrackMap.Notes.Count == 0) {
					return;
				}if(getCurrentNoteIndex < 0){
					return;
				}if(getCurrentNoteIndex >= getTrackMap.Notes.Count){
					return;
				}
				getTrackMap.Notes[getCurrentNoteIndex] = value;
			}
		}

		public static int getTrackMapGridPositionMax{
			get{
				if(!audioPlayer.hasAudioClip || getTrackMap.header.SecondPer32Note <= 0)
					return 120;

				return Mathf.FloorToInt(audioPlayer.getClipLength.totalSeconds / getTrackMap.header.SecondPer32Note) + 1;
			}
		}

		public static int getCurrentNoteGridPosition{
			get{

				if(getTrackMap.header.SecondPer32Note <= 0)
					return getTrackMap.header.LeadInGridCount;

				//if(!audioPlayer.hasAudioClip)
				//	return 0;

				//playing交給需要判斷狀態的地方自行判斷
				//if(audioPlayer.isPlaying)
				//	return Mathf.FloorToInt(audioPlayer.getProgress.totalSeconds / getTrackMap.header.SecondPer32Note);

				if(getCurrentNote == null)
					return getTrackMap.header.LeadInGridCount;
				
				return getCurrentNote.position;
			}
		}

		public NotePrefabsField prefabs;

		private float timeOutTimer;
		private float timeOut = 0.5f;

		#if UNITY_EDITOR

		public UnityEditor.SceneAsset scene_TouchStart;

		#endif

		public string scene_TouchStart_name;

		void Awake () {
			getInstance = this;
			m_TrackMap = new TrackMap();		
		}

		// Use this for initialization
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {

			timeOutTimer -= Time.deltaTime;
			if(timeOutTimer < 0)
				timeOutTimer = 0;

			if(Input.GetKeyDown(KeyCode.Escape)){
				if(timeOutTimer > 0){
					SceneManager.LoadScene(scene_TouchStart_name);
				}else{
					timeOutTimer = timeOut;
					if(Application.platform == RuntimePlatform.Android)
						AndroidTool.MakeToast("再按一次回到開始畫面");
					else
						debugLog.Log("再按一次回到開始畫面");
				}
			}
		}

		#if UNITY_EDITOR

		void OnValidate () {
			if(scene_TouchStart != null){
				scene_TouchStart_name = scene_TouchStart.name;
			}
		}

		#endif
	}

}