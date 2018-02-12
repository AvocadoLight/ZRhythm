using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class AudioPlayerManager : MonoBehaviour {

		public static AudioPlayerManager getInstance{private set;get;}

		private AudioSource m_AudioSource;

		public AudioSource getAudioSource{
			get{
				return m_AudioSource;
			}
		}

		private AudioListener m_AudioListener;

		public AudioListener getAudioListener{
			get{
				return m_AudioListener;
			}
		}

		public bool hasAudioClip{
			get{
				return (m_AudioSource != null && m_AudioSource.clip != null);
			}
		}

		private Clock m_AudioSourceProgress = new Clock();

		public Clock getProgress{
			get{
				if(!hasAudioClip){
					m_AudioSourceProgress.Set(0);
				}else{
					//m_AudioSourceProgress.Set(getAudioSource.time);
					//better for android
					//   time sample       samples
					//        /       *       /
					//     samples        frequency
					m_AudioSourceProgress.Set((float)getAudioSource.timeSamples / (float)getAudioSource.clip.frequency);
				}
				return m_AudioSourceProgress;
			}
		}

		private Clock m_AudioClipLength = new Clock();

		public Clock getClipLength{
			get{
				if(!hasAudioClip){
					m_AudioClipLength.Set(0);
				}else{
					m_AudioClipLength.Set(getAudioSource.clip.length);
				}
				return m_AudioClipLength;
			}
		}

		public bool isPlaying {
			get{
				return getAudioSource.isPlaying;
			}
		}

		void Awake () {
			if(getInstance == null){
				getInstance = this;
				m_AudioListener = GetComponent<AudioListener> ();
				if(m_AudioListener == null)
					m_AudioListener = gameObject.AddComponent<AudioListener> ();
				m_AudioSource = GetComponent<AudioSource> ();
				if(m_AudioSource == null)
					m_AudioSource = gameObject.AddComponent<AudioSource> ();
			}else{
				Debug.Log(this.GetType().ToString() + " : tow manager is exists delete the second",this);
				Destroy(this);
			}
		}

		public void SetClip (AudioClip clip) {
			getAudioSource.clip = clip;
		}

		public void Seek ( float time) {
			getAudioSource.time = time;
		}

		public void Play () {
			getAudioSource.Play ();
		}

		public void Stop () {
			getAudioSource.Stop ();
		}

		public void Pause () {
			getAudioSource.Pause ();
		}

		public void Continue () {
			getAudioSource.UnPause ();
		}


		//time samples test
		/*void OnGUI () {
			if(!editor.audioPlayer.hasAudioClip)
				return;

			var source = editor.audioPlayer.getAudioSource;

			GUILayout.Label("  time progress:\t" + source.time.ToString());
			GUILayout.Label("  clip length:\t" + source.clip.length.ToString());

			GUILayout.Label("  sample index:\t" + source.timeSamples.ToString());
			GUILayout.Label("  total samples:\t" + source.clip.samples.ToString());

			var percent = (float)source.timeSamples / (float)source.clip.samples;

			var samplesTotime = (float)source.clip.samples / (float)source.clip.frequency;

			GUILayout.Label("  sample to time:\t" + 
				(samplesTotime * percent).ToString()
			);

			GUILayout.Label("  samples to time:\t" + 
				(samplesTotime).ToString()
			);
		}*/

	}
}