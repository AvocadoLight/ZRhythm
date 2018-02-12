using UnityEngine;
using System.Collections;
using System.IO;

namespace BurningxEmpires.ZRhythm.Selector{

	using selector = GameMapSelectorManager;

	public class GameMapElement : MonoBehaviour {

		public GameMapElementsManager manager{
			get{
				return GameMapElementsManager.getInstance;
			}
		}

		public GameMapTempFolder temp;

		public TrackMap trackMap;

		private UILabel m_Label;
		private UITexture m_Texture;

		private bool isInit = false;

		private float offset = 25;

		private float senstive;

		private float move;

		private Vector3 mouseDown;

		void Awake () {
			m_Label = GetComponentInChildren<UILabel>();
			m_Texture = GetComponent<UITexture>();
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {			
			var pos = transform.position;
			var loc = transform.localPosition;
			pos.x = Mathf.Abs(pos.x);
			loc.y = (1 - 1 / transform.lossyScale.x * pos.x / ConfigUtility.width) * offset;
			transform.localPosition = loc;
		}

		public void Init(GameMapTempFolder _temp){
			temp = _temp;
			StartCoroutine(_Init());
		}

		IEnumerator _Init(){

			string path = null;

			path = ConfigUtility.fileLoadPath(
				Path.Combine(
					temp.folderPath,
					temp.chunkHeader.trackMapFileCode
				)
			);

			WWW trackmap = new WWW(	path);
			WWW texture = null;

			path = 	Path.Combine(
				temp.folderPath,
				temp.chunkHeader.backgroundFileCode
			);

			bool hasTexture = File.Exists(path);

			path = ConfigUtility.fileLoadPath(path);

			if(hasTexture)
				texture = new WWW(path);

			yield return trackmap;

			if(hasTexture)
				yield return texture;


			trackMap = TrackMap.FromJson(trackmap.text);

			if(hasTexture)
				m_Texture.mainTexture = texture.texture;

			m_Label.text = trackMap.header.Title + " - " + trackMap.header.Artist;
			isInit = true;
		}

		void OnPress(bool isPress){
			if(isPress){
				mouseDown = Input.mousePosition;
			}else{
				move = Vector3.Distance(mouseDown,Input.mousePosition);
			}
		}

		void OnClick(){
			//Selection
			if(!isInit)
				return;

			if(move>senstive)
				return;

			if(selector.getActiveGameMapElement == this.gameObject)
				return;		

			manager.onElementClick(this);

			print(temp.folderPath);

		}

		public Texture2D getTexture(){
			return m_Texture.mainTexture as Texture2D;
		}
	
	}
}