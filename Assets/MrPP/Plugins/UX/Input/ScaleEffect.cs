using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;
using UnityEngine;
namespace MrPP{
    namespace UX
    {
        public class ScaleEffect : MonoBehaviour, IEffect
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
                GDGeek.TweenScale.Begin(target, 0.05f, oldScale_);
            }

            public void focusEnter()
            {
                GDGeek.TweenScale.Begin(target, 0f, oldScale_ * _scale);
            }

            public void reset()
            {
                target.transform.localScale = oldScale_;
            }
            private Vector3 oldScale_;
            public float _scale = 1.1f;

            virtual public void Awake()
            {
                oldScale_ = target.transform.localScale;
            }

            public void inputDown() { 
            

                GDGeek.TweenScale.Begin(target, 0.05f, oldScale_);
            }

            public void inputUp()
            {

                GDGeek.TweenScale.Begin(target, 0f, oldScale_ * _scale);
            }
        }
    }
}
