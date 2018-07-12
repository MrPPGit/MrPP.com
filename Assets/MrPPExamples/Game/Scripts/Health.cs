using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Example { 
    public class Health : MonoBehaviour {

        [SerializeField]
        private GameObject[] _points;
	    

        public void refresh(int health)
        {
            for (int i = 0; i < _points.Length; ++i) {
                _points[i].gameObject.SetActive(i < health);
            }
        }
    }
}