using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Helper
{
    public class JustEditor : MonoBehaviour
    {
        /*
                public void Awake()
                {


                    this.gameObject.transform.position = new  Vector3(1, 1, 1);

                    transform_position =
                   Transform parent = this.gameObject.transform.parent;
                    this.gameObject.transform.SetParent(WorldAnchor);
                    this.gameObject.transform.localPosition = new Vector3(1, 1, 1);
                    this.gameObject.transform.SetParent(parent);
                    
    }
         */


#if !UNITY_EDITOR
        // Use this for initialization
        void Awake()
        {
                DestroyImmediate(this.gameObject);
        }

#endif

    }
}