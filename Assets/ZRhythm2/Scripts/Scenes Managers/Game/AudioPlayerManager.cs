using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Game{

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

					if(!isPlaying){						
						return m_AudioSourceProgress;
					}
					float progress = (float)getAudioSource.timeSamples / (float)getAudioSource.clip.frequency;
					m_AudioSourceProgress.Set(progress);
				
					if(float.IsNaN(m_AudioSourceProgress.totalSeconds)){
						m_AudioSourceProgress.Set (0);
					}
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
			getInstance = this;

			m_AudioListener = GetComponent<AudioListener> ();
			if(m_AudioListener == null)
				m_AudioListener = gameObject.AddComponent<AudioListener> ();
			m_AudioListener.hideFlags = HideFlags.HideInInspector;

			m_AudioSource = GetComponent<AudioSource> ();
			if(m_AudioSource == null)
				m_AudioSource = gameObject.AddComponent<AudioSource> ();		
			m_AudioSource.hideFlags = HideFlags.HideInInspector;

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
	}

}