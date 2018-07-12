using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Helper { 
    [ExecuteInEditMode]
    public class SphereClip : MonoBehaviour {
	    [SerializeField]
	    private Transform _sphere = null;
	    private Vector4 sphere_;
	    private Vector4 getSphere(){
		    var point = _sphere.position;
		    var a = point.x;
		    var b = point.y;
		    var c = point.z;
		    var r = _sphere.localScale.x/2;
		    //var d = -(a*point.x+b*point.y+c*point.z); 
		    return new Vector4 (a,b,c,r);
	    }
	    // Update is called once per frame
	    void Update () {
		    Vector4 sphere = getSphere ();

		    if (sphere != sphere_) {
			    sphere_ = sphere;
			    Renderer[] renderers = this.GetComponentsInChildren<Renderer> ();
                foreach(Renderer renderer in renderers){
                    Material[] materials = renderer.sharedMaterials;
                    if (materials != null) { 
                        foreach (Material material in materials) {
                            if (material != null) { 
                                material.SetVector("_Sphere", sphere_);
                            }
                        }
                    }

                }

		    }

	    }
    }
}