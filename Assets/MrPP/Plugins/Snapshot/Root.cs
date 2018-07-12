using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP
{
    namespace Snapshot
    {
        public class Root : GDGeek.Singleton<Root>
        {
            internal void deleteAllTarget()
            {
                Target[] targets = this.gameObject.GetComponentsInChildren<Target>();
                for (int i = 0; i < targets.Length; ++i) {
                    
                    Destroy(targets[i].gameObject);
                }
            }
            /*
            internal void deleteAllTargetNetworkVersion()
            {
                Target[] targets = this.gameObject.GetComponentsInChildren<Target>();
                for (int i = 0; i < targets.Length; ++i)
                {
                  	  targets[i].GetComponent<NetworkCreater>().DoDestroy();
                }
            }*/
        }
    }
}
