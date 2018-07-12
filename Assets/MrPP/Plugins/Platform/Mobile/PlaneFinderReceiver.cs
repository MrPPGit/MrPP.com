using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP{
    public class PlaneFinderReceiver : GDGeek.Singleton<PlaneFinderReceiver>
    {

        public interface IListener{
            void doFound();
            void doLost();

        }

        private HashSet<IListener> listeners_ = new HashSet<IListener>();

        public void addListener(IListener listener){
            
            listeners_.Add(listener);

        }
        public void removeListener(IListener listener){
            listeners_.Remove(listener);

        }
     
        public void onInteractiveHitTest()
        {

        }

        public void onAutomaticHitTest()
        {
            
            automaticHitTest_ = true;

        }
        private bool automaticHitTest_ = false;
        private bool automaticHit_ = false;
        private void LateUpdate()
        {

            //Debug.Log(Camera.main.transform.position);
            if(automaticHit_ != automaticHitTest_){



                automaticHit_ = automaticHitTest_;
                if(automaticHit_){
                    Debug.Log("on hit" + listeners_.Count);
                    foreach(IListener listener in listeners_){
                        listener.doFound();
                    }
                   

                   // if()
                    Debug.Log("on hit");
                }else{

                    foreach (IListener listener in listeners_)
                    {
                        listener.doLost();
                    }
                    Debug.Log("out hit");
                }
            }
            automaticHitTest_ = false;
        }

    }
}