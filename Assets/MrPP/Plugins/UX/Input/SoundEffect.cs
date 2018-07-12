using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;
using UnityEngine;
namespace MrPP
{
    namespace UX
    {
        public class SoundEffect : MonoBehaviour, IEffect
        {


            [SerializeField]
            private AudioSource _focusExit;
            [SerializeField]
            private AudioSource _focusEnter;
            [SerializeField]
            private AudioSource _inputDown;
            [SerializeField]
            private AudioSource _inputUp;

            public void focusExit()
            {
                if (_focusExit != null) {
                    _focusExit.Play();
                }
            }

            public void focusEnter()
            {

                if (_focusEnter != null)
                {
                    _focusEnter.Play();
                }

            }

           
            public void inputDown()
            {

                if (_inputDown != null)
                {
                    _inputDown.Play();
                }
            }

            public void inputUp()
            {


                if (_inputUp != null)
                {
                    _inputUp.Play();
                }
            }
        }
    }
}
