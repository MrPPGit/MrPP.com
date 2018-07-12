using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MrPP { 
    public class _SharedCollectionVuforia : MonoBehaviour {

        public void follow(Transform transform) {

#if UNITY_UWP
            UnityEngine.XR.WSA.WorldAnchor worldAnchor = this.GetComponent<UnityEngine.XR.WSA.WorldAnchor>();
            if (worldAnchor != null)
            {
                DestroyImmediate(worldAnchor);
            }
#endif
            this.transform.SetParent(transform);
            this.transform.localPosition = Vector3.zero;
            this.transform.localEulerAngles = Vector3.zero;// (0, 0, 0);
        }
        public void anchorage() {
            this.transform.SetParent(null);
#if UNITY_UWP

            this.gameObject.AddComponent<UnityEngine.XR.WSA.WorldAnchor>();
#endif
        }
    }
}