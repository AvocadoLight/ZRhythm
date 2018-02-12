using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Game{

	public class GameScorer : MonoBehaviour {

		public static GameScorer getInstance{private set;get;}

		public GameManager manager{get{return GameManager.getInstance;}}

		public int count_Excellent = 0;
		public int count_Good = 0;
		public int count_Bad = 0;
		public int count_Miss = 0;
		public int total_Score = 0;

		void Awake () {
			getInstance = this;
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void AddExcellent (int value) {
			count_Excellent += value;
			AddScore(value * manager.config.score_Excellent);
		}

		public void AddGood (int value) {
			count_Good += value;
			AddScore(value * manager.config.score_Good);
		}

		public void AddBad (int value) {
			count_Bad += value;
			AddScore(value * manager.config.score_Bad);
		}

		public void AddMiss (int value) {
			count_Miss += value;
			AddScore(value * manager.config.score_Miss);
		}

		public void AddScore (int value) {			
			total_Score += value;
		}
	}

}