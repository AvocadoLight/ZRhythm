using UnityEngine;
using System.Collections;

public class PopupLoading : MonoBehaviour {

	public static GameObject m_gameobject;

	public static GameObject Creat(){
		if(m_gameobject == null)
			m_gameobject = Resources.Load<GameObject>(
				"Prefabs/PopupLoading");
		Transform parent = GameObject.Find("UI Root").transform;
		GameObject m = Instantiate<GameObject>(m_gameobject);
		if(parent != null)
			m.transform.SetParent(parent);
		m.transform.localScale = Vector3.one;
		return m;
	}


}
