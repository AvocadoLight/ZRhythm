using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using BurningxEmpires.ZRhythm.Tools;

namespace BurningxEmpires.ZRhythm.Selector{
	
	public class GameMapSelectorManager : MonoBehaviour {

		public static GameMapSelectorManager getInstance{private set;get;}

		private GameMapLoader m_loader;

		public static GameMapLoader loader{get{return getInstance.m_loader;}}

		public GameMapElement currentGameMapElement;

		public static GameMapElement getCurrentGameMapElement{
			get{
				return getInstance.currentGameMapElement;
			}set{
				getInstance.currentGameMapElement = value;	
			}
		}

		public GameMapElement activeGameMapElement;

		public static GameMapElement getActiveGameMapElement{
			get{
				return getInstance.activeGameMapElement;
			}set{
				getInstance.activeGameMapElement = value;
			}
		}

		public UITexture backgroundImage;
		public UILabel text_Title;
		public UILabel text_Artist;
		public UILabel text_AudioProgress;

		void Awake () {
			getInstance = this;
			m_loader = GameMapLoader.LoadGameMaps();
			text_Title.text = string.Empty;
			text_Artist.text = string.Empty;
			text_AudioProgress.text = string.Empty;
		}

		void Start () {
			GameMapElementsManager.getInstance.LoadTrackMapFileObjects();
			if(loader.GameMapTempFolders.Count == 0){
				getInstance.text_Title.text = "還沒有任何的譜面唷";
				return;
			}
		}

		private float timeOutTimer;
		private float timeOut = 0.5f;

		#if UNITY_EDITOR

		public UnityEditor.SceneAsset scene_TouchStart;
		public UnityEditor.SceneAsset scene_Game;

		#endif

		public string scene_TouchStart_name;
		public string scene_Game_name;

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
						Debug.Log("再按一次回到開始畫面");
				}
			}

			if(AudioClipPlayer.mode == AudioClipPlayer.PlayMode.none)
				text_AudioProgress.text = string.Empty;
			else{
				text_AudioProgress.text =
					string.Format ("{0}:{1} / {2}:{3}",
						AudioClipPlayer.state.currectInstanceMinutes.ToString ("00"),
						AudioClipPlayer.state.currectInstanceSeconds.ToString ("00"),
						AudioClipPlayer.state.totalInstanceMinutes.ToString ("00"),
						AudioClipPlayer.state.totalInstanceSeconds.ToString ("00"));
			}
		}

		public static void UpdateUIText (TrackMap trackMap) {			
			getInstance.text_Title.text = string.Format("標題:{0}",trackMap.header.Title);
			getInstance.text_Artist.text = string.Format("作者:{0}",trackMap.header.Artist);
		}

		public static void SetBackground(Texture2D texture){
			getInstance.backgroundImage.mainTexture = texture;
		}

		#if UNITY_EDITOR

		void OnValidate () {
			if(scene_TouchStart != null){
				scene_TouchStart_name = scene_TouchStart.name;
			}
			if(scene_Game != null){
				scene_Game_name = scene_Game.name;
			}
		}

		#endif
	}
}