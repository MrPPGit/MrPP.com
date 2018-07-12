using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Platform { 
    public class HeroRefresh : MonoBehaviour, HerosManager.IListener, Database.IListener
    {

        [SerializeField]
        private PlatformInfo.Type[] _showTypes;

        private HashSet<PlatformInfo.Type> showTypes_ = null;

        public void OnDestroy()
        {
            if (HerosManager.IsInitialized)
            {
                HerosManager.Instance.removeListener(this);
            }
            if (Database.IsInitialized)
            {
                Database.Instance.removeListener(this);
            }
        }



       


       public void Start()
        {

            showTypes_ = new HashSet<PlatformInfo.Type>();
            foreach (PlatformInfo.Type type in _showTypes)
            {
                showTypes_.Add(type);
            }

            if (HerosManager.IsInitialized)
            {
                HerosManager.Instance.addListener(this);
            }
            if (Database.IsInitialized)
            {
                Database.Instance.addListener(this);
            }

        }
        public void onHeroAdd(Hero hero)
        {
            Hero[] heros = HerosManager.Instance.getHeros(showTypes_);

            
            if (heros == null || heros.Length < 1)
            {
                return;
            }
            
            this.refresh(heros);
        }

        public void onHeroChanged(Hero hero)
        {
            Hero[] heros = HerosManager.Instance.getHeros(showTypes_);
            if (heros == null || heros.Length < 1)
            {
                return;
            }
            this.refresh(heros);
        }

        public void onHeroRemove(Hero hero)
        {
            Hero[] heros = HerosManager.Instance.getHeros(showTypes_);
            if (heros == null || heros.Length < 1)
            {
                return;
            }
            this.refresh(heros);
        }
        private void readHero(Hero hero, ref DeviceInfo info) {
            switch(hero.state)
            {
                case Hero.State.Joined:
                    info.state = DeviceInfo.State.Joined;
                    break;
                case Hero.State.Ready:
                    info.state = DeviceInfo.State.Ready;
                    break;
            }
            info.ip = hero.data.ip;
            info.title = hero.data.name;
            info.platform = hero.data.platform;
            info.follow = hero.transform;
            info.id = hero.data.id;
            if (info.id == Database.Instance.godIndex)
            {
                info.selected = true;
            }
            else {
                info.selected = false;
            }


        }
        private void refresh(Hero[] heros)
        {
         
            DeviceInfoList infoList = HudManager.Instance.infoList;
            infoList.clear();
            foreach (Hero hero in heros) {
                DeviceInfo info = infoList.create();
                readHero(hero,ref info);
            }
            HudManager.Instance.barRefresh();
        }

        public void godIndexChange(int godIndex)
        {
            Hero[] heros = HerosManager.Instance.getHeros(showTypes_);
            if (heros == null || heros.Length < 1)
            {
                return;
            }
            refresh(heros);
        }
    }
}