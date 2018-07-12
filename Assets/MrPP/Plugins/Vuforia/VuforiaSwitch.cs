using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP {
    public class VuforiaSwitch : MonoBehaviour {

        public UnityEvent OnOpen;
        public UnityEvent OnClose;
        [SerializeField]
        private bool _isOpen;
        public bool isOpen {
            get {
                return _isOpen;
            }
        }
        // Use this for initialization
        void Start() {
            _isOpen = Vuforia.VuforiaBehaviour.Instance.enabled;
        }

        public void switchVuforia(){
            _isOpen = Vuforia.VuforiaBehaviour.Instance.enabled;
            _isOpen = !_isOpen;
            Vuforia.VuforiaBehaviour.Instance.enabled = _isOpen;
            refresh();
        }
        private void refresh() {
            if (_isOpen) {
                if(OnOpen != null){
                    OnOpen.Invoke();
                }
            }
            else {

                if (OnClose != null)
                {
                    OnClose.Invoke();
                }
            }
        }



	    
    }
}