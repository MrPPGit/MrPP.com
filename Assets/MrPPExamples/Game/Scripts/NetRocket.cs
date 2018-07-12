using MrPP.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Example { 

    public class NetRocket : NetworkBehaviour {

        [SerializeField]
        private Collider _collider;
        private float time_;



        [SerializeField]
        private float maxAge_ = 4.0f;
        // Use this for initialization
        [ServerCallback]
        void Start () {
            time_ = 0f;
        }

        [ServerCallback]
        void Update () {
            time_ += Time.deltaTime;
            if (time_ > maxAge_) {

                Myth.Furnace.NetworkDestory(this.gameObject);
            }
            // _collider.isTrigger

        }

        [ServerCallback]
        void OnCollisionEnter(Collision collision)
        {
           GameObject obj = collision.gameObject;
            
            PlayerHandler ph = obj.GetComponent<PlayerHandler>();
            if (ph != null) {
                ph.player.damage(1);
            }
            Myth.Furnace.NetworkDestory(this.gameObject);
        }
    }
}