using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Myth {
    public class View : MonoBehaviour
    {

        [SerializeField]
        private InfoBoard _board;
        public InfoBoard board
        {
            get
            {
                return _board;
            }
        }
        [SerializeField]
        private GameObject _anchor;
        public IAnchor anchor {
            get {
                return _anchor.GetComponent<IAnchor>();
            }
        }
    }
}