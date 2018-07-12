using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.Networking;
namespace MrPP {
    [RequireComponent(typeof(NetworkDiscoveryReceiver), typeof(NetworkManager))]
    public class NetworkSystem : GDGeek.Singleton<NetworkSystem> {


        [SerializeField]
        private string _stateName;
        private FSM fsm_ = new FSM();

        /// <summary>
        /// Class to track discovered session information.
        /// </summary>
        public class SessionInfo
        {
            public string name;
            public string ip;
        }
        /// <summary>
        /// Keeps track of current remote sessions.
        /// </summary>
        private SessionInfo sessions_;

        public SessionInfo sessions
        {
            get
            {
                return sessions_;
            }
        }

        /// <summary>
        /// Called by UnityEngine when a broadcast is received. 
        /// </summary>
        /// <param name="fromAddress">When the broadcast came from</param>
        /// <param name="data">The data in the broad cast. Not currently used, but could
        /// be used for differentiating rooms or similar.</param>
        public void receivedBroadcast(string fromAddress, string data)
        {
            Debug.Log(fromAddress);
            Debug.Log(data);
            string serverIp = fromAddress.Substring(fromAddress.LastIndexOf(':') + 1);

            sessions_ = new SessionInfo() { ip = serverIp, name = data };

            if (SessionChanged != null)
            {
                SessionChanged.Invoke();
            }
         
        }


        public UnityEvent SessionChanged;
        /// <summary>
        /// Event raised when connected or disconnected.
        /// </summary>
       // public UnityEvent ConnectionStatusChanged;




        /// <summary>
        /// Keeps track of the IP address of the system that sent the 
        /// broadcast.  We will use this IP address to connect and 
        /// download anchor data.
        /// </summary>
        public SessionInfo server { get; private set; }


        [SerializeField]
        private NetworkDiscoveryReceiver _discovery;
      
        private void OnDestroy()
        {
            _discovery.onReceiver -= receivedBroadcast;
        }
        public void host()
        {
            fsm_.post("host");
        }
        public void listening()
        {
            fsm_.post("listening");
        }
        private void Awake()
        {

            if (_discovery == null)
            {
                _discovery = this.gameObject.GetComponent<NetworkDiscoveryReceiver>();
            }


            _discovery.onReceiver += receivedBroadcast;


            fsm_.addState("await", awaitState());
            fsm_.addState("host", hostState());
            fsm_.addState("alone", aloneState());
            fsm_.addState("listening", listeningState());
            fsm_.addState("client", clientState());
            fsm_.init("await");
            // Add our computer name to the broadcast data for use in the session name.
            _discovery.broadcastData = PlatformInfo.LocalComputerName + '\0';
           // NetworkServer.Reset();

           
        }

        private StateBase aloneState()
        {
            State state = new State();
            state.onStart += delegate {

                _stateName = "host state;";
                Debug.Log(" i am hero");
                NetworkManager.singleton.serverBindToIP = true;

                NetworkManager.singleton.StartHost();


            };

            state.onOver += delegate
            {
                NetworkManager.singleton.StopHost();
            };
            return state;
        }

        private StateBase awaitState()
        {
            State state = new State();
            state.addAction("host", "host");
            state.addAction("listening", "listening");
            return state;
        }

        private StateBase clientState()
        {
            State state = new State();
            state.onStart += delegate
            {
                _stateName = "client state;";
                NetworkManager.singleton.networkAddress = server.ip;
                NetworkManager.singleton.StartClient();
            };
            state.onOver += delegate
            {

                NetworkManager.singleton.StopClient();
            };
            return state;
        }

        private StateBase listeningState()
        {
            State state = new State();
            state.onStart += delegate {
                _stateName = "clinet state;";
                sessions_ = null;
                _discovery.Initialize();
                _discovery.StartAsClient();
            };
            state.addAction("join", delegate (FSMEvent evt)
            {
                SessionInfo session = (SessionInfo)(evt.obj);
                this.server = session;
                return "client";
            });
            state.addAction("host", "host");
            state.addAction("alone", "alone");
            state.onOver += delegate
            {
                _discovery.StopBroadcast();
            };
            return state;
        }

        private StateBase hostState()
        {
            State state = new State();
            state.onStart += delegate {

                _stateName = "host state;";
                Debug.Log(" i am hero");
                NetworkManager.singleton.serverBindToIP = true;

                NetworkManager.singleton.StartHost();


                _discovery.Initialize();
                _discovery.StartAsServer();


              


            };

            state.addAction("client", "client");
            state.onOver += delegate
            {
                NetworkManager.singleton.StopHost();
                _discovery.StopBroadcast();
            };
            return state;
        }


        /// <summary>
        /// Tracks if we are currently connected to a session.
        /// </summary>
        public bool connected
        {
            get
            {
                // We are connected if we are the server or if we aren't running discovery
               return (_discovery.isServer || !_discovery.running);
            }
        }

        public bool running {
            get {

                return _discovery.running;
            }

        }

        public void alone()
        {

            fsm_.post("alone");
        }
        public void join() {
            if (sessions_ != null) { 
                join(sessions_);
            }
        }
        public void join(SessionInfo session)
        {
            fsm_.post("join", session);
        }

        /// <summary>
        /// Call to join a session
        /// </summary>
        /// <param name="session">Information about the session to join</param>
        private void clientState(SessionInfo session)
        {
            //stopListening();
            // We have to parse the server IP to make the string friendly to the windows APIs.
            server = session;
            NetworkManager.singleton.networkAddress = server.ip;

            // And join the networked experience as a client.
            NetworkManager.singleton.StartClient();


            // if (ConnectionStatusChanged != null)
            //  {
            //     ConnectionStatusChanged.Invoke();
            // }
        }


    }
}