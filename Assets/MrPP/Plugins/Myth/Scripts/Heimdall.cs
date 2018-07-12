//using MrPP.Helper.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MrPP.Myth { 
    public class Heimdall : GDGeek.Singleton<Heimdall> {

	    // Use this for initialization
	    void Awake ()
        {
            close();
        }
        public void open()
        {
            //openCamera();
            //openInput();

            Bifrost[] bifrosts = GameObject.FindObjectsOfType<Bifrost>();
            foreach (Bifrost bifrost in bifrosts) {

                bifrost.open();
            }
            //Bifrost
        }

        public void close()
        {
            Bifrost[] bifrosts = GameObject.FindObjectsOfType<Bifrost>();
            foreach (Bifrost bifrost in bifrosts)
            {
                bifrost.close();
            }
           // closeInput();
            //closeCamera();
        }

       


    }
}