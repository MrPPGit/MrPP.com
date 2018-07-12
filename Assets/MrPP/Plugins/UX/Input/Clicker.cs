#if UNITY_WSA

using HoloToolkit.Unity.InputModule;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using GDGeek;

namespace MrPP
{
    namespace UX
    {
        public class Clicker : MonoBehaviour, GDGeek.IExecute
#if UNITY_WSA
            ,IInputClickHandler, IInputHandler
#endif
        {



            [SerializeField]
            private bool _globalListener = false;


            public UnityEvent OnClicked;
            public void execute()
            {
                if (OnClicked != null)
                {
                    OnClicked.Invoke();
                }
            }
#if UNITY_WSA
            public void Start(){
                if(_globalListener){
                    this.gameObject.AskComponent<SetGlobalListener>();
                }
            }
            public void OnInputClicked(InputClickedEventData eventData)
            {

                if (ready_) { 
                    execute();
                    ready_ = false;
                }

            }

            private bool ready_ = false;
            public void OnInputDown(InputEventData eventData)
            {
                ready_ = true;
            }
            public void OnInputUp(InputEventData eventData)
            {
            }
#else
            void OnMouseUpAsButton()
            {
                if (!_globalListener)
                {
                    Debug.Log("click");
                    execute();
                }
            }
            void Update() {
                if (_globalListener) {
                    if (Input.GetMouseButtonUp(0)) {

                        execute();
                    }
                }
            }
#endif


        }
    }
}