using GDGeek;
//using MrPP.Helper.SharingWithUNET;
using MrPP.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MrPP.Myth { 
    public class DeviceCtrl : MonoBehaviour, CtrlInterface
    {
        private FSM fsm_ = new FSM();
        [SerializeField]
        private string _stateName = string.Empty;

        private NetworkSystem networkSystem_;
        // Use this for initialization
        void Start () {
            networkSystem_ = NetworkSystem.Instance;
          
           // fsm_.addState("logo", logo());
            fsm_.addState("device", device());
            fsm_.addState("whoIsServer", whoIsServer(), "device");
            fsm_.addState("IAmClient", IAmClient(), "device");
            fsm_.addState("alone", alone(), "device");
            fsm_.addState("whoIsGod", whoIsGod(), "device");
            fsm_.addState("running", running(), "device");
            fsm_.init("whoIsServer");

        }
        /*
        private StateBase logo()
        {
            State state = TaskState.Create(delegate {
                Task task = new TaskWait(1.0f);
                TaskManager.PushFront(task, delegate {

                    Root.Instance.view.anchor.hide();
                    Root.Instance.view.board.state = InfoBoard.State.Logo;
                });
                return task;

            }, this.fsm_, "whoIsServer");
            return state;
        }*/
        bool _hasAnchor = false;
        private StateBase device()
        {

            State state = new State();
            state.onStart += delegate
            {
                Root.Instance.view.anchor.hide();
            };
            state.addAction(Ctrl.TrackingFoundEvent, delegate(FSMEvent evt) {
                Transform ts = (Transform)(evt.obj);
#if UNITY_UWP
                UnityEngine.XR.WSA.WorldAnchor wa = Root.Instance.view.anchor.gameObject.GetComponent<UnityEngine.XR.WSA.WorldAnchor>();
                GameObject.DestroyImmediate(wa);
#endif
                Root.Instance.view.anchor.gameObject.transform.SetParent(ts);
                Root.Instance.view.anchor.gameObject.transform.localPosition = Vector3.zero;
                Root.Instance.view.anchor.gameObject.transform.localEulerAngles = Vector3.zero;
                Root.Instance.view.anchor.gameObject.transform.SetParent(Root.Instance.view.transform);
#if UNITY_UWP
                Root.Instance.view.anchor.gameObject.gameObject.AddComponent<UnityEngine.XR.WSA.WorldAnchor>();
#endif
                Root.Instance.view.anchor.show();
                _hasAnchor = true;
                //Root.Instance.view.board.state = InfoBoard.State.Hide;
            });

            state.addAction(Ctrl.GodEvent, delegate (FSMEvent evt)
            {
                uint id = (uint)evt.obj;
                Model.Instance.godId = id;
            });
            return state;
        }


        
        private StateBase alone()
        {
            State state = TaskState.Create(delegate {
                WhoIsGod wid = null;
                Task task = new Task();
                TaskManager.AddAndIsOver(task, delegate
                {

                    wid = Altar.LocalComponent<WhoIsGod>();
                    return wid != null;
                });
                TaskManager.PushBack(task, delegate
                {
                    wid.IAmGod();
                });
                return task;

            }, this.fsm_, "running");
            return state;
        }


        private StateBase running()
        {
            State state = new State();
            state.onStart += delegate
            {

                Root.Instance.view.anchor.go();
                Root.Instance.view.board.state = InfoBoard.State.Hide;
                _stateName = "running";
                Debug.Log("state:" + _stateName);
                Heimdall.Instance.open();
            };
            state.onOver += delegate
            {
                Heimdall.Instance.close();
            };
            return state;
        }

     

        private StateBase whoIsGod()
        {
            State state = TaskState.Create(delegate {
                if (_hasAnchor)
                {
                    Root.Instance.view.board.state = InfoBoard.State.Hide;
                }
                else {
                    Root.Instance.view.board.state = InfoBoard.State.Scan;
                }
               
                Task task = new Task();
                TaskManager.AddAndIsOver(task, delegate
                {
                    return Model.Instance.hasGod;
                });
                return task;

            }, this.fsm_, "running");

            state.addAction(Ctrl.IAmGod, delegate
            {
                WhoIsGod wid = Altar.LocalComponent<WhoIsGod>();
                wid.IAmGod();
            });
            state.onStart += delegate
            {
                _stateName = "whoIsGod";

                Debug.Log("state:" + _stateName);
            };
           
         
            return state;
        }
        private StateBase IAmClient() {

            State state =  TaskState.Create(delegate {

                Task task =  new GDGeek.TaskWait(0.3f);
                TaskManager.PushBack(task, delegate
                {
                    NetworkSystem.SessionInfo sessionInfo = this.getSessionInfo();
                    if (sessionInfo != null && networkSystem_.running)
                    {
                        networkSystem_.join(sessionInfo);
                    }
                });
                return task;
            }, this.fsm_, delegate {
                if (Model.Instance.hasGod)
                {
                    return "running";
                }
                return "whoIsGod";
            });

           
            return state;


        }
        private StateBase whoIsServer()
        {
            State state = new State();

            state.addAction(Ctrl.TrackingFoundEvent, delegate (FSMEvent evt) {

                if (_hasAnchor)
                {
                    Root.Instance.view.board.state = InfoBoard.State.Hide;

                }
            });


            state.onStart += delegate
            {
                _stateName = "whoIsServer";

                Root.Instance.view.anchor.stop();
                Debug.Log("state:" + _stateName);
                
                Root.Instance.view.board.state  = InfoBoard.State.ScanAndAlone;
            };
            state.addAction(Ctrl.GetOtherServerAddress, "IAmClient");

            state.addAction(Ctrl.IAmServer, delegate
            {
                if (networkSystem_.running)
                {
                    networkSystem_.host();
                   
                    return "whoIsGod";

                }
                return "";
            });

            state.addAction(Ctrl.StartEvent, delegate
            {
                if (networkSystem_.running)
                {
                    networkSystem_.host();
                    return "alone";
                }
                return "";
            });
           
            state.onOver += delegate
            {
                Root.Instance.view.anchor.wait();
            };
            return state;
        }


        private NetworkSystem.SessionInfo getSessionInfo()
        {
            return networkSystem_.sessions;
            /*
            var sessionList = networkSystem_.remoteSessions;
            NetworkSystem.SessionInfo ret = null;

            if (sessionList != null && sessionList.Count > 0) {
                foreach (var session in sessionList)
                {
                    if (session.Value != null) {
                        if (ret == null)
                        {
                            ret = session.Value;
                        }
                       
                    }
                }
                return ret;


            }

            return null;*/
        }
        /*
        private bool serverIsEditor(MrNetworkDiscovery.SessionInfo sessionInfo)
        {
            if (sessionInfo != null && sessionInfo_.name == "MrPP.Editor") {
                return true;
            }
            return false;
        }*/
      
      
        public void post(FSMEvent evt)
        {
            this.fsm_.post(evt);
        }
    }
}