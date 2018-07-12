using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Helper { 
    public class KeyToEvent : MonoBehaviour {

        public KeyCode _key;
        public UnityEngine.Events.UnityEvent _action;
	    // Update is called once per frame
	    void Update () {
            if (Input.GetKeyDown(_key)) {
                if (_action != null) {
                    _action.Invoke();
                }

            }
	    }
    }
}