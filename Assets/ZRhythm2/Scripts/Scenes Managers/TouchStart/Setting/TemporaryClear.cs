using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using  BurningxEmpires.ZRhythm.Tools;

namespace BurningxEmpires.ZRhythm{
	
	public class TemporaryClear : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void onClick () {
			if(UnityEngine.Application.platform == RuntimePlatform.Android){
				/*AndroidTool.AlertDialogBox(
					"警告",
					"這個動作會刪除暫存資料夾的所有檔案和資料夾,確定要繼續嗎?",
					"確定",
					Clear);*/
				var box = new AlertDialogBox(
					"警告",
					"這個動作會刪除暫存資料夾的所有檔案和資料夾,確定要繼續嗎?",
					(num)=>{Clear();}); 
				box.setButton(AlertDialogBox.BUTTON.POSITIVE,"確定");
				box.Show();
			}else{
				Clear ();
			}
		}

		void Clear () {

			DirectoryInfo directory = 
				new DirectoryInfo(ConfigUtility.temporaryPath);

			//delete files:
			foreach (FileInfo file in directory.GetFiles()) 
				file.Delete();
			//delete directories in this directory:
			//print(directory.Name);
			foreach (DirectoryInfo subDirectory in directory.GetDirectories()){
				//print(subDirectory.Name +" "+ directory.Name);
				subDirectory.Delete(true);
			}

			print("已清除所有暫存資料");
			if(Application.platform == RuntimePlatform.Android)
				AndroidTool.MakeToast("已清除所有暫存資料");

		}
	}

}