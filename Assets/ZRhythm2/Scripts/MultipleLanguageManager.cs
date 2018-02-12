using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace BurningxEmpires.ZRhythm{
	
	public class MultipleLanguageManager : MonoBehaviour {

		public bool parse = false;
		public bool creatCsvLngFile = false;
		[Multiline(20)]
		public string text;

		public CSV csv;

		private static MultipleLanguageManager m_Instance;

		public static MultipleLanguageManager getInstance{
			get{
				if(m_Instance == null){
					m_Instance = (new GameObject(" Multiple Language Manager")).AddComponent<MultipleLanguageManager>();
				}
				return m_Instance;
			}
		}

		void Awake () {
			if(m_Instance == null){
				m_Instance = this;
			}else if (m_Instance != this){
				print("a MLM has exsit");
				Destroy(this.gameObject);
			}
			if(parse){
				csv = new CSV(text);
			}else{
				csv = new CSV(1,1);
			}
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			text = csv.ToString();
			if(creatCsvLngFile){
				creatCsvLngFile = false;
				File.WriteAllText(Path.Combine(ConfigUtility.persistentDataPath,"CHN.lng"),csv.ToString());
			}
		}

		public void Value(string key,string language,string content){
			if(!csv.HasColumn(language)){
				csv.AddColumn(language);
			}
			if(!csv.HasRow(key)){
				csv.AddRow(key);
			}
			csv.SetValue(key,language,content);
		}

		public void AddColumn(string arg){		
			csv.AddColumn(arg);
		}

		public void AddRow(string arg){
			csv.AddRow(arg);
		}
	}
}