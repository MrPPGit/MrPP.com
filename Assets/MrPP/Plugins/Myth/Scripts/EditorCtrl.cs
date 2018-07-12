using GDGeek;
using MrPP.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth {
    public class EditorCtrl : MonoBehaviour, CtrlInterface
    {

        //private MrNetworkDiscovery networkDiscovery_;
        private FSM fsm_ = new FSM();
        void Start() {

          //  networkDiscovery_ = MrNetworkDiscovery.Instance;
            fsm_.addState("IAmServer", IAmServer());
            fsm_.addState("whoIsGod", whoIsGod());
            fsm_.addState("running", running());
            fsm_.init("IAmServer");
        }

        private StateBase IAmServer()
        {

            State state = TaskState.Create(delegate {
                Task task = new Task();
             
                task.isOver = delegate()
                {
                    if (NetworkSystem.Instance.running) {
                        return true;
                    }
                    return false;
                };
                TaskManager.PushFront(task, delegate {
                    Root.Instance.view.board.state = InfoBoard.State.Editor;//.SetActive(false);
                    Root.Instance.view.anchor.hide();
                });
                TaskManager.PushBack(task, delegate {
                    if (NetworkSystem.Instance.running)
                    {
                        NetworkSystem.Instance.host();//
                       // networkDiscovery_.startHosting("MrPP.Editor");
                    }

                });

                return task;

            }, this.fsm_, "whoIsGod");

            return state;
        }

        private StateBase whoIsGod()
        {


            State state = TaskState.Create(delegate {
                Task task = new Task();
                TaskManager.AddAndIsOver(task, delegate
                {

                    return Model.Instance.hasGod;
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
                Root.Instance.view.board.state = InfoBoard.State.Hide;
                Heimdall.Instance.open();
            };

            state.onOver += delegate
            {
                Heimdall.Instance.close();
            };
            return state;
        }

       
        public void post(FSMEvent evt)
        {
            this.fsm_.post(evt);
        }
    }
}