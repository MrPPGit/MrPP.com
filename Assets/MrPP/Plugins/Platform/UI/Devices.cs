using MrPP.Myth;
using MrPP.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class Devices : MonoBehaviour {
        [SerializeField]
        private DeviceItem _phototype;

        private List<DeviceItem> list_ = new List<DeviceItem>();

        public int getIndex(DeviceItem item) {
            return list_.FindIndex(a => a.id == item.id);
        }
        // Use this for initialization
        void Start () {
            _phototype.gameObject.SetActive(false);
        }

        public void refresh(Hero[] heros, int selected) {
            
            foreach (DeviceItem item in list_) {

                Destroy(item.gameObject);
            }
            list_.Clear();

            foreach (Hero hero in heros) {
               
                DeviceItem item = GameObject.Instantiate(_phototype);
                item.transform.SetParent(this.transform);
                item.transform.position = _phototype.transform.position;
                item.transform.localScale = _phototype.transform.localScale;
                if ( hero.data.id == selected) {
                    item.select();
                }
                item.gameObject.SetActive(true);
                item.name = "UI@"+hero.data.name;
                item.refresh();
                list_.Add(item);
                item.setup(hero.transform, hero.data, hero.state);
            }
          
        }

        public void refresh(DeviceInfoList list)
        {

            foreach (DeviceItem device in list_) {
                DestroyImmediate(device.gameObject);
            }
            list_.Clear();
            foreach (DeviceInfo info in list) {
                DeviceItem item = GameObject.Instantiate(_phototype);
                item.setup(info);
                item.transform.SetParent(this.transform);
                item.transform.position = _phototype.transform.position;
                item.transform.localScale = _phototype.transform.localScale;
                item.gameObject.SetActive(true);
                list_.Add(item);

            }
            //throw new NotImplementedException();
        }

        public bool check()
        {
            bool ret = true;
            bool selected = false;
            foreach (DeviceItem item in list_)
            {

                if (item.state != DeviceItem.State.Ready) {
                    ret = false;
                    break;
                }
                if (item.selected) {
                    if (selected)
                    {
                        ret = false;
                        break;
                    }
                    else {
                        selected = true;
                    }
                }
            }
            
            return ret && selected;
        }
    }
}