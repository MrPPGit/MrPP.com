using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;
using MrPP.Myth;
using MrPP.Platform;
using UnityEngine;
namespace MrPP { 
    public class MobileServerProcess : MonoBehaviour, HudManager.IListener
    {
        private FSM fsm_ = new FSM();

        



        public void OnDestroy()
        {
            if (HudManager.IsInitialized)
            {
                HudManager.Instance.removeListener(this);
            }
        }




        // Use this for initialization
        void Start () {

            HudManager.Instance.addListener(this);
          
            fsm_.addState("begin", begin());
            fsm_.addState("server", server());
            fsm_.addState("running", running());
            fsm_.init("begin");
               // fsm_.addActiona()
	    }
        public void ready(){
            fsm_.post("ready");

        }
        public void start()
        {
            this.fsm_.post("start");
        }
        private StateBase running()
        {
            State state = new State();
            state.onStart += delegate
            {
                Heimdall.Instance.open();
                HudManager.Instance.running();
            };
            state.onOver += delegate
            {
                Heimdall.Instance.close();
            };
            return state;
        }
        public void select(DeviceItem item)
        {

            fsm_.post("select", item.id);
        }

        private StateBase server()
        {
            State state = TaskState.Create(delegate {


                Task task = new Task();
                TaskManager.AddAndIsOver(task, delegate
                {
                    return Model.Instance.hasGod;
                });
                TaskManager.PushFront(task, delegate
                {
                    //创建服务器
                    NetworkSystem.Instance.host();
                    Debug.Log("server");
                    //显示识别图
                    HudManager.Instance.marking();
                });
                TaskManager.PushBack(task, delegate
                {
                    Yggdrasil.Instance.transform.position = Camera.main.transform.position;
                    Yggdrasil.Instance.transform.rotation = Camera.main.transform.rotation;

                });
                return task;

            }, this.fsm_, "running");


        
            state.addAction("select", delegate (FSMEvent evt)
            {
                Database.Instance.godIndex = (int)(evt.obj);
               


            });
            state.addAction("start", delegate
            {

                WhoIsGod wid = Altar.LocalComponent<WhoIsGod>();
                wid.sheIsGod((uint)(Database.Instance.godIndex));


            });

            //state.addAction("");

            return state;
        }
      
        private StateBase begin()
        {
            State state = new State();
            state.addAction("ready", "server");
            state.onStart += delegate {


                HudManager.Instance.loading();
            };
            state.onOver += delegate {


            };
            return state;
        }

        public void onStart()
        {
            this.start();
        }

        public void onSelected(DeviceItem item)
        {
            this.select(item);
        }
    }
}
