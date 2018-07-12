using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MrPP{
	public class LogMessage : GDGeek.Singleton<LogMessage> {
		[SerializeField]
		private Text _text;
		public void show(string message){
			_text.text = "(<color=#00ff0f>"+message+"</color>)";
		}
	}
}