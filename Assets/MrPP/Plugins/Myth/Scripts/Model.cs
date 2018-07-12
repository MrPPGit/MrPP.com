using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace MrPP.Myth { 
  
    public class Model : NetworkBehaviour
    {

        public interface IListener {

            void refresh(Model model);
        }
        private HashSet<IListener> listeners_ = new HashSet<IListener>();
        public void addListener(IListener listener) {
            listeners_.Add(listener);
        }
        public void removeListener(IListener listener) {
            listeners_.Remove(listener);
        }
        static Model Instance_ = null;

        
        static public Model Instance
        {
            get {
                return Instance_;
            }
        }

        private void Awake()
        {
            Instance_ = this;
        }
        private bool hasGod_ = false;
        public bool hasGod {
            get {
                return hasGod_;
            }
        }

        public uint godId
        {
            get
            {
                return _godId;
            }
            set
            {
                _godId = value;
            }
        }
        [SerializeField]
        [SyncVar(hook = "changeGodId")]
        private uint _godId = 0;
        private void changeGodId(uint id)
        {
            _godId = id;
            hasGod_ = true;
            refresh();

        }
        void refresh() {
            foreach (IListener listener in listeners_) {
                listener.refresh(this);
            }

        }
    }
}