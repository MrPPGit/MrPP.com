using GDGeek;
using MrPP.Myth;
using MrPP.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

namespace MrPP.Platform
{
    public class HoloLensProcess : MonoBehaviour, StartTarget.IListener
    {

        private FSM fsm_ = new FSM();
        private UnityAction _sessionListChanged;
        [SerializeField]
        private string _stateName;
        private string stateName {
            set {
                Debug.LogWarning("state:" + value);
                _stateName = value;
            }
        }

        void OnDestroy() {
            if (StartTarget.IsInitialized) {
                StartTarget.Instance.removeListener(this);
            }
            if (_sessionListChanged != null && NetworkSystem.IsInitialized && NetworkSystem.Instance.SessionChanged != null)
            {
                NetworkSystem.Instance.SessionChanged.RemoveListener(_sessionListChanged);
            }


        }
        // Use this for initialization
        
        void Start()
        {
            fsm_.addState("listening", listening());
            fsm_.addState("alone", alone());
            fsm_.addState("join", join());
            fsm_.addState("scanning", scanning());
            fsm_.addState("wait", wait());
            fsm_.addState("running", running());

            fsm_.init("listening");

            _sessionListChanged = new UnityAction(onSessionListChanged);
            NetworkSystem.Instance.SessionChanged.AddListener(_sessionListChanged);
            StartTarget.Instance.addListener(this);
        }

        private StateBase alone()
        {
            State state = TaskState.Create(delegate {
                Task task = new Task();
                TaskManager.PushFront(task, delegate
                {
                    NetworkSystem.Instance.alone();
                });
                return task;
            }, this.fsm_, "running");
            return state;
        }

       
        private StateBase scanning()
        {
            State state = new State();
            state.onStart += delegate
            {
                VuforiaBehaviour.Instance.enabled = true;
            };
            state.addAction("found", delegate(FSMEvent evt)
            {
                Transform tran = (Transform)(evt.obj);
                Yggdrasil.Instance.transform.position = tran.position;
                Yggdrasil.Instance.transform.rotation = tran.rotation;
                Hero hero = Myth.Altar.LocalComponent<Hero>();
                hero.ready();

                return "wait";
            });
            state.onOver += delegate
            {

                VuforiaBehaviour.Instance.enabled = false;
            };
            return state;
        }

      
            
		void onSessionListChanged(){
            fsm_.post("session");

        }
        private StateBase running()
        {
            State state = new State();
            state.onStart += delegate
            {
                this.stateName = "running";
                Heimdall.Instance.open();
            };
            state.onOver += delegate
            {
                Heimdall.Instance.close();
            };
            return state;
        }

        private StateBase wait()
        {
            State state = TaskState.Create(delegate {

                stateName = "wait";
                Task task = new Task();
                TaskManager.AddAndIsOver(task, delegate
                {
                    return Model.Instance.hasGod;
                });
              
                return task;

            }, this.fsm_, "running");

            return state;
        }
      
        private StateBase join()
        {
            State state = TaskState.Create(delegate {

                this.stateName = "join";
                Task task = new GDGeek.TaskWait(0.3f);
                TaskManager.PushBack(task, delegate
                {
                    NetworkSystem.SessionInfo session = NetworkSystem.Instance.sessions;
                   
                    if (session != null && NetworkSystem.Instance.running)
                    {
                        NetworkSystem.Instance.join(session);
                    }
                });
                return task;
            }, this.fsm_, "scanning");


            return state;

        }

        private StateBase listening()
        {
            State state = TaskState.Create(delegate {
                return new TaskWait(5);
            }, this.fsm_, "alone");

            state.onStart += delegate
            {
                HudManager.Instance.gameObject.SetActive(false);
                this.stateName = "listening";
                NetworkSystem.Instance.listening();
            };
            state.addAction("session", "join");
            return state;
        }

        public void onFound(Transform transform)
        {
            this.fsm_.post("found", transform);
        }

        public void onLost()
        {
           
        }
    }
}