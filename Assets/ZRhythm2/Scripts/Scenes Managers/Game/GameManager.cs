using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Ionic.Zip;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
using System.Drawing;
#endif

namespace BurningxEmpires.ZRhythm.Game{

	using helper = GameNoteHelper;

	public class GameManager : MonoBehaviour {

		public static GameManager getInstance{private set;get;}

		public GameScorer scorer{get{return GameScorer.getInstance;}}

		public GameConfigManager config{get{return GameConfigManager.getInstance;}}

		public GameEffectManager effect{get{return GameEffectManager.getInstance;}}

		public CountDownManager countDown{get{return CountDownManager.getInstance;}}

		public AudioPlayerManager audioPlayer{get{return AudioPlayerManager.getInstance;}}

		//struct
		public static GameMapCache currentGameMapCache;

		public TrackMap currentTrackMap;

		public UITexture backgorundImage;

		public NotePrefabsField prefabs;

		public GameObject noteHelperPrefab;

		public Transform noteRoot;

		[Range(0.1f,1f)]
		public float showRange = 1;

		//[HideInInspector]
		public List<helper> helpers = new List<helper>();

		public bool isPrepare = false;

		public bool isGenerated = false;

		void Awake () {
			getInstance = this;
		}

		void Start () {
			

			//Load Track Map
			//Load Background Image
			//Load Audio Clip
			if(currentGameMapCache.isTemp)
				StartCoroutine(LoadByTemp ());
			else
				Debug.LogError("You Can Ony Load GameMap By TempFolder");

			//Generate Notes

			//Prepare Countdown
		}

		IEnumerator LoadTrackMap (GameMapTempFolder temp) {			
			string path = ConfigUtility.fileLoadPath(
				Path.Combine(
					temp.folderPath,
					temp.chunkHeader.trackMapFileCode
				)
			);

			WWW trackmap = new WWW(	path);
			//yield return trackmap;

			while(!trackmap.isDone){
				GameLoadingDialog.getInstance.ReportProgress(trackmap.progress/3f);
				yield return null;
			}

			currentTrackMap = TrackMap.FromJson(trackmap.text);

		}

		IEnumerator LoadAudioClip (GameMapTempFolder temp) {
			
			string path = Path.Combine(
				temp.folderPath,
				temp.chunkHeader.audioFileCode
			);

			bool hasAudio = File.Exists(path);

			if(!hasAudio)
				Debug.LogError("Temp Folder Audio Is Not Exists");

			path = ConfigUtility.fileLoadPath(path);

			WWW clip = new WWW(path);

			//yield return clip;

			while(!clip.isDone){
				GameLoadingDialog.getInstance.ReportProgress(0.33f + clip.progress/3f);
				yield return null;
			}

			if(!string.IsNullOrEmpty( clip.error)){
				Debug.LogError("Temp Folder Audio Error : " + clip.error);
			}

			string extension = Path.GetExtension(currentTrackMap.header.AudioFileFullName);
			extension = extension.ToLower ();

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX

			if(extension.CompareTo(ConfigUtility.WavExtension) == 0){
				audioPlayer.SetClip(clip.GetAudioClip(true,false,AudioType.WAV));
				//audioPlayer.Play();
			}else if(extension.CompareTo(ConfigUtility.Mp3Extension) == 0){
				audioPlayer.SetClip(NAudioPlayer.FromMp3Data(clip.bytes));
				//audioPlayer.Play();
			}else{
				Debug.LogError("無法辨識副檔名:" + extension);
				hasAudio = false;
			}
#else
			if(extension.CompareTo(ConfigUtility.WavExtension) == 0){
				audioPlayer.SetClip(clip.GetAudioClip(true,false,AudioType.WAV));
				audioPlayer.Play();
			}else if(extension.CompareTo(ConfigUtility.Mp3Extension) == 0){
				audioPlayer.SetClip(clip.GetAudioClip(true,false,AudioType.MPEG));
				audioPlayer.Play();
			}else{
				Debug.LogError("無法辨識副檔名:" + extension);
				hasAudio = false;
			}
#endif
		}

		IEnumerator LoadTexture (GameMapTempFolder temp) {
			string path = 	Path.Combine(
				temp.folderPath,
				temp.chunkHeader.backgroundFileCode
			);

			bool hasTexture = File.Exists(path);

			if(!hasTexture){
				yield break;
			}

			path = ConfigUtility.fileLoadPath(path);

			WWW texture = new WWW(path);

			//yield return texture;

			while(!texture.isDone){
				GameLoadingDialog.getInstance.ReportProgress(0.66f + texture.progress/3f);
				yield return null;
			}

			if(!string.IsNullOrEmpty( texture.error)){
				Debug.LogError("Temp Folder texture Error : " + texture.error);
			}

			backgorundImage.mainTexture = texture.texture;
		}

