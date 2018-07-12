using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Example {
    public class Body : MonoBehaviour {

        [SerializeField]
        private Health _health;


        [SerializeField]
        private GameObject _offset;
        public Health health {
            get {
                return _health;
            }
        }

        public void show()
        {
            _offset.SetActive(true);
        }

        public void hide()
        {
            _offset.SetActive(false);
        }
    }
}