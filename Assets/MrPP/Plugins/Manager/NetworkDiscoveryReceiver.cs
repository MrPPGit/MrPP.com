using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace MrPP { 
    public class NetworkDiscoveryReceiver : NetworkDiscovery
    {


        public delegate void Receiver(string fromAddress, string data);
        public event Receiver onReceiver;


        /// <summary>
        /// Called by UnityEngine when a broadcast is received. 
        /// </summary>
        /// <param name="fromAddress">When the broadcast came from</param>
        /// <param name="data">The data in the broad cast. Not currently used, but could
        /// be used for differentiating rooms or similar.</param>
        public override void OnReceivedBroadcast(string fromAddress, string data)
        {
            base.OnReceivedBroadcast(fromAddress, data);
            Debug.Log("?!!!!");
            if(onReceiver != null)
            {
                Debug.Log("onReceiver");
                onReceiver(fromAddress, data);
            }
            
        }
      
    }
}