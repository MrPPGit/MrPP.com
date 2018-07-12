using MrPP;
using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Creater : NetworkBehaviour
{


    public override void OnStartClient()
    {

        Debug.Log("OnStartClient!!!");
    }
    // Use this for initialization
    void Start ()
    {
        if (!this.isServer) { 
            MrPP.Myth.Furnace furnace = MrPP.Myth.Altar.LocalComponent<MrPP.Myth.Furnace>();
            Furnace.Create(new HeadCreater.Parameter(12345));
        }
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
