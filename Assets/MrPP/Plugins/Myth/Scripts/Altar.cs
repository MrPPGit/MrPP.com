using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Myth
{ 
    public class Altar : NetworkBehaviour
    {

        private static Altar Instance_;
        public static Altar Instance
        {
            get
            {
                return Instance_;
            }

        }

        public void Start()
        {

            if (this.isLocalPlayer)
            {
                Instance_ = this;
            }
        }
        public static T LocalComponent<T>()
        {
            if (Instance_ != null) {
                return Instance_.gameObject.GetComponent<T>();
            }
            return default(T);
        }
        public static bool AmIGod
        {
            get
            {
                WhoIsGod wig = Altar.LocalComponent<WhoIsGod>();
                if (wig != null && !wig.isItMe())
                {
                    return false;
                }
                return true;

            }
        }
    }
}