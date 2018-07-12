using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Myth {

    [RequireComponent(typeof(AsgardInt))]
    public class HubFunction : MonoBehaviour {

        public UnityEvent _function;
        private int? number_ = null;

        public void call(AsgardInt aInt) {
            if (number_ == null)
            {
                number_ = aInt.data;
            }
            else {
                if (number_.Value != aInt.data) {
                    for (int i = number_.Value; i < aInt.data; ++i) {
                        if (_function != null)
                        {
                            _function.Invoke();
                        }
                       
                    }
                    number_ = aInt.data;

                }

            }
        }
    }
}