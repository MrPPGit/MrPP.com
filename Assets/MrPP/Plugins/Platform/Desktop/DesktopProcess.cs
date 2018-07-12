using GDGeek;
using MrPP.Myth;
using MrPP.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP { 
    public class DesktopProcess : MonoBehaviour, HudManager.IListener
    {

        private FSM fsm_ = new FSM();




        public void OnDestroy()
        {
            if (HudManager.IsInitialized)
            {
                HudManager.Instance.removeListener(this);
            }
          
        }

        void Start ()
        {
         
            
            HudManager.Instance.addListener(this);
            fsm_.addState("server", server());
            fsm_.addState("running", running());
          

            fsm_.init("server");

        }

        public void start() {
            this.fsm_.post("start");
        }
        private StateBase running()
        {
            State state = new State();
            state.onStart += delegate
            {
                HudManager.Instance.running();
                Heimdall.Instance.open();
            };
            state.onOver += delegate
            {
                Heimdall.Instance.close();
            };
            return state;
        }
        public void selected(DeviceItem item) {
            
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


                    //refreshHero();
                    //创建服务器
                    NetworkSystem.Instance.host();

                    //显示识别图
                    HudManager.Instance.marking();
                    //Desktop.Instance.hud.showImageTarget();
                });
                TaskManager.PushBack(task, delegate
                {
                    //Desktop.Instance.hud.closeImageTarget();
                    //NetworkSystem.Instance.s
                });
                return task;

            }, this.fsm_, "running");


        
            state.addAction("select", delegate(FSMEvent evt)
            {
                Database.Instance.godIndex = (int)(evt.obj);


            });
            state.addAction("start", delegate
            {

                WhoIsGod wid = Altar.LocalComponent<WhoIsGod>();
                wid.sheIsGod((uint)(Database.Instance.godIndex));
                //设置控制者，并切换状态。

            });
          
            //state.addAction("");
           
            return state;
        }

        public void onStart()
        {
            this.start();
        }

        public void onSelected(DeviceItem item)
        {
            this.selected(item);
        }

        public void onCreateServer()
        {
            throw new NotImplementedException();
        }
    }
}