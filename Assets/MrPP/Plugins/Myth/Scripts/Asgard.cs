using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth
{
    public class Asgard : MonoBehaviour
    {
        private bool _enable = false;
        // Use this for initialization
        void Start()
        {
            homing();
        }
        void homing() {
            if (!_enable)
            {
                if (Yggdrasil.IsInitialized)
                {
                    this.transform.SetParent(Yggdrasil.Instance.transform);
                    _enable = true;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            homing();
        }
    }
}