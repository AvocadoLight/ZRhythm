using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Game{
	
	public class GameConfigManager : MonoBehaviour {

		public static GameConfigManager getInstance{private set;get;}

		//seconds
		public float range_Excellent = 0.15f;
		public float range_Good = 0.25f;
		public float range_Bad = 0.5f;
		//values
		public int score_Excellent = 5;
		public int score_Good = 3;
		public int score_Bad = 1;
		public int score_Miss = 0;

		void Awake () {
			getInstance = this;
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}

}