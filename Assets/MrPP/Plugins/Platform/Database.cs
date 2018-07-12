using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP
{
    public class Database : GDGeek.Singleton<Database>
    {
        public interface IListener {
            void godIndexChange(int godIndex);
        }
        private HashSet<IListener> listeners_ = new HashSet<IListener>();
        public void addListener(IListener listner) {
            listeners_.Add(listner);
        }
        public void removeListener(IListener listener) {
            listeners_.Remove(listener);
        }
        [SerializeField]
        private int _godIndex = -1;
        public int godIndex
        {
            get
            {
                return _godIndex;
            }
            set
            {
                _godIndex = value;
                foreach (IListener listener in listeners_){
                    listener.godIndexChange(_godIndex);
                }
            }
        }
    }
}
