using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour {

	public static GameResultManager getInstance{private set;get;}

	public static string currentGameMapName;
	public static int count_Excellent = 0;
	public static int count_Good = 0;
	public static int count_Bad = 0;
	public static int count_Miss = 0;
	public static int total_Score = 0;

	public UILabel text_currentGameMapName;
	public UILabel text_Excellent;
	public UILabel text_Good;
	public UILabel text_Bad;
	public UILabel text_Miss;
	public UILabel text_Score;

	// Use this for initialization
	void Start () {
		text_currentGameMapName.text = currentGameMapName;
		text_Excellent.text = count_Excellent.ToString();
		text_Good.text = count_Good.ToString();
		text_Bad.text = count_Bad.ToString();
		text_Miss.text = count_Miss.ToString();
		text_Score.text = total_Score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Goto_GameMapSelector () {
		LoadScene (scene_GameMapSelector_name);
	}

	public void LoadScene (string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	#if UNITY_EDITOR

	public UnityEditor.SceneAsset scene_GameMapSelector;

	#endif

	public string scene_GameMapSelector_name;

	#if UNITY_EDITOR

	void OnValidate () {
		if(scene_GameMapSelector != null){
			scene_GameMapSelector_name = scene_GameMapSelector.name;
		}
	}

	#endif
}
