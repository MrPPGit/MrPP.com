using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Platform { 
    public class DeviceInfo : MonoBehaviour {
        public enum State
        {
            Joined,
            Ready,
        };

        public State state;
        public string ip;
        public Transform follow;
        public int id;
        public bool selected;
        public string title;
        public MrPP.PlatformInfo.Type platform;
	   
    }
}