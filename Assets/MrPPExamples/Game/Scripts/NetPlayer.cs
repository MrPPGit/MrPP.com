using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Example { 
    public class NetPlayer : NetworkBehaviour {

        [SerializeField]
        private Body _iPad;


        [SerializeField]
        private Body _Hololens;

        private Body platform_;


        [SyncVar(hook = "healthChange")]
        private int _health = 10;



        private void healthChange(int health) {
            Debug.Log("health" + health);
            _health = health;
            platform_.health.refresh(_health);
            //_healthBar.refresh(_health);
        }


        [Command]
        void CmdFire() {
            
            MrPP.Myth.Furnace.Create(new NetRocketCreater.Parameter(this.netId.Value));
        }
	    // Use this for initialization
	    void Start () {
            _iPad.gameObject.SetActive(false);
            _Hololens.gameObject.SetActive(false);
            platform_ = _Hololens;

            platform_.gameObject.SetActive(true);
            if (!this.isLocalPlayer)
            {

                platform_.show();
                this.platform_.gameObject.layer = LayerMask.NameToLayer("Enemy");
            }
            else
            {
                platform_.hide();
                this.platform_.gameObject.layer =  LayerMask.NameToLayer("Body");
            }
        }
        [ServerCallback]
        public void damage(int power) {
            _health -= power;
        }
        public void fire() {
            if (this.isLocalPlayer)
            {
                CmdFire();
            }
        }
        
    }
}