using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Game{
	public class GameEffectManager : MonoBehaviour {

		public static GameEffectManager getInstance{private set;get;}

		public float fadeTime = 0.25f;

		[SerializeField]
		private GameObject noteFadingPrefab;

		[SerializeField]
		private GameObject noteExcellentPrefab;

		[SerializeField]
		private GameObject noteGoodPrefab;

		[SerializeField]
		private GameObject noteBadPrefab;

		[SerializeField]
		private GameObject noteMissPrefab;

		[SerializeField]
		private Transform root;

		void Awake () {
			getInstance = this;
		}

		void Start () {
			root.DestroyChildren();
		}

		public void NoteFade (Vector3 position) {
			GenerateEffect(position,noteFadingPrefab);
		}

		public void NoteExcellent (Vector3 position) {
			GenerateEffect(position,noteExcellentPrefab);
		}

		public void NoteGood (Vector3 position) {
			GenerateEffect(position,noteGoodPrefab);
		}

		public void NoteBad (Vector3 position) {
			GenerateEffect(position,noteBadPrefab);
		}

		public void NoteMiss (Vector3 position) {
			GenerateEffect(position,noteMissPrefab);
		}

		void GenerateEffect (Vector3 position,GameObject prefab) {
			var effect = Instantiate(prefab,root) as GameObject;
			effect.transform.localScale = Vector3.one;
			effect.transform.localPosition = position;
			Destroy(effect,fadeTime);
		}

	}

}