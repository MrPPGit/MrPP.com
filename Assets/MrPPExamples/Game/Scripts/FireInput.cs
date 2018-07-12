using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Example { 
    public class FireInput : MonoBehaviour {

        public void fire() {
            NetPlayer player =  MrPP.Myth.Altar.LocalComponent<NetPlayer>();
            if (player != null) {
                player.fire();
            }
        }
    }
}