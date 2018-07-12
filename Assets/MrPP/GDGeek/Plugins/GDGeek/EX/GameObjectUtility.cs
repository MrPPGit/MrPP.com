using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek {

    public static class GameObjectUtility {
        public static void enableBehaviour<T>(this GameObject obj) where T : MonoBehaviour
        {
            T[] list = obj.GetComponents<T>();
            foreach (T t in list)
            {
                t.enabled = true;
            }

        }

        public static void disableBehaviour<T>(this GameObject obj) where T : MonoBehaviour
        {
            T[] list = obj.GetComponents<T>();
            foreach (T t in list)
            {
                t.enabled = false;
            }

        }
        public static void destroyComponent<T>(this GameObject obj) where T : Component
        {

            T[] list = obj.GetComponents<T>();
            foreach (T t in list) {
                GameObject.DestroyImmediate(t);
            }

        }
        public static T AskComponent<T>(this GameObject obj) where T:Component  
	    {
            
		    T component = obj.GetComponent<T>();
		    if (component == null) {
			    component = obj.AddComponent<T> ();
		    }
		    return component;
	
	    }

	    public static void show(this GameObject obj) {
            Debug.Log("我不是很喜欢这个接口，因为show指代的是显示，而active会影响更多，这两个不是一个意思，这个是别人加入到这个库里面的，下个版本会取消这个接口");
		    obj.SetActive(true);	
	    }

	    public static void hide(this GameObject obj) {

            Debug.Log("我不是很喜欢这个接口，因为hide指代的是显示，而active会影响更多，这两个不是一个意思，这个是别人加入到这个库里面的，下个版本会取消这个接口");
            obj.SetActive(false);
	    }

        public static Renderer getRenderer(this GameObject obj)
        {
                return obj.gameObject.GetComponent<Renderer>();

        }

        public static Collider getCollider(this GameObject obj) {

             return obj.gameObject.GetComponent<Collider>();
        }
	   

    }
}