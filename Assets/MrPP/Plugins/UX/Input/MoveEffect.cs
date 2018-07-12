using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;
using UnityEngine;
namespace MrPP
{
    namespace UX
    {
        public class MoveEffect : MonoBehaviour, IEffect
        {


            public GameObject _target;
            private GameObject target
            {
                get
                {
                    if (_target != null)
                    {
                        return _target;
                    }
                    else
                    {
                        return this.gameObject;
                    }
                }
            }


            public void focusExit()
            {
                GDGeek.TweenLocalPosition.Begin(target, 0.05f, oldPosition);
            }

            public void focusEnter()
            {

               GDGeek.TweenLocalPosition.Begin(target, 0f, oldPosition + _offset);
            }

            public void reset()
            {
                target.transform.localPosition = oldPosition;
            }
            private Vector3 oldPosition;

            [SerializeField]
            private Vector3 _offset = new Vector3(0, 0, -0.01f);

            virtual public void Awake()
            {
                oldPosition = target.transform.localPosition;
            }

            public void inputDown()
            {
                Debug.LogWarning("down");
                GDGeek.TweenLocalPosition.Begin(target, 0.05f, oldPosition);
            }

            public void inputUp()
            {

                Debug.LogWarning("up");
                GDGeek.TweenLocalPosition.Begin(target, 0f, oldPosition +_offset);
            }
        }
    }
}
