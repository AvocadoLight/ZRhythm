using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  BurningxEmpires.ZRhythm.Tools;
using System.Windows.Forms;

namespace BurningxEmpires.ZRhythm{
	
	public class TemporaryPathSelector : MonoBehaviour {

		public UILabel Content;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			Content.text = ConfigUtility.temporaryPath;
		}
		public void onSelect () {
			if(UnityEngine.Application.platform == RuntimePlatform.Android){
				//要求API level 21
				AndroidFilePicker.PickFolder((path)=>{
					ConfigUtility.temporaryPath = path;
					//AndroidTool.MakeToast("路徑:" + path);
				});
			}else{
				var openFolder = 
					OpenFolder.Open(
						"請選擇一個資料夾"
					);
				var result = openFolder.ShowDialog();
				if( result == DialogResult.OK )
				{
					var path = openFolder.SelectedPath;
					ConfigUtility.temporaryPath = path;
					//print(path);
				}
			}
		}
	}
}