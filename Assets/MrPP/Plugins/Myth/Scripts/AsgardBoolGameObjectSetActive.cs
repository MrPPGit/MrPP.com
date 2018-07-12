using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth { 
    public class AsgardBoolGameObjectSetActive : MonoBehaviour {
        [SerializeField]
        private GameObject _gameObject;
        public void setActive(AsgardBool aBool) {
            _gameObject.SetActive(aBool.data);
        }
    }
}