		IEnumerator LoadByTemp () {

			GameLoadingDialog.getInstance.Open();

			GameMapTempFolder temp = new GameMapTempFolder(currentGameMapCache.filePath);

			string path = null;

			WWW texture = null;

			//trackMap
			yield return StartCoroutine( LoadTrackMap(temp));

			GenerateNotes ();
			isGenerated = true;

			audioPlayer.Stop();

			//audio
			yield return StartCoroutine( LoadAudioClip(temp));

			audioPlayer.Stop();

			//texture
			yield return StartCoroutine( LoadTexture(temp));

			print("|GenerateNotes|");
			
			GameLoadingDialog.getInstance.Close();
			
			countDown.StartCountDown();
			yield return new WaitUntil(()=>{
				return countDown.isDone;
			});
			audioPlayer.Play();
			isPrepare = true;
		}

		void GenerateNotes () {

			noteRoot.DestroyChildren();

			var notes = currentTrackMap.Notes;

			for (int i = 0;	i < notes.Count; i++) {

				helper useable = GenerateNote(notes[i].type);

				helpers.Add(useable);

				useable.note.noteId = notes[i].InstanceID;

				var note_page = currentTrackMap.GetPage(notes[i].position);

				useable.page = note_page;

				useable.getTransform.localPosition = 
					new Vector3(
						currentTrackMap.GetPositionX(notes[i].Xoffset),
						currentTrackMap.GetPositionY(notes[i].position));

				useable.getTransform.localEulerAngles = 
					Vector3.forward * notes[i].angle;

				var alpha = 0f;

				var current_page = -1f;

				current_page = currentTrackMap.GetPage(Mathf.FloorToInt(audioPlayer.getProgress.totalSeconds/currentTrackMap.header.SecondPer32Note));

				if(Mathf.Abs(current_page - note_page) < showRange){
					alpha = 1 - (Mathf.Abs(note_page - current_page) / showRange);
				}

				useable.SetAlpha(alpha);

			}

		}

		helper GenerateNote (NoteType type) {

			GameObject note = Instantiate(noteHelperPrefab,noteRoot) as GameObject;

			var old_helper = note.GetComponent<helper>();
			Destroy(old_helper);

			helper _helper = null;

			var note_trans = note.transform;
			note_trans.localScale = Vector3.one;

			GameObject content = null;

			switch (type) {
			case NoteType.Tap:
				_helper = note.AddComponent<Helper_Tap>();
				content = Instantiate(prefabs.NoteTapPrefab,note_trans);
				break;
			case NoteType.Hold:
				_helper = note.AddComponent<Helper_Hold>();
				content = Instantiate(prefabs.NoteHoldPrefab,note_trans);
				break;
			case NoteType.Swipe:
				_helper = note.AddComponent<Helper_Swipe>();
				content = Instantiate(prefabs.NoteSwipePrefab,note_trans);
				break;
			default:
				Debug.LogError(ExceptionList.error.Value);
				break;				
			}

			if(content == null){
				Debug.LogError(ExceptionList.error.Value);
				return null;
			}

			var old_collider = content.GetComponent<BoxCollider>();
			var collider = note.GetComponent<BoxCollider>();

			collider.center = old_collider.center;
			collider.size = old_collider.size;

			Destroy(old_collider);

			var content_trans = content.transform;
			content_trans.localPosition = Vector3.zero;
			content_trans.localScale = Vector3.one;

			//return note.GetComponent<helper>();
			return _helper;
		}

		// Update is called once per frame
		void Update () {			

			if(isGenerated)
				updateNotes ();

			if(isPrepare && !audioPlayer.isPlaying && !GamePauseDialog.getInstance.gameObject.activeSelf){
				GameResultManager.currentGameMapName = 
					currentTrackMap.header.Title + " - " + currentTrackMap.header.Artist;
				GameResultManager.count_Excellent = scorer.count_Excellent;
				GameResultManager.count_Good = scorer.count_Good;
				GameResultManager.count_Bad = scorer.count_Bad;
				GameResultManager.count_Miss = scorer.count_Miss;
				GameResultManager.total_Score = scorer.total_Score;
				Goto_GameResult();
			}
		}

		void updateNotes () {

			if(!audioPlayer.isPlaying)
				return;

			var depth = 20;

			var progress = audioPlayer.getProgress.totalSeconds;

			var current_gridPosition = 0f;

			if(isPrepare && audioPlayer.isPlaying){
				current_gridPosition = progress/currentTrackMap.header.SecondPer32Note;
			}else{
				current_gridPosition = (-countDown.progressValue)/currentTrackMap.header.SecondPer32Note;
			}

			var current_page = currentTrackMap.GetPage(current_gridPosition);

			for (int i = 0;	i < helpers.Count; i++) {

				var note = helpers[i];

				var alpha = 0f;

				if(Mathf.Abs(current_page - note.page) < showRange){
					alpha = 1 - (Mathf.Abs(note.page - current_page) / showRange);
				}

				if((int)note.page == (int)current_page){
					note.getTransform.localScale = 
						Vector3.Lerp(note.getTransform.localScale,Vector3.one,Time.deltaTime*2);
				}else{
					note.getTransform.localScale = Vector3.one * 0.8f;
				}

				note.SetAlpha(alpha);

				note.SetDepth(depth);

				note.note.SetDepth(depth);

				note.onUpdate();

				depth -= note.note.childCount;
			}

		}

