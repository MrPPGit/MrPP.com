using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MrPP.Platform { 
    public class StartButton : MonoBehaviour {

        [SerializeField]
        private Button _button;
        [SerializeField]
        private Text _text;


        public void close() {
            _text.color = Color.gray;
            _button.enabled = false;
        }
        public void open() {

            _text.color = Color.black;
            _button.enabled = true;
        }
        public void refresh(DeviceInfoList list)
        {

            bool check = true;
            bool hasGod = false;
            foreach (DeviceInfo info in list) {
                if (info.state == DeviceInfo.State.Joined) {
                    check = false;
                    break;
                }
                if (Database.Instance.godIndex == info.id) {
                    hasGod = true;
                }
            }


            if (check && hasGod)
            {
                open();
            }
            else {
                close();
            }
        }
    }
}