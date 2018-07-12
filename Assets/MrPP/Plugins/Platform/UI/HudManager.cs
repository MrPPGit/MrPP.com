using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrPP.Platform {
    public class HudManager : GDGeek.Singleton<HudManager>
    {
        public interface IListener {

            void onStart();
            void onSelected(DeviceItem item);
        }

        private HashSet<IListener> listeners_ = new HashSet<IListener>();

        public void addListener(IListener listener)
        {
            listeners_.Add(listener);
        }
        public void removeListener(IListener listener)
        {
            listeners_.Remove(listener);
        }


        public void start()
        {
            foreach (IListener listener in listeners_)
            {
                listener.onStart();
            }
        }
        public void selected(DeviceItem item)
        {
            foreach (IListener listener in listeners_) {
                listener.onSelected(item);
            }
        }

        
      
        [SerializeField]
        private Backgroup _backgroup;
        [SerializeField]
        private Loading _loading;
        [SerializeField]
        private Mark _mark;
        [SerializeField]
        private Bar _bar;


        [SerializeField]
        private DeviceInfoList _list;

        public DeviceInfoList infoList {
            get {
               return _list;
            }
        }

        public void selecting()
        {
            _backgroup.show();
            _loading.hide();
            _mark.hide();
            _bar.hide(false);
        }

        public void loading()
        {
            _backgroup.hide();
            _loading.show();
            _mark.hide();
            _bar.hide(false);
            Debug.Log("loading");


        }
        public void marking()
        {
            _backgroup.show();
            _mark.show();
            _loading.hide();
            _bar.show(false);
            Debug.Log("marking");
        }

        public void running()
        {
            _backgroup.hide();
            _mark.hide();
            _loading.hide();
            _bar.hide(true);
            Debug.Log("running");
        }
        public void showBar() {
            _bar.show(true);
        }

        public void hideBar() {
            _bar.hide(true);
        }
        public void barRefresh() {
            _bar.refresh(_list);
        }
    }
}