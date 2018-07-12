using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Myth { 
    public class HerosManager : GDGeek.Singleton<HerosManager> {
        private Dictionary<uint, Hero> dictionary_ = new Dictionary<uint, Hero>();
       
        public interface IListener {
            void onHeroChanged(Hero hero);
            void onHeroAdd(Hero hero);
            void onHeroRemove(Hero hero);

        }
        private HashSet<IListener> _listeners = new HashSet<IListener>();

        public void addListener(IListener listener) {
            _listeners.Add(listener);
        }
        public void removeListener(IListener listener) {
            _listeners.Remove(listener);
        }
      
        public void add(Hero hero) {
            dictionary_.Add(hero.netId.Value, hero);

            foreach (IListener l in this._listeners) {
                l.onHeroAdd(hero);
            }
        }

        public void remove(Hero hero) {
            dictionary_.Remove(hero.netId.Value);
            foreach (IListener l in this._listeners)
            {
                l.onHeroRemove(hero);
            }
        }

        internal void refresh(Hero hero)
        {
            foreach (IListener l in this._listeners)
            {
                l.onHeroChanged(hero);
            }
        }

        public Hero[] getHeros(HashSet<PlatformInfo.Type> types)
        {

            List<Hero> heros = new List<Hero>();
            foreach (var kv in dictionary_) {
                if (types.Contains(kv.Value.data.platform)) {
                    heros.Add(kv.Value);
                }

            }
            return heros.ToArray();
        }

        public Hero getHeroByNetId(uint id)
        {
            return dictionary_[id];
        }
    }
}