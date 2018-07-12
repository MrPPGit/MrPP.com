using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Myth {
    public class WhoIsGod : NetworkBehaviour
    {



        [Command]
        public void CmdSheIsGod(uint id) {
            Model.Instance.godId = id;
        }

        //god is a girl
        public void sheIsGod(uint id) {
            CmdSheIsGod(id);
        }
        public void IAmGod()
        {

            CmdSheIsGod(this.netId.Value);
        }
        public bool isItMe()
        {
            
            if (Model.Instance.hasGod) {
                Hero hero = Altar.LocalComponent<Hero>();
                return Model.Instance.godId == hero.data.id;
            }
            return true;
            
        }
    }
}