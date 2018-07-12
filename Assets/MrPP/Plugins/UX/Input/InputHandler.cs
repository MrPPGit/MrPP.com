#if UNITY_WSA
using HoloToolkit.Unity.InputModule;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
namespace MrPP { 
    namespace UX { 
        public class InputHandler : MonoBehaviour
#if UNITY_WSA
            , IFocusable, IInputHandler
#endif
        {


            public UnityEvent onFocusEnter;
            public UnityEvent onFocusExit;
            public UnityEvent onInputDown;
            public UnityEvent onInputUp;

            private void focusEnter() {
                IEffect[] effects = this.gameObject.GetComponents<IEffect>();
                foreach (var e in effects)
                {
                    e.focusEnter();
                }
                if (onFocusEnter != null)
                {
                    onFocusEnter.Invoke();
                }
            }

            private void focusExit()
            {
                IEffect[] effects = this.gameObject.GetComponents<IEffect>();
                foreach (var e in effects)
                {
                    e.focusExit();
                }
                if (onFocusExit != null)
                {
                    onFocusExit.Invoke();
                }
            }
            private void inputDown() {
                IEffect[] effects = this.gameObject.GetComponents<IEffect>();
                foreach (var e in effects)
                {
                    e.inputDown();
                }
                if (onInputDown != null)
                {
                    onInputDown.Invoke();
                }
            }

            private void inputUp()
            {
                IEffect[] effects = this.gameObject.GetComponents<IEffect>();
                foreach (var e in effects)
                {
                    e.inputUp();
                }
                if (onInputUp != null)
                {
                    onInputUp.Invoke();
                }
            }


#if UNITY_WSA
            public void OnFocusEnter()
            {
                focusEnter();
            }
          
            public void OnFocusExit()
            {
                focusExit();
               
            }

            public void OnInputDown(InputEventData eventData)
            {
                inputDown();
            }

            public void OnInputUp(InputEventData eventData)
            {
                inputUp();
            }

#else
            public void OnMouseEnter()
            {
                focusEnter();
            }

            public void OnMouseExit()
            {
                focusExit();

            }

            public void OnMouseDown()
            {
                inputDown();
            }

            public void OnMouseUp()
            {
                inputUp();
            }
        
#endif
        }
    }
}