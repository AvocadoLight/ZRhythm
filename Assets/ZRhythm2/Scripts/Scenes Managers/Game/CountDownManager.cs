using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownManager : MonoBehaviour {

	public static CountDownManager getInstance{private set;get;}

	public Transform text_3;
	public Transform text_2;
	public Transform text_1;

	public float progressValue{
		get{
			return progress_detail + progress_header;
		}
	}

	private float progress_detail;

	private int progress_header;

	public bool isDone = false;

	void Awake () {
		getInstance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartCountDown () {
		isDone = false;
		StartCoroutine(CountDown());
	}

	void sleep () {
		text_1.gameObject.SetActive(false);
		text_2.gameObject.SetActive(false);
		text_3.gameObject.SetActive(false);
	}

	IEnumerator CountDown () {
		progress_header = 2;
		sleep();
		text_3.gameObject.SetActive(true);
		yield return StartCoroutine(tween (text_3));
		progress_header = 1;
		sleep();
		text_2.gameObject.SetActive(true);
		yield return StartCoroutine(tween (text_2));
		progress_header = 0;
		sleep();
		text_1.gameObject.SetActive(true);
		yield return StartCoroutine(tween (text_1));
		sleep();
		progress_detail = 0;
		isDone = true;
	}

	IEnumerator tween (Transform trans){
		float timer = 1;
		while(timer > 0){
			timer -= Time.deltaTime;
			trans.localScale = Vector3.one * (timer + 1);
			progress_detail = timer;
			yield return null;
		}
		progress_detail = 0;
	}


}
