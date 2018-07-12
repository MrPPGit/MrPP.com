using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Myth { 
    public  class Bifrost : MonoBehaviour {
        public bool amIGod
        {
            get
            {
                return Altar.AmIGod;

            }
        }
        public UnityEvent _onOpen;
        public UnityEvent _onClose;
        public virtual void close()
        {
            if (_onClose != null)
            {
                _onClose.Invoke();

            }
        }
        public virtual void open() {
            if (_onOpen != null) {
                _onOpen.Invoke();

            }
        }
    }
}