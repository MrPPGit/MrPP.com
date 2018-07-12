using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP{
	public class LogConsole : MonoBehaviour {

		// Use this for initialization
		void Start () {

			Application.logMessageReceived += received;
		}
        private void OnApplicationQuit()
        {
            Application.logMessageReceived -= received;
        }
        private void received(string condition, string stackTrace, LogType type){
		
			switch (type) {
			case LogType.Log:
				LogDebug.Log (condition, stackTrace, Color.black);
				break;
			case LogType.Warning:
				LogDebug.Log (condition, stackTrace, Color.yellow);
			break;
			case LogType.Assert:
			case LogType.Error:
			case LogType.Exception:
				LogDebug.Log (condition, stackTrace, Color.red);
				break;
				

			}
		
		}
	}
}