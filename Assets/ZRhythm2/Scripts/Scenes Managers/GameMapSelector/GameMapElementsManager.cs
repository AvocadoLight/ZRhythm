using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace BurningxEmpires.ZRhythm.Selector{
	
	using selector = GameMapSelectorManager;

	public class GameMapElementsManager : MonoBehaviour {

		public static GameMapElementsManager getInstance;

		public UICenterOnChild centerOnChild;

		private UIGrid m_grid;

		public UIGrid grid{
			get{
				if(m_grid == null)
					m_grid = centerOnChild.GetComponent<UIGrid>();
				return m_grid;
			}
		}

		private Transform _root;

		public Transform root{
			get{
				if(_root == null){
					_root = centerOnChild.transform;
				}
				return _root;
			}
		}

		public GameObject prefab;

		public Transform pointer;

		Coroutine _load;

		void Awake () {
			getInstance = this;
			root.DestroyChildren ();
			//trackMapFileRoot.onCenter += OnTrackMapFileOnCenter;
			//trackMapFileRoot.onFinished += OnTrackMapFileOnFinished;
		}

		public void LoadTrackMapFileObjects () {
			foreach(var temp in selector.loader.GameMapTempFolders){
				var ob = Instantiate(prefab,root)as GameObject;
				ob.transform.localScale = Vector3.one;
				ob.transform.localPosition = Vector3.zero;

				var tmfOb = ob.GetComponent<GameMapElement>();
				tmfOb.Init(temp);
			}
			grid.enabled = true;
		}

		void OnElementCenter (GameObject go) {
			//SelectionManager.getInstance.currentTrackMapFile = go.GetComponent<TrackMapFileObject>();
		}

		void OnElementCentered () {		

			if(selector.getCurrentGameMapElement != null&&
				centerOnChild.centeredObject == selector.getCurrentGameMapElement.gameObject)
				return;

			selector.getCurrentGameMapElement = centerOnChild.centeredObject.GetComponent<GameMapElement>();

			if(selector.getCurrentGameMapElement == selector.getActiveGameMapElement) {
				selector.SetBackground(selector.getCurrentGameMapElement.getTexture());
				selector.UpdateUIText(selector.getCurrentGameMapElement.trackMap);
				LoadAudio();
				//Centered
				BurningxEmpires.ZRhythm.Game.GameManager.currentGameMapCache.filePath = 
					selector.getCurrentGameMapElement.temp.folderPath;
				BurningxEmpires.ZRhythm.Game.GameManager.currentGameMapCache.isTemp = true;					
			}

		}

		public void onElementClick(GameMapElement tmfOb){

			centerOnChild.CenterOn(tmfOb.transform);

			selector.getActiveGameMapElement = tmfOb;

			if(AudioClipPlayer.mode == AudioClipPlayer.PlayMode.none){

				if(centerOnChild.onFinished == null){
					centerOnChild.onFinished += OnElementCentered;
				}else{
					var list = centerOnChild.onFinished.GetInvocationList();
					if(list == null ||list.Length == 0){
						centerOnChild.onFinished += OnElementCentered;
					}
				}
			}
			//LoadAudio();
		}


		public void LoadAudio(){
			if(_load != null)
				StopCoroutine(_load);

			_load = StartCoroutine(loadAudio());
		}

		IEnumerator loadAudio(){

			var tmfOb = selector.getCurrentGameMapElement;

			string path = ConfigUtility.fileLoadPath(Path.Combine(tmfOb.temp.folderPath,tmfOb.temp.chunkHeader.audioFileCode));

			WWW www = new WWW(path);

			yield return www;

			string audioExtension = Path.GetExtension(tmfOb.trackMap.header.AudioFileFullName);

			audioExtension = audioExtension.ToLower();

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX

			if(audioExtension == ConfigUtility.Mp3Extension){
				AudioClipPlayer.PlayAudioWithData(www.bytes);
			}else if (audioExtension == ConfigUtility.WavExtension){
				var clip = www.GetAudioClip(true,false,AudioType.WAV);
				AudioClipPlayer.PlayAudio(clip);
			}

#else

			var clip = www.GetAudioClip(true,false,AudioType.MPEG);
			AudioClipPlayer.PlayAudio(clip);

			/*if(audioExtension == ConfigUtility.WavExtension){
				AudioClipPlayer.PlayAudioWithData(www.bytes);
			}else if (audioExtension == ConfigUtility.Mp3Extension){
				var clip = www.GetAudioClip(true,false,AudioType.MPEG);
				AudioClipPlayer.PlayAudio(clip);
			}*/

#endif
		}
	}
}