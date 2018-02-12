using UnityEngine;
using System.Collections;
using System.IO;

using System.Runtime;
using System.Runtime.InteropServices;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using NAudio;
using NAudio.Wave;

namespace BurningxEmpires.ZRhythm.Selector{

	public class AudioClipPlayer : MonoBehaviour {

		public static AudioClipPlayer Instance;

		public static PlayerState state;
		public static AudioClipPlayer.PlayMode mode = AudioClipPlayer.PlayMode.none;

		private AudioSource mAudioSource;

		private IWavePlayer mWaveOutDevice;
		private WaveStream mMainOutputStream;
		private WaveChannel32 mVolumeStream;

		void Awake(){
			Instance = this;
			mAudioSource = this.gameObject.AddComponent<AudioSource>();
			state = new PlayerState();
			mode = PlayMode.none;
		}

		// Use this for initialization
		void Start ()
		{

		}

		/// <summary>
		/// 未實作
		/// <para></para>
		/// Plaies the effect sound.
		/// </summary>
		/// <param name="oneShot">If set to <c>true</c> one shot.</param>
		public void PlayEffectSound (AudioClip audio,bool oneShot = true)
		{
			if(oneShot){
				Instance.mAudioSource.PlayOneShot(audio);
			}else{
				Instance.mAudioSource.Stop();
				Instance.mAudioSource.clip = audio;
				Instance.mAudioSource.Play();
			}
		}

		// Update is called once per frame
		void Update ()
		{
			//private IWavePlayer mWaveOutDevice;
			//private WaveStream mMainOutputStream;
			//private WaveChannel32 mVolumeStream;	

			state = PlayerState.Zero;

			if(Application.platform == RuntimePlatform.Android){
				if(mAudioSource.isPlaying){
					state.lengthSeconds = mAudioSource.clip.length;
					state.currentSeconds = mAudioSource.time;
				}
			}else{
				if(mode == PlayMode.waveOutDevice){
					if(mMainOutputStream != null){
						if(mWaveOutDevice != null){
							if(mWaveOutDevice.PlaybackState == PlaybackState.Playing){
								state.lengthSeconds = (float)mMainOutputStream.TotalTime.TotalSeconds;
								state.currentSeconds = (float)mMainOutputStream.CurrentTime.TotalSeconds;
								return;
							}
						}
					}
				}else if(mode == PlayMode.audioSource){
					state.lengthSeconds = mAudioSource.clip.length;
					state.currentSeconds = mAudioSource.time;
				}
			}
		}

		public static void PlayAudio(AudioClip audioclip){
			Instance.UnloadAudio();
			Instance.mAudioSource.clip = audioclip;
			Instance.mAudioSource.Play();
			mode = PlayMode.audioSource;
		}

		public static void PlayAudioWithData (byte[] data){		
			Instance.UnloadAudio ();
			if(data == null)
				return;
			
			if(Instance.LoadAudioFromData (data)){				
				Instance.mWaveOutDevice.Play ();
				mode = PlayMode.waveOutDevice;	
			}else{				
				try{
				Instance.mAudioSource.clip = NAudioPlayer.FromMp3Data(data);
				Instance.mAudioSource.Play();
				mode = PlayMode.audioSource;
				}catch(System.Exception ex) {
					Debug.Log ("Error! " + ex.Message);
				}
			}

		}

		private bool LoadAudioFromData (byte[] data)
		{
			try {
				MemoryStream tmpStr = new MemoryStream (data);
				mMainOutputStream = new Mp3FileReader (tmpStr);
				mVolumeStream = new WaveChannel32 (mMainOutputStream);

				mWaveOutDevice = new WaveOut ();
				mWaveOutDevice.Init (mVolumeStream);

				return true;
			} catch (System.Exception ex) {
				Debug.Log ("Error! " + ex.Message);
			}
			return false;
		}

		void OnDestroy ()
		{
			UnloadAudio ();
		}

		void OnApplicationFocus( bool focusStatus )
		{

		}

		void OnApplicationPause( bool pauseStatus )
		{
			if(pauseStatus)
				PasueAudio();
			else
				PlayAudio();
		}

		public void OnApplicationQuit ()
		{
			//UnloadAudio();
		}

		private void PlayAudio(){
			if (mWaveOutDevice == null) return;
			if (mWaveOutDevice.PlaybackState == PlaybackState.Playing)return;
			mWaveOutDevice.Play();
		}

		private void PasueAudio(){

			if(mAudioSource != null && mode == PlayMode.audioSource)
				mAudioSource.Pause();

			if (mWaveOutDevice != null) {
				mWaveOutDevice.Pause();
			}
			//if (mMainOutputStream != null) {
			// this one really closes the file and ACM conversion
			//mVolumeStream.Close ();
			//mVolumeStream = null;
			//
			// this one does the metering stream
			//mMainOutputStream.Close ();
			//mMainOutputStream = null;
			//}
			//if (mWaveOutDevice != null) {
			//	mWaveOutDevice.Dispose ();
			//	mWaveOutDevice = null;
			//}
		}

		private void UnloadAudio ()
		{
			if(mAudioSource != null)
				mAudioSource.Stop();

			if (mWaveOutDevice != null) {
				mWaveOutDevice.Stop ();
			}
			if (mMainOutputStream != null) {
				// this one really closes the file and ACM conversion
				mVolumeStream.Close ();
				mVolumeStream = null;

				// this one does the metering stream
				mMainOutputStream.Close ();
				mMainOutputStream = null;
			}
			if (mWaveOutDevice != null) {
				mWaveOutDevice.Dispose ();
				mWaveOutDevice = null;
			}

		}


		[System.Serializable]
		public struct PlayerState{
			public float currentSeconds;
			public float lengthSeconds;
			public float position{
				get{
					return currentSeconds/lengthSeconds;
				}
			}
			public int currectInstanceSeconds {
				get{
					return Mathf.FloorToInt( currentSeconds % 60f);		
				}
			}
			public int currectInstanceMinutes{
				get{
					return Mathf.FloorToInt( currentSeconds / 60f);		
				}
			}
			public int totalInstanceSeconds {
				get{
					return Mathf.FloorToInt( lengthSeconds % 60f);		
				}
			}
			public int totalInstanceMinutes{
				get{
					return Mathf.FloorToInt( lengthSeconds / 60f);		
				}
			}

			public static PlayerState Zero{
				get{
					PlayerState state;
					state.currentSeconds = 0;
					state.lengthSeconds = 0;
					return state;
				}
			}
		}

		public enum PlayMode:int{
			none 			= 0,
			audioSource 	= 1,
			waveOutDevice 	= 2
		}
	}
}