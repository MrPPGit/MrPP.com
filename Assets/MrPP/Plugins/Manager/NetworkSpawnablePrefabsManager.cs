using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP {

    [RequireComponent(typeof(NetworkManager))]
    public class NetworkSpawnablePrefabsManager : GDGeek.Singleton<NetworkSpawnablePrefabsManager> {

        private Dictionary<string, GameObject> prefabs_ = new Dictionary<string, GameObject>(); 
        public void Start()
        {
           
            foreach (GameObject obj in NetworkManager.singleton.spawnPrefabs) {
                Debug.Log(obj.name);
                prefabs_[obj.name] = obj;
            }
        }

        public GameObject getPrefab(string type)
        {
            Debug.Log(name);
            return prefabs_[type];
        }

        public GameObject create(string type)
        {
            return Instantiate(getPrefab(type));
        }
    }
}