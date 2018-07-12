using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP{
    public class PlaneFinderListener : MonoBehaviour, PlaneFinderReceiver.IListener {
        public UnityEvent _onFound;
        public UnityEvent _onLost;
        private bool listening_ = false;
        public void Start()
        {
            if(PlaneFinderReceiver.IsInitialized){
                PlaneFinderReceiver.Instance.addListener(this);
                listening_ = true;
            }else{

                Debug.LogError("can't add listener!");
            }
        }


        public void OnDestroy()
        {
            if(PlaneFinderReceiver.IsInitialized){
                PlaneFinderReceiver.Instance.removeListener(this);
            }
        }
        public void doFound(){

            Debug.Log("do found!");
            if(_onFound != null){
                _onFound.Invoke();
                Debug.Log("plane found!!!");

            }

        }
        public void doLost(){
            if(_onLost != null){
                _onLost.Invoke();

            }

        }
    	
    }
}