		void OnDestroy () {
			
		}

		#if UNITY_EDITOR

		public UnityEditor.SceneAsset scene_GameResult;

		public UnityEditor.SceneAsset scene_GameMapSelector;

		#endif

		public string scene_GameResult_name;
		public string scene_GameMapSelector_name;

		public void Goto_GameResult () {
			LoadScene (scene_GameResult_name);
		}

		public void Goto_GameMapSelector () {
			LoadScene (scene_GameMapSelector_name);
		}

		public void LoadScene (string sceneName) {
			SceneManager.LoadScene(sceneName);
		}

		#if UNITY_EDITOR

		void OnValidate () {
			if(scene_GameResult != null){
				scene_GameResult_name = scene_GameResult.name;
			}
			if(scene_GameMapSelector != null){
				scene_GameMapSelector_name = scene_GameMapSelector.name;
			}
		}

		#endif

		//Load Track Map
		//Load Background Image
		//Load Audio Clip		
		/*
void Load () {
			using (ZipFile zip = ZipFile.Read (GameMapPath)) {  
				zip.AlternateEncodingUsage = ZipOption.Always;
				zip.AlternateEncoding = System.Text.Encoding.UTF8;
				foreach (ZipEntry file in zip) {
					string extension = Path.GetExtension (file.FileName).ToLower ();

					if (extension.CompareTo(TrackMap.extension) == 0){
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX

						string jsonData = string.Empty;
						using (var stream = new MemoryStream ()) {
							file.Extract (stream);
							using(var reader = new StreamReader(stream)){
								jsonData = reader.ReadToEnd();						
							}
						}
						currentTrackMap = TrackMap.FromJson(jsonData);

#else
						string path = ConfigUtility.fileLoadPath(string.Format("{0}!/{1}", GameMapPath,file.FileName));
						WWW www = new WWW(path);
						currentTrackMap = TrackMap.FromJson(www.text);
						print(www.text);
#endif
					}else if (extension.CompareTo(ConfigUtility.JpgExtension) == 0|| extension.CompareTo(ConfigUtility.PngExtension) == 0){
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
						using (var stream = new MemoryStream ()) {
							file.Extract (stream);
							byte[] datas = stream.ToArray();
							//Image image = Bitmap.FromStream(stream);
							Bitmap image = new Bitmap(stream);
							var texture = new Texture2D(image.Width,image.Height);
							for(int x = 0;x<image.Width;x++){
								for(int y = 0;y<image.Height;y++){
									System.Drawing.Color color = image.GetPixel(x,image.Height-y-1);
									texture.SetPixel(x,y,new UnityEngine.Color32(color.R,color.G,color.B,color.A));
								}
							}
							image.Dispose ();
							texture.Apply ();
							backgorundImage.mainTexture = texture;
						}
#else
						string path = ConfigUtility.fileLoadPath(string.Format("{0}!/{1}", GameMapPath,file.FileName));
						WWW www = new WWW(path);
						backgorundImage.mainTexture = www.texture;
						print(www.texture);
#endif
					}else if (extension.CompareTo(ConfigUtility.Mp3Extension) == 0|| extension.CompareTo(ConfigUtility.WavExtension) == 0){

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
						using (var stream = new MemoryStream ()) {
							file.Extract (stream);
							byte[] datas = stream.ToArray();
							if (extension.CompareTo(ConfigUtility.Mp3Extension) == 0){									
								audioPlayer.SetClip( NAudioPlayer.FromMp3Data(datas));
							}else if(extension.CompareTo(ConfigUtility.WavExtension)==0){
								WAV wav = new WAV(datas);
								AudioClip audioClip = AudioClip.Create(file.FileName, wav.SampleCount, wav.ChannelCount,wav.Frequency, false);
								audioClip.SetData(wav.LeftChannel, 0);
								audioPlayer.SetClip(audioClip);
							}
						}
#else
						//string path = "file:///" + GameMapPath + "!/" + file.FileName;
						string path = ConfigUtility.fileLoadPath(string.Format("{0}!/{1}", GameMapPath,file.FileName));
						WWW www = new WWW(path);
						AudioClip audioclip = www.audioClip;
						audioPlayer.SetClip(audioClip);
						print(audioclip);
#endif
					}
				}
			}
		}
		*/
	
		public struct GameMapCache{
			public string filePath;
			public bool isTemp;
		}

	}

}
