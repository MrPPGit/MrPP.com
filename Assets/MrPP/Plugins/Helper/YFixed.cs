using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MrPP.Helper { 
    public class YFixed : MonoBehaviour {

        [SerializeField]
        private Transform _target;
        [SerializeField]
        private Transform _old;

        public Transform target {
            get {
                return _target;
            }
        }
        private float distance_ = 0.0f;
        public void Awake()
        {
            distance_ = Vector3.Distance(this.transform.position, target.position);
        }
        // Update is called once per frame
        void LateUpdate () {
            if (_target != null) {
               // float distance = Vector3.Distance(this.transform.position, target.position);
                Vector3 position = _old.position;
                position.y = target.position.y;
                Ray ray = new Ray(target.position, position- target.position);
                Debug.DrawRay(target.position, position- target.position);
                this.transform.position = ray.GetPoint(distance_);
                //this.transform.position = position;
            }
	    }
    }
}