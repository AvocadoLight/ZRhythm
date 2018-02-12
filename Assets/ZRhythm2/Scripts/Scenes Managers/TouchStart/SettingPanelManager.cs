using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm{
	
	public class SettingPanelManager : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void Show () {
			gameObject.SetActive(true);
		}

		public void Hide () {
			gameObject.SetActive(false);
		}
	}

}