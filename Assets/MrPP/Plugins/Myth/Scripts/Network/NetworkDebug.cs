using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Myth { 

    public class NetworkDebug : NetworkBehaviour, GDGeek.IExecute
    {

        [SerializeField]
        private NetworkIdentity[] _ids;
        public void execute() {
            _ids =  GameObject.FindObjectsOfType<NetworkIdentity>();
        }
    
        public override void OnStartClient() {
            GDGeek.Task task = new GDGeek.TaskWait(5.0f);
            GDGeek.TaskManager.PushBack(task, delegate
            {
                // _ids = GameObject.FindObjectsOfType<NetworkIdentity>();
                foreach (var i in _ids)
                {
                    if (i == null) {
                        _ids = GameObject.FindObjectsOfType<NetworkIdentity>();
                        break;
                    }
                  //  Debug.Log("name:" + i.gameObject.name + ",id:" + i.netId.Value.ToString());
                }
                foreach (var i in _ids)
                {
                    Debug.Log("name:" + i.gameObject.name + ",id:" + i.netId.Value.ToString());
                }
            });
            GDGeek.TaskManager.Run(task);

        }
      
    }
}