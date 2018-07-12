using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class PlatformOnOff : MonoBehaviour {

        [SerializeField]
        private PlatformInfo.Type[] _platforms;
	    // Use this for initialization
	    void Awake () {
            bool on = false;
            foreach (PlatformInfo.Type type in _platforms) {
                if (type == PlatformInfo.Instance.type) {
                    on = true;
                    break;
                }
            }
            if (!on) {
                Destroy(this.gameObject);
            }
	    }
	
	    // Update is called once per frame
	    void Update () {
		
	    }
    }
}