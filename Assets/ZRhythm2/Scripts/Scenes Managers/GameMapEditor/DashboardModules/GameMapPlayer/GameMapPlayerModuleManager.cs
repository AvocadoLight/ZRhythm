using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	//hen 像是錄音機播放按鈕的辣個
	public class GameMapPlayerModuleManager : BlockModuleManager {

		public static GameMapPlayerModuleManager getInstace{private set;get;}

		public BlockElement_Button_Continue button_continue;
		public BlockElement_Button_Pause button_pause;
		public BlockElement_Button_Play button_play;
		public BlockElement_Button_Record button_record;
		public BlockElement_Button_Stop button_stop;

		void Awake () {
			if(getInstace == null){
				getInstace = this;		
			}else{
				Debug.Log(this.GetType().ToString() + " : tow manager is exists delete the second",this);
				Destroy(this);
			}
		}

		void Start () {
			updateState ();
		}

		void Update () {
			updateState ();
		}

		public void updateState () {

			var hasAudio = editor.audioPlayer.hasAudioClip;
			var isPlaying = editor.audioPlayer.isPlaying;


			button_record.SetEnable( hasAudio && !isPlaying );
			button_continue.SetEnable( hasAudio );
			button_play.SetEnable( hasAudio );
		
			button_continue.SetActive( !isPlaying );
			button_play.SetActive( !isPlaying );

			button_pause.SetActive( isPlaying );
			button_stop.SetActive( isPlaying );

		}

		public void Seek ( float time) {
			editor.audioPlayer.Seek (time);
			updateState ();
		}

		public void Play () {
			if(editor.audioPlayer.hasAudioClip){
				editor.audioPlayer.Play ();
			}
			updateState ();
		}

		public void Stop () {
			editor.audioPlayer.Stop ();
			updateState ();
		}

		//TODO:完成功能
		public void Pause () {
			editor.audioPlayer.Stop ();
			updateState ();
		}

		public void Continue () {

			if(editor.audioPlayer.hasAudioClip){
				float gridMax =(float)editor.getTrackMapGridPositionMax;
				float gridNow =(float)editor.getCurrentNoteGridPosition;
				float gridPos= 0;

				if(gridMax<=0 || gridNow <0)
					gridPos=0;
				else
					gridPos = gridNow/gridMax;
				
				Seek(editor.audioPlayer.getClipLength.totalSeconds * gridPos);

				editor.audioPlayer.Play ();
			}

			updateState ();
		}

	}
}