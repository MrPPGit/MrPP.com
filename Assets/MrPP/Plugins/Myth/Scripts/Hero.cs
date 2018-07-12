using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MrPP.Helper;
using System;
using System.Net;
using UnityEngine.Events;

namespace MrPP.Myth {
    
    public class Hero : NetworkBehaviour
    {

        public UnityEvent _onRefresh;
        [System.Serializable]
        public struct Data {
            [SerializeField]
            public PlatformInfo.Type platform;
            [SerializeField]
            public string ip;
            [SerializeField]
            public string name;

            public int id
            {
                get
                {

                    if (string.IsNullOrEmpty(ip)) {
                        return 0;
                    }
                    return BitConverter.ToInt32(IPAddress.Parse(ip).GetAddressBytes(), 0);
                }
            }


        }

        public enum State{
            Joined,
            Ready,
        }

        [SerializeField]
        [SyncVar(hook = "stateChanged")]
        private State _state;

        public void ready() {
            this.CmdPlayerState(State.Ready);
        }

        public State state
        {
            get
            {
                return _state;
            }
        }

        private void stateChanged(State state)
        {
            _state = state;
            refresh();
        }

        [SerializeField]
        private int _id;



        [SerializeField]
        private uint _netId;



        [SerializeField]
        [SyncVar(hook = "dataChanged")]
        private Data _data;

         
        public Data data
        {
            get
            {
                return _data;
            }
        }

        private void dataChanged(Data data)
        {
            _data = data;
            refresh();
        }
        private void refresh() {
            this.gameObject.name = "Hero@" + _data.name;
            _id = this.data.id;
            if (HerosManager.IsInitialized){
                HerosManager.Instance.refresh(this);
            }
            if (_onRefresh != null) {
                _onRefresh.Invoke();
            }
        }


      

        public override float GetNetworkSendInterval()
        {

            return 0.033f;
        }
        public override void OnStartClient() {
            base.OnStartClient();
            refresh();
            _netId = this.netId.Value;
        }
        public void OnDestroy()
        {

            if (HerosManager.IsInitialized)
            {
                HerosManager.Instance.remove(this);
            }
        }

        [Command(channel = 1)]
        public void CmdPlayerMessage(Data data)
        {
            this._data = data;
        }
        [Command(channel = 1)]
        public void CmdPlayerState(State state)
        {
            this._state = state;
        }
  

//        private NetworkSystem networkDiscovery_ = null;
        private void Start()
        {

//            networkDiscovery_ = NetworkSystem.Instance;
            if (isLocalPlayer)
            {
                
                Data data;
                data.ip = PlatformInfo.LocalIp;

                Debug.Log("!!!" + data.ip);
                data.platform = PlatformInfo.Instance.type;
                data.name = PlatformInfo.LocalComputerName;
                Debug.Log(data.ip);
               // this._data = data;
              //  _id = BitConverter.ToInt32(IPAddress.Parse(data.ip).GetAddressBytes(), 0);
                CmdPlayerMessage(data);

               
            }
       
            if (HerosManager.IsInitialized)
            {
                HerosManager.Instance.add(this);
            }
        }
       
        private void Update()
        {
            if (isLocalPlayer)
            {
                transform.position = Camera.main.transform.position;
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}