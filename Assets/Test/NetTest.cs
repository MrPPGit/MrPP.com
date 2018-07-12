using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class NetTest : MonoBehaviour {

	    // Use this for initialization
	    void Start () {
            NetworkSystem.Instance.host();
	    }
	
	    // Update is called once per frame
	    void Update () {
		
	    }
    }
}