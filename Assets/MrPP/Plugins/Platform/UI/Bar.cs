using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MrPP.Platform
{
    public class Bar : MonoBehaviour
    {





        [SerializeField]
        private StartButton _start;


        [SerializeField]
        private GameObject _showButton;


        [SerializeField]
        private GameObject _hideButton;


        [SerializeField]
        private GameObject _show;
        [SerializeField]
        private GameObject _hide;
        [SerializeField]
        Devices _devices;

        public void hide(bool button)
        {
            _show.hide();
            _hide.show();
            _hideButton.hide();
            if (button)
            {
                _showButton.show();
            }
            else {

                _showButton.hide();
            }
        }

        public void show(bool button)
        {
            _show.show();
            _hide.hide();
            _showButton.hide();
            if (button)
            {
                _hideButton.show();
            }
            else
            {

                _hideButton.hide();
            }
        }

        public void refresh(DeviceInfoList list)
        {
            _devices.refresh(list);
            _start.refresh(list);
        }
    }

}