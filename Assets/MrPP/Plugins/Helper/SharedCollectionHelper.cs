using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP {
    public class SharedCollectionHelper : GDGeek.Singleton<SharedCollectionHelper> {
        [SerializeField]
        private bool _vuforia = false;
        public bool vuforia {
            get {
                return _vuforia;
            }
        }

        public static GameObject GetGameObject() {
          
           return SharedCollectionHelper.Instance.gameObject;
            
        }


        public static Transform GetTransform()
        {
            
             return SharedCollectionHelper.Instance.transform;
        }
    }
}