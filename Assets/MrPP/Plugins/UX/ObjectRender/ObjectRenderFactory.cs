using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class ObjectRenderFactory : MonoBehaviour {
        [SerializeField]
        private Transform _target;
        public ObjectRender create() {
            GameObject obj = new GameObject("RenderOffset");
            obj.transform.SetParent(this.transform);
           
            ObjectRender or = obj.AddComponent<ObjectRender>();
            or.setup( _target);
            return or;
        }
    }
}