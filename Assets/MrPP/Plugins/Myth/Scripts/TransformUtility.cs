using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth { 
    public static class TransformUtility{

        public static void setAsgardPosition(this Transform transform, Vector3 position)
        {
            Transform parent = transform.parent;
            transform.SetParent(Yggdrasil.Instance.transform);
            transform.localPosition = position;
            transform.SetParent(parent);

        }


        public static Vector3 getAsgardPosition(this Transform transform)
        {
            Transform parent = transform.parent;
            transform.SetParent(Yggdrasil.Instance.transform);
            Vector3 ret = transform.localPosition;
            transform.SetParent(parent);
            return ret;
        }

        public static void setAsgardRotation(this Transform transform, Quaternion rotation)
        {
            Transform parent = transform.parent;
            transform.SetParent(Yggdrasil.Instance.transform);
            transform.localRotation = rotation;
            transform.SetParent(parent);

        }

        public static void setAsgardTransform(this Transform transform, Vector3 position, Quaternion rotation) {
            Transform parent = transform.parent;
            transform.SetParent(Yggdrasil.Instance.transform);
            transform.localRotation = rotation;
            transform.localPosition = position;
            transform.SetParent(parent);
            
        }
        public static Quaternion getAsgardRotation(this Transform transform)
        {
            Transform parent = transform.parent;
            transform.SetParent(Yggdrasil.Instance.transform);
            Quaternion ret = transform.localRotation;
            transform.SetParent(parent);
            return ret;
        }
    }
}