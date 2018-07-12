using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Platform { 
    public class Backgroup : MonoBehaviour {

	  
        public void hide()
        {
            this.gameObject.SetActive(false);
        }

        public void show()
        {

            this.gameObject.SetActive(true);
        }
    }
}