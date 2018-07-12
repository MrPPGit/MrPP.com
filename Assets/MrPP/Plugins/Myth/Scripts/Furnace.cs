using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Myth { 
    public class Furnace : NetworkBehaviour {

        private static Furnace Instance_;
        private static List<INetworkCreaterParameter> Prestrain_ = new List<INetworkCreaterParameter>();
        public static Furnace Instance
        {
            get
            {
             
                return Instance_;
            }

        }

        public override void OnStartClient()
        {
            Debug.Log("Start!!!" + this.gameObject.name);

        }
        public void Start()
        {
            Debug.Log("Start");
            if (this.isLocalPlayer)
            {
                Instance_ = this;

                foreach (INetworkCreaterParameter parameter in Prestrain_) {
                    Create(parameter);
                }
            }
        }
      

        [Command]
        public void CmdCreate(string type, byte[] bytes)
        {
            GameObject prefab = NetworkSpawnablePrefabsManager.Instance.getPrefab(type);
            GameObject instance = NetworkSpawnablePrefabsManager.Instance.create(type);// Instantiate(prefab) as GameObject;;

            INetworkCreater create = instance.GetComponent<INetworkCreater>();
            if (create != null && bytes != null) {
                create.reader.readFrom(bytes);
            }
            NetworkServer.Spawn(instance);

        }


        [Command]
        public void CmdDestory(GameObject obj)
        {
            NetworkServer.Destroy(obj);
        }

        public static void NetworkDestory(GameObject obj)
        {
            // GameObject prefab = NetworkSpawnablePrefabsManager.Instance.getPrefab(name);
            if (Furnace.Instance != null)
            {
                Furnace.Instance.CmdDestory(obj);
            }
            else {
                GameObject.Destroy(obj);
            }
        }
        public static void Create(string type)
        {
            Create(new NetworkCreaterNoneParameter(type));
        }
        public static void Create(INetworkCreaterParameter parameter = null) {

           
            if (Furnace.Instance == null)
            {
                Prestrain_.Add(parameter);
            }
            else {
                Debug.Log(Furnace.Instance);
                byte[] bytes = null;
                if (parameter != null)
                {
                    IMessageWriter writer = parameter.writer;
                    if (writer != null) {

                        bytes = writer.writeTo();
                    }
                }
                    
                Furnace.Instance.CmdCreate(parameter.type, bytes);
            }
          
        }
    }
}