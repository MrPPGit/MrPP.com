using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

namespace MrPP { 
    public class VufriaConfirm : MonoBehaviour {


        public UnityEvent _onFound;
        public UnityEvent _onLost;
        [SerializeField]
        private float _deviation = 5;
        [SerializeField]
        private TrackableHandler[] _handlers;
        private bool previous_ = false;

        // Use this for initialization
        void Start () {
            _handlers = this.gameObject.GetComponentsInChildren<TrackableHandler>();
        }
	
	    // Update is called once per frame
	    void Update () {
            bool result = check();
            if (result != previous_) {
                previous_ = result;
                if (result)
                {
                    if (_onFound != null)
                    {
                        _onFound.Invoke();
                    }
                }
                else {

                    if (_onLost != null)
                    {
                        _onLost.Invoke();
                    }
                }
            }

	    }


        private bool check() {
            if (_handlers.Length < 2) {
                return false;
            }
            foreach (TrackableHandler handler in _handlers) {
                if (handler.status == TrackableBehaviour.Status.NO_POSE) {
                    return false;
                }
            }
            TrackableHandler target = _handlers[0];
            for (int i = 1; i < _handlers.Length; ++i) {
                float angle = Quaternion.Angle(target.transform.rotation, _handlers[i].transform.rotation);
                if (angle > _deviation) {
                    return false;
                }
            }


            return true;
        }
    }

}