using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Myth
{
    public class AsgardTranformLocked : MonoBehaviour, Model.IListener
    {
        [SerializeField]
        private MonoBehaviour[] _behaviours;

        public void Start() {

            Model.Instance.addListener(this);
        }

        public void OnDestroy()
        {
            if (Model.Instance != null) {

                Model.Instance.removeListener(this);
            }
        }



        public void refresh(Model model)
        {
            if (!Altar.AmIGod)
            {
                foreach (MonoBehaviour b in _behaviours)
                {
                    b.enabled = false;
                }
            }
            else
            {
                foreach (MonoBehaviour b in _behaviours)
                {
                    b.enabled = true;
                }
            }
            // LocalPlayer.
        }
    }
}