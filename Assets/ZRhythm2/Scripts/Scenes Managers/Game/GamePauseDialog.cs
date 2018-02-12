using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Game{
	public class GamePauseDialog : MonoBehaviour {

		public GameManager manager{
			get{
				return GameManager.getInstance;	
			}
		}

		public static GamePauseDialog getInstance{get;private set;}

		void Awake () {
			getInstance = this;
		}

		void Start () {
			Close();
		}

		public void Close (){
			gameObject.SetActive(false);
			manager.audioPlayer.Continue ();
		}

		public void Open (){
			gameObject.SetActive(true);
			manager.audioPlayer.Pause ();
		}


	}